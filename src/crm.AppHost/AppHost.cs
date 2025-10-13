var builder = DistributedApplication.CreateBuilder(args);


var postgres = builder.AddPostgres("postgres").WithPgAdmin();
var postgresdb = postgres.AddDatabase("postgresdb");

var migration = builder.AddProject<Projects.crm_MigrationService>("migration")
    .WithReference(postgresdb)
    .WaitFor(postgres)
    .WaitFor(postgresdb);

builder.AddProject<Projects.crm_api>("crm-api")
    .WithReference(postgresdb)
    .WithReference(migration)
    .WaitFor(migration);

builder.Build().Run();
