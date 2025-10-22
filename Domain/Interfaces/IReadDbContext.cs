namespace test_ci_cd_for_cqrs.Domain.Interfaces
{
    public interface IReadDbContext : IDisposable, IAsyncDisposable
    {
        IQueryable<T> Query<T>() where T : class; // supports Include, ThenInclude, projection
    }
}
