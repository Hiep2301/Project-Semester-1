using Persistence;
using DAL;

namespace BL
{
    public class OrderBL
    {
        private OrderDAL orderDal = new OrderDAL();
        public bool CreateOrder(Orders order)
        {
            return orderDal.CreateOrder(order);
        }
    }
}