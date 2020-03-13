using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserDatabaseApi.Models;

namespace UserDatabaseApi.Data
{
    public static class CosmosDBDefinitions
    {
        private static string accountURI = "https://safewayz-database1.documents.azure.com:443/";
        private static string accountKey = "KrhDx0RTy5VRoVvUmdA9VH7v4wxLtFhApSvD66c9kJwkQaP96pwgBUv9SB1NuTMK3nmaxWBljNFC7GdPpDyZyA==";
        public static string DatabaseId { get; private set; } = "UserList";
        public static string UserProfileId { get; private set; } = "UserProfile";

        public static IDocumentDBConnection GetConnection()
        {
            return new DefaultDocumentDBConnection(accountURI, accountKey, DatabaseId);
        }

        public static async Task Initialize()
        {
            var connection = GetConnection();

            await connection.Client
                .CreateDatabaseIfNotExistsAsync(
                    new Database { Id = DatabaseId });

            DocumentCollection myCollection = new DocumentCollection();
            myCollection.Id = UserProfileId;
            myCollection.IndexingPolicy =
               new IndexingPolicy(new RangeIndex(DataType.String)
               { Precision = -1 });
            myCollection.IndexingPolicy.IndexingMode = IndexingMode.Consistent;
            var res = await connection.Client.CreateDocumentCollectionIfNotExistsAsync(
                UriFactory.CreateDatabaseUri(DatabaseId),
                myCollection);
            if (res.StatusCode == System.Net.HttpStatusCode.Created)
                await InitData(connection);
        }
        private static async Task InitData(IDocumentDBConnection connection)
        {
            List<UserModel> allItems = new List<UserModel>();
            for (int i = 0; i < 6; i++)
            {
                var curr = new UserModel();
                allItems.Add(curr);
                curr.Name = "Name" + i;
                curr.Description = "Description" + i;
                curr.Completed = i % 2 == 0;
                curr.Id = Guid.NewGuid().ToString();
                curr.Owner = i > 3 ? "frank@fake.com" : "John@fake.com";
                if (i > 1)
                    curr.AssignedTo = new Person
                    {
                        Name = "Francesco",
                        Surname = "Abbruzzese",
                        Id = Guid.NewGuid().ToString()
                    };
                else
                    curr.AssignedTo = new Person
                    {
                        Name = "John",
                        Surname = "Black",
                        Id = Guid.NewGuid().ToString()
                    };
                var innerlList = new List<UserModel>();
                for (var j = 0; j < 4; j++)
                {
                    innerlList.Add(new UserModel
                    {
                        Name = "ChildrenName" + i + "_" + j,
                        Description = "ChildrenDescription" + i + "_" + j,
                        Id = Guid.NewGuid().ToString()
                    });
                }
                curr.SubItems = innerlList;
                var team = new List<UserModel>();
                for (var j = 0; j < 4; j++)
                {
                    team.Add(new UserModel
                    {
                        Name = "TeamMemberName" + i + "_" + j,
                        Surname = "TeamMemberSurname" + i + "_" + j,
                        Id = Guid.NewGuid().ToString()
                    });
                }
                curr.Team = team;
            }
            foreach (var item in allItems)
            {
                await connection.Client
                    .CreateDocumentAsync(
                    UriFactory
                        .CreateDocumentCollectionUri(
                            DatabaseId, UserProfileId),
                    item);

            }
        }
    }
}
