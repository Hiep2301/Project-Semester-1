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

        public Customer GetCustomerByName(string name)
        {
            return customerDal.GetCustomerByName(DbConfig.OpenConnection(), name);
        }

        public List<Customer> GetAllCustomer()
        {
            return customerDal.GetAllCustomer(DbConfig.OpenConnection());
        }
    }
}