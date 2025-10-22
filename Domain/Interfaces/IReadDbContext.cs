namespace CQRS_App.Domain.Interfaces;

public interface IReadDbContext : IDisposable, IAsyncDisposable
{
    IQueryable<T> Query<T>() where T : class; // supports Include, ThenInclude, projection
}
