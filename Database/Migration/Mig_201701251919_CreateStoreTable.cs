using Database.Helper;
using FluentMigrator;

namespace Database
{
    [Migration(201701251919, "CreateStoreTable")]
    public class Mig_201701251919_CreateStoreTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("Store")
                .WithIdColumn()
                .WithColumn("Name").AsString().NotNullable();
        }
    }
}