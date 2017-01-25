using Database.Helper;
using FluentMigrator;

namespace Database
{
    [Migration(201701251923, "AddGenderEnumTable")]
    public class Mig_201701251923_AddGenderEnumTable : Migration
    {
        public override void Up()
        {
            Create.Table("GenderEnum")
                .WithIdColumn()
                .WithColumn("Label").AsString().NotNullable();

            Execute.Sql("SET IDENTITY_INSERT GenderEnum ON");

            Insert.IntoTable("GenderEnum")
                .Row(new
                {
                    Id = 1,
                    Label = "Man"
                })
                .Row(new
                {
                    Id = 2,
                    Label = "Woman"
                });

            Execute.Sql("SET IDENTITY_INSERT GenderEnum OFF");
        }

        public override void Down()
        {
            Delete.FromTable("GenderEnum").AllRows();
            Delete.Table("GenderEnum");
        }
    }
}