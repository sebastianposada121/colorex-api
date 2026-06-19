namespace ColorexApp.Application.DTOs.Dashboard;

public sealed record AdminSummaryResponse(
    int TotalGlobalStock,
    int ActiveBranches,
    int PendingTransfers,
    string TopSellingProductName,
    string TopSellingSku,
    int TopSellingQuantity
);

public sealed record BranchSummaryResponse(
    string BranchName,
    int TotalStock,
    int PendingTransfers,
    int TodaySales,
    decimal TodayRevenue
);
