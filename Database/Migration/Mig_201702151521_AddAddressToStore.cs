using FluentMigrator;

namespace Database
{
    [Migration(201702151521, "AddAddressToStore")]
    public class Mig_201702151521_AddAddressToStore : Migration
    {
        public override void Up()
        {
            Alter.Table("Store")
                .AddColumn("StreetNumber").AsInt32().Nullable()
                .AddColumn("Street").AsString().Nullable()
                .AddColumn("Country").AsString().Nullable()
                .AddColumn("ZipCode").AsString().Nullable();
        }

        public override void Down()
        {
            Delete.Column("StreetNumber").FromTable("Store");
            Delete.Column("Street").FromTable("Store");
            Delete.Column("Country").FromTable("Store");
            Delete.Column("ZipCode").FromTable("Store");
        }
    }
}