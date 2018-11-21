using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace WhatIf.Migrations.Migrations.Iteration1
{
    [Migration(1)]
    public class Mig001_CreateUserTable : Migration
    {
        private const string TableName = "User";
        public override void Down()
        {
            Delete.ForeignKey("FK_UserRole_User").OnTable(TableName);
            Delete.ForeignKey("FK_UserRole_Role").OnTable(TableName);
            Delete.Table(TableName);
        }

        public override void Up()
        {
            Create.Table(TableName)
               .WithColumn("UserId").AsInt32().NotNullable()
               .WithColumn("RoleId").AsInt16().NotNullable();

            var compKey = new[] { "UserId", "RoleId" };
            Create.PrimaryKey("PK_UserRole").OnTable("UserRole").Columns(compKey);

            Create.ForeignKey("FK_UserRole_User").FromTable("UserRole").ForeignColumn("UserId").ToTable("User").PrimaryColumn("UserId");
            Create.ForeignKey("FK_UserRole_Role").FromTable("UserRole").ForeignColumn("RoleId").ToTable("Role").PrimaryColumn("RoleId");
            Insert.IntoTable(TableName).Row(new { UserId = 1337, RoleId = 42 });
        }
    }
}
