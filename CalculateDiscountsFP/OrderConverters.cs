using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateDiscountsFP
{
    public static class OrderConverters
    {
        public static void PrintOrder(this Order order)
        {
            Console.WriteLine($"Order price is: {order.Price * order.Quantity:C1}, Eligible discount is: {order.Discount:C1}");
        }
    }
}
