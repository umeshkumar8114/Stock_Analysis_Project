# Multi-Form Stock Market Visualizer

A C# Windows Forms application built with **Visual Studio 2022** that processes and visualizes historical stock market data. This project demonstrates advanced data handling, normalization techniques, and the use of multi-form UI architecture.

## 📈 Key Features
* **Multi-Form Interface:** Automatically launches a new form for every stock selected, with the primary stock anchored in the main input window.
* **OHLC Candlestick Charts:** Renders stock prices in professional candlestick format with custom color logic (Green/Lime for Up, Red for Down).
* **Volume Plotting:** Integrated Column plots to visualize trading volume alongside price action.
* **Data Normalization:** All charts are normalized to ensure accurate visual comparison across different price scales.
* **Gapless Timeline:** Custom logic to remove weekend and holiday gaps, providing a clean, continuous horizontal axis for daily data.
* **Dynamic Updating:** A "Refresh" feature allows users to update chart parameters instantly without re-parsing the source CSV files.

## 🛠 Technical Details
* **Language:** C#
* **Framework:** .NET Framework (Windows Forms)
* **Data Sources:** Local Yahoo Finance CSV files (Daily, Weekly, and Monthly formats).
* **Design Pattern:** Utilizes **Data Binding** to link stock data objects directly to the Chart control for high performance.
* **Naming Convention:** All UI controls follow the strict `controlType_name` convention (e.g., `button_loadData`, `chart_stockPrice`).

## 📂 Data Structure
The application expects stock data to be stored in a directory named `Stock Data`. Files should follow the naming convention:
- `XXX-Day.csv`
- `XXX-Week.csv`
- `XXX-Month.csv`

## 📝 Documentation
All functions and methods include XML documentation (`///`) explaining purposes, arguments, and return values. Every line of logic is individually commented to ensure code maintainability and clarity.
