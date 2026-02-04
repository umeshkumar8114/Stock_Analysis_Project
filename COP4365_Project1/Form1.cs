using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO; // robust CSV parser



namespace COP4365_Project1
{
    public partial class Form1 : Form
    {
        // Holds the full set loaded from CSV
        private List<CandleStick> listOfCandlesticks = new List<CandleStick>();

        // Holds the filtered set and is safe for binding
        private BindingList<CandleStick> boundCandlesticks = new BindingList<CandleStick>();

        private string loadedSymbol = "";   // holds ticker symbol like AAPL
        private string loadedPeriod = "";   // holds period like Day, Week, or Month


        /// <summary>
        /// Initializes the form, configures default dates, maps grid columns, and sets chart colors.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
           

            dateTimePicker_startDate.Value = new DateTime(2024, 1, 1); // preset start date
            dateTimePicker_endDate.Value = DateTime.Today; // preset end date


            var s = chart_stockData.Series["OHLC"]; // get candle series
            s["PriceUpColor"] = "Lime"; // up color
            s["PriceDownColor"] = "Red"; // down color

            chart_stockData.Legends.Clear();        // remove chart legend for more space
            openFileDialog_pickFile.Multiselect = true; // allow multiple stock files

        }

        /// <summary>
        /// Orchestrates read -> filter -> normalize -> display.
        /// </summary>
        private void update()
        {
            readCandlesticksFromFile(); // load CSV into listOfCandlesticks
            filterCandlesticks(); // apply date range into boundCandlesticks
            normalize(); // set Y axis min/max with 2% padding
            displayCandlesticks(); // bind to grid and chart
        }

