using System;
using System.Collections.Generic;
using System.Linq;

namespace StockPurchaseDictionary
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> stocks = new Dictionary<string, string>
            {
                { "aapl", "Apple" },
                { "ge", "General Electric" },
                { "msft", "Microsoft" }
            };

            stocks.Add("ip", "International Paper");

            List<(string ticker, int shares, double price)> purchases = new List<(string, int, double)>();
            purchases.Add(("aapl",10,100 ));
            purchases.Add(("aapl", 40, 101.23 ));
            purchases.Add(("aapl", 25, 98.02));
            purchases.Add(("ge", 4, 8.75));
            purchases.Add(("ge", 59, 7.36));
            purchases.Add(("ge", 100, 11.22));
            purchases.Add(("msft", 4, 50.88));
            purchases.Add(("msft", 89, 65.54));
            purchases.Add(("msft", 37, 59.01));
            purchases.Add(("ip", 56, 22.22));
            purchases.Add(("ip", 1, 23.00));
            purchases.Add(("ip", 1000, 9.50));


            Dictionary<string, double> totalHeldAtValue = new Dictionary<string, double>();

            // joins stocks dictionary with purchases list
            var fullTrades = stocks.Join(purchases,
                stock => stock.Key,
                purchase => purchase.ticker,
                (s, p) => new { CompName = s.Value, Shares = p.shares , Price = p.price}
                );

            foreach (var trade in fullTrades)
            {
                // checks if key exists in dictionary
                if (totalHeldAtValue.ContainsKey(trade.CompName))
                {
                    //adds current value of invested and adds it to the incoming equity value
                    totalHeldAtValue[trade.CompName] = totalHeldAtValue[trade.CompName] + TotalInvested(trade.Shares, trade.Price);
                } else
                {
                    // adds key/value pair to dictionary cause it doesnt exist
                    totalHeldAtValue.Add(trade.CompName, TotalInvested(trade.Shares, trade.Price));
                }
            }

            // multiplies shares by purchase price
            double TotalInvested(int shares, double price)
            {
                return price * shares;
            }

            Console.ReadKey();
        }
    }
}
