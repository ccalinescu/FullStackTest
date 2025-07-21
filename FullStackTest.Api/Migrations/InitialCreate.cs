using FluentMigrator;

namespace FullStackTest.Api.Migrations
{
    [Migration(2025072001)]
    public class InitialCreate : Migration
    {
        public override void Up()
        {
            Create.Table("Tasks")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Completed").AsBoolean().NotNullable();

            Insert.IntoTable("Tasks").Row(new { Name = "Task 1", Completed = false });
            Insert.IntoTable("Tasks").Row(new { Name = "Task 2", Completed = false });
            Insert.IntoTable("Tasks").Row(new { Name = "Task 3", Completed = false });
        }

        public override void Down()
        {
            Delete.Table("Tasks");
        }
    }
}
