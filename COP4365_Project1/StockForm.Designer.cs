namespace COP4365_Project1
{
    partial class StockForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea9 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea10 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series9 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series10 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart_stockData = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.button_refresh = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker_startDate = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker_endDate = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.chart_stockData)).BeginInit();
            this.SuspendLayout();
            // 
            // chart_stockData
            // 
            this.chart_stockData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea9.Name = "ChartArea_candlesticks";
            chartArea10.AlignWithChartArea = "ChartArea_candlesticks";
            chartArea10.Name = "ChartArea_volume";
            this.chart_stockData.ChartAreas.Add(chartArea9);
            this.chart_stockData.ChartAreas.Add(chartArea10);
            this.chart_stockData.Location = new System.Drawing.Point(37, 153);
            this.chart_stockData.Name = "chart_stockData";
            series9.ChartArea = "ChartArea_candlesticks";
            series9.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
            series9.IsXValueIndexed = true;
            series9.Legend = "Legend1";
            series9.Name = "OHLC";
            series9.XValueMember = "Date";
            series9.YValueMembers = "MaxPrice, MinPrice, OpenPrice, ClosePrice";
            series9.YValuesPerPoint = 4;
            series10.ChartArea = "ChartArea_volume";
            series10.IsXValueIndexed = true;
            series10.Legend = "Legend1";
            series10.Name = "Volume";
            series10.XValueMember = "Date";
            series10.YValueMembers = "Volume";
            this.chart_stockData.Series.Add(series9);
            this.chart_stockData.Series.Add(series10);
            this.chart_stockData.Size = new System.Drawing.Size(838, 583);
            this.chart_stockData.TabIndex = 0;
            this.chart_stockData.Text = "chart1";
            this.chart_stockData.Click += new System.EventHandler(this.chart1_Click);
            // 
            // button_refresh
            // 
            this.button_refresh.Location = new System.Drawing.Point(550, 49);
            this.button_refresh.Name = "button_refresh";
            this.button_refresh.Size = new System.Drawing.Size(243, 71);
            this.button_refresh.TabIndex = 1;
            this.button_refresh.Text = "Refresh Chart";
            this.button_refresh.UseVisualStyleBackColor = true;
            this.button_refresh.Click += new System.EventHandler(this.button_refresh_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Start Date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "End Date";
            // 
            // dateTimePicker_startDate
            // 
            this.dateTimePicker_startDate.Location = new System.Drawing.Point(161, 28);
            this.dateTimePicker_startDate.Name = "dateTimePicker_startDate";
            this.dateTimePicker_startDate.Size = new System.Drawing.Size(359, 31);
            this.dateTimePicker_startDate.TabIndex = 4;
            // 
            // dateTimePicker_endDate
            // 
            this.dateTimePicker_endDate.Location = new System.Drawing.Point(161, 89);
            this.dateTimePicker_endDate.Name = "dateTimePicker_endDate";
            this.dateTimePicker_endDate.Size = new System.Drawing.Size(359, 31);
            this.dateTimePicker_endDate.TabIndex = 5;
            // 
            // StockForm
            // 
            this.ClientSize = new System.Drawing.Size(930, 748);
            this.Controls.Add(this.dateTimePicker_endDate);
            this.Controls.Add(this.dateTimePicker_startDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_refresh);
            this.Controls.Add(this.chart_stockData);
            this.Name = "StockForm";
            ((System.ComponentModel.ISupportInitialize)(this.chart_stockData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart_stockData;
        private System.Windows.Forms.Button button_refresh;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePicker_startDate;
        private System.Windows.Forms.DateTimePicker dateTimePicker_endDate;
    }
}