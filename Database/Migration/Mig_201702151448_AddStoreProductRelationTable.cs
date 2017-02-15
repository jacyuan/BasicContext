using FluentMigrator;

namespace Database
{
    [Migration(201702151448, "AddStoreProductRelationTable")]
    public class Mig_201702151448_AddStoreProductRelationTable : Migration
    {
        public override void Up()
        {
            Create.Table("Store_product")
                .WithColumn("Store_id").AsInt32().NotNullable()
                .WithColumn("Product_id").AsInt32().NotNullable();

            Create.PrimaryKey("PK_Store_product").OnTable("Store_product").Columns(new[] {"Store_id", "Product_id"});
        }

        public override void Down()
        {
            Delete.PrimaryKey("PK_Store_product").FromTable("Store_product");
            Delete.Table("Store_product");
        }
    }
}