using KiotaPosts.Client;
using KiotaPosts.Client.Models;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace KiotaPosts.Services;

public class PostsService
{
    private readonly PostsClient _postsClient;

    public PostsService()
    {
        // API requires no authentication, so use the anonymous
        // authentication provider
        var authProvider = new AnonymousAuthenticationProvider();
        // Create request adapter using the HttpClient-based implementation
        var adapter = new HttpClientRequestAdapter(authProvider);
        // Create the API client
        _postsClient = new PostsClient(adapter);
    }

    public async Task Demo()
    {
        Console.WriteLine("Hello, World!");
        
        // GET /posts/{id}
        var specificPostId = 5;
        var specificPost = await _postsClient.Posts[specificPostId].GetAsync();
        Console.WriteLine($"Retrieved post - ID: {specificPost?.Id}, Title: {specificPost?.Title}, Body: {specificPost?.Body}");

        // POST /posts
        var newPost = new Post
        {
            UserId = 42,
            Title = "Testing Kiota-generated API client",
            Body = "Hello world!"
        };

        var createdPost = await _postsClient.Posts.PostAsync(newPost);
        Console.WriteLine($"Created new post with ID: {createdPost?.Id}");

        // PATCH /posts/{id}
        var update = new Post
        {
            // Only update title
            Title = "Updated title"
        };

        var updatedPost = await _postsClient.Posts[specificPostId].PatchAsync(update);
        Console.WriteLine($"Updated post - ID: {updatedPost?.Id}, Title: {updatedPost?.Title}, Body: {updatedPost?.Body}");

        // DELETE /posts/{id}
        _postsClient.Posts[specificPostId].DeleteAsync().Wait();
    }

    public async Task GetPostsCount()
    {
        // GET /posts
        var allPosts = await _postsClient.Posts.GetAsync();
        Console.WriteLine($"Retrieved {allPosts?.Count} posts.");
    }
}