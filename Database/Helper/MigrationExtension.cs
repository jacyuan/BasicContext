using FluentMigrator.Builders.Create.Table;

namespace Database.Helper
{
    public static class MigrationExtension
    {
        public static ICreateTableColumnOptionOrWithColumnSyntax WithIdColumn(this ICreateTableWithColumnSyntax create)
        {
            return create.WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity();
        }
    }
}