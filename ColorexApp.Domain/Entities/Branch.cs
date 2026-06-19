namespace ColorexApp.Domain.Entities;

public class Branch
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Code { get; private set; } = string.Empty;
    public string DianResolutionPrefix { get; private set; } = string.Empty;
    public string DianRangeStart { get; private set; } = string.Empty;
    public string DianRangeEnd { get; private set; } = string.Empty;
    public bool IsActive { get; private set; }

    private Branch() { }

    private Branch(Guid id, string name, string code, string dianResolutionPrefix, string dianRangeStart, string dianRangeEnd, bool isActive)
    {
        Id = id;
        Name = name;
        Code = code;
        DianResolutionPrefix = dianResolutionPrefix;
        DianRangeStart = dianRangeStart;
        DianRangeEnd = dianRangeEnd;
        IsActive = isActive;
    }

    public static Branch Create(string name, string code, string dianResolutionPrefix, string dianRangeStart, string dianRangeEnd)
        => new(Guid.NewGuid(), name, code, dianResolutionPrefix, dianRangeStart, dianRangeEnd, true);

    public void Update(string name, string code, string dianResolutionPrefix, string dianRangeStart, string dianRangeEnd)
    {
        Name = name;
        Code = code;
        DianResolutionPrefix = dianResolutionPrefix;
        DianRangeStart = dianRangeStart;
        DianRangeEnd = dianRangeEnd;
    }

    public void Activate() => IsActive = true;
    public void Deactivate() => IsActive = false;
}
