namespace COP4365_Project1
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.dateTimePicker_startDate = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker_endDate = new System.Windows.Forms.DateTimePicker();
            this.label_startDate = new System.Windows.Forms.Label();
            this.label_endDate = new System.Windows.Forms.Label();
            this.button_pickFile = new System.Windows.Forms.Button();
            this.openFileDialog_pickFile = new System.Windows.Forms.OpenFileDialog();
            this.chart_stockData = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.button_refresh = new System.Windows.Forms.Button();
            this.candlestickBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.chart_stockData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.candlestickBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dateTimePicker_startDate
            // 
            this.dateTimePicker_startDate.Location = new System.Drawing.Point(125, 17);
            this.dateTimePicker_startDate.Name = "dateTimePicker_startDate";
            this.dateTimePicker_startDate.Size = new System.Drawing.Size(293, 31);
            this.dateTimePicker_startDate.TabIndex = 0;
            this.dateTimePicker_startDate.ValueChanged += new System.EventHandler(this.dateTimePicker_startDate_ValueChanged);
            // 
            // dateTimePicker_endDate
            // 
            this.dateTimePicker_endDate.Location = new System.Drawing.Point(125, 54);
            this.dateTimePicker_endDate.Name = "dateTimePicker_endDate";
            this.dateTimePicker_endDate.Size = new System.Drawing.Size(293, 31);
            this.dateTimePicker_endDate.TabIndex = 1;
            this.dateTimePicker_endDate.ValueChanged += new System.EventHandler(this.dateTimePicker_endDate_ValueChanged);
            // 
            // label_startDate
            // 
            this.label_startDate.AutoSize = true;
            this.label_startDate.Location = new System.Drawing.Point(9, 20);
            this.label_startDate.Name = "label_startDate";
            this.label_startDate.Size = new System.Drawing.Size(108, 25);
            this.label_startDate.TabIndex = 2;
            this.label_startDate.Text = "Start Date";
            this.label_startDate.Click += new System.EventHandler(this.label1_Click);
            // 
            // label_endDate
            // 
            this.label_endDate.AutoSize = true;
            this.label_endDate.Location = new System.Drawing.Point(9, 57);
            this.label_endDate.Name = "label_endDate";
            this.label_endDate.Size = new System.Drawing.Size(101, 25);
            this.label_endDate.TabIndex = 3;
            this.label_endDate.Text = "End Date";
            // 
            // button_pickFile
            // 
            this.button_pickFile.Location = new System.Drawing.Point(12, 98);
            this.button_pickFile.Name = "button_pickFile";
            this.button_pickFile.Size = new System.Drawing.Size(282, 60);
            this.button_pickFile.TabIndex = 4;
            this.button_pickFile.Text = "Choose Ticker and Period";
            this.button_pickFile.UseVisualStyleBackColor = true;
            this.button_pickFile.Click += new System.EventHandler(this.button_pickFile_Click);
            // 
            // openFileDialog_pickFile
            // 
            this.openFileDialog_pickFile.FileName = "AAPL-Month.csv";
            this.openFileDialog_pickFile.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_pickFile_FileOk);
            // 
            // chart_stockData
            // 
            this.chart_stockData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea_candlesticks";
            chartArea2.AlignWithChartArea = "ChartArea_candlesticks";
            chartArea2.Name = "ChartArea_volume";
            this.chart_stockData.ChartAreas.Add(chartArea1);
            this.chart_stockData.ChartAreas.Add(chartArea2);
            this.chart_stockData.Location = new System.Drawing.Point(436, 18);
            this.chart_stockData.Name = "chart_stockData";
            series1.ChartArea = "ChartArea_candlesticks";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
            series1.IsXValueIndexed = true;
            series1.Name = "OHLC";
            series1.XValueMember = "Date";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            series1.YValueMembers = "MaxPrice, MinPrice, OpenPrice, ClosePrice";
            series1.YValuesPerPoint = 4;
            series2.ChartArea = "ChartArea_volume";
            series2.IsXValueIndexed = true;
            series2.Name = "Volume";
            series2.XValueMember = "Date";
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            series2.YValueMembers = "Volume";
            series2.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.UInt64;
            this.chart_stockData.Series.Add(series1);
            this.chart_stockData.Series.Add(series2);
            this.chart_stockData.Size = new System.Drawing.Size(1020, 782);
            this.chart_stockData.TabIndex = 6;
            this.chart_stockData.Text = "chart1";
            // 
            // button_refresh
            // 
            this.button_refresh.Location = new System.Drawing.Point(14, 188);
            this.button_refresh.Name = "button_refresh";
            this.button_refresh.Size = new System.Drawing.Size(169, 68);
            this.button_refresh.TabIndex = 7;
            this.button_refresh.Text = "Refresh Chart";
            this.button_refresh.UseVisualStyleBackColor = true;
            this.button_refresh.Click += new System.EventHandler(this.button_refresh_Click);
            // 
            // candlestickBindingSource
            // 
            this.candlestickBindingSource.DataSource = typeof(COP4365_Project1.CandleStick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1474, 1129);
            this.Controls.Add(this.button_refresh);
            this.Controls.Add(this.chart_stockData);
            this.Controls.Add(this.button_pickFile);
            this.Controls.Add(this.label_endDate);
            this.Controls.Add(this.label_startDate);
            this.Controls.Add(this.dateTimePicker_endDate);
            this.Controls.Add(this.dateTimePicker_startDate);
            this.Name = "Form1";
            this.Text = "Stock data";
            ((System.ComponentModel.ISupportInitialize)(this.chart_stockData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.candlestickBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker_startDate;
        private System.Windows.Forms.DateTimePicker dateTimePicker_endDate;
        private System.Windows.Forms.Label label_startDate;
        private System.Windows.Forms.Label label_endDate;
        private System.Windows.Forms.Button button_pickFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog_pickFile;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_stockData;
        private System.Windows.Forms.BindingSource candlestickBindingSource;
        private System.Windows.Forms.Button button_refresh;
    }
}

