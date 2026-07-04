using Hangfire;
using KiotaPosts.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHangfire(config => config.UseInMemoryStorage());
builder.Services.AddHangfireServer();
builder.Services.AddSingleton<PostsService>();

var webApplication = builder.Build();
webApplication.UseHangfireDashboard();

var serviceProvider = webApplication.Services;

var backgroundJobClient = serviceProvider.GetRequiredService<IBackgroundJobClient>();
var recurringJobManager = serviceProvider.GetRequiredService<IRecurringJobManager>();
var postsService = serviceProvider.GetRequiredService<PostsService>();

backgroundJobClient.Enqueue(() => postsService.Demo());

recurringJobManager.AddOrUpdate(
    "job",
    () => postsService.GetPostsCount(),
    Cron.Minutely);

await webApplication.RunAsync();