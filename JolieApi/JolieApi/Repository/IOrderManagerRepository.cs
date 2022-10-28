using JolieApi.DataContext;
using JolieApi.Models;
using JolieApi.ViewModels;

namespace JolieApi.Repository
{
    public interface IOrderManagerRepository
    {
        List<Order> GetOrders();
        void PlaceOrder(PlaceOrderRequest order);
    }

    public class OrderManagerRepository : IOrderManagerRepository
    {
        private readonly JolieDataContext _context;

        public OrderManagerRepository(JolieDataContext context)
        {
            this._context = context;
        }

        public List<Order> GetOrders()
        {
            return _context.orders.Select(s => s).ToList();
        }

        public void PlaceOrder(PlaceOrderRequest order)
        {
            Order newOrder = new()
            {
                order_id = 0,
                user_id = order.user_id,
                natural_hair_type = order.natural_hair_type,
                hair_structure = order.hair_structure,
                scalp_moisture = order.scalp_moisture,
                hair_treat = order.hair_treat,
                hair_goal = order.hair_goal,
                formula = order.formula,
                color = order.color,
                scent = order.scent,
                shampoo_name = order.shampoo_name,
                created_at = DateTime.UtcNow
            };

            _context.orders.Add(newOrder);
            _context.SaveChanges();
        }
    }
}