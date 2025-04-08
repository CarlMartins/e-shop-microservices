namespace Basket.API.Basket.GetBasket;

public record GetBasketRequest(string UserName);
public record GetBasketResponse(ShoppingCart ShoppingCart);

public class GetBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket/{username}", async ([FromRoute] string username, ISender sender) =>
        {
            var result = await sender.Send(new GetBasketQuery(username));
            
            var response = result.Adapt<GetBasketResponse>();
            
            return Results.Ok(response);
        })
        .WithName("GetBasketByUsername")
        .Produces<GetBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get basket by username")
        .WithDescription("Get basket by username");
    }
}