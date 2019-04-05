using CMDataAccess.Repositories.Abstract;
using CMEntities.Entities;
using System.Data;

namespace CMDataAccess.Repositories.Concrete
{
    
    public class CustomerDetailsRepository : GenericRepository<CustomerDetails>, ICustomerDetailsRepository
    {
        public CustomerDetailsRepository(IDbTransaction transaction)
            : base(transaction)
        {

        }

    }
}
