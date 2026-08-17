using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using TaskPilot.Api.Configurations;
using TaskPilot.Api.DTOs.Dashboard;
using TaskPilot.Api.Interfaces;
using TaskPilot.Api.Models;

namespace TaskPilot.Api.Respositories;

public class DashboardRepository : IDashboardRepository
{
    private readonly IMongoCollection<User> _users;
    public DashboardRepository(IOptions<MongoDbSettings> options)
    {
        var client = new MongoClient(options.Value.ConnectionString);
        var database = client.GetDatabase(options.Value.DatabaseName);
        _users = database.GetCollection<User>(options.Value.UserCollection);
    }

    public async Task<DashboardDto> GetDashboardDataAsync(string userId)
    {
        //var pipeline = new[]
        //{
        //    new BsonDocument("$match", new BsonDocument("_id", ObjectId.Parse(userId))),
        //    new BsonDocument("$lookup",
        //        new BsonDocument
        //        {
        //            { "from", "Projects" },
        //            { "localField", "_id" },
        //            { "foreignField", "createdBy" },
        //            { "as", "projects" }
        //        }
        //    ),
        //    new BsonDocument("$lookup",
        //        new BsonDocument
        //        {
        //            { "from", "Todos" },
        //            { "localField", "projects._id" },
        //            { "foreignField", "projectId" },
        //            { "as", "tasks" }
        //        }
        //    ),
        //    new BsonDocument("$project",
        //        new BsonDocument
        //        {
        //            { "_id", 0 },
        //            { "TotalProjects",  new BsonDocument("$size", "$projects") },
        //            { "TotalTasks",  new BsonDocument("$size", "$tasks") },
        //            { "TotalCompletedTasks",  new BsonDocument("$size",
        //                new BsonDocument("$filter",
        //                    new BsonDocument
        //                    {
        //                        { "input", "$tasks" },
        //                        { "as", "task"},
        //                        { "cond", new BsonDocument("$eq", new BsonArray{"$$task.status", 1}) }
        //                })
        //            )},
        //            { "TotalPendingTasks",  new BsonDocument("$size",
        //                new BsonDocument("$filter",
        //                    new BsonDocument
        //                    {
        //                        { "input", "$tasks" },
        //                        { "as", "task" },
        //                        { "cond", new BsonDocument("$eq", new BsonArray{"$$task.status", 0})}
        //                    }
        //                )
        //            )},
        //            { "RecentProjects",  new BsonDocument("$slice", new BsonArray{"$projects", 3})}
        //        }
        //    )
        //};

        var pipeline = new[]
        {
            new BsonDocument("$match", new BsonDocument("_id" ,ObjectId.Parse(userId))),
            new BsonDocument(
                "$lookup", new BsonDocument
                {
                    {"from", "Projects" },
                    {"let", new BsonDocument("userId", "$_id")},
                    {"pipeline", new BsonArray
                        {
                            new BsonDocument("$match", new BsonDocument(
                                "$expr",
                                new BsonDocument("$eq", new BsonArray{ "$createdBy", "$$userId" })
                            )),
                            new BsonDocument("$lookup", new BsonDocument
                            {
                                {"from", "Todos" },
                                {"localField", "_id" },
                                {"foreignField", "projectId" },
                                {"as", "tasks" }
                            }),
                            new BsonDocument("$set", new BsonDocument{
                                { "taskCount", new BsonDocument("$size", "$tasks") },
                                { "$completedTaskCount", new BsonDocument("$size", new BsonDocument("$filter", new BsonDocument{}))
                                }
                            }),
                        }
                    },
                    {"as", "projects" }
                })
        };

        var data = await _users.Aggregate<DashboardDto>(pipeline).FirstOrDefaultAsync();
        return data;
    }
}
