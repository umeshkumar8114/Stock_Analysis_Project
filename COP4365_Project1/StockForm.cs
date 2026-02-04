using COP4365_Project1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COP4365_Project1
{
    public partial class StockForm : Form
    {
        private List<CandleStick> candles;   // holds all candlestick data (price + volume) for the loaded stock
        private string symbol;               // stores the stock ticker symbol (e.g., "AAPL")
        private string period;               // stores the selected time period (e.g., "Day", "Week", or "Month")
        private DateTime startDate;          // keeps track of the current start date filter for the chart
        private DateTime endDate;            // keeps track of the current end date filter for the chart


        public StockForm(string symbol, string period, List<CandleStick> candles,
                         DateTime start, DateTime end)
        {
            InitializeComponent();  // build form controls
            this.symbol = symbol;                       // store the stock ticker symbol (e.g., AAPL)
            this.period = period;                       // store the time period (e.g., Day, Week, Month)
            this.candles = candles;                     // store the full list of candlestick data passed from the main form
            this.startDate = start;                     // store the starting date range initially selected on the main form
            this.endDate = end;                         // store the ending date range initially selected on the main form

            dateTimePicker_startDate.Value = start;     // set the start date picker on this StockForm to match the passed start date
            dateTimePicker_endDate.Value = end;         // set the end date picker on this StockForm to match the passed end date

            chart_stockData.Legends.Clear();            // remove the default legend from the chart to save space
            DisplayCandlesticks();                      // draw the chart using the filtered candlestick data

            var s = chart_stockData.Series["OHLC"];     // get the candlestick series from the chart
            s["PriceUpColor"] = "Lime";                 // set green color for price increases (close > open)
            s["PriceDownColor"] = "Red";                // set red color for price decreases (close < open)


        }
        /// <summary>
        /// Applies new start/end date filters and refreshes chart without reloading.
        /// </summary>
        private void button_refresh_Click(object sender, EventArgs e)
        {
            if (candles == null || candles.Count == 0) return;   // no data yet

            // use new values from pickers
            startDate = dateTimePicker_startDate.Value;  //starting date
            endDate = dateTimePicker_endDate.Value;  //ending date

            // re-filter and redraw chart
            DisplayCandlesticks();
        }

        private void DisplayCandlesticks()
        {
            // filter candles based on selected start and end dates from the pickers
            var filtered = candles
                .Where(c => c.Date >= dateTimePicker_startDate.Value &&   // include only candles on or after start date
                            c.Date <= dateTimePicker_endDate.Value)       // include only candles on or before end date
                .ToList();                                                // convert result to a list


            chart_stockData.DataSource = new BindingList<CandleStick>(filtered); // prepare filtered data for chart binding
            chart_stockData.Series["OHLC"].XValueMember = "Date";                // ensure X-axis uses Date
            chart_stockData.Series["OHLC"].YValueMembers = "MaxPrice,MinPrice,OpenPrice,ClosePrice"; // rebind OHLC
            chart_stockData.Series["Volume"].XValueMember = "Date";              // ensure Volume chart uses Date
            chart_stockData.Series["Volume"].YValueMembers = "Volume";           // rebind Volume data

            chart_stockData.Titles.Clear();                                         // remove old titles if present

            // add stock name and period as first line of the chart title
            chart_stockData.Titles.Add($"{symbol}-{period}");                       // e.g., "AAPL-Day"

            // add date range as second line of the chart title
            chart_stockData.Titles.Add($"{dateTimePicker_startDate.Value.ToShortDateString()} – {dateTimePicker_endDate.Value.ToShortDateString()}");

            // if there is at least one candle in the filtered list, adjust Y-axis range
            if (filtered.Count > 0)
            {
                var area = chart_stockData.ChartAreas["ChartArea_candlesticks"];    // access main price chart area

                area.AxisY.Minimum = (double)filtered.Min(c => c.MinPrice) * 0.98;  // set Y-axis lower limit (2% padding below min)
                area.AxisY.Maximum = (double)filtered.Max(c => c.MaxPrice) * 1.02;  // set Y-axis upper limit (2% padding above max)
            }

            // rebind and refresh chart visuals
            chart_stockData.DataBind();                                             // update chart with filtered data
            chart_stockData.Invalidate();                                           // force chart to redraw
            chart_stockData.Update();                                               // apply visual updates immediately

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
