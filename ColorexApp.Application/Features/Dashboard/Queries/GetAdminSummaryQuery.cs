using ColorexApp.Application.DTOs.Dashboard;
using MediatR;

namespace ColorexApp.Application.Features.Dashboard.Queries;

public sealed record GetAdminSummaryQuery : IRequest<AdminSummaryResponse>;
