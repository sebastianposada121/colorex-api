using ColorexApp.Application.DTOs.Dashboard;
using MediatR;

namespace ColorexApp.Application.Features.Products.Queries;

public sealed record GetTopSellingProductQuery : IRequest<TopSellingProductResponse?>;

public sealed record TopSellingProductResponse(string Name, string Sku, int TotalSold);
