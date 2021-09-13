using Microsoft.EntityFrameworkCore;
using System;

namespace NotificationScheduling.Infrastructure
{
    public class DbFactory : IDisposable
    {
        private bool _disposed;
        private Func<NotificationSchedulingContext> _instanceFunc;
        private DbContext _dbContext;
        public DbContext DbContext => _dbContext ?? (_dbContext = _instanceFunc.Invoke());

        public DbFactory(Func<NotificationSchedulingContext> dbContextFactory)
        {
            _instanceFunc = dbContextFactory;
        }

        public void Dispose()
        {
            if (!_disposed && _dbContext != null)
            {
                _disposed = true;
                _dbContext.Dispose();
            }
        }
    }
}
