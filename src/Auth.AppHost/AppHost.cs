var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.ClaimsAuth>("claimsauth");

builder.AddProject<Projects.JwtAuth>("jwtauth");

builder.Build().Run();
