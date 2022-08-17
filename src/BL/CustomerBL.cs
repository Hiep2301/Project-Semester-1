using Persistence;
using DAL;

namespace BL
{
    public class CustomerBL
    {
        private CustomerDAL customerDal = new CustomerDAL();

        public Customer Login(Customer customer)
        {
            return customerDal.Login(DbConfig.OpenConnection(), customer);
        }

        public Customer GetCustomerById(int id)
        {
            return customerDal.GetCustomerById(DbConfig.OpenConnection(), id);
        }
    }
}