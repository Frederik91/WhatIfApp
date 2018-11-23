using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace WhatIf.Migrations.Migrations.Iteration1
{
    [Migration(2)]
    public class Mig001_CreateSessionTable : Migration
    {
        private const string TableName = "Session";
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
                .WithColumn("JoinId").AsInt16().NotNullable()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("LeaderId").AsGuid()
                .WithColumn("Started").AsBoolean()
                .WithColumn("Ended").AsBoolean();
            
            Create.PrimaryKey("PK_Session").OnTable(TableName).Column("Id");

            Create.ForeignKey("FK_Session_User").FromTable(TableName).ForeignColumn("Id").ToTable("User").PrimaryColumn("Id");
        }
    }
}
