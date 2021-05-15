using CalculateDiscountsFP.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CalculateDiscountsFP
{
    public static class DiscountCalculations
    {
        private static Func<Order, bool> _isEligibleFor10Disc = o => o.Price * o.Quantity >= 500;
        private static Func<Order, bool> _isEligibleFor35Disc = o => o.Price * o.Quantity >= 1000;
        private static Func<Order, bool> _isEligibleFor50Disc = o => o.Price * o.Quantity >= 2000;
        private static Func<Order, double> _calculate10Disc = o => o.Price * o.Quantity / 10;
        private static Func<Order, double> _calculate35Disc = o => o.Price * o.Quantity * 35 / 100;
        private static Func<Order, double> _calculate50Disc = o => o.Price * o.Quantity / 2;


        public static void ActualDiscountCalculation()
        {
            GetOrders().Select(o => GetOrderDiscount(o, GetRules())).ToList()
                .ForEach(o => o.PrintOrder());
        }

        public static Order GetOrderDiscount(Order order, List<(Func<Order, bool> Qualifier, Func<Order, double> Calculator)> rules)
        {
            var discount = rules
                .Where(r => r.Qualifier(order))
                .Select(r => r.Calculator(order))
                .OrderBy(o => o).Take(3).ToList().GetAverage();

            order.Discount = discount;
            return order;
        }

        public static List<(Func<Order, bool> Qualifier, Func<Order, double> Calculator)> GetRules()
        {


            return new List<(Func<Order, bool> Qualifier, Func<Order, double> Calculator)>()
            {
                (_isEligibleFor10Disc,_calculate10Disc),
                (_isEligibleFor35Disc,_calculate35Disc),
                (_isEligibleFor50Disc,_calculate50Disc),
            };
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
