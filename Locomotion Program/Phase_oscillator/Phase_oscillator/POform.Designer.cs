namespace Phase_oscillator
{
    partial class POform
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
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.GetPort = new System.Windows.Forms.Button();
            this.PortNumberBox = new System.Windows.Forms.ComboBox();
            this.Serialport_openandclose_button = new System.Windows.Forms.Button();
            this.PotentiometerL11 = new System.Windows.Forms.Label();
            this.PotentiometerL12 = new System.Windows.Forms.Label();
            this.PotentiometerL21 = new System.Windows.Forms.Label();
            this.PotentiometerL22 = new System.Windows.Forms.Label();
            this.PotentiometerR11 = new System.Windows.Forms.Label();
            this.PotentiometerR12 = new System.Windows.Forms.Label();
            this.PotentiometerR21 = new System.Windows.Forms.Label();
            this.PotentiometerR22 = new System.Windows.Forms.Label();
            this.DebugLabel = new System.Windows.Forms.Label();
            this.POT_ON = new System.Windows.Forms.Button();
            this.Timecounter = new System.Windows.Forms.Label();
            this.MINPressureUpDown = new System.Windows.Forms.NumericUpDown();
            this.MAXPressureUpDown = new System.Windows.Forms.NumericUpDown();
            this.MIN_pressure_input = new System.Windows.Forms.Button();
            this.MAX_pressure_input = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.upload_Button = new System.Windows.Forms.Button();
            this.pattern_flont_comboBox1 = new System.Windows.Forms.ComboBox();
            this.pattern_flont_comboBox2 = new System.Windows.Forms.ComboBox();
            this.DebugLabel2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.L1_Normal_Force_label = new System.Windows.Forms.Label();
            this.L2_Normal_Force_label = new System.Windows.Forms.Label();
            this.R1_Normal_Force_label = new System.Windows.Forms.Label();
            this.R2_Normal_Force_label = new System.Windows.Forms.Label();
            this.Force_sensor_button = new System.Windows.Forms.Button();
            this.START_button = new System.Windows.Forms.Button();
            this.oscillator_timer = new System.Windows.Forms.Timer(this.components);
            this.oscillate_timer_label = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.state_label = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.L1_state = new System.Windows.Forms.Label();
            this.L2_state = new System.Windows.Forms.Label();
            this.R1_state = new System.Windows.Forms.Label();
            this.R2_state = new System.Windows.Forms.Label();
            this.pattern_flont_comboBox3 = new System.Windows.Forms.ComboBox();
            this.pattern_flont_comboBox4 = new System.Windows.Forms.ComboBox();
            this.pattern_back_comboBox1 = new System.Windows.Forms.ComboBox();
            this.pattern_back_comboBox2 = new System.Windows.Forms.ComboBox();
            this.pattern_back_comboBox3 = new System.Windows.Forms.ComboBox();
            this.pattern_back_comboBox4 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.omega_numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.NAV_degrees_label = new System.Windows.Forms.Label();
            this.NAV_to_radians = new System.Windows.Forms.Label();
            this.FB_weight_label = new System.Windows.Forms.Label();
            this.FB_weight_numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.omega_FBweight_upload_button = new System.Windows.Forms.Button();
            this.OMEGA_FBW_label = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.Arduino_TIMER = new System.Windows.Forms.Label();
            this.ATTITUDE_L1_label = new System.Windows.Forms.Label();
            this.ATTITUDE_R1_label = new System.Windows.Forms.Label();
            this.ATTITUDE_L2_label = new System.Windows.Forms.Label();
            this.ATTITUDE_R2_label = new System.Windows.Forms.Label();
            this.phase_chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.MINPressureUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MAXPressureUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.omega_numericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FB_weight_numericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.phase_chart)).BeginInit();
            this.SuspendLayout();
            // 
            // serialPort1
            // 
            this.serialPort1.BaudRate = 19200;
            this.serialPort1.ReadBufferSize = 16384;
            this.serialPort1.WriteBufferSize = 4096;
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serial_Port_DataReceived);
            // 
            // GetPort
            // 
            this.GetPort.Location = new System.Drawing.Point(47, 72);
            this.GetPort.Name = "GetPort";
            this.GetPort.Size = new System.Drawing.Size(120, 23);
            this.GetPort.TabIndex = 0;
            this.GetPort.Text = "シリアルポート取得";
            this.GetPort.UseVisualStyleBackColor = true;
            this.GetPort.Click += new System.EventHandler(this.GetPort_Click);
            // 
            // PortNumberBox
            // 
            this.PortNumberBox.FormattingEnabled = true;
            this.PortNumberBox.Location = new System.Drawing.Point(25, 43);
            this.PortNumberBox.Name = "PortNumberBox";
            this.PortNumberBox.Size = new System.Drawing.Size(121, 20);
            this.PortNumberBox.TabIndex = 1;
            this.PortNumberBox.SelectedIndexChanged += new System.EventHandler(this.PortNumberBox_SelectedIndexChanged);
            // 
            // Serialport_openandclose_button
            // 
            this.Serialport_openandclose_button.Location = new System.Drawing.Point(173, 72);
            this.Serialport_openandclose_button.Name = "Serialport_openandclose_button";
            this.Serialport_openandclose_button.Size = new System.Drawing.Size(84, 23);
            this.Serialport_openandclose_button.TabIndex = 2;
            this.Serialport_openandclose_button.Text = "OPEN";
            this.Serialport_openandclose_button.UseVisualStyleBackColor = true;
            this.Serialport_openandclose_button.Click += new System.EventHandler(this.Serialport_openandclose_button_click);
            // 
            // PotentiometerL11
            // 
            this.PotentiometerL11.AutoSize = true;
            this.PotentiometerL11.Location = new System.Drawing.Point(34, 737);
            this.PotentiometerL11.Name = "PotentiometerL11";
            this.PotentiometerL11.Size = new System.Drawing.Size(23, 12);
            this.PotentiometerL11.TabIndex = 3;
            this.PotentiometerL11.Text = "L11";
            // 
            // PotentiometerL12
            // 
            this.PotentiometerL12.AutoSize = true;
            this.PotentiometerL12.Location = new System.Drawing.Point(34, 756);
            this.PotentiometerL12.Name = "PotentiometerL12";
            this.PotentiometerL12.Size = new System.Drawing.Size(23, 12);
            this.PotentiometerL12.TabIndex = 4;
            this.PotentiometerL12.Text = "L12";
            // 
            // PotentiometerL21
            // 
            this.PotentiometerL21.AutoSize = true;
            this.PotentiometerL21.Location = new System.Drawing.Point(103, 737);
            this.PotentiometerL21.Name = "PotentiometerL21";
            this.PotentiometerL21.Size = new System.Drawing.Size(23, 12);
            this.PotentiometerL21.TabIndex = 5;
            this.PotentiometerL21.Text = "L21";
            // 
            // PotentiometerL22
            // 
            this.PotentiometerL22.AutoSize = true;
            this.PotentiometerL22.Location = new System.Drawing.Point(103, 756);
            this.PotentiometerL22.Name = "PotentiometerL22";
            this.PotentiometerL22.Size = new System.Drawing.Size(23, 12);
            this.PotentiometerL22.TabIndex = 6;
            this.PotentiometerL22.Text = "L22";
            // 
            // PotentiometerR11
            // 
            this.PotentiometerR11.AutoSize = true;
            this.PotentiometerR11.Location = new System.Drawing.Point(179, 737);
            this.PotentiometerR11.Name = "PotentiometerR11";
            this.PotentiometerR11.Size = new System.Drawing.Size(25, 12);
            this.PotentiometerR11.TabIndex = 7;
            this.PotentiometerR11.Text = "R11";
            // 
            // PotentiometerR12
            // 
            this.PotentiometerR12.AutoSize = true;
            this.PotentiometerR12.Location = new System.Drawing.Point(179, 756);
            this.PotentiometerR12.Name = "PotentiometerR12";
            this.PotentiometerR12.Size = new System.Drawing.Size(25, 12);
            this.PotentiometerR12.TabIndex = 8;
            this.PotentiometerR12.Text = "R12";
            // 
            // PotentiometerR21
            // 
            this.PotentiometerR21.AutoSize = true;
            this.PotentiometerR21.Location = new System.Drawing.Point(260, 737);
            this.PotentiometerR21.Name = "PotentiometerR21";
            this.PotentiometerR21.Size = new System.Drawing.Size(25, 12);
            this.PotentiometerR21.TabIndex = 9;
            this.PotentiometerR21.Text = "R21";
            // 
            // PotentiometerR22
            // 
            this.PotentiometerR22.AutoSize = true;
            this.PotentiometerR22.Location = new System.Drawing.Point(260, 756);
            this.PotentiometerR22.Name = "PotentiometerR22";
            this.PotentiometerR22.Size = new System.Drawing.Size(25, 12);
            this.PotentiometerR22.TabIndex = 10;
            this.PotentiometerR22.Text = "R22";
            // 
            // DebugLabel
            // 
            this.DebugLabel.AutoSize = true;
            this.DebugLabel.Location = new System.Drawing.Point(30, 536);
            this.DebugLabel.Name = "DebugLabel";
            this.DebugLabel.Size = new System.Drawing.Size(37, 12);
            this.DebugLabel.TabIndex = 11;
            this.DebugLabel.Text = "Debug";
            // 
            // POT_ON
            // 
            this.POT_ON.Location = new System.Drawing.Point(24, 456);
            this.POT_ON.Name = "POT_ON";
            this.POT_ON.Size = new System.Drawing.Size(75, 23);
            this.POT_ON.TabIndex = 12;
            this.POT_ON.Text = "POT ON";
            this.POT_ON.UseVisualStyleBackColor = true;
            this.POT_ON.Click += new System.EventHandler(this.potentiometer_click);
            // 
            // Timecounter
            // 
            this.Timecounter.Location = new System.Drawing.Point(0, 0);
            this.Timecounter.Name = "Timecounter";
            this.Timecounter.Size = new System.Drawing.Size(100, 23);
            this.Timecounter.TabIndex = 81;
            // 
            // MINPressureUpDown
            // 
            this.MINPressureUpDown.DecimalPlaces = 3;
            this.MINPressureUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            196608});
            this.MINPressureUpDown.Location = new System.Drawing.Point(173, 123);
            this.MINPressureUpDown.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            this.MINPressureUpDown.Name = "MINPressureUpDown";
            this.MINPressureUpDown.Size = new System.Drawing.Size(120, 19);
            this.MINPressureUpDown.TabIndex = 16;
            // 
            // MAXPressureUpDown
            // 
            this.MAXPressureUpDown.DecimalPlaces = 3;
            this.MAXPressureUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            196608});
            this.MAXPressureUpDown.Location = new System.Drawing.Point(24, 123);
            this.MAXPressureUpDown.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            this.MAXPressureUpDown.Name = "MAXPressureUpDown";
            this.MAXPressureUpDown.Size = new System.Drawing.Size(120, 19);
            this.MAXPressureUpDown.TabIndex = 17;
            // 
            // MIN_pressure_input
            // 
            this.MIN_pressure_input.Location = new System.Drawing.Point(218, 148);
            this.MIN_pressure_input.Name = "MIN_pressure_input";
            this.MIN_pressure_input.Size = new System.Drawing.Size(75, 23);
            this.MIN_pressure_input.TabIndex = 18;
            this.MIN_pressure_input.Text = "INPUT";
            this.MIN_pressure_input.UseVisualStyleBackColor = true;
            this.MIN_pressure_input.Click += new System.EventHandler(this.MIN_pressure_input_click);
            // 
            // MAX_pressure_input
            // 
            this.MAX_pressure_input.Location = new System.Drawing.Point(70, 148);
            this.MAX_pressure_input.Name = "MAX_pressure_input";
            this.MAX_pressure_input.Size = new System.Drawing.Size(75, 23);
            this.MAX_pressure_input.TabIndex = 19;
            this.MAX_pressure_input.Text = "INPUT";
            this.MAX_pressure_input.UseVisualStyleBackColor = true;
            this.MAX_pressure_input.Click += new System.EventHandler(this.MAX_pressure_input_click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 12);
            this.label1.TabIndex = 20;
            this.label1.Text = "<最大圧力値[MPa]>";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(150, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 12);
            this.label2.TabIndex = 21;
            this.label2.Text = "<最小圧力値[MPa]>";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 12);
            this.label3.TabIndex = 22;
            this.label3.Text = "<COMポート番号>";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 575);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 12);
            this.label4.TabIndex = 23;
            this.label4.Text = "<角度・脚荷重>";
            // 
            // upload_Button
            // 
            this.upload_Button.Location = new System.Drawing.Point(36, 257);
            this.upload_Button.Name = "upload_Button";
            this.upload_Button.Size = new System.Drawing.Size(75, 23);
            this.upload_Button.TabIndex = 25;
            this.upload_Button.Text = "UPLOAD";
            this.upload_Button.UseVisualStyleBackColor = true;
            this.upload_Button.Click += new System.EventHandler(this.upload_Button_click);
            // 
            // pattern_flont_comboBox1
            // 
            this.pattern_flont_comboBox1.FormattingEnabled = true;
            this.pattern_flont_comboBox1.Items.AddRange(new object[] {
            "No",
            "NN",
            "NF",
            "NB",
            "FN",
            "FF",
            "FB",
            "BN",
            "BF",
            "BB"});
            this.pattern_flont_comboBox1.Location = new System.Drawing.Point(74, 199);
            this.pattern_flont_comboBox1.Name = "pattern_flont_comboBox1";
            this.pattern_flont_comboBox1.Size = new System.Drawing.Size(53, 20);
            this.pattern_flont_comboBox1.TabIndex = 27;
            this.pattern_flont_comboBox1.Text = "No";
            // 
            // pattern_flont_comboBox2
            // 
            this.pattern_flont_comboBox2.FormattingEnabled = true;
            this.pattern_flont_comboBox2.Items.AddRange(new object[] {
            "No",
            "NN",
            "NF",
            "NB",
            "FN",
            "FF",
            "FB",
            "BN",
            "BF",
            "BB"});
            this.pattern_flont_comboBox2.Location = new System.Drawing.Point(133, 199);
            this.pattern_flont_comboBox2.Name = "pattern_flont_comboBox2";
            this.pattern_flont_comboBox2.Size = new System.Drawing.Size(53, 20);
            this.pattern_flont_comboBox2.TabIndex = 31;
            this.pattern_flont_comboBox2.Text = "No";
            // 
            // DebugLabel2
            // 
            this.DebugLabel2.AutoSize = true;
            this.DebugLabel2.Location = new System.Drawing.Point(117, 262);
            this.DebugLabel2.Name = "DebugLabel2";
            this.DebugLabel2.Size = new System.Drawing.Size(106, 12);
            this.DebugLabel2.TabIndex = 35;
            this.DebugLabel2.Text = "UPLOAD PATTERN";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 184);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 12);
            this.label6.TabIndex = 36;
            this.label6.Text = "<脚動作パターン>";
            // 
            // L1_Normal_Force_label
            // 
            this.L1_Normal_Force_label.AutoSize = true;
            this.L1_Normal_Force_label.Location = new System.Drawing.Point(30, 627);
            this.L1_Normal_Force_label.Name = "L1_Normal_Force_label";
            this.L1_Normal_Force_label.Size = new System.Drawing.Size(17, 12);
            this.L1_Normal_Force_label.TabIndex = 38;
            this.L1_Normal_Force_label.Text = "L1";
            // 
            // L2_Normal_Force_label
            // 
            this.L2_Normal_Force_label.AutoSize = true;
            this.L2_Normal_Force_label.Location = new System.Drawing.Point(30, 701);
            this.L2_Normal_Force_label.Name = "L2_Normal_Force_label";
            this.L2_Normal_Force_label.Size = new System.Drawing.Size(17, 12);
            this.L2_Normal_Force_label.TabIndex = 39;
            this.L2_Normal_Force_label.Text = "L2";
            // 
            // R1_Normal_Force_label
            // 
            this.R1_Normal_Force_label.AutoSize = true;
            this.R1_Normal_Force_label.Location = new System.Drawing.Point(166, 627);
            this.R1_Normal_Force_label.Name = "R1_Normal_Force_label";
            this.R1_Normal_Force_label.Size = new System.Drawing.Size(19, 12);
            this.R1_Normal_Force_label.TabIndex = 40;
            this.R1_Normal_Force_label.Text = "R1";
            // 
            // R2_Normal_Force_label
            // 
            this.R2_Normal_Force_label.AutoSize = true;
            this.R2_Normal_Force_label.Location = new System.Drawing.Point(166, 701);
            this.R2_Normal_Force_label.Name = "R2_Normal_Force_label";
            this.R2_Normal_Force_label.Size = new System.Drawing.Size(19, 12);
            this.R2_Normal_Force_label.TabIndex = 41;
            this.R2_Normal_Force_label.Text = "R2";
            // 
            // Force_sensor_button
            // 
            this.Force_sensor_button.Location = new System.Drawing.Point(105, 456);
            this.Force_sensor_button.Name = "Force_sensor_button";
            this.Force_sensor_button.Size = new System.Drawing.Size(104, 23);
            this.Force_sensor_button.TabIndex = 42;
            this.Force_sensor_button.Text = "圧力センサ ON";
            this.Force_sensor_button.UseVisualStyleBackColor = true;
            this.Force_sensor_button.Click += new System.EventHandler(this.Force_sensor_button_click);
            // 
            // START_button
            // 
            this.START_button.Location = new System.Drawing.Point(215, 456);
            this.START_button.Name = "START_button";
            this.START_button.Size = new System.Drawing.Size(70, 23);
            this.START_button.TabIndex = 47;
            this.START_button.Text = "START";
            this.START_button.UseVisualStyleBackColor = true;
            this.START_button.Click += new System.EventHandler(this.start_button_click);
            // 
            // oscillator_timer
            // 
            this.oscillator_timer.Tick += new System.EventHandler(this.oscillator_timer_Tick);
            // 
            // oscillate_timer_label
            // 
            this.oscillate_timer_label.AutoSize = true;
            this.oscillate_timer_label.Location = new System.Drawing.Point(89, 514);
            this.oscillate_timer_label.Name = "oscillate_timer_label";
            this.oscillate_timer_label.Size = new System.Drawing.Size(31, 12);
            this.oscillate_timer_label.TabIndex = 54;
            this.oscillate_timer_label.Text = "TIME";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 432);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 56;
            this.label9.Text = "<試験状態>";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(30, 491);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(31, 12);
            this.label10.TabIndex = 57;
            this.label10.Text = "状態:";
            // 
            // state_label
            // 
            this.state_label.AutoSize = true;
            this.state_label.Location = new System.Drawing.Point(67, 491);
            this.state_label.Name = "state_label";
            this.state_label.Size = new System.Drawing.Size(29, 12);
            this.state_label.TabIndex = 58;
            this.state_label.Text = "停止";
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 801);
            this.splitter1.TabIndex = 59;
            this.splitter1.TabStop = false;
            // 
            // L1_state
            // 
            this.L1_state.AutoSize = true;
            this.L1_state.Location = new System.Drawing.Point(30, 601);
            this.L1_state.Name = "L1_state";
            this.L1_state.Size = new System.Drawing.Size(53, 12);
            this.L1_state.TabIndex = 64;
            this.L1_state.Text = "L1脚状態";
            // 
            // L2_state
            // 
            this.L2_state.AutoSize = true;
            this.L2_state.Location = new System.Drawing.Point(30, 677);
            this.L2_state.Name = "L2_state";
            this.L2_state.Size = new System.Drawing.Size(53, 12);
            this.L2_state.TabIndex = 65;
            this.L2_state.Text = "L2脚状態";
            // 
            // R1_state
            // 
            this.R1_state.AutoSize = true;
            this.R1_state.Location = new System.Drawing.Point(166, 601);
            this.R1_state.Name = "R1_state";
            this.R1_state.Size = new System.Drawing.Size(55, 12);
            this.R1_state.TabIndex = 66;
            this.R1_state.Text = "R1脚状態";
            // 
            // R2_state
            // 
            this.R2_state.AutoSize = true;
            this.R2_state.Location = new System.Drawing.Point(166, 677);
            this.R2_state.Name = "R2_state";
            this.R2_state.Size = new System.Drawing.Size(55, 12);
            this.R2_state.TabIndex = 67;
            this.R2_state.Text = "R2脚状態";
            // 
            // pattern_flont_comboBox3
            // 
            this.pattern_flont_comboBox3.FormattingEnabled = true;
            this.pattern_flont_comboBox3.Items.AddRange(new object[] {
            "No",
            "NN",
            "NF",
            "NB",
            "FN",
            "FF",
            "FB",
            "BN",
            "BF",
            "BB"});
            this.pattern_flont_comboBox3.Location = new System.Drawing.Point(192, 199);
            this.pattern_flont_comboBox3.Name = "pattern_flont_comboBox3";
            this.pattern_flont_comboBox3.Size = new System.Drawing.Size(53, 20);
            this.pattern_flont_comboBox3.TabIndex = 72;
            this.pattern_flont_comboBox3.Text = "No";
            // 
            // pattern_flont_comboBox4
            // 
            this.pattern_flont_comboBox4.FormattingEnabled = true;
            this.pattern_flont_comboBox4.Items.AddRange(new object[] {
            "No",
            "NN",
            "NF",
            "NB",
            "FN",
            "FF",
            "FB",
            "BN",
            "BF",
            "BB"});
            this.pattern_flont_comboBox4.Location = new System.Drawing.Point(251, 199);
            this.pattern_flont_comboBox4.Name = "pattern_flont_comboBox4";
            this.pattern_flont_comboBox4.Size = new System.Drawing.Size(53, 20);
            this.pattern_flont_comboBox4.TabIndex = 73;
            this.pattern_flont_comboBox4.Text = "No";
            // 
            // pattern_back_comboBox1
            // 
            this.pattern_back_comboBox1.FormattingEnabled = true;
            this.pattern_back_comboBox1.Items.AddRange(new object[] {
            "No",
            "NN",
            "NF",
            "NB",
            "FN",
            "FF",
            "FB",
            "BN",
            "BF",
            "BB"});
            this.pattern_back_comboBox1.Location = new System.Drawing.Point(74, 225);
            this.pattern_back_comboBox1.Name = "pattern_back_comboBox1";
            this.pattern_back_comboBox1.Size = new System.Drawing.Size(53, 20);
            this.pattern_back_comboBox1.TabIndex = 82;
            this.pattern_back_comboBox1.Text = "No";
            // 
            // pattern_back_comboBox2
            // 
            this.pattern_back_comboBox2.FormattingEnabled = true;
            this.pattern_back_comboBox2.Items.AddRange(new object[] {
            "No",
            "NN",
            "NF",
            "NB",
            "FN",
            "FF",
            "FB",
            "BN",
            "BF",
            "BB"});
            this.pattern_back_comboBox2.Location = new System.Drawing.Point(133, 225);
            this.pattern_back_comboBox2.Name = "pattern_back_comboBox2";
            this.pattern_back_comboBox2.Size = new System.Drawing.Size(53, 20);
            this.pattern_back_comboBox2.TabIndex = 83;
            this.pattern_back_comboBox2.Text = "No";
            // 
            // pattern_back_comboBox3
            // 
            this.pattern_back_comboBox3.FormattingEnabled = true;
            this.pattern_back_comboBox3.Items.AddRange(new object[] {
            "No",
            "NN",
            "NF",
            "NB",
            "FN",
            "FF",
            "FB",
            "BN",
            "BF",
            "BB"});
            this.pattern_back_comboBox3.Location = new System.Drawing.Point(192, 225);
            this.pattern_back_comboBox3.Name = "pattern_back_comboBox3";
            this.pattern_back_comboBox3.Size = new System.Drawing.Size(53, 20);
            this.pattern_back_comboBox3.TabIndex = 84;
            this.pattern_back_comboBox3.Text = "No";
            // 
            // pattern_back_comboBox4
            // 
            this.pattern_back_comboBox4.FormattingEnabled = true;
            this.pattern_back_comboBox4.Items.AddRange(new object[] {
            "No",
            "NN",
            "NF",
            "NB",
            "FN",
            "FF",
            "FB",
            "BN",
            "BF",
            "BB"});
            this.pattern_back_comboBox4.Location = new System.Drawing.Point(251, 225);
            this.pattern_back_comboBox4.Name = "pattern_back_comboBox4";
            this.pattern_back_comboBox4.Size = new System.Drawing.Size(53, 20);
            this.pattern_back_comboBox4.TabIndex = 85;
            this.pattern_back_comboBox4.Text = "No";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(32, 202);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 86;
            this.label5.Text = "前脚";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(34, 232);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 87;
            this.label7.Text = "後脚";
            // 
            // omega_numericUpDown
            // 
            this.omega_numericUpDown.DecimalPlaces = 2;
            this.omega_numericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.omega_numericUpDown.Location = new System.Drawing.Point(150, 341);
            this.omega_numericUpDown.Name = "omega_numericUpDown";
            this.omega_numericUpDown.Size = new System.Drawing.Size(76, 19);
            this.omega_numericUpDown.TabIndex = 88;
            // 
            // NAV_degrees_label
            // 
            this.NAV_degrees_label.AutoSize = true;
            this.NAV_degrees_label.Location = new System.Drawing.Point(30, 343);
            this.NAV_degrees_label.Name = "NAV_degrees_label";
            this.NAV_degrees_label.Size = new System.Drawing.Size(103, 12);
            this.NAV_degrees_label.TabIndex = 89;
            this.NAV_degrees_label.Text = "固有角速度[deg/s]";
            // 
            // NAV_to_radians
            // 
            this.NAV_to_radians.AutoSize = true;
            this.NAV_to_radians.Location = new System.Drawing.Point(238, 343);
            this.NAV_to_radians.Name = "NAV_to_radians";
            this.NAV_to_radians.Size = new System.Drawing.Size(47, 12);
            this.NAV_to_radians.TabIndex = 90;
            this.NAV_to_radians.Text = "0[rad/s]";
            // 
            // FB_weight_label
            // 
            this.FB_weight_label.AutoSize = true;
            this.FB_weight_label.Location = new System.Drawing.Point(32, 369);
            this.FB_weight_label.Name = "FB_weight_label";
            this.FB_weight_label.Size = new System.Drawing.Size(88, 12);
            this.FB_weight_label.TabIndex = 91;
            this.FB_weight_label.Text = "FB係数[rad/sN]";
            // 
            // FB_weight_numericUpDown
            // 
            this.FB_weight_numericUpDown.DecimalPlaces = 3;
            this.FB_weight_numericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.FB_weight_numericUpDown.Location = new System.Drawing.Point(150, 367);
            this.FB_weight_numericUpDown.Name = "FB_weight_numericUpDown";
            this.FB_weight_numericUpDown.Size = new System.Drawing.Size(76, 19);
            this.FB_weight_numericUpDown.TabIndex = 92;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(16, 321);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 12);
            this.label12.TabIndex = 93;
            this.label12.Text = "<定数選択>";
            // 
            // omega_FBweight_upload_button
            // 
            this.omega_FBweight_upload_button.Location = new System.Drawing.Point(36, 393);
            this.omega_FBweight_upload_button.Name = "omega_FBweight_upload_button";
            this.omega_FBweight_upload_button.Size = new System.Drawing.Size(75, 23);
            this.omega_FBweight_upload_button.TabIndex = 94;
            this.omega_FBweight_upload_button.Text = "UPLOAD";
            this.omega_FBweight_upload_button.UseVisualStyleBackColor = true;
            this.omega_FBweight_upload_button.Click += new System.EventHandler(this.Omega_FBweight_upload_button_Click);
            // 
            // OMEGA_FBW_label
            // 
            this.OMEGA_FBW_label.AutoSize = true;
            this.OMEGA_FBW_label.Location = new System.Drawing.Point(117, 398);
            this.OMEGA_FBW_label.Name = "OMEGA_FBW_label";
            this.OMEGA_FBW_label.Size = new System.Drawing.Size(106, 12);
            this.OMEGA_FBW_label.TabIndex = 95;
            this.OMEGA_FBW_label.Text = "OMEGA_FBWEIGHT";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(30, 514);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(49, 12);
            this.label11.TabIndex = 96;
            this.label11.Text = "VSTimer";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(136, 514);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(73, 12);
            this.label13.TabIndex = 97;
            this.label13.Text = "ArduinoTimer";
            // 
            // Arduino_TIMER
            // 
            this.Arduino_TIMER.AutoSize = true;
            this.Arduino_TIMER.Location = new System.Drawing.Point(215, 514);
            this.Arduino_TIMER.Name = "Arduino_TIMER";
            this.Arduino_TIMER.Size = new System.Drawing.Size(31, 12);
            this.Arduino_TIMER.TabIndex = 98;
            this.Arduino_TIMER.Text = "TIME";
            // 
            // ATTITUDE_L1_label
            // 
            this.ATTITUDE_L1_label.AutoSize = true;
            this.ATTITUDE_L1_label.Location = new System.Drawing.Point(30, 615);
            this.ATTITUDE_L1_label.Name = "ATTITUDE_L1_label";
            this.ATTITUDE_L1_label.Size = new System.Drawing.Size(69, 12);
            this.ATTITUDE_L1_label.TabIndex = 99;
            this.ATTITUDE_L1_label.Text = "ATITUDE L1";
            // 
            // ATTITUDE_R1_label
            // 
            this.ATTITUDE_R1_label.AutoSize = true;
            this.ATTITUDE_R1_label.Location = new System.Drawing.Point(166, 615);
            this.ATTITUDE_R1_label.Name = "ATTITUDE_R1_label";
            this.ATTITUDE_R1_label.Size = new System.Drawing.Size(78, 12);
            this.ATTITUDE_R1_label.TabIndex = 100;
            this.ATTITUDE_R1_label.Text = "ATTITUDE R1";
            // 
            // ATTITUDE_L2_label
            // 
            this.ATTITUDE_L2_label.AutoSize = true;
            this.ATTITUDE_L2_label.Location = new System.Drawing.Point(30, 689);
            this.ATTITUDE_L2_label.Name = "ATTITUDE_L2_label";
            this.ATTITUDE_L2_label.Size = new System.Drawing.Size(76, 12);
            this.ATTITUDE_L2_label.TabIndex = 101;
            this.ATTITUDE_L2_label.Text = "ATTITUDE L2";
            // 
            // ATTITUDE_R2_label
            // 
            this.ATTITUDE_R2_label.AutoSize = true;
            this.ATTITUDE_R2_label.Location = new System.Drawing.Point(166, 689);
            this.ATTITUDE_R2_label.Name = "ATTITUDE_R2_label";
            this.ATTITUDE_R2_label.Size = new System.Drawing.Size(78, 12);
            this.ATTITUDE_R2_label.TabIndex = 102;
            this.ATTITUDE_R2_label.Text = "ATTITUDE R2";
            // 
            // phase_chart
            // 
            this.phase_chart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            chartArea1.Name = "ChartArea1";
            this.phase_chart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.phase_chart.Legends.Add(legend1);
            this.phase_chart.Location = new System.Drawing.Point(356, 184);
            this.phase_chart.Name = "phase_chart";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Polar;
            series1.Legend = "Legend1";
            series1.Name = "phaseL1";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Polar;
            series2.Legend = "Legend1";
            series2.Name = "phaseL2";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Polar;
            series3.Legend = "Legend1";
            series3.Name = "phaseR1";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Polar;
            series4.Legend = "Legend1";
            series4.Name = "phaseR2";
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Polar;
            series5.Legend = "Legend1";
            series5.Name = "order";
            this.phase_chart.Series.Add(series1);
            this.phase_chart.Series.Add(series2);
            this.phase_chart.Series.Add(series3);
            this.phase_chart.Series.Add(series4);
            this.phase_chart.Series.Add(series5);
            this.phase_chart.Size = new System.Drawing.Size(420, 427);
            this.phase_chart.TabIndex = 103;
            this.phase_chart.Text = "chart1";
            // 
            // POform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 801);
            this.Controls.Add(this.phase_chart);
            this.Controls.Add(this.ATTITUDE_R2_label);
            this.Controls.Add(this.ATTITUDE_L2_label);
            this.Controls.Add(this.ATTITUDE_R1_label);
            this.Controls.Add(this.ATTITUDE_L1_label);
            this.Controls.Add(this.Arduino_TIMER);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.OMEGA_FBW_label);
            this.Controls.Add(this.omega_FBweight_upload_button);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.FB_weight_numericUpDown);
            this.Controls.Add(this.FB_weight_label);
            this.Controls.Add(this.NAV_to_radians);
            this.Controls.Add(this.NAV_degrees_label);
            this.Controls.Add(this.omega_numericUpDown);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pattern_back_comboBox4);
            this.Controls.Add(this.pattern_back_comboBox3);
            this.Controls.Add(this.pattern_back_comboBox2);
            this.Controls.Add(this.pattern_back_comboBox1);
            this.Controls.Add(this.pattern_flont_comboBox4);
            this.Controls.Add(this.pattern_flont_comboBox3);
            this.Controls.Add(this.R2_state);
            this.Controls.Add(this.R1_state);
            this.Controls.Add(this.L2_state);
            this.Controls.Add(this.L1_state);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.state_label);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.oscillate_timer_label);
            this.Controls.Add(this.START_button);
            this.Controls.Add(this.Force_sensor_button);
            this.Controls.Add(this.R2_Normal_Force_label);
            this.Controls.Add(this.R1_Normal_Force_label);
            this.Controls.Add(this.L2_Normal_Force_label);
            this.Controls.Add(this.L1_Normal_Force_label);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.DebugLabel2);
            this.Controls.Add(this.pattern_flont_comboBox2);
            this.Controls.Add(this.pattern_flont_comboBox1);
            this.Controls.Add(this.upload_Button);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MAX_pressure_input);
            this.Controls.Add(this.MIN_pressure_input);
            this.Controls.Add(this.MAXPressureUpDown);
            this.Controls.Add(this.MINPressureUpDown);
            this.Controls.Add(this.Timecounter);
            this.Controls.Add(this.POT_ON);
            this.Controls.Add(this.DebugLabel);
            this.Controls.Add(this.PotentiometerR22);
            this.Controls.Add(this.PotentiometerR21);
            this.Controls.Add(this.PotentiometerR12);
            this.Controls.Add(this.PotentiometerR11);
            this.Controls.Add(this.PotentiometerL22);
            this.Controls.Add(this.PotentiometerL21);
            this.Controls.Add(this.PotentiometerL12);
            this.Controls.Add(this.PotentiometerL11);
            this.Controls.Add(this.Serialport_openandclose_button);
            this.Controls.Add(this.PortNumberBox);
            this.Controls.Add(this.GetPort);
            this.Name = "POform";
            this.Text = "Phase_Oscillator";
            ((System.ComponentModel.ISupportInitialize)(this.MINPressureUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MAXPressureUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.omega_numericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FB_weight_numericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.phase_chart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Button GetPort;
        private System.Windows.Forms.ComboBox PortNumberBox;
        private System.Windows.Forms.Button Serialport_openandclose_button;
        private System.Windows.Forms.Label PotentiometerL11;
        private System.Windows.Forms.Label PotentiometerL12;
        private System.Windows.Forms.Label PotentiometerL21;
        private System.Windows.Forms.Label PotentiometerL22;
        private System.Windows.Forms.Label PotentiometerR11;
        private System.Windows.Forms.Label PotentiometerR12;
        private System.Windows.Forms.Label PotentiometerR21;
        private System.Windows.Forms.Label PotentiometerR22;
        private System.Windows.Forms.Label DebugLabel;
        private System.Windows.Forms.Button POT_ON;
        private System.Windows.Forms.Label Timecounter;
        private System.Windows.Forms.NumericUpDown MINPressureUpDown;
        private System.Windows.Forms.NumericUpDown MAXPressureUpDown;
        private System.Windows.Forms.Button MIN_pressure_input;
        private System.Windows.Forms.Button MAX_pressure_input;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button upload_Button;
        private System.Windows.Forms.ComboBox pattern_flont_comboBox1;
        private System.Windows.Forms.ComboBox pattern_flont_comboBox2;
        private System.Windows.Forms.Label DebugLabel2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label L1_Normal_Force_label;
        private System.Windows.Forms.Label L2_Normal_Force_label;
        private System.Windows.Forms.Label R1_Normal_Force_label;
        private System.Windows.Forms.Label R2_Normal_Force_label;
        private System.Windows.Forms.Button Force_sensor_button;
        private System.Windows.Forms.Button START_button;
        private System.Windows.Forms.Timer oscillator_timer;
        private System.Windows.Forms.Label oscillate_timer_label;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label state_label;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Label L1_state;
        private System.Windows.Forms.Label L2_state;
        private System.Windows.Forms.Label R1_state;
        private System.Windows.Forms.Label R2_state;
        private System.Windows.Forms.ComboBox pattern_flont_comboBox3;
        private System.Windows.Forms.ComboBox pattern_flont_comboBox4;
        private System.Windows.Forms.ComboBox pattern_back_comboBox1;
        private System.Windows.Forms.ComboBox pattern_back_comboBox2;
        private System.Windows.Forms.ComboBox pattern_back_comboBox3;
        private System.Windows.Forms.ComboBox pattern_back_comboBox4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown omega_numericUpDown;
        private System.Windows.Forms.Label NAV_degrees_label;
        private System.Windows.Forms.Label NAV_to_radians;
        private System.Windows.Forms.Label FB_weight_label;
        private System.Windows.Forms.NumericUpDown FB_weight_numericUpDown;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button omega_FBweight_upload_button;
        private System.Windows.Forms.Label OMEGA_FBW_label;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label Arduino_TIMER;
        private System.Windows.Forms.Label ATTITUDE_L1_label;
        private System.Windows.Forms.Label ATTITUDE_R1_label;
        private System.Windows.Forms.Label ATTITUDE_L2_label;
        private System.Windows.Forms.Label ATTITUDE_R2_label;
        private System.Windows.Forms.DataVisualization.Charting.Chart phase_chart;
    }
}