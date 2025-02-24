namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductCommand(Guid Id) : ICommand<Unit>;

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Product id is required");
    }
}

public class DeleteProductCommandHandler 
    (IDocumentSession session, ILogger<DeleteProductCommandHandler> logger)
    : ICommandHandler<DeleteProductCommand, Unit>
{
    public async Task<Unit> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteProductCommandHandler called with {@Command}", command);
        
        session.Delete<Product>(command.Id);
        await session.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}