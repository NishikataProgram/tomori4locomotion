using System;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;

namespace Phase_oscillator
{
    public partial class POform : Form
    {
        StreamWriter sw;
        int SERIAL_BAUDRATE = 19200;
        int oscillator_flag = 0;
        int AT_L1;
        int AT_L2;
        int AT_R1;
        int AT_R2;
        double PHASE_L1;
        double PHASE_L2;
        double PHASE_R1;
        double PHASE_R2;
        double angle_velocity_L1;
        double angle_velocity_L2;
        double angle_velocity_R1;
        double angle_velocity_R2;
        double Arduino_TIME_TO_VS;
        static double oscillator_time = 0.0000;
        Double Arduino_TIME;
        Double AV_L1;
        Double AV_L2;
        Double AV_R1;
        Double AV_R2;
        Double PHASE_L1_Value_Double;
        Double PHASE_L2_Value_Double;
        Double PHASE_R1_Value_Double;
        Double PHASE_R2_Value_Double;
        Double[] Normal_Force_Double = new Double[4];

        public POform()
        {
            InitializeComponent();
            PortNumberBox.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        static private double ToRadians(double degrees)
        {
            return  (Math.PI / 180) * degrees;
        }

        static private double ToDegrees(double radians)
        {
            return  (180 / Math.PI) * radians;
        }


        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        ///シリアルポート取得ボタンをクリックしたときの処理///
        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        private void GetPort_Click(object sender, EventArgs e)
        {
            GetPortList();
        }

        private void GetPortList()
        {
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                PortNumberBox.Items.Add(port);
                Console.WriteLine(port);
                PortNumberBox.SelectedItem = port;
            }
        }


        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        //シリアルポートオープンボタンをクリックした時の処理//　　
        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        private void Serialport_openandclose_button_click(object sender, EventArgs e)
        {
            if (Serialport_openandclose_button.Text == "OPEN")
            {
                SerialPortOPEN();
                Serialport_openandclose_button.Text = "CLOSE";
                oscillator_timer.Enabled = true;
                state_label.Text = "シリアルポート接続";
            }
            else
            {
                oscillator_flag = 0;
                
                SerialPortCLOSE();
                Serialport_openandclose_button.Text = "OPEN";            
                oscillator_timer.Enabled = false;
                state_label.Text = "シリアルポート切断";
            }
        }
       
        private void SerialPortOPEN()
        {
            string portname = PortNumberBox.SelectedItem.ToString(); //ポート名をポートボックスから選択し，代入
            serialPort1.BaudRate = SERIAL_BAUDRATE;
            serialPort1.PortName = portname;
            serialPort1.Open();
        }

        private void SerialPortCLOSE()
        {
            serialPort1.Close();
        }


        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        ////////////////////コンボボックス////////////////////
        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        private void PortNumberBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        /////////POT_ONボタンをクリックした時の処理///////////　　
        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        private void potentiometer_click(object sender, EventArgs e)
        {
            if (POT_ON.Text == "POT ON")
            {
                serialPort1.Write("POT ON");
                POT_ON.Text = "POT OFF";
            }
            else if (POT_ON.Text == "POT OFF")
            {
                serialPort1.Write("POT OFF");
                POT_ON.Text = "POT ON";
            }
        }


        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        //////////////データを受信するための処理//////////////
        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        private void serial_Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Double Potentiometer_L11_Value_Double;
            Double Potentiometer_L12_Value_Double;
            Double Potentiometer_L21_Value_Double;
            Double Potentiometer_L22_Value_Double;
            Double Potentiometer_R11_Value_Double;
            Double Potentiometer_R12_Value_Double;
            Double Potentiometer_R21_Value_Double;
            Double Potentiometer_R22_Value_Double;
            String PostOffice = serialPort1.ReadTo(":");
            String Mail = serialPort1.ReadLine();

