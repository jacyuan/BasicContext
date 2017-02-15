using FluentMigrator;

namespace Database
{
    [Migration(201702151611, "AddDiscriminatorForEmployee")]
    public class Mig_201702151611_AddDiscriminatorForEmployee : Migration
    {
        public override void Up()
        {
            Alter.Table("Employee")
                .AddColumn("EmployeeType").AsInt32().NotNullable();
        }

        public override void Down()
        {
            Delete.Column("EmployeeType").FromTable("Employee");
        }
    }
}