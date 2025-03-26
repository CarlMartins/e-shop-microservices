using Marten.Schema;

namespace Catalog.API.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        await using var session = store.LightweightSession();

        if (await session.Query<Product>().AnyAsync(cancellation))
            return;

        session.Store<Product>(GetPreconfiguredProducts());
        
        await session.SaveChangesAsync(cancellation);
    }

    private static IEnumerable<Product> GetPreconfiguredProducts() => new List<Product>
    {
        new()
        {
            Id = new Guid("61cf3b47-0ece-4be5-8f3b-470ece9be5ea"),
            Name = "iPhone X",
            Description = "This phone is the company's biggest change to its flagship smartphone in years.",
            ImageFile = "product-1.png",
            Price = 950.00m,
            Categories = ["Smartphone"]
        },
        new()
        {
            Id = new Guid("af8e39d2-c88e-4f40-8e39-d2c88e7f4085"),
            Name = "Samsung 10",
            Description = "This phone is the company's biggest change to its flagship smartphone in years.",
            ImageFile = "product-2.png",
            Price = 950.00m,
            Categories = ["Smartphone"]
        },
        new()
        {
            Id = new Guid("bbeb702a-6242-4797-ab70-2a62422797f7"),
            Name = "Huawei Plus",
            Description = "This phone is the company's biggest change to its flagship smartphone in years.",
            ImageFile = "product-3.png",
            Price = 650.00m,
            Categories = ["White Appliances"]
        },
        new()
        {
            Id = new Guid("59394a52-271f-4a5d-b94a-52271f2a5dd8"),
            Name = "Xiaomi Mi 9",
            Description = "This phone is the company's biggest change to its flagship smartphone in years.",
            ImageFile = "product-4.png",
            Price = 470.00m,
            Categories = ["White Appliances"]
        },
        new()
        {
            Id = new Guid("64a75aa0-84b0-4cb2-a75a-a084b00cb2d0"),
            Name = "LG G7 ThinQ",
            Description = "This phone is the company's biggest change to its flagship smartphone in years.",
            ImageFile = "product-5.png",
            Price = 240.00m,
            Categories = ["Home Kitchen"]
        },
        new()
        {
            Id = new Guid("186f5ae4-a4ea-4879-af5a-e4a4eab879eb"),
            Name = "Panasonic Lumix",
            Description = "This phone is the company's biggest change to its flagship smartphone in years.",
            ImageFile = "product-6.png",
            Price = 240.00m,
            Categories = ["Camera"]
        }
    };
}