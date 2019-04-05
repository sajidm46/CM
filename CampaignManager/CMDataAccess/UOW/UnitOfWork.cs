
using CMDataAccess.Repositories.Abstract;
using CMDataAccess.Repositories.Concrete;
using EZRunner.Logger;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDataAccess.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private IEzLog _logger = EzLogger.Instance;
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["CampaignManagerDB"].ConnectionString;

        //private INotificationRepository _notificationRepository;
        private ICampaignRepository _campaignRepository;
        private ICustomerDetailsRepository _customerDetailsRepository;
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private bool _disposed;

        public UnitOfWork()
        {
            _logger.LogError("About to open connection");
            _logger.LogError("ConnectionString:" + connectionString);
            try
            {
                _connection = new SqlConnection(connectionString);
                _connection.Open();
                _transaction = _connection.BeginTransaction();
            }
            catch (Exception e)
            {

                _logger.LogError("ERROR CONNECTING " + e.Message);
            }

        }

        //public INotificationRepository NotificationRepository
        //{
        //    get { return _notificationRepository ?? (_notificationRepository = new NotificationRepository(_transaction)); }
        //}

        public ICampaignRepository CampaignRepository
        {
            get { return _campaignRepository ?? (_campaignRepository = new CampaignRepository(_transaction)); }
        }

        public ICustomerDetailsRepository CustomerDetailsRepository
        {
            get { return _customerDetailsRepository ?? (_customerDetailsRepository = new CustomerDetailsRepository(_transaction)); }
        }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                resetRepositories();
            }
        }

        private void resetRepositories()
        {
            //_notificationRepository = null;
            _campaignRepository = null;
        }

        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        private void dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _disposed = true;
            }
        }

        ~UnitOfWork()
        {
            dispose(false);
        }
    }
}
