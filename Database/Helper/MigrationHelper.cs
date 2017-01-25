using FluentMigrator.Builders.Create;
using FluentMigrator.Builders.Create.Table;

namespace Database.Helper
{
    public static class MigrationHelper
    {
        public static ICreateTableWithColumnOrSchemaOrDescriptionSyntax WithIdColumn(this ICreateTableWithColumnSyntax create)
        {
            return create.WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity();
        }
    }
}