using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateDiscountsFP
{
    public static class DiscountCalculations
    {
        public static void ActualDiscountCalculation()
        {
            var orders = GetOrders();
            orders.Select(o => GetOrderDiscount(o, GetRules())).ToList();
            orders.ForEach(o => o.PrintOrder());
        }

        public static Order GetOrderDiscount(Order order, List<(Func<Order, bool> Qualifier, Func<Order, double> Calculator)> rules)
        {
            var discount = rules
                .Where(r => r.Qualifier(order))
                .Select(r => r.Calculator(order))
                .OrderBy(o => o).Take(3).Average();

            order.Discount = discount;
            return order;
        }

        public static List<(Func<Order, bool> Qualifier, Func<Order, double> Calculator)> GetRules()
        {
            return new List<(Func<Order, bool> Qualifier, Func<Order, double> Calculator)>()
            {
                (IsEligibleFor0Disc,Calculate0Disc),
                (IsEligibleFor10Disc,Calculate10Disc),
                (IsEligibleFor35Disc,Calculate35Disc),
                (IsEligibleFor50Disc,Calculate50Disc),
            };
        }

        public static bool IsEligibleFor0Disc(Order order)
        {
            return order.Price * order.Quantity < 500;
        }

        public static double Calculate0Disc(Order order)
        {
            return 0;
        }

        public static bool IsEligibleFor10Disc(Order order)
        {
            return order.Price * order.Quantity >= 500;
        }

        public static double Calculate10Disc(Order order)
        {
            return order.Price * order.Quantity / 10;
        }

        public static bool IsEligibleFor35Disc(Order order)
        {
            return order.Price * order.Quantity >= 1000;
        }

        public static double Calculate35Disc(Order order)
        {
            return order.Price * order.Quantity * 35 / 100;
        }

        public static bool IsEligibleFor50Disc(Order order)
        {
            return order.Price * order.Quantity >= 2000;
        }

        public static double Calculate50Disc(Order order)
        {
            return order.Price * order.Quantity / 2;
        }

        public static List<Order> GetOrders()
        {
            return new List<Order>()
            {
                new Order(){Id=1,Price=35,Quantity=10},
                new Order(){Id=2,Price=10,Quantity=200},
                new Order(){Id=3,Price=5.5,Quantity=25},
                new Order(){Id=4,Price=10.1,Quantity=53},
                new Order(){Id=5,Price=20,Quantity=65},
            };
        }
    }
}
