using System;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table.DataServices;
using Elmah;
using System.Collections;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace FriendlyForms
{
    public class ErrorEntity : TableServiceEntity
    {
        public string SerializedError { get; set; }

        public ErrorEntity() { }
        public ErrorEntity(Error error)
            : base(string.Empty, (DateTime.MaxValue.Ticks - DateTime.UtcNow.Ticks).ToString("d19"))
        {
            this.SerializedError = ErrorXml.EncodeString(error);
        }
    }

    public class TableErrorLog : ErrorLog
    {
        private string connectionString;

        public override ErrorLogEntry GetError(string id)
        {
            return new ErrorLogEntry(this, id, ErrorXml.DecodeString(CloudStorageAccount.Parse(connectionString).CreateCloudTableClient().GetTableServiceContext().CreateQuery<ErrorEntity>("elmaherrors").Where(e => e.PartitionKey == string.Empty && e.RowKey == id).First().SerializedError));
        }

        public override int GetErrors(int pageIndex, int pageSize, IList errorEntryList)
        {
            var count = 0;
            var context = CloudStorageAccount.Parse(connectionString).CreateCloudTableClient().GetTableServiceContext();
            foreach (var error in context.CreateQuery<ErrorEntity>("elmaherrors").Where(e => e.PartitionKey == string.Empty).AsTableServiceQuery(context).Take((pageIndex + 1) * pageSize).ToList().Skip(pageIndex * pageSize))
            {
                errorEntryList.Add(new ErrorLogEntry(this, error.RowKey, ErrorXml.DecodeString(error.SerializedError)));
                count += 1;
            }
            return count;
        }

        public override string Log(Error error)
        {
            if (!HttpContext.Current.IsDebuggingEnabled)
            {
                var entity = new ErrorEntity(error);
                var context = CloudStorageAccount.Parse(connectionString).CreateCloudTableClient().GetTableServiceContext();
                context.AddObject("elmaherrors", entity);
                context.SaveChangesWithRetries();
                return entity.RowKey;
            }
            return "";
        }

        public TableErrorLog(IDictionary config)
        {
            connectionString = (string)config["connectionString"] ?? RoleEnvironment.GetConfigurationSettingValue((string)config["connectionStringName"]);
            Initialize();
        }

        public TableErrorLog(string connectionString)
        {
            this.connectionString = connectionString;
            Initialize();
        }

        void Initialize()
        {
            if (!HttpContext.Current.IsDebuggingEnabled)
                CloudStorageAccount.Parse(connectionString).CreateCloudTableClient().GetTableReference("elmaherrors").CreateIfNotExists();
        }
    }

}