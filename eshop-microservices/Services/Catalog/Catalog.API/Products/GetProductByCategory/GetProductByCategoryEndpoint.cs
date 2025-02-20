using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Products.GetProductByCategory;

public record GetProductByCategoryResponse(IEnumerable<Product> Products);


public class GetProductByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}", async ([FromRoute]string category, ISender sender) =>
            { 
                var products = await sender.Send(new GetProductByCategoryQuery(category));

                return Results.Ok(products.Adapt<GetProductByCategoryResponse>());
            })
            .WithName("GetProductByCategory")
            .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get products by category")
            .WithDescription("Get products by category");
    }
}