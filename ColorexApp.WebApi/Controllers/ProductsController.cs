using ColorexApp.Application.DTOs.Products;
using ColorexApp.Application.Features.Products.Commands;
using ColorexApp.Application.Features.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ColorexApp.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/products")]
public sealed class ProductsController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllProductsQuery();
        var result = await sender.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetProductByIdQuery(id);
        var result = await sender.Send(query, cancellationToken);
        return Ok(result);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateProductCommand(
            request.Name, request.BaseReference, request.BasePrice, request.Cost,
            request.Variants.Select(v => new CreateVariantCommand(v.Size, v.Color, v.ColorHex, v.Stock)).ToList());
        var id = await sender.Send(command, cancellationToken);
        return Ok(new { id });
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateProductCommand(id, request.Name, request.BasePrice, request.Cost);
        await sender.Send(command, cancellationToken);
        return NoContent();
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteProductCommand(id);
        await sender.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpGet("top-selling")]
    public async Task<IActionResult> GetTopSelling(CancellationToken cancellationToken)
    {
        var query = new GetTopSellingProductQuery();
        var result = await sender.Send(query, cancellationToken);
        return Ok(result);
    }
}
