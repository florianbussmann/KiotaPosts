using Hangfire;
using KiotaPosts.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHangfire(config => config.UseInMemoryStorage());
builder.Services.AddHangfireServer();
builder.Services.AddSingleton<PostsService>();

var host = builder.Build();
var serviceProvider = host.Services;

var backgroundJobClient = serviceProvider.GetRequiredService<IBackgroundJobClient>();
var recurringJobManager = serviceProvider.GetRequiredService<IRecurringJobManager>();
var postsService = serviceProvider.GetRequiredService<PostsService>();

backgroundJobClient.Enqueue(() => postsService.Demo());

recurringJobManager.AddOrUpdate(
    "job",
    () => postsService.GetPostsCount(),
    Cron.Minutely);

await host.RunAsync();