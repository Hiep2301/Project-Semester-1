using Persistence;

namespace DAL
{
    public interface IOrderDAL
    {
        public bool CreateOrder(Orders order);
        public List<Orders> GetAllOrderInDay();
    }
}