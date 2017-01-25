using Database.Helper;
using FluentMigrator;

namespace Database
{
    [Migration(201701251924, "AddEmployeeTable")]
    public class Mig201701251924AddEmployeeTable : Migration
    {
        public override void Up()
        {
            Create.Table("Employee")
                .WithIdColumn()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Age").AsInt32().Nullable()
                .WithColumn("Gender").AsInt32().NotNullable()
                .ForeignKey("FK_Employee_GenderEnum", "GenderEnum", "Id")
                .WithColumn("Store_id").AsInt32().NotNullable()
                .ForeignKey("FK_Employee_Store", "Store", "Id");
        }

        public override void Down()
        {
            Delete.ForeignKey("FK_Employee_Store").OnTable("Employee");
            Delete.ForeignKey("FK_Employee_GenderEnum").OnTable("Employee");
            Delete.Table("Employee");
        }
    }
}