namespace ColorexApp.Application.Abstractions.Data;

public interface ISqlDbContext
{
    Task<IEnumerable<T>> QueryAsync<T>(string sql, object? parameters = null, CancellationToken ct = default);
}
