using DatabaseHandler.Entity;

namespace DatabaseHandler.Model
{
    public class OrderModel
    {
        public bool Save(Order order)
        {
            // lưu order
            // lưu danh sách orderDetail 
            // tất cả trong một transaction.
            return false;
        }
    }
}