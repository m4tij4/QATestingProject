var builder = DistributedApplication.CreateBuilder(args);

var postgresDb = builder.AddPostgres("postgresDb").WithPgAdmin();
var redisCache = builder.AddRedis("redisCache");

var apiService = builder.AddProject<Projects.QATestingProject_ApiService>("apiservice")
    .WithReference(postgresDb);

builder.AddProject<Projects.QATestingProject_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WithReference(redisCache);

builder.Build().Run();
