using Contact_zoo_at_home.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Contact_zoo_at_home.Application
{
    /// <summary>
    /// Just a little realization, of services with specified connection, transaction or dbContext
    /// </summary>
    public abstract class BaseService : IDisposable
    {
        private bool _disposeConnection;
        private bool _disposeDbContext = true;
        private DbConnection? _connection;
        private DbTransaction? _transaction;
        protected ApplicationDbContext _dbContext;

        public BaseService()
        {
            _disposeConnection = true;
            _connection = DBConnections.GetNewDbConnection();
            _dbContext = new ApplicationDbContext(_connection);
        }

        public BaseService(DbConnection activeDbConnection)
        {
            if(activeDbConnection == null)
            {
                throw new ArgumentNullException(nameof(activeDbConnection));
            }

            _connection = activeDbConnection;
            _dbContext = new ApplicationDbContext(_connection);
        }

        public BaseService(DbTransaction activeDbTransaction)
        {
            if (activeDbTransaction?.Connection is null)
            {
                throw new ArgumentNullException(nameof(activeDbTransaction));
            }

            _connection = activeDbTransaction.Connection;
            _transaction = activeDbTransaction;
            _dbContext = new ApplicationDbContext(_connection);
            _dbContext.Database.UseTransaction(activeDbTransaction);
        }

        public BaseService(ApplicationDbContext activeDbContext)
        {
            if (activeDbContext is null)
            {
                throw new ArgumentNullException(nameof(activeDbContext));
            }

            _disposeDbContext = false;
            _dbContext = activeDbContext;
        }

        public void Dispose()
        {
            // disposing class-managed connections
            // and do not disposing external connections

            if (_disposeDbContext)
            {
                _dbContext.Dispose();
            }

            if (_disposeConnection)
            {
                _connection?.Dispose();
            }
        }
    }
}
