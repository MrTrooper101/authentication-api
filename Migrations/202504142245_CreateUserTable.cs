using FluentMigrator;

namespace authentication_api.Migrations
{
    [Migration(202504142245)]
    public class _202504142245_CreateUserTable:Migration
    {
        public override void Up()
        {
            Create.Table("Users")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("FirstName").AsString(50).NotNullable()
                .WithColumn("MiddleName").AsString(50).Nullable()
                .WithColumn("LastName").AsString(50).NotNullable()
                .WithColumn("Email").AsString(100).NotNullable().Unique()
                .WithColumn("DateOfBirth").AsDateTime().NotNullable()
                .WithColumn("Gender").AsString(10).NotNullable()
                .WithColumn("ContactNumber").AsString(20).Nullable()
                .WithColumn("Address").AsString(200).Nullable()
                .WithColumn("PasswordHash").AsString(255).NotNullable()
                .WithColumn("IsEmailConfirmed").AsBoolean().NotNullable()
                .WithColumn("EmailConfirmationToken").AsString(255).Nullable()
                .WithColumn("CreatedAt").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
                .WithColumn("UpdatedAt").AsDateTime().Nullable()
                .WithColumn("DeletedAt").AsDateTime().Nullable();
        }

        public override void Down()
        {
            Delete.Table("Users");
        }
    }
}