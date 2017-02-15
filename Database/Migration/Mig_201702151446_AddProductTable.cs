using Database.Helper;
using FluentMigrator;

namespace Database
{
    [Migration(201702151446, "AddProductTable")]
    public class Mig_201702151446_AddProductTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("Product")
                .WithIdColumn()
                .WithColumn("Name").AsString(100).NotNullable().Unique();
        }
    }
}