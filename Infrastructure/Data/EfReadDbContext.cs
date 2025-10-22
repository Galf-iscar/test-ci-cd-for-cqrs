using CQRS_App.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CQRS_App.Infrastructure.Data;

internal sealed class EfReadDbContext : IReadDbContext, IDisposable
{
    private readonly DBcontext _db;
    private bool _disposed;

    public EfReadDbContext(DBcontext db)
    {
        _db = db;
        _db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        _db.ChangeTracker.AutoDetectChangesEnabled = false;
    }

    public IQueryable<T> Query<T>() where T : class => _db.Set<T>().AsNoTracking();

    // Implement both sync and async disposal
    public void Dispose()
    {
        if (!_disposed)
        {
            _db?.Dispose();
            _disposed = true;
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (!_disposed)
        {
            if (_db != null)
            {
                await _db.DisposeAsync();
            }

            _disposed = true;
        }
    }
}