        /// <summary>
        /// Reads CSV with header: Ticker, Period, Date, Open, High, Low, Close, Volume.
        /// Ignores Ticker and Period. Returns CandleStick list sorted by Date.
        /// </summary>
        private List<CandleStick> readCandlesticksFromFile(string filePath)
        {
            var result = new List<CandleStick>(); // output

            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath)) return result; // guard

            // auto-detect delimiter from header line
            string firstLine = File.ReadLines(filePath).FirstOrDefault() ?? string.Empty; // header
            string delimiter = firstLine.Contains(';') ? ";" : ","; // pick ; or ,

            using (var parser = new TextFieldParser(filePath))
            {
                parser.SetDelimiters(delimiter);              // set delimiter
                parser.HasFieldsEnclosedInQuotes = true;      // handle quoted fields
                parser.TrimWhiteSpace = true;                 // trim spaces

                if (parser.EndOfData) return result;          // empty file guard

                // ---- header ----
                string[] header = parser.ReadFields() ?? Array.Empty<string>(); // read header row
                string Clean(string s) => (s ?? "").Replace("\ufeff", "").Trim().Trim('"').ToLowerInvariant().Replace(" ", ""); //normalize
                var cols = header.Select(Clean).ToArray();     // normalize header

                // map indices (allow simple aliases)
                int Find(params string[] names)              // define a helper method that can take multiple possible column names
                {
                    for (int i = 0; i < cols.Length; i++)   // loop through all column names in the header
                        foreach (var n in names)            // loop through all aliases given
                            if (cols[i] == n) return i;     // if a header column matches one alias, return that column index
                    return -1;                              // return -1 if no match found
                }

                // find the index of each needed column using common name variations
                int idxDate = Find("date");                         // look for "date" column
                int idxOpen = Find("open", "openprice");            // look for "open" or "openprice"
                int idxHigh = Find("high", "maxprice");             // look for "high" or "maxprice"
                int idxLow = Find("low", "minprice");               // look for "low" or "minprice"
                int idxClose = Find("close", "closeprice", "adjclose"); // look for "close", "closeprice", or "adjclose"
                int idxVol = Find("volume", "vol");                 // look for "volume" or "vol"

                // verify that all required columns were found
                if (idxDate < 0 || idxOpen < 0 || idxHigh < 0 || idxLow < 0 || idxClose < 0 || idxVol < 0)
                {
                    // show message if any required column is missing
                    MessageBox.Show(this,
                        "CSV header parsed as:\n\n" + string.Join(" | ", cols) +  // show what the header was
                        "\n\nExpected: date, open, high, low, close, volume.",    // show what we expected
                        "CSV Header Mapping Failed",                              // title of message box
                        MessageBoxButtons.OK, MessageBoxIcon.Error);              // show error icon
                    return result;                                                // stop and return empty list
                }

                // ---- rows ----
                while (!parser.EndOfData)                        // loop until the parser reaches end of file
                {
                    string[] cells = parser.ReadFields();        // read the next line and split into fields automatically
                    if (cells == null) continue;                 // skip if line is null (empty)

                    int need = Math.Max(idxVol, Math.Max(idxClose, idxDate)); // get the largest column index we’ll need
                    if (cells.Length <= need) continue;          // skip if this line doesn’t have enough columns

                    // extract and clean each field from the CSV
                    string dateText = (cells[idxDate] ?? "").Trim().Trim('"');  // trim spaces and quotes from date
                    string openText = (cells[idxOpen] ?? "").Trim().Trim('"');  // trim spaces and quotes from open
                    string highText = (cells[idxHigh] ?? "").Trim().Trim('"');  // trim spaces and quotes from high
                    string lowText = (cells[idxLow] ?? "").Trim().Trim('"');    // trim spaces and quotes from low
                    string closeText = (cells[idxClose] ?? "").Trim().Trim('"');// trim spaces and quotes from close
                    string volText = (cells[idxVol] ?? "").Trim().Trim('"').Replace(",", ""); // trim and remove commas in volume

                    // parse the cleaned text values into proper data types
                    if (!DateTime.TryParseExact(dateText, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt)) continue; // skip if date invalid
                    if (!decimal.TryParse(openText, NumberStyles.Number, CultureInfo.InvariantCulture, out var open)) continue;  // skip if open invalid
                    if (!decimal.TryParse(highText, NumberStyles.Number, CultureInfo.InvariantCulture, out var high)) continue;  // skip if high invalid
                    if (!decimal.TryParse(lowText, NumberStyles.Number, CultureInfo.InvariantCulture, out var low)) continue;    // skip if low invalid
                    if (!decimal.TryParse(closeText, NumberStyles.Number, CultureInfo.InvariantCulture, out var close)) continue;// skip if close invalid
                    if (!ulong.TryParse(volText, out var vol)) continue;                                                         // skip if volume invalid

                    // create a new CandleStick object using the parsed values and add to list
                    result.Add(new CandleStick(dt, open, high, low, close, vol));
                }

            }

            // sort ascending by date
            return result.OrderBy(r => r.Date).ToList();
        }


        /// <summary>
        /// Wrapper: reads CSV using the selected file from OpenFileDialog.
        /// </summary>
        private void readCandlesticksFromFile()
        {
            listOfCandlesticks = readCandlesticksFromFile(openFileDialog_pickFile.FileName); // load file

        }

        /// <summary>
        /// Filters by date range and returns a new list.
        /// </summary>
        /// <param name="input">Unfiltered list.</param>
        /// <param name="start">Start date inclusive.</param>
        /// <param name="end">End date inclusive.</param>
        /// <returns>Filtered list.</returns>
        private List<CandleStick> filterCandlesticks(List<CandleStick> input, DateTime start, DateTime end)
        {
            return input.Where(c => c.Date >= start.Date && c.Date <= end.Date).ToList(); // filter by dates
        }

        /// <summary>
        /// Wrapper: filters current list using the two DateTimePickers.
        /// </summary>
        private void filterCandlesticks()
        {
            var filtered = filterCandlesticks(listOfCandlesticks, dateTimePicker_startDate.Value, dateTimePicker_endDate.Value); // get range
            boundCandlesticks = new BindingList<CandleStick>(filtered); // bindable list
        }

        /// <summary>
        /// Sets AxisY min/max on the candle chart area with 2% padding.
        /// </summary>
        /// <param name="input">Filtered list.</param>
        private void normalize(List<CandleStick> input)
        {
            if (input == null || input.Count == 0) return; // nothing to do

            double minLow = (double)input.Min(c => c.MinPrice); // min low
            double maxHigh = (double)input.Max(c => c.MaxPrice); // max high

            var area = chart_stockData.ChartAreas["ChartArea_candlesticks"]; // target area
            area.AxisY.Minimum = minLow * 0.98; // pad 2% below
            area.AxisY.Maximum = maxHigh * 1.02; // pad 2% above
        }

        /// <summary>
        /// Wrapper: normalizes using the current filtered list.
        /// </summary>
        private void normalize()
        {
            normalize(boundCandlesticks.ToList()); // call worker
        }

        /// <summary>
        /// Binds filtered data to the grid and the chart, then refreshes.
        /// </summary>
        /// <param name="input">Filtered list.</param>
        private void displayCandlesticks(List<CandleStick> input)
        {
            candlestickBindingSource.DataSource = input; // grid source
            chart_stockData.DataSource = input; // chart source
            chart_stockData.DataBind(); // refresh chart

            var priceArea = chart_stockData.ChartAreas["ChartArea_candlesticks"]; // get price area
            priceArea.AxisX.MajorGrid.Enabled = false; // cleaner X grid

            var volArea = chart_stockData.ChartAreas["ChartArea_volume"]; // get volume area
            volArea.AlignWithChartArea = "ChartArea_candlesticks"; // align X axes


            chart_stockData.Titles.Clear();
            chart_stockData.Titles.Add($"{loadedSymbol}-{loadedPeriod}");
            chart_stockData.Titles.Add($"{dateTimePicker_startDate.Value.ToShortDateString()} – {dateTimePicker_endDate.Value.ToShortDateString()}");


        }

        /// <summary>
        /// Wrapper: displays current filtered list.
        /// </summary>
        private void displayCandlesticks()
        {
            displayCandlesticks(boundCandlesticks.ToList()); // call worker
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker_startDate_ValueChanged(object sender, EventArgs e)
        {
            if (listOfCandlesticks.Count == 0) return; // no data yet
            //filterCandlesticks(); // re-filter
            //normalize(); // re-normalize
            //displayCandlesticks(); // re-bind and redraw
        }

        private void dataGridView_tickerData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dateTimePicker_endDate_ValueChanged(object sender, EventArgs e)
        {
            if (listOfCandlesticks.Count == 0) return; // no data yet
            //filterCandlesticks(); // re-filter
            //normalize(); // re-normalize
            //displayCandlesticks(); // re-bind and redraw
        }

        /// <summary>
        /// Opens a file picker filtered to CSV, defaulting to the "Stock Data" folder.
        /// </summary>
        private void button_pickFile_Click(object sender, EventArgs e)
        {
            openFileDialog_pickFile.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*"; // only CSV by default
            openFileDialog_pickFile.InitialDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Stock Data"); // default folder
            openFileDialog_pickFile.ShowDialog(this); // open dialog
        }
        /// <summary>
        /// When a file is chosen, run the full pipeline and handle any load errors.
        /// </summary>
        private void openFileDialog_pickFile_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                // allow multiple file selection
                var files = openFileDialog_pickFile.FileNames;                  // get all selected file paths
                if (files.Length == 0) return;                                  // exit if no file chosen

                for (int i = 0; i < files.Length; i++)                          // loop through each selected file
                {
                    string file = files[i];                                     // current file path
                    var list = readCandlesticksFromFile(file);                  // parse CSV into CandleStick list
                    if (list.Count == 0) continue;                              // skip empty or invalid files

                    // extract symbol & period from filename like AAPL-Day.csv
                    var name = Path.GetFileNameWithoutExtension(file);           // get filename without extension
                    var parts = name.Split('-');                                 // split by dash to separate symbol & period
                    string symbol = parts.Length > 0 ? parts[0].ToUpper() : "UNKNOWN"; // first part is ticker, fallback "UNKNOWN"
                    string period = parts.Length > 1 ? parts[1] : "Day";         // second part is period, fallback "Day"

                    if (i == 0)                                                  // first file → main window
                    {
                        listOfCandlesticks = list;                               // store data for main form
                        loadedSymbol = symbol;                                   // remember symbol for title
                        loadedPeriod = period;                                   // remember period for title
                        filterCandlesticks();                                    // filter by current date pickers
                        normalize();                                             // adjust Y-axis scale
                        displayCandlesticks();                                   // draw chart on main form
                    }
                    else                                                         // subsequent files → new windows
                    {
                        var sf = new StockForm(symbol, period, list,             // create new StockForm for each additional file
                                               dateTimePicker_startDate.Value,   // pass current start date
                                               dateTimePicker_endDate.Value);    // pass current end date
                        sf.Show();                                               // open the new StockForm window
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Failed to load file(s).\n\n{ex.Message}", // show error if something goes wrong
                    "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);    // with message box title and icon
            }


        }
        /// <summary>
        /// Re-applies the date filters and redraws the chart
        /// using the currently loaded data (no reload required).
        /// </summary>
        private void button_refresh_Click(object sender, EventArgs e)
        {
            // If no data has been loaded yet, silently ignore
            if (listOfCandlesticks == null || listOfCandlesticks.Count == 0)
                return;

            // Re-filter based on current date picker values
            filterCandlesticks();

            // Recalculate axis range and rebind chart
            normalize();
            displayCandlesticks();
        }
    }
}
