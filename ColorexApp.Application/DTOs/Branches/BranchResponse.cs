namespace ColorexApp.Application.DTOs.Branches;

public sealed record BranchResponse(
    Guid Id,
    string Name,
    string Code,
    string DianResolutionPrefix,
    string DianRangeStart,
    string DianRangeEnd,
    bool IsActive
);

public sealed record CreateBranchRequest(
    string Name,
    string Code,
    string DianResolutionPrefix,
    string DianRangeStart,
    string DianRangeEnd
);
