using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace byr.IDP.Migrations
{
    public partial class AddAliceByr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("0ba12832-2da4-44fa-96db-ff6f33c422fd"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("20035719-3f7b-4206-ac66-13f947376e93"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("25ebf00f-bc84-49bc-934f-057024e16875"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("41e182a9-2dba-43e7-a97c-82c3b3acaa6b"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("64138a55-13b1-44d9-83b6-b0834216cee1"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("64977657-2f43-4d67-b80c-2f0078c104c9"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("c8be72c5-11e2-4fc0-be0a-846154e85ff4"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("e408ac68-90a8-4f87-aa5e-edc368b5db6c"));

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("21dbbf8d-a91d-4afc-a540-c3f6d9e7fd67"), "256992a9-cb26-411d-b14a-138fde9a57c7", "country", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "nl" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("3754058e-ead5-44f7-b114-e8ae8d272a66"), "71d2bd9a-3aad-4c04-8ec1-87888ef4081f", "given_name", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "David" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("5b93682a-e2e4-4b4c-b76c-22b1fa5dad55"), "c52867f6-0ec1-4161-8900-9320e8e6d0c0", "family_name", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "Flagg" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("a76bd531-138e-4d3f-9474-802947e96bee"), "5dbf80eb-35c5-4a6e-b3da-43c52946534b", "country", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "be" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("ae6ad9a6-0fc3-4d60-98c7-2d5eb8a6836c"), "4c5e61ea-824f-4030-97a0-f247b12bf6e4", "family_name", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "Flagg" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("b67036af-1545-49cd-b203-cc59f5d44b72"), "febed8be-fec4-460d-8ec7-a3e996cb9421", "role", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "FreeUser" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("dbeb5c70-66f2-44ea-9a3a-e8962fd6b167"), "d0e1ce0f-3aa9-437e-9dc9-84e0e5670f1f", "role", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "PayingUser" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("efb0d6f3-0efc-4eea-8b7e-00759af5460a"), "d484e793-c7b1-4761-be54-9083036b5dd3", "given_name", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "Emma" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"),
                column: "ConcurrencyStamp",
                value: "5765e02a-eb14-451d-8924-a0d4eff1e7d4");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"),
                column: "ConcurrencyStamp",
                value: "e81cd01e-5501-4097-9954-0d6e0d482be0");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "ConcurrencyStamp", "Password", "Subject", "UserName" },
                values: new object[] { new Guid("ca48eb08-46f0-493b-aad6-ce567205e507"), true, "27fe4c87-932a-4e2c-a748-aa79701a28f0", "password", "1Subject", "Alice" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("1a17d296-4f4f-4159-960c-fa4e7c72246d"), "5fa3442c-11d5-47e7-880f-eac0cf06cd43", "family_name", new Guid("ca48eb08-46f0-493b-aad6-ce567205e507"), "Smith" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("33b16eb8-3f56-4c20-abe7-2e5e2a95e7e8"), "3482a805-0975-42f2-80e7-8fc6fc957767", "given_name", new Guid("ca48eb08-46f0-493b-aad6-ce567205e507"), "Alice" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("83148847-bb4e-4227-8137-b4887d119ef2"), "54b1e6d4-770e-4177-9307-f33231023786", "role", new Guid("ca48eb08-46f0-493b-aad6-ce567205e507"), "PayingUser" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("c9ed2dd8-047e-4427-90c5-3c77942175ae"), "47756fa7-1acc-45b7-a37f-57aad0b36f62", "country", new Guid("ca48eb08-46f0-493b-aad6-ce567205e507"), "be" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("1a17d296-4f4f-4159-960c-fa4e7c72246d"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("21dbbf8d-a91d-4afc-a540-c3f6d9e7fd67"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("33b16eb8-3f56-4c20-abe7-2e5e2a95e7e8"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("3754058e-ead5-44f7-b114-e8ae8d272a66"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("5b93682a-e2e4-4b4c-b76c-22b1fa5dad55"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("83148847-bb4e-4227-8137-b4887d119ef2"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("a76bd531-138e-4d3f-9474-802947e96bee"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("ae6ad9a6-0fc3-4d60-98c7-2d5eb8a6836c"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("b67036af-1545-49cd-b203-cc59f5d44b72"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("c9ed2dd8-047e-4427-90c5-3c77942175ae"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("dbeb5c70-66f2-44ea-9a3a-e8962fd6b167"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("efb0d6f3-0efc-4eea-8b7e-00759af5460a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ca48eb08-46f0-493b-aad6-ce567205e507"));

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("0ba12832-2da4-44fa-96db-ff6f33c422fd"), "c41d8418-2f74-418a-b45e-8875e47ac1bc", "role", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "FreeUser" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("20035719-3f7b-4206-ac66-13f947376e93"), "df44780e-7f99-4284-a50b-d2fadde9d92f", "country", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "nl" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("25ebf00f-bc84-49bc-934f-057024e16875"), "804c2dd4-cfa8-4dae-9f17-645d767d3eb6", "given_name", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "David" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("41e182a9-2dba-43e7-a97c-82c3b3acaa6b"), "7492ebd9-9f7b-47b7-bee0-8b7abd44d13e", "family_name", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "Flagg" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("64138a55-13b1-44d9-83b6-b0834216cee1"), "b5ac7976-79a5-4265-8207-2f26d882d3ad", "given_name", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "Emma" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("64977657-2f43-4d67-b80c-2f0078c104c9"), "8b8f92d4-31e7-4284-87d5-e5df2c3926ee", "family_name", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "Flagg" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("c8be72c5-11e2-4fc0-be0a-846154e85ff4"), "70e77813-7e5c-4c20-919b-28f3e4837ab6", "role", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "PayingUser" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("e408ac68-90a8-4f87-aa5e-edc368b5db6c"), "cd7863be-1516-40f2-a389-be5ce2bc71a2", "country", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "be" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"),
                column: "ConcurrencyStamp",
                value: "f03acb1c-dae9-4a19-84db-1031c8818b3d");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"),
                column: "ConcurrencyStamp",
                value: "84e34723-9cc9-4bfb-867f-1dfe072b4d7c");
        }
    }
}
