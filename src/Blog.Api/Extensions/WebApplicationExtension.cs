using Asp.Versioning;
using Asp.Versioning.Builder;
using Blog.Api.Endpoints.V1;
using Blog.Api.Middlewares;
using Blog.Api.Services;
using Blog.Infrastructure;
using Blog.Shared.Responses;
using Blog.Shared.Responses.Posts;
using Blog.Shared.Responses.Users;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Threading;

namespace Blog.Api.Extensions;

internal static class WebApplicationExtension
{
    public static WebApplication UseServices(this WebApplication app)
    {
        app.UseMiddleware<ErrorHandlerMiddleware>();

        app.AddMigrations();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.MapEndpoints();
        app.MapWebSocketsEndpoints();

        return app;
    }

    private static void AddMigrations(this IApplicationBuilder app)
    {
        try
        {
            using var scope = app.ApplicationServices.CreateScope();
            var servceDb = scope.ServiceProvider.GetService<BlogDbContext>();
            if (servceDb?.Database?.GetPendingMigrations().Any() ?? false)
                servceDb.Database.Migrate();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }
    }

    private static WebApplication MapWebSocketsEndpoints(this WebApplication app)
    {
        app.UseWebSockets();

        app.Map("/notifications", WebSocketsEndpoints.NotificationsAsync);

        return app;
    }


    private static WebApplication MapEndpoints(this WebApplication app)
    {
        app.MapGet("", async () => await Task.FromResult(Results.Ok("Service running...")));

        app.NewVersionedApi("Users").MapUsersEndpointsV1();

        app.NewVersionedApi("Posts").MapPostsEndpointsV1();

        return app;
    }

    private static IVersionedEndpointRouteBuilder? MapUsersEndpointsV1(this IVersionedEndpointRouteBuilder? versionedBuilder)
    {
        var usersV1 = versionedBuilder!.MapGroup("v1")
            .HasApiVersion(new ApiVersion(1, 0));

        usersV1.MapPost("/users", UsersEndpoints.CreateUserAsync)
            .WithName("CreateUser")
            .Produces<BaseResponse<Guid>>(StatusCodes.Status200OK)
            .Produces<BaseResponse>(StatusCodes.Status400BadRequest)
            .Produces<BaseResponse>(StatusCodes.Status422UnprocessableEntity)
            .WithOpenApi()
            .MapToApiVersion(1, 0);

        usersV1.MapPost("/users/authenticate", UsersEndpoints.AuthenticateUserAsync)
            .WithName("AuthenticateUser")
            .Produces<BaseResponse<AuthenticateUserResponse>>(StatusCodes.Status200OK)
            .Produces<BaseResponse>(StatusCodes.Status400BadRequest)
            .Produces<BaseResponse>(StatusCodes.Status422UnprocessableEntity)
            .WithOpenApi()
            .MapToApiVersion(1, 0);

        return versionedBuilder;
    }

    private static IVersionedEndpointRouteBuilder? MapPostsEndpointsV1(this IVersionedEndpointRouteBuilder? versionedBuilder)
    {
        var postsV1 = versionedBuilder!.MapGroup("v1")
            .HasApiVersion(new ApiVersion(1, 0));

        postsV1.MapPost("/posts", PostsEndpoints.CreatePostAsync).RequireAuthorization()
            .WithName("CreatePost")
            .Produces<BaseResponse<Guid>>(StatusCodes.Status200OK)
            .Produces<BaseResponse>(StatusCodes.Status400BadRequest)
            .Produces<BaseResponse>(StatusCodes.Status422UnprocessableEntity)
            .WithOpenApi()
            .MapToApiVersion(1, 0);

        postsV1.MapPut("/posts", PostsEndpoints.UpdatePostAsync).RequireAuthorization()
            .WithName("UpdatePost")
            .Produces<BaseResponse>(StatusCodes.Status200OK)
            .Produces<BaseResponse>(StatusCodes.Status400BadRequest)
            .Produces<BaseResponse>(StatusCodes.Status422UnprocessableEntity)
            .WithOpenApi()
            .MapToApiVersion(1, 0);

        postsV1.MapDelete("/posts", PostsEndpoints.DeletePostAsync).RequireAuthorization()
            .WithName("DeletePost")
            .Produces<BaseResponse>(StatusCodes.Status200OK)
            .Produces<BaseResponse>(StatusCodes.Status400BadRequest)
            .Produces<BaseResponse>(StatusCodes.Status422UnprocessableEntity)
            .WithOpenApi()
            .MapToApiVersion(1, 0);

        postsV1.MapGet("/posts/byuserid", PostsEndpoints.GetPostSByUserIdAsync).RequireAuthorization()
            .WithName("GetPostsByUserId")
            .Produces<BaseResponse<PaginatedListResponse<PostResponse>>>(StatusCodes.Status200OK)
            .Produces<BaseResponse>(StatusCodes.Status400BadRequest)
            .Produces<BaseResponse>(StatusCodes.Status422UnprocessableEntity)
            .WithOpenApi()
            .MapToApiVersion(1, 0);

        postsV1.MapGet("/posts", PostsEndpoints.GetPostsAsync)
            .WithName("GetPosts")
            .Produces<BaseResponse<PaginatedListResponse<PostResponse>>>(StatusCodes.Status200OK)
            .Produces<BaseResponse>(StatusCodes.Status400BadRequest)
            .Produces<BaseResponse>(StatusCodes.Status422UnprocessableEntity)
            .WithOpenApi()
            .MapToApiVersion(1, 0);

        return versionedBuilder;
    }
}
