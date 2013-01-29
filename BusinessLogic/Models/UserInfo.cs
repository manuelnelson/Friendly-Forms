using System.IO;
using System.Xml.Serialization;

namespace BusinessLogic.Models
{
    public class UserInfo
    {
        public string FirstName { get; set; }
        public string Email { get; set; }
        public int Id { get; set; }
        public int RoleId { get; set; }

        public override string ToString()
        {
            var serializer = new XmlSerializer(typeof(UserInfo));
            using (var stream = new StringWriter())
            {
                serializer.Serialize(stream, this);
                return stream.ToString();
            }
        }

        public static UserInfo FromString(string userContextData)
        {
            var serializer = new XmlSerializer(typeof(UserInfo));
            using (var stream = new StringReader(userContextData))
            {
                return serializer.Deserialize(stream) as UserInfo;
            }
        }
    }
}