            if (PostOffice == "Potentiometer_L11")
            {
                if (Double.TryParse(Mail, out Potentiometer_L11_Value_Double))
                {
                    PotentiometerL11.Text = Potentiometer_L11_Value_Double.ToString() ;
                }
            }
            else if (PostOffice == "Potentiometer_L12")
            {
                if (Double.TryParse(Mail, out Potentiometer_L12_Value_Double))
                {
                    PotentiometerL12.Text = Potentiometer_L12_Value_Double.ToString() ;
                }
            }
            else if (PostOffice == "Potentiometer_L21")
            {
                if (Double.TryParse(Mail, out Potentiometer_L21_Value_Double))
                {
                    PotentiometerL21.Text = Potentiometer_L21_Value_Double.ToString() ;
                }
            }
            else if (PostOffice == "Potentiometer_L22")
            {
                if (Double.TryParse(Mail, out Potentiometer_L22_Value_Double))
                {
                    PotentiometerL22.Text = Potentiometer_L22_Value_Double.ToString() ;
                }
            }
            else if (PostOffice == "Potentiometer_R11")
            {
                if (Double.TryParse(Mail, out Potentiometer_R11_Value_Double))
                {
                    PotentiometerR11.Text = Potentiometer_R11_Value_Double.ToString() ;
                }
            }
            else if (PostOffice == "Potentiometer_R12")
            {
                if (Double.TryParse(Mail, out Potentiometer_R12_Value_Double))
                {
                    PotentiometerR12.Text = Potentiometer_R12_Value_Double.ToString() ;
                }
            }
            else if (PostOffice == "Potentiometer_R21")
            {
                if (Double.TryParse(Mail, out Potentiometer_R21_Value_Double))
                {
                    PotentiometerR21.Text = Potentiometer_R21_Value_Double.ToString() ;
                }
            }
            else if (PostOffice == "Potentiometer_R22")
            {
                if (Double.TryParse(Mail, out Potentiometer_R22_Value_Double))
                {
                    PotentiometerR22.Text = Potentiometer_R22_Value_Double.ToString() ;
                }
            }
            else if (PostOffice == "L1_Normal_Force")
            {
                if (Double.TryParse(Mail, out Normal_Force_Double[0]))
                {

                    if (Normal_Force_Double[0] < 0.2) Normal_Force_Double[0] = 0;
                    L1_Normal_Force_label.Text = Normal_Force_Double[0].ToString() ;
                    if (Normal_Force_Double[0] > 0) L1_state.Text = "STANCE";
                    else if (Normal_Force_Double[0] <= 0) L1_state.Text = "SWING";

                }
            }
            else if (PostOffice == "L2_Normal_Force")
            {
                if (Double.TryParse(Mail, out Normal_Force_Double[1]))
                {

                    if (Normal_Force_Double[1] < 0.2) Normal_Force_Double[1] = 0;
                    L2_Normal_Force_label.Text = Normal_Force_Double[1].ToString() ;
                    if (Normal_Force_Double[1] > 0) L2_state.Text = "STANCE";
                    else if (Normal_Force_Double[1] <= 0) L2_state.Text = "SWING";

                }
            }
            else if (PostOffice == "R1_Normal_Force")
            {
                if (Double.TryParse(Mail, out Normal_Force_Double[2]))
                {
                    if (Normal_Force_Double[2] < 0.2) Normal_Force_Double[2] = 0;
                    R1_Normal_Force_label.Text = Normal_Force_Double[2].ToString() ;
                    if (Normal_Force_Double[2] > 0) R1_state.Text = "STANCE";
                    else if (Normal_Force_Double[2] <= 0) R1_state.Text = "SWING";
                }
            }
            else if (PostOffice == "R2_Normal_Force")
            {
                if (Double.TryParse(Mail, out Normal_Force_Double[3]))
                {
                    if (Normal_Force_Double[3] < 0.2) Normal_Force_Double[3] = 0;
                    R2_Normal_Force_label.Text = Normal_Force_Double[3].ToString() ;
                    if (Normal_Force_Double[3] > 0) R2_state.Text = "STANCE";
                    else if (Normal_Force_Double[3] <= 0) R2_state.Text = "SWING";
                }
            }
            else if (PostOffice == "PHASE_L1")
            {
                if (Double.TryParse(Mail, out PHASE_L1_Value_Double))
                {
                    PHASE_L1 = PHASE_L1_Value_Double;
                }
            }
            else if (PostOffice == "PHASE_L2")
            {
                if (Double.TryParse(Mail, out PHASE_L2_Value_Double))
                {
                    PHASE_L2 = PHASE_L2_Value_Double;
                }
            }
            else if (PostOffice == "PHASE_R1")
            {
                if (Double.TryParse(Mail, out PHASE_R1_Value_Double))
                {
                    PHASE_R1 = PHASE_R1_Value_Double;
                }
            }
            else if (PostOffice == "PHASE_R2")
            {
                if (Double.TryParse(Mail, out PHASE_R2_Value_Double))
                {
                    PHASE_R2 = PHASE_R2_Value_Double;
                }
            }
            else if (PostOffice == "AV_L1")
            {
                if (Double.TryParse(Mail, out AV_L1))
                {
                    angle_velocity_L1 = AV_L1;
                }
            }
            else if (PostOffice == "AV_L2")
            {
                if (Double.TryParse(Mail, out AV_L2))
                {
                    angle_velocity_L2 = AV_L2;
                }
            }
            else if (PostOffice == "AV_R1")
            {
                if (Double.TryParse(Mail, out AV_R1))
                {
                    angle_velocity_R1 = AV_R1;
                }
            }
            else if (PostOffice == "AV_R2")
            {
                if (Double.TryParse(Mail, out AV_R2))
                {
                    angle_velocity_R2 = AV_R2;
                }
            }
            else if (PostOffice == "ATTITUDE_L1")
            {
                ATTITUDE_L1_label.Text = Mail;
            }
            else if (PostOffice == "ATTITUDE_L2")
            {
                ATTITUDE_L2_label.Text = Mail;
            }
            else if (PostOffice == "ATTITUDE_R1")
            {
                ATTITUDE_R1_label.Text = Mail;
            }
            else if (PostOffice == "ATTITUDE_R2")
            {
                ATTITUDE_R2_label.Text = Mail;
            }
            else if (PostOffice == "TIMER")
            {
                if (Double.TryParse(Mail, out Arduino_TIME))
                {
                    Arduino_TIME_TO_VS=Arduino_TIME;
                }
            }
            else if (PostOffice == "DebugPrint")
            {
                DebugLabel.Text = Mail;
            }
        }


        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        /////////////最高圧力を入力するための関数/////////////
        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        private void MAX_pressure_input_click(object sender, EventArgs e)
        {
            String MAXPDebugString;
            String DataSender_Max_pressure;
            DataSender_Max_pressure = MAXPressureUpDown.Value.ToString();
            serialPort1.Write("ITV:MAXP:" + DataSender_Max_pressure + ":");
            MAXPDebugString = DataSender_Max_pressure + "\n";
          
        }


        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        /////////////最低圧力を入力するための関数/////////////
        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        private void MIN_pressure_input_click(object sender, EventArgs e)
        {
            String minPDebugString;
            String DataSender_Min_pressure;
            DataSender_Min_pressure = MINPressureUpDown.Value.ToString();
            serialPort1.Write("ITV:minP:" + DataSender_Min_pressure + ":");
            minPDebugString = DataSender_Min_pressure + "\n";
        }


        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        ////////////////脚の動きを指定する関数////////////////
        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        private void upload_Button_click(object sender, EventArgs e)
        {
            String DebugString;
            String SendDataString;

            SendDataString = "PHASE_DRIVE:L1:" + pattern_flont_comboBox1.SelectedItem.ToString() + ":";
            SendDataString = SendDataString + pattern_flont_comboBox2.SelectedItem.ToString() + ":";
            SendDataString = SendDataString + pattern_flont_comboBox3.SelectedItem.ToString() + ":";
            SendDataString = SendDataString + pattern_flont_comboBox4.SelectedItem.ToString() + ":";
            serialPort1.Write(SendDataString);
            DebugString = SendDataString + "\n";

            SendDataString = "PHASE_DRIVE:L2:" + pattern_back_comboBox1.SelectedItem.ToString() + ":";
            SendDataString = SendDataString + pattern_back_comboBox2.SelectedItem.ToString() + ":";
            SendDataString = SendDataString + pattern_back_comboBox3.SelectedItem.ToString() + ":";
            SendDataString = SendDataString + pattern_back_comboBox4.SelectedItem.ToString() + ":";
            serialPort1.Write(SendDataString);
            DebugString = DebugString + SendDataString + "\n";

            SendDataString = "PHASE_DRIVE:R1:" + pattern_flont_comboBox1.SelectedItem.ToString() + ":";
            SendDataString = SendDataString + pattern_flont_comboBox2.SelectedItem.ToString() + ":";
            SendDataString = SendDataString + pattern_flont_comboBox3.SelectedItem.ToString() + ":";
            SendDataString = SendDataString + pattern_flont_comboBox4.SelectedItem.ToString() + ":";
            serialPort1.Write(SendDataString);
            DebugString = DebugString + SendDataString + "\n";

            SendDataString = "PHASE_DRIVE:R2:" + pattern_back_comboBox1.SelectedItem.ToString() + ":";
            SendDataString = SendDataString + pattern_back_comboBox2.SelectedItem.ToString() + ":";
            SendDataString = SendDataString + pattern_back_comboBox3.SelectedItem.ToString() + ":";
            SendDataString = SendDataString + pattern_back_comboBox4.SelectedItem.ToString() + ":";
            serialPort1.Write(SendDataString);
            DebugString = DebugString + SendDataString + "\n";

            
            DebugLabel2.Text = DebugString;
        }

        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        /////////////FB係数と固有角速度を送る処理/////////////
        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        private void Omega_FBweight_upload_button_Click(object sender, EventArgs e)
        {
            String SendDataString_OMEGA_AND_FBW;
            String DebugString_OMEGA_AND_FBW;
            String DataSender_omega_string = ToRadians((double)omega_numericUpDown.Value).ToString();
            String DataSender_sigma_string = FB_weight_numericUpDown.Value.ToString();

            SendDataString_OMEGA_AND_FBW = "OMEGA_AND_FBW:OMEGA:" + DataSender_omega_string + ":";
            serialPort1.Write(SendDataString_OMEGA_AND_FBW);
            DebugString_OMEGA_AND_FBW = SendDataString_OMEGA_AND_FBW + "\n";

            NAV_to_radians.Text = ToRadians((double)omega_numericUpDown.Value).ToString();

            SendDataString_OMEGA_AND_FBW = "OMEGA_AND_FBW:FB_weight:" + DataSender_sigma_string + ":";
            serialPort1.Write(SendDataString_OMEGA_AND_FBW);
            DebugString_OMEGA_AND_FBW = DebugString_OMEGA_AND_FBW + SendDataString_OMEGA_AND_FBW;

            OMEGA_FBW_label.Text = DebugString_OMEGA_AND_FBW;
        }

        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        ////////////脚荷重情報を取得するための処理////////////
        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        private void Force_sensor_button_click(object sender, EventArgs e)
        {

            if (Force_sensor_button.Text == "圧力センサ ON")
            {
                serialPort1.Write("Normal_force_ON");
                Force_sensor_button.Text = "圧力センサ OFF";

            }
            else if (Force_sensor_button.Text == "圧力センサ OFF")
            {
                serialPort1.Write("Normal_force_OFF");
                Force_sensor_button.Text = "圧力センサ ON";
            }

        }


        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        ///////////スタートボタンを押したときの処理///////////
        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        private void start_button_click(object sender, EventArgs e)
        {
            if (START_button.Text=="START")
            {
                START_button.Text = "STOP";
                oscillator_flag = 1;
                state_label.Text = "試験中";
                oscillator_timer.Enabled = true;
                sw = new StreamWriter(@"C:\Users\tomor\Desktop\修士研究(西方)\Result_Box\TEST.csv");
                sw.WriteLine("TIME,Atime,L11,L12,L21,L22,R11,R12,R21,R22,NF_L1,NF_L2,NF_R1,NF_R2,AV_L1,AV_L2,AV_R1,AV_R2,phase_L1,phase_L2,phase_R1,phase_R2,attitude_L1,attitude_L2,attitude_R1,attitude_R2");
                serialPort1.Write("DRIVE");
                
            }
            else if (START_button.Text == "STOP")
            {
                START_button.Text = "START";
                oscillator_flag = 0;
                state_label.Text = "試験停止";
                oscillator_timer.Enabled = false;
                oscillator_time = 0;
                sw.Close();
                serialPort1.Write("DRIVE_STOP");

            }
        }


        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        ///////////スタートボタンを押したときの処理///////////
        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        private void oscillator_timer_Tick(object sender, EventArgs e)
        {
            double oscillate_time = oscillator_time/10 ;//[s]
            double phasecossum = Math.Cos(PHASE_L1) + Math.Cos(PHASE_L2) + Math.Cos(PHASE_R1) + Math.Cos(PHASE_R2);
            double phasesinsum = Math.Sin(PHASE_L1) + Math.Sin(PHASE_L2) + Math.Sin(PHASE_R1) + Math.Sin(PHASE_R2);
            double avephasecossum = phasecossum / 4;
            double avephasesinsum = phasesinsum / 4;

            if (oscillator_flag==1)
            {
               
                oscillator_time++;
                oscillate_timer_label.Text = oscillate_time.ToString();
                Arduino_TIMER.Text = Arduino_TIME_TO_VS.ToString();
                sw.WriteLine(oscillator_time / 10 + "," + Arduino_TIME_TO_VS + "," + PotentiometerL11.Text + "," + PotentiometerL12.Text + "," + PotentiometerL21.Text + "," + PotentiometerL22.Text + "," + PotentiometerR11.Text + "," + PotentiometerR12.Text + "," + PotentiometerR21.Text + "," + PotentiometerR22.Text + "," + L1_Normal_Force_label.Text + "," + L2_Normal_Force_label.Text + "," + R1_Normal_Force_label.Text + "," + R2_Normal_Force_label.Text + "," + angle_velocity_L1 + "," + angle_velocity_L2 + "," + angle_velocity_R1 + "," + angle_velocity_R2 + "," + PHASE_L1 + "," + PHASE_L2 + "," + PHASE_R1 + "," + PHASE_R2 + "," + ATTITUDE_L1_label.Text.Replace("\r","") + "," + ATTITUDE_L2_label.Text.Replace("\r", "") + "," + ATTITUDE_R1_label.Text.Replace("\r", "") + "," + ATTITUDE_R2_label.Text.Replace("\r", ""));
               



                //ここからグラフ描画処理------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                phase_chart.Series.Clear();
                phase_chart.Series.Add("phaseL1");
                phase_chart.Series.Add("phaseL2");
                phase_chart.Series.Add("phaseR1");
                phase_chart.Series.Add("phaseR2");
                phase_chart.Series.Add("order");
                phase_chart.Series["phaseL1"].ChartType = SeriesChartType.Polar;
                phase_chart.Series["phaseL2"].ChartType = SeriesChartType.Polar;
                phase_chart.Series["phaseR1"].ChartType = SeriesChartType.Polar;
                phase_chart.Series["phaseR2"].ChartType = SeriesChartType.Polar;
                phase_chart.Series["order"].ChartType = SeriesChartType.Polar;
                phase_chart.Series["phaseL1"].Points.AddXY(ToDegrees(PHASE_L1), 1);
                phase_chart.Series["phaseL2"].Points.AddXY(ToDegrees(PHASE_L2), 1);
                phase_chart.Series["phaseR1"].Points.AddXY(ToDegrees(PHASE_R1), 1);
                phase_chart.Series["phaseR2"].Points.AddXY(ToDegrees(PHASE_R2), 1);
                phase_chart.Series["order"].Points.AddXY(ToDegrees(Math.Atan2(avephasesinsum,avephasecossum)),Math.Sqrt(Math.Pow(phasecossum/4,2)+Math.Pow(phasesinsum/4,2)));
                phase_chart.Series["phaseL1"].MarkerSize = 10;
                phase_chart.Series["phaseL2"].MarkerSize = 10;
                phase_chart.Series["phaseR1"].MarkerSize = 10;
                phase_chart.Series["phaseR2"].MarkerSize = 10;
                phase_chart.Series["order"].MarkerSize = 10;
                phase_chart.Series["phaseL1"].BorderColor = Color.FromArgb(0,0,0,0);
                phase_chart.Series["phaseL2"].BorderColor = Color.FromArgb(0, 0, 0, 0);
                phase_chart.Series["phaseR1"].BorderColor = Color.FromArgb(0, 0, 0, 0);
                phase_chart.Series["phaseR2"].BorderColor = Color.FromArgb(0, 0, 0, 0);
                phase_chart.Series["order"].BorderColor = Color.FromArgb(0, 0, 0, 0);
                phase_chart.Series["phaseL1"].MarkerColor = Color.FromArgb(255,0,0);
                phase_chart.Series["phaseL2"].MarkerColor = Color.FromArgb(0,255,0);
                phase_chart.Series["phaseR1"].MarkerColor = Color.FromArgb(0,0,255);
                phase_chart.Series["phaseR2"].MarkerColor = Color.FromArgb(128,128,128);
                phase_chart.Series["order"].MarkerColor = Color.FromArgb(0, 0, 0);
                phase_chart.Series["phaseL1"].MarkerStyle = MarkerStyle.Circle;
                phase_chart.Series["phaseL2"].MarkerStyle = MarkerStyle.Circle;
                phase_chart.Series["phaseR1"].MarkerStyle = MarkerStyle.Circle;
                phase_chart.Series["phaseR2"].MarkerStyle = MarkerStyle.Circle;
                phase_chart.Series["order"].MarkerStyle = MarkerStyle.Circle;
                //ここまでグラフ描画処理------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------



                //コンソール画面への出力------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                /*時間*/
                Console.Write("time: "); Console.Write(oscillate_time); Console.Write("  ");
                /*Arduino時間*/
                Console.Write("Arduino_time: "); Console.Write(Arduino_TIME_TO_VS); Console.Write("  ");
                /*荷重*/
                //Console.Write("n_force: "); Console.Write(Normal_Force_Double[0]); Console.Write("  ");
                /*位相*/
                Console.Write("phase: "); Console.Write(ToDegrees(PHASE_L1)); Console.Write("  ");
                /*姿勢*/
                Console.Write("attitude: "); Console.Write(ATTITUDE_L1_label.Text); Console.Write("  ");
                /*終点*/
                Console.WriteLine("");
                //コンソール画面への出力ここまで----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                
            }
            else if (oscillator_flag==0)
            {
                
            }
        }

        private void POform_FormClosing(object sender, FormClosedEventArgs e)
        {
            if (serialPort1.IsOpen==false)
            {
                oscillator_flag = 0;
                oscillator_time = 0;
                serialPort1.Close();
            }
        }

        

     
    }

}



