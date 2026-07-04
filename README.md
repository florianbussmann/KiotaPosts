# KiotaPosts

Demonstrates usage of [Kiota API client generator](https://learn.microsoft.com/de-de/openapi/kiota/overview) to build a
sample app in .NET that calls a REST API that doesn't require authentication as shown
in [Build API clients for .NET](https://learn.microsoft.com/de-de/openapi/kiota/quickstarts/dotnet).

```shell
$ kiota generate -l CSharp -c PostsClient -n KiotaPosts.Client -d ./src/posts-api.yml -o ./Client
Generation completed successfully
Client base url set to https://jsonplaceholder.typicode.com
```