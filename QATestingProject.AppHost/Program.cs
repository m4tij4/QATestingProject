
var builder = DistributedApplication.CreateBuilder(args);

var postgresDb = builder.AddPostgres("postgresDb").WithPgAdmin();

var redisCache = builder.AddRedis("redisCache");

var seq = builder.AddSeq("seq");


var apiService = builder.AddProject<Projects.QATestingProject_ApiService>("apiservice")
                        .WithReference(postgresDb)
                        .WithReference(seq);

builder.AddProject<Projects.QATestingProject_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WithReference(redisCache)
    .WithReference(seq);

builder.Build().Run();
