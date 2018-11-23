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
            //Delete.ForeignKey("FK_UserRole_User").OnTable(TableName);
            //Delete.ForeignKey("FK_UserRole_Role").OnTable(TableName);
            Delete.Table(TableName);
        }

        public override void Up()
        {
            Create.Table(TableName)
                .WithColumn("Id").AsGuid().NotNullable()
                .WithColumn("Nickname").AsString().NotNullable()
                .WithColumn("SessionId").AsGuid();

            Create.PrimaryKey("PK_User").OnTable(TableName).Column("Id");
        }
    }
}
