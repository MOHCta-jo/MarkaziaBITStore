var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.MarkaziaBITStore>("markaziabitstore");

builder.Build().Run();
