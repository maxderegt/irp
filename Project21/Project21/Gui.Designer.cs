using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace Project21
{
    partial class clientGui
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(clientGui));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.loginPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.comPanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.comLabel = new System.Windows.Forms.Label();
            this.comTextBox = new System.Windows.Forms.TextBox();
            this.comOKButton = new System.Windows.Forms.Button();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.infoLabel = new System.Windows.Forms.Label();
            this.logInButton = new System.Windows.Forms.Button();
            this.passBox = new System.Windows.Forms.TextBox();
            this.accNameBox = new System.Windows.Forms.TextBox();
            this.clientPanel = new System.Windows.Forms.Panel();
            this.button6 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.button3 = new System.Windows.Forms.Button();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.secondsv = new System.Windows.Forms.Label();
            this.minutesv = new System.Windows.Forms.Label();
            this.energyv = new System.Windows.Forms.Label();
            this.reqpowerv = new System.Windows.Forms.Label();
            this.distancev = new System.Windows.Forms.Label();
            this.kmhv = new System.Windows.Forms.Label();
            this.rpmv = new System.Windows.Forms.Label();
            this.pulsev = new System.Windows.Forms.Label();
            this.actpowerv = new System.Windows.Forms.Label();
            this.actPower = new System.Windows.Forms.Label();
            this.seconds = new System.Windows.Forms.Label();
            this.minutes = new System.Windows.Forms.Label();
            this.energy = new System.Windows.Forms.Label();
            this.reqPower = new System.Windows.Forms.Label();
            this.distance = new System.Windows.Forms.Label();
            this.kmh = new System.Windows.Forms.Label();
            this.rpm = new System.Windows.Forms.Label();
            this.pulse = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.loginPanel.SuspendLayout();
            this.comPanel.SuspendLayout();
            this.clientPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.SuspendLayout();
            // 
            // loginPanel
            // 
            this.loginPanel.BackColor = System.Drawing.Color.Transparent;
            this.loginPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("loginPanel.BackgroundImage")));
            this.loginPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.loginPanel.Controls.Add(this.label1);
            this.loginPanel.Controls.Add(this.comPanel);
            this.loginPanel.Controls.Add(this.passwordLabel);
            this.loginPanel.Controls.Add(this.usernameLabel);
            this.loginPanel.Controls.Add(this.infoLabel);
            this.loginPanel.Controls.Add(this.logInButton);
            this.loginPanel.Controls.Add(this.passBox);
            this.loginPanel.Controls.Add(this.accNameBox);
            this.loginPanel.Location = new System.Drawing.Point(11, 11);
            this.loginPanel.Margin = new System.Windows.Forms.Padding(2);
            this.loginPanel.Name = "loginPanel";
            this.loginPanel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.loginPanel.Size = new System.Drawing.Size(663, 573);
            this.loginPanel.TabIndex = 4;
            this.loginPanel.Enter += new System.EventHandler(this.loginPanel_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(265, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 24);
            this.label1.TabIndex = 11;
            this.label1.Text = "Please login";
            // 
            // comPanel
            // 
            this.comPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.comPanel.BackColor = System.Drawing.Color.Transparent;
            this.comPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("comPanel.BackgroundImage")));
            this.comPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.comPanel.Controls.Add(this.label2);
            this.comPanel.Controls.Add(this.comLabel);
            this.comPanel.Controls.Add(this.comTextBox);
            this.comPanel.Controls.Add(this.comOKButton);
            this.comPanel.Location = new System.Drawing.Point(0, 0);
            this.comPanel.Margin = new System.Windows.Forms.Padding(2);
            this.comPanel.Name = "comPanel";
            this.comPanel.Size = new System.Drawing.Size(663, 599);
            this.comPanel.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.label2.Location = new System.Drawing.Point(219, 247);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(237, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "(If invalid COM port, a simulation will start)";
            // 
            // comLabel
            // 
            this.comLabel.AutoSize = true;
            this.comLabel.BackColor = System.Drawing.Color.Transparent;
            this.comLabel.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.comLabel.Location = new System.Drawing.Point(207, 68);
            this.comLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.comLabel.Name = "comLabel";
            this.comLabel.Size = new System.Drawing.Size(249, 24);
            this.comLabel.TabIndex = 2;
            this.comLabel.Text = "Please select COM port ";
            // 
            // comTextBox
            // 
            this.comTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.comTextBox.Location = new System.Drawing.Point(268, 195);
            this.comTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.comTextBox.Name = "comTextBox";
            this.comTextBox.Size = new System.Drawing.Size(141, 20);
            this.comTextBox.TabIndex = 1;
            // 
            // comOKButton
            // 
            this.comOKButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("comOKButton.BackgroundImage")));
            this.comOKButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.comOKButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comOKButton.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comOKButton.ForeColor = System.Drawing.Color.White;
            this.comOKButton.Location = new System.Drawing.Point(199, 356);
            this.comOKButton.Margin = new System.Windows.Forms.Padding(2);
            this.comOKButton.Name = "comOKButton";
            this.comOKButton.Size = new System.Drawing.Size(279, 58);
            this.comOKButton.TabIndex = 0;
            this.comOKButton.Text = "OK";
            this.comOKButton.UseVisualStyleBackColor = true;
            this.comOKButton.Click += new System.EventHandler(this.comOKButton_Click);
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.BackColor = System.Drawing.Color.Transparent;
            this.passwordLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.passwordLabel.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passwordLabel.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.passwordLabel.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.passwordLabel.Location = new System.Drawing.Point(196, 180);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(59, 12);
            this.passwordLabel.TabIndex = 10;
            this.passwordLabel.Text = "Password";
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.BackColor = System.Drawing.Color.Transparent;
            this.usernameLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.usernameLabel.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernameLabel.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.usernameLabel.Location = new System.Drawing.Point(196, 121);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(62, 12);
            this.usernameLabel.TabIndex = 9;
            this.usernameLabel.Text = "Username";
            // 
            // infoLabel
            // 
            this.infoLabel.AutoSize = true;
            this.infoLabel.BackColor = System.Drawing.Color.Transparent;
            this.infoLabel.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.infoLabel.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.infoLabel.Location = new System.Drawing.Point(241, 256);
            this.infoLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(189, 12);
            this.infoLabel.TabIndex = 8;
            this.infoLabel.Text = "Fill in name en password to log in";
            // 
            // logInButton
            // 
            this.logInButton.BackColor = System.Drawing.Color.Transparent;
            this.logInButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("logInButton.BackgroundImage")));
            this.logInButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.logInButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.logInButton.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logInButton.ForeColor = System.Drawing.Color.White;
            this.logInButton.Location = new System.Drawing.Point(189, 365);
            this.logInButton.Margin = new System.Windows.Forms.Padding(2);
            this.logInButton.Name = "logInButton";
            this.logInButton.Size = new System.Drawing.Size(279, 58);
            this.logInButton.TabIndex = 4;
            this.logInButton.Text = "Login";
            this.logInButton.UseVisualStyleBackColor = false;
            this.logInButton.Click += new System.EventHandler(this.logInButton_Click_1);
            // 
            // passBox
            // 
            this.passBox.BackColor = System.Drawing.SystemColors.Window;
            this.passBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.passBox.Location = new System.Drawing.Point(198, 204);
            this.passBox.Margin = new System.Windows.Forms.Padding(2);
            this.passBox.Name = "passBox";
            this.passBox.PasswordChar = '*';
            this.passBox.Size = new System.Drawing.Size(270, 20);
            this.passBox.TabIndex = 6;
            // 
            // accNameBox
            // 
            this.accNameBox.BackColor = System.Drawing.SystemColors.Window;
            this.accNameBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.accNameBox.Location = new System.Drawing.Point(198, 146);
            this.accNameBox.Margin = new System.Windows.Forms.Padding(2);
            this.accNameBox.Name = "accNameBox";
            this.accNameBox.Size = new System.Drawing.Size(270, 20);
            this.accNameBox.TabIndex = 5;
            // 
            // clientPanel
            // 
            this.clientPanel.BackColor = System.Drawing.Color.Transparent;
            this.clientPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("clientPanel.BackgroundImage")));
            this.clientPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.clientPanel.Controls.Add(this.button6);
            this.clientPanel.Controls.Add(this.label4);
            this.clientPanel.Controls.Add(this.label3);
            this.clientPanel.Controls.Add(this.comboBox3);
            this.clientPanel.Controls.Add(this.button3);
            this.clientPanel.Controls.Add(this.chart1);
            this.clientPanel.Controls.Add(this.secondsv);
            this.clientPanel.Controls.Add(this.minutesv);
            this.clientPanel.Controls.Add(this.energyv);
            this.clientPanel.Controls.Add(this.reqpowerv);
            this.clientPanel.Controls.Add(this.distancev);
            this.clientPanel.Controls.Add(this.kmhv);
            this.clientPanel.Controls.Add(this.rpmv);
            this.clientPanel.Controls.Add(this.pulsev);
            this.clientPanel.Controls.Add(this.actpowerv);
            this.clientPanel.Controls.Add(this.actPower);
            this.clientPanel.Controls.Add(this.seconds);
            this.clientPanel.Controls.Add(this.minutes);
            this.clientPanel.Controls.Add(this.energy);
            this.clientPanel.Controls.Add(this.reqPower);
            this.clientPanel.Controls.Add(this.distance);
            this.clientPanel.Controls.Add(this.kmh);
            this.clientPanel.Controls.Add(this.rpm);
            this.clientPanel.Controls.Add(this.pulse);
            this.clientPanel.Location = new System.Drawing.Point(11, 11);
            this.clientPanel.Margin = new System.Windows.Forms.Padding(2);
            this.clientPanel.Name = "clientPanel";
            this.clientPanel.Size = new System.Drawing.Size(1091, 532);
            this.clientPanel.TabIndex = 3;
            this.clientPanel.Visible = false;
            // 
            // button6
            // 
            this.button6.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button6.BackgroundImage")));
            this.button6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.ForeColor = System.Drawing.Color.White;
            this.button6.Location = new System.Drawing.Point(411, 350);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(105, 33);
            this.button6.TabIndex = 62;
            this.button6.Text = "Historical data";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(477, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 15);
            this.label4.TabIndex = 55;
            this.label4.Text = "Fietsgegevens";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(154, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(147, 15);
            this.label3.TabIndex = 54;
            this.label3.Text = "Grafiek fietsgegevens";
            // 
            // comboBox3
            // 
            this.comboBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(194)))), ((int)(((byte)(194)))));
            this.comboBox3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox3.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox3.ForeColor = System.Drawing.Color.DimGray;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(413, 389);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(231, 23);
            this.comboBox3.TabIndex = 50;
            // 
            // button3
            // 
            this.button3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button3.BackgroundImage")));
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(522, 350);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(105, 33);
            this.button3.TabIndex = 51;
            this.button3.Text = "refresh";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.Color.Transparent;
            this.chart1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.chart1.BackImageAlignment = System.Windows.Forms.DataVisualization.Charting.ChartImageAlignmentStyle.Center;
            this.chart1.BorderlineColor = System.Drawing.Color.Black;
            chartArea2.AxisX.Maximum = 30D;
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            this.chart1.Location = new System.Drawing.Point(34, 85);
            this.chart1.Name = "chart1";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.IsVisibleInLegend = false;
            series2.Name = "Series1";
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(357, 360);
            this.chart1.TabIndex = 48;
            this.chart1.Text = "chart1";
            // 
            // secondsv
            // 
            this.secondsv.AutoSize = true;
            this.secondsv.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.secondsv.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.secondsv.Location = new System.Drawing.Point(529, 281);
            this.secondsv.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.secondsv.Name = "secondsv";
            this.secondsv.Size = new System.Drawing.Size(111, 22);
            this.secondsv.TabIndex = 43;
            this.secondsv.Text = "no data yet";
            // 
            // minutesv
            // 
            this.minutesv.AutoSize = true;
            this.minutesv.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minutesv.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.minutesv.Location = new System.Drawing.Point(529, 257);
            this.minutesv.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.minutesv.Name = "minutesv";
            this.minutesv.Size = new System.Drawing.Size(111, 22);
            this.minutesv.TabIndex = 42;
            this.minutesv.Text = "no data yet";
            // 
            // energyv
            // 
            this.energyv.AutoSize = true;
            this.energyv.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.energyv.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.energyv.Location = new System.Drawing.Point(529, 229);
            this.energyv.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.energyv.Name = "energyv";
            this.energyv.Size = new System.Drawing.Size(111, 22);
            this.energyv.TabIndex = 41;
            this.energyv.Text = "no data yet";
            // 
            // reqpowerv
            // 
            this.reqpowerv.AutoSize = true;
            this.reqpowerv.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reqpowerv.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.reqpowerv.Location = new System.Drawing.Point(529, 202);
            this.reqpowerv.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.reqpowerv.Name = "reqpowerv";
            this.reqpowerv.Size = new System.Drawing.Size(111, 22);
            this.reqpowerv.TabIndex = 40;
            this.reqpowerv.Text = "no data yet";
            // 
            // distancev
            // 
            this.distancev.AutoSize = true;
            this.distancev.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.distancev.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.distancev.Location = new System.Drawing.Point(529, 178);
            this.distancev.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.distancev.Name = "distancev";
            this.distancev.Size = new System.Drawing.Size(111, 22);
            this.distancev.TabIndex = 39;
            this.distancev.Text = "no data yet";
            // 
            // kmhv
            // 
            this.kmhv.AutoSize = true;
            this.kmhv.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kmhv.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.kmhv.Location = new System.Drawing.Point(529, 154);
            this.kmhv.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.kmhv.Name = "kmhv";
            this.kmhv.Size = new System.Drawing.Size(111, 22);
            this.kmhv.TabIndex = 38;
            this.kmhv.Text = "no data yet";
            // 
            // rpmv
            // 
            this.rpmv.AutoSize = true;
            this.rpmv.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rpmv.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.rpmv.Location = new System.Drawing.Point(529, 128);
            this.rpmv.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.rpmv.Name = "rpmv";
            this.rpmv.Size = new System.Drawing.Size(111, 22);
            this.rpmv.TabIndex = 37;
            this.rpmv.Text = "no data yet";
            // 
            // pulsev
            // 
            this.pulsev.AutoSize = true;
            this.pulsev.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pulsev.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.pulsev.Location = new System.Drawing.Point(529, 100);
            this.pulsev.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.pulsev.Name = "pulsev";
            this.pulsev.Size = new System.Drawing.Size(111, 22);
            this.pulsev.TabIndex = 36;
            this.pulsev.Text = "no data yet";
            // 
            // actpowerv
            // 
            this.actpowerv.AutoSize = true;
            this.actpowerv.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.actpowerv.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.actpowerv.Location = new System.Drawing.Point(529, 305);
            this.actpowerv.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.actpowerv.Name = "actpowerv";
            this.actpowerv.Size = new System.Drawing.Size(111, 22);
            this.actpowerv.TabIndex = 35;
            this.actpowerv.Text = "no data yet";
            // 
            // actPower
            // 
            this.actPower.AutoSize = true;
            this.actPower.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.actPower.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.actPower.Location = new System.Drawing.Point(428, 305);
            this.actPower.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.actPower.Name = "actPower";
            this.actPower.Size = new System.Drawing.Size(100, 22);
            this.actPower.TabIndex = 34;
            this.actPower.Text = "ActPower";
            // 
            // seconds
            // 
            this.seconds.AutoSize = true;
            this.seconds.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.seconds.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.seconds.Location = new System.Drawing.Point(428, 281);
            this.seconds.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.seconds.Name = "seconds";
            this.seconds.Size = new System.Drawing.Size(89, 22);
            this.seconds.TabIndex = 33;
            this.seconds.Text = "Seconds";
            // 
            // minutes
            // 
            this.minutes.AutoSize = true;
            this.minutes.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minutes.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.minutes.Location = new System.Drawing.Point(428, 257);
            this.minutes.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.minutes.Name = "minutes";
            this.minutes.Size = new System.Drawing.Size(81, 22);
            this.minutes.TabIndex = 32;
            this.minutes.Text = "Minutes";
            // 
            // energy
            // 
            this.energy.AutoSize = true;
            this.energy.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.energy.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.energy.Location = new System.Drawing.Point(428, 229);
            this.energy.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.energy.Name = "energy";
            this.energy.Size = new System.Drawing.Size(75, 22);
            this.energy.TabIndex = 31;
            this.energy.Text = "Energy";
            // 
            // reqPower
            // 
            this.reqPower.AutoSize = true;
            this.reqPower.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reqPower.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.reqPower.Location = new System.Drawing.Point(428, 202);
            this.reqPower.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.reqPower.Name = "reqPower";
            this.reqPower.Size = new System.Drawing.Size(105, 22);
            this.reqPower.TabIndex = 30;
            this.reqPower.Text = "ReqPower";
            // 
            // distance
            // 
            this.distance.AutoSize = true;
            this.distance.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.distance.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.distance.Location = new System.Drawing.Point(428, 178);
            this.distance.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.distance.Name = "distance";
            this.distance.Size = new System.Drawing.Size(90, 22);
            this.distance.TabIndex = 29;
            this.distance.Text = "Distance";
            // 
            // kmh
            // 
            this.kmh.AutoSize = true;
            this.kmh.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kmh.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.kmh.Location = new System.Drawing.Point(428, 154);
            this.kmh.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.kmh.Name = "kmh";
            this.kmh.Size = new System.Drawing.Size(49, 22);
            this.kmh.TabIndex = 28;
            this.kmh.Text = "kmh";
            // 
            // rpm
            // 
            this.rpm.AutoSize = true;
            this.rpm.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rpm.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.rpm.Location = new System.Drawing.Point(428, 128);
            this.rpm.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.rpm.Name = "rpm";
            this.rpm.Size = new System.Drawing.Size(53, 22);
            this.rpm.TabIndex = 27;
            this.rpm.Text = "Rpm";
            // 
            // pulse
            // 
            this.pulse.AutoSize = true;
            this.pulse.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pulse.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.pulse.Location = new System.Drawing.Point(428, 100);
            this.pulse.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.pulse.Name = "pulse";
            this.pulse.Size = new System.Drawing.Size(60, 22);
            this.pulse.TabIndex = 23;
            this.pulse.Text = "Pulse";
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // clientGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(76)))), ((int)(((byte)(61)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(688, 537);
            this.Controls.Add(this.clientPanel);
            this.Controls.Add(this.loginPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "clientGui";
            this.Text = "Gui";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Gui_FormClosing);
            this.Load += new System.EventHandler(this.Gui_Load);
            this.loginPanel.ResumeLayout(false);
            this.loginPanel.PerformLayout();
            this.comPanel.ResumeLayout(false);
            this.comPanel.PerformLayout();
            this.clientPanel.ResumeLayout(false);
            this.clientPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel loginPanel;
        private System.Windows.Forms.TextBox passBox;
        private System.Windows.Forms.TextBox accNameBox;
        private System.Windows.Forms.Button logInButton;
        private System.Windows.Forms.Label infoLabel;
        private System.Windows.Forms.Panel comPanel;
        private System.Windows.Forms.Label comLabel;
        private System.Windows.Forms.TextBox comTextBox;
        private System.Windows.Forms.Button comOKButton;
        private System.Windows.Forms.Panel clientPanel;
        private System.Windows.Forms.Label secondsv;
        private System.Windows.Forms.Label minutesv;
        private System.Windows.Forms.Label energyv;
        private System.Windows.Forms.Label reqpowerv;
        private System.Windows.Forms.Label distancev;
        private System.Windows.Forms.Label kmhv;
        private System.Windows.Forms.Label rpmv;
        private System.Windows.Forms.Label pulsev;
        private System.Windows.Forms.Label actpowerv;
        private System.Windows.Forms.Label actPower;
        private System.Windows.Forms.Label seconds;
        private System.Windows.Forms.Label minutes;
        private System.Windows.Forms.Label energy;
        private System.Windows.Forms.Label reqPower;
        private System.Windows.Forms.Label distance;
        private System.Windows.Forms.Label kmh;
        private System.Windows.Forms.Label rpm;
        private System.Windows.Forms.Label pulse;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.Button button6;
    }
}