using ColorexApp.Application.DTOs.Invoices;
using MediatR;

namespace ColorexApp.Application.Features.Invoices.Queries;

public sealed record GetTodayInvoicesQuery : IRequest<List<InvoiceResponse>>;
