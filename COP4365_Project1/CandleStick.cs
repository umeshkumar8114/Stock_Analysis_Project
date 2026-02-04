using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COP4365_Project1
{
    public class CandleStick
    {
        public System.DateTime Date { get; set; }
        public decimal OpenPrice { get; set; }
        public decimal ClosePrice { get; set; }
        public decimal MaxPrice { get; set; }
        public decimal MinPrice { get; set; }
        public ulong Volume { get; set; }

        public CandleStick() { } // needed for binding

        // Full constructor used when creating a candlestick from CSV data
        public CandleStick(DateTime date, decimal open, decimal high, decimal low, decimal close, ulong volume)
        {
            Date = date;        // assign Date
            OpenPrice = open;   // assign Open price
            MaxPrice = high;    // assign High price
            MinPrice = low;     // assign Low price
            ClosePrice = close; // assign Close price
            Volume = volume;    // assign Volume
        }
    }
}

