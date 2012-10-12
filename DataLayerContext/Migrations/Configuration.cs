using Models;

namespace DataLayerContext.Migrations
{
    using System.Data.Entity.Migrations;

    public class Configuration : DbMigrationsConfiguration<DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(DataContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //context.Users.AddOrUpdate(u => u.Email,
            //    new User()
            //        {
            //            Email = "elnels@gmail.com",
            //            FirstName = "Manny",
            //            LastName = "Nelson",
            //            Password = "testPassword",
            //            Verified = true
            //        });
            //context.Courts.AddOrUpdate(c => c.UserId,
            //  new Court
            //      {
            //          AuthorOfPlan = 1,
            //          CaseNumber = 1,
            //          CountyId = 1,
            //          PlanType = 1,
            //          UserId = 1
            //      }
            //);
            //context.Participants.AddOrUpdate( p => p.UserId,
            //    new Participant()
            //        {
            //            DefendantCustodialParent = 1,
            //            DefendantsName = "Manny",
            //            DefendantRelationship = 1,
            //            PlaintiffCustodialParent = 1,
            //            PlaintiffRelationship = 1,
            //            PlaintiffsName = "Her",
            //            UserId = 1
            //        });
            //
            context.Counties.AddOrUpdate(u => u.CountyName,
                new County()
                    {
                        CountyName = "Cobb",
                    },
                new County()
                    {
                        CountyName = "Dekalb",
                    },
                new County()
                    {
                        CountyName = "Fulton",
                    },
                new County()
                    {
                        CountyName = "Gwinnett",
                    }
                    );
        }
    }
}
