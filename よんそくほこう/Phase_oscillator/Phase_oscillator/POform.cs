using System;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Windows.Forms.DataVisualization.Charting;


namespace Phase_oscillator
{
    public partial class POform : Form
    {
        StreamWriter sw;
        Double[] Normal_Force_Double = new Double[4];
        double[] diff_phase = new double[4];
        int oscillator_flag = 0;
        static double oscillator_time=0.0;
        double[] diff_phase_old = new double[4];





        public POform()
        {
            InitializeComponent();
            PortNumberBox.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        static private double ToRadians(double degrees)
        {
            double radiasn;
            return radiasn = (Math.PI / 180) * degrees;
        }

        static private double ToDegrees(double radians)
        {
            double degrees;
            return degrees = (180 / Math.PI) * radians;
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
                SerialPortCLOSE();
                Serialport_openandclose_button.Text = "OPEN";            
                oscillator_timer.Enabled = false;
                state_label.Text = "シリアルポート切断";

            }
        }

        private void SerialPortOPEN()
        {
            string portname = PortNumberBox.SelectedItem.ToString(); //ポート名をポートボックスから選択し，代入
            serialPort1.BaudRate = 9600;
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
                    PotentiometerL11.Text = Potentiometer_L11_Value_Double.ToString() + "[deg]";
                }
            }
            else if (PostOffice == "Potentiometer_L12")
            {
                if (Double.TryParse(Mail, out Potentiometer_L12_Value_Double))
                {
                    PotentiometerL12.Text = Potentiometer_L12_Value_Double.ToString() + "[deg]";
                }
            }
            else if (PostOffice == "Potentiometer_L21")
            {
                if (Double.TryParse(Mail, out Potentiometer_L21_Value_Double))
                {
                    PotentiometerL21.Text = Potentiometer_L21_Value_Double.ToString() + "[deg]";
                }
            }
            else if (PostOffice == "Potentiometer_L22")
            {
                if (Double.TryParse(Mail, out Potentiometer_L22_Value_Double))
                {
                    PotentiometerL22.Text = Potentiometer_L22_Value_Double.ToString() + "[deg]";
                }
            }
            else if (PostOffice == "Potentiometer_R11")
            {
                if (Double.TryParse(Mail, out Potentiometer_R11_Value_Double))
                {
                    PotentiometerR11.Text = Potentiometer_R11_Value_Double.ToString() + "[deg]";
                }
            }
            else if (PostOffice == "Potentiometer_R12")
            {
                if (Double.TryParse(Mail, out Potentiometer_R12_Value_Double))
                {
                    PotentiometerR12.Text = Potentiometer_R12_Value_Double.ToString() + "[deg]";
                }
            }
            else if (PostOffice == "Potentiometer_R21")
            {
                if (Double.TryParse(Mail, out Potentiometer_R21_Value_Double))
                {
                    PotentiometerR21.Text = Potentiometer_R21_Value_Double.ToString() + "[deg]";
                }
            }
            else if (PostOffice == "Potentiometer_R22")
            {
                if (Double.TryParse(Mail, out Potentiometer_R22_Value_Double))
                {
                    PotentiometerR22.Text = Potentiometer_R22_Value_Double.ToString() + "[deg]";
                }
            }
            else if (PostOffice == "DebugPrint")
            {
                DebugLabel.Text = Mail;
            }
            if (PostOffice == "L1_Normal_Force")
            {
                if (Double.TryParse(Mail, out Normal_Force_Double[0]))
                {
                    if (Normal_Force_Double[0] < 1.0) Normal_Force_Double[0] = 0;
                    L1_Normal_Force_label.Text = Normal_Force_Double[0].ToString() + "[N]";
                    if (Normal_Force_Double[0] > 0) L1_state.Text = "STANCE";
                    else if (Normal_Force_Double[0] <= 0) L1_state.Text = "SWING";
                }
            }
            else if (PostOffice == "L2_Normal_Force")
            {
                if (Double.TryParse(Mail, out Normal_Force_Double[1]))
                {
                    if (Normal_Force_Double[1] < 1.0) Normal_Force_Double[1] = 0;
                    L2_Normal_Force_label.Text = Normal_Force_Double[1].ToString() + "[N]";
                    if (Normal_Force_Double[1] > 0) L2_state.Text = "STANCE";
                    else if (Normal_Force_Double[1] <= 0) L2_state.Text = "SWING";
                }
            }
            else if (PostOffice == "R1_Normal_Force")
            {
                if (Double.TryParse(Mail, out Normal_Force_Double[2]))
                {
                    if (Normal_Force_Double[2] < 1.0) Normal_Force_Double[2] = 0;
                    R1_Normal_Force_label.Text = Normal_Force_Double[2].ToString() + "[N]";
                    if (Normal_Force_Double[2] > 0) R1_state.Text = "STANCE";
                    else if (Normal_Force_Double[2] <= 0) R1_state.Text = "SWING";
                }
            }
            else if (PostOffice == "R2_Normal_Force")
            {
                if (Double.TryParse(Mail, out Normal_Force_Double[3]))
                {
                    if (Normal_Force_Double[3] < 1.0) Normal_Force_Double[3] = 0;
                    R2_Normal_Force_label.Text = Normal_Force_Double[3].ToString() + "[N]";
                    if (Normal_Force_Double[3] > 0) R2_state.Text = "STANCE";
                    else if (Normal_Force_Double[3] <= 0) R2_state.Text = "SWING";
                }
            }

        }



        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        /////////////最高圧力を入力するための関数/////////////
        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        private void MAX_pressure_input_click(object sender, EventArgs e)
        {
            string DataSender_Max_pressure = MAXPressureUpDown.Value.ToString();
            serialPort1.Write("ITV:MAXP:"+DataSender_Max_pressure+":");
        }


        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        /////////////最低圧力を入力するための関数/////////////
        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        private void MIN_pressure_input_click(object sender, EventArgs e)
        {
            string DataSender_Min_pressure = MAXPressureUpDown.Value.ToString();
            serialPort1.Write("ITV:minP:" + DataSender_Min_pressure + ":");
        }


        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        ////////////////脚の動きを指定する関数////////////////
        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        private void PointPatternButton_click(object sender, EventArgs e)
        {
            //String DebugString;
            //String SendDataString;

            //SendDataString = "GFGA:PointDrive:" + PointPatternDelay.Value.ToString() + ":LegL1:";
            //SendDataString = SendDataString + pattern_comoBox1.SelectedItem.ToString() + ":";
            //SendDataString = SendDataString + pattern_comboBox2.SelectedItem.ToString() + ":";
            //serialPort1.Write(SendDataString);
            //DebugString = SendDataString + "\n";
            //DebugLabel2.Text = DebugString;
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
        ///////////////位相の角速度を求める処理///////////////
        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        public static double calculate(double omega, double sigma, double normal_force, double phase)
        {
            double diff_phase;
            return diff_phase = omega - sigma * normal_force * Math.Cos(ToRadians(phase));//[rad]
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
                sw.WriteLine("TIME,L11,L12,L21,L22,R11,R12,R21,R22,NF_L1,NF_L2,NF_R1,NF_R2,diff_L1,diff_L2,diff_R1,diff_R2,phase_L1,phase_L2,phase_R1,phase_R2");

            }
            else if (START_button.Text == "STOP")
            {
                START_button.Text = "START";
                oscillator_flag = 0;
                state_label.Text = "試験停止";
                oscillator_timer.Enabled = false;
                oscillator_time = 0;
                sw.Close();

            }

        }


        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        /////////////////////////積分/////////////////////////
        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        private static double INTEGRAL(double initial_Integrable_function,double final_Initegrable_function, double initial_time, double final_time,double dt)
        {
            double ans=0;
            for (double count=initial_time; count<final_time;count++)
            {
                ans = ans + ((initial_Integrable_function + final_Initegrable_function) * dt / 2);//台形積分による計算
            }
            return ans;
        }


        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        ///////////スタートボタンを押したときの処理///////////
        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        private void oscillator_timer_Tick(object sender, EventArgs e)
        {
            double omega = 0.4;//[rad/s]
            double sigma = 0.0052;//[rad/(s*N)]
            double oscillate_time = oscillator_time / 10;
            //double phase_pattern_range=0.0;

            
            double[] phase = new double[4];//[deg]
            double[] Initial_Phase = new double[4] { 0, 0, 0, 0 };
            double[] oscillator_phase = new double[4];
            double[] phai = new double[4];
            double[] oscillator_phase_old = new double[4];
            //double[] diff_phase_old = new double[4];


            if (oscillator_flag==1)
            {
                oscillator_time++;
                oscillate_timer_label.Text = oscillate_time.ToString();



                for (int count = 0; count < 4; count++)//アルゴリズム
                {

             
                diff_phase[count] = calculate(omega, sigma, Normal_Force_Double[count]*(1/2.5), oscillator_phase[count]);//[rad/s]
                oscillator_phase[count] = INTEGRAL(diff_phase[count], diff_phase_old[count], 0, oscillator_time, 0.1);//[rad]
                diff_phase_old[count] = diff_phase[count];


                }


                sw.WriteLine(oscillator_time + "," + PotentiometerL11.Text + ","
                           + PotentiometerL12.Text + "," + PotentiometerL21.Text + ","
                           + PotentiometerL22.Text + "," + PotentiometerR11.Text + ","
                           + PotentiometerR12.Text + "," + PotentiometerR21.Text + ","
                           + PotentiometerR22.Text + "," + L1_Normal_Force_label.Text + ","
                           + L2_Normal_Force_label.Text + "," + R1_Normal_Force_label.Text + ","
                           + R2_Normal_Force_label.Text + "," + diff_phase[0] + ","
                           + diff_phase[1] + "," + diff_phase[2] + ","
                           + diff_phase[3] + "," + oscillator_phase[0] + ","
                           + oscillator_phase[1] + "," + oscillator_phase[2] + ","
                           + oscillator_phase[3]);

                //ここからグラフ描画処理------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


                // 1.Seriesの追加
                L1phase_chart.Series.Clear();
                    L1phase_chart.Series.Add("phaseL1");
                    L2phase_chart.Series.Clear();
                    L2phase_chart.Series.Add("phaseL2");
                    R1phase_chart.Series.Clear();
                    R1phase_chart.Series.Add("phaseR1");
                    R2phase_chart.Series.Clear();
                    R2phase_chart.Series.Add("phaseR2");

                    // 2.グラフのタイプの設定
                    L1phase_chart.Series["phaseL1"].ChartType = SeriesChartType.Polar;
                    L2phase_chart.Series["phaseL2"].ChartType = SeriesChartType.Polar;
                    R1phase_chart.Series["phaseR1"].ChartType = SeriesChartType.Polar;
                    R2phase_chart.Series["phaseR2"].ChartType = SeriesChartType.Polar;

                    // 3.座標の入力
                    L1phase_chart.Series["phaseL1"].Points.AddXY(ToDegrees(oscillator_phase[0]), 1);
                    L2phase_chart.Series["phaseL2"].Points.AddXY(ToDegrees(oscillator_phase[1]), 1);
                    R1phase_chart.Series["phaseR1"].Points.AddXY(ToDegrees(oscillator_phase[2]), 1);
                    R2phase_chart.Series["phaseR2"].Points.AddXY(ToDegrees(oscillator_phase[3]), 1);

                    // 4.マーカーの大きさ
                    L1phase_chart.Series["phaseL1"].MarkerSize = 10;
                    L2phase_chart.Series["phaseL2"].MarkerSize = 10;
                    R1phase_chart.Series["phaseR1"].MarkerSize = 10;
                    R2phase_chart.Series["phaseR2"].MarkerSize = 10;

                    // 5.マーカーの形
                    L1phase_chart.Series["phaseL1"].MarkerStyle = MarkerStyle.Circle;
                    L2phase_chart.Series["phaseL2"].MarkerStyle = MarkerStyle.Circle;
                    R1phase_chart.Series["phaseR1"].MarkerStyle = MarkerStyle.Circle;
                    R2phase_chart.Series["phaseR2"].MarkerStyle = MarkerStyle.Circle;



                //ここまでグラフ描画処理------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------



                //コンソール画面への出力------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                /*時間*/
                Console.Write("time: "); Console.Write(oscillate_time); Console.Write("  ");
                /*荷重*/
                Console.Write("n_force: "); Console.Write(Normal_Force_Double[0]); Console.Write("  ");
                /*cos(phai)*/
                Console.Write("cos(phai): "); Console.Write(Math.Cos(oscillator_phase[0])); Console.Write("  ");
                /*角速度(現在)*/
                Console.Write("diff: "); Console.Write(diff_phase[0]); Console.Write("  ");
                /*角速度(過去)*/
                Console.Write("diff: "); Console.Write(diff_phase_old[0]); Console.Write("  ");
                /*位相*/
                Console.Write("phase: "); Console.Write(ToDegrees(oscillator_phase[0])); Console.Write("  ");
                /*終点*/
                Console.WriteLine("");
                //コンソール画面への出力ここまで----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

            }
            else if (oscillator_flag==0)
            {
                
            }
        }

      
    }

}