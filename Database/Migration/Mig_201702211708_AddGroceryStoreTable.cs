using FluentMigrator;

namespace Database
{
    [Migration(201702211708, "AddGroceryStoreTable")]
    public class Mig_201702211708_AddGroceryStoreTable : Migration
    {
        public override void Up()
        {
            Create.Table("GroceryStore")
                .WithColumn("Id")
                .AsInt32()
                .NotNullable()
                .PrimaryKey()
                .ForeignKey("FK_GroceryStore_Id_Store_Id", "Store", "Id")
                .WithColumn("FullDayStore").AsBoolean().NotNullable().WithDefaultValue(false);
        }

        public override void Down()
        {
            Delete.ForeignKey("FK_GroceryStore_Id_Store_Id").OnTable("GroceryStore");
            Delete.Table("GroceryStore");
        }
    }
}