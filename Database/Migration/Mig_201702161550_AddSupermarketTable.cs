using FluentMigrator;

namespace Database
{
    [Migration(201702161550, "AddSupermarketTable")]
    public class Mig_201702161550_AddSupermarketTable : Migration
    {
        public override void Up()
        {
            Create.Table("Supermarket")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().ForeignKey("FK_Supermarket_Id_Store_Id", "Store", "Id")
                .WithColumn("Surface").AsFloat().Nullable();
        }

        public override void Down()
        {
            Delete.ForeignKey("FK_Supermarket_Id_Store_Id").OnTable("Supermarket");
            Delete.Table("Supermarket");
        }
    }
}