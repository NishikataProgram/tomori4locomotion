using System;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading;


namespace Phase_oscillator
{
    public partial class POform : Form
    {
        StreamWriter sw;
        int SERIAL_BAUDRATE = 19200;

        Double[] Normal_Force_Double = new Double[4];
        double[] diff_phase = new double[4];
        int oscillator_flag = 0;
        static double oscillator_time=0.0000;
        double[] diff_phase_old = new double[4];
        double PHASE_L1;
        double PHASE_L2;
        double PHASE_R1;
        double PHASE_R2;
        double attitude_L1;
        double attitude_L2;
        double attitude_R1;
        double attitude_R2;

        Double angle_velocity_L1;
        Double angle_velocity_L2;
        Double angle_velocity_R1;
        Double angle_velocity_R2;

        Double AV_L1;
        Double AV_L2;
        Double AV_R1;
        Double AV_R2;
        Double test;

        Double PHASE_L1_Value_Double;
        Double PHASE_L2_Value_Double;
        Double PHASE_R1_Value_Double;
        Double PHASE_R2_Value_Double;

        Double ATTITUDE_L1;
        Double ATTITUDE_L2;
        Double ATTITUDE_R1;
        Double ATTITUDE_R2;


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

            /*
            Double PHASE_L1_Value_Double;
            Double PHASE_L2_Value_Double;
            Double PHASE_R1_Value_Double;
            Double PHASE_R2_Value_Double;
            */

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

                    if (Normal_Force_Double[0] < 0.2) Normal_Force_Double[0] = 0;
                    L1_Normal_Force_label.Text = Normal_Force_Double[0].ToString() + "[N]";
                    if (Normal_Force_Double[0] > 0) L1_state.Text = "STANCE";
                    else if (Normal_Force_Double[0] <= 0) L1_state.Text = "SWING";

                }
            }
            else if (PostOffice == "L2_Normal_Force")
            {
                if (Double.TryParse(Mail, out Normal_Force_Double[1]))
                {

                    if (Normal_Force_Double[1] < 0.2) Normal_Force_Double[1] = 0;
                    L2_Normal_Force_label.Text = Normal_Force_Double[1].ToString() + "[N]";
                    if (Normal_Force_Double[1] > 0) L2_state.Text = "STANCE";
                    else if (Normal_Force_Double[1] <= 0) L2_state.Text = "SWING";

                }
            }
            else if (PostOffice == "R1_Normal_Force")
            {
                if (Double.TryParse(Mail, out Normal_Force_Double[2]))
                {

                    if (Normal_Force_Double[2] < 0.2) Normal_Force_Double[2] = 0;
                    R1_Normal_Force_label.Text = Normal_Force_Double[2].ToString() + "[N]";
                    if (Normal_Force_Double[2] > 0) R1_state.Text = "STANCE";
                    else if (Normal_Force_Double[2] <= 0) R1_state.Text = "SWING";

                }
            }
            else if (PostOffice == "R2_Normal_Force")
            {
                if (Double.TryParse(Mail, out Normal_Force_Double[3]))
                {
                    if (Normal_Force_Double[3] < 0.2) Normal_Force_Double[3] = 0;
                    R2_Normal_Force_label.Text = Normal_Force_Double[3].ToString() + "[N]";
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
            else if (PostOffice == "ATTITUDE_L1")
            {
                if (Double.TryParse(Mail, out ATTITUDE_L1))
                {
                    attitude_L1 = ATTITUDE_L1;
                }
            }
            else if (PostOffice == "ATTITUDE_L2")
            {
                if (Double.TryParse(Mail, out ATTITUDE_L2))
                {
                    attitude_L2 = ATTITUDE_L2;
                }
            }
            else if (PostOffice == "ATTITUDE_R1")
            {
                if (Double.TryParse(Mail, out ATTITUDE_R1))
                {
                    attitude_R1 = ATTITUDE_R1;
                }
            }
            else if (PostOffice == "ATTITUDE_R2")
            {
                if (Double.TryParse(Mail, out ATTITUDE_R2))
                {
                    attitude_R2 = ATTITUDE_R2;
                }
            }




            else if (PostOffice == "test")//通信テストフラグ
            {
                
                if (Double.TryParse(Mail, out test))
                {
                    
                        Console.WriteLine(test);
                    
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
        private void upload_Button_click(object sender, EventArgs e)
        {
            String DebugString;
            String SendDataString;
            //String DataSender_omega_string = ToRadians((double)omega_numericUpDown.Value).ToString();
            //String DataSender_sigma_string = FB_weight_numericUpDown.Value.ToString();

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

            /*
            SendDataString = "PHASE_DRIVE:OMEGA:" + DataSender_omega_string + ":";
            serialPort1.Write(SendDataString);
            DebugString = DebugString + SendDataString + "\n";

            NAV_to_radians.Text = ToRadians((double)omega_numericUpDown.Value).ToString();

            SendDataString = "PHASE_DRIVE:FB_weight:" + DataSender_sigma_string + ":";
            serialPort1.Write(SendDataString);
            DebugString = DebugString + SendDataString;
            */
            


            DebugLabel2.Text = DebugString;
        }


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
                sw.WriteLine("TIME,L11,L12,L21,L22,R11,R12,R21,R22,NF_L1,NF_L2,NF_R1,NF_R2,diff_L1,diff_L2,diff_R1,diff_R2,phase_L1,phase_L2,phase_R1,phase_R2");
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
            double[] oscillator_phase = new double[4];//[rad]
            string[] attitude_string = new string[4];
            
            

            if (oscillator_flag==1)
            {
                
                oscillator_time++;
                oscillate_timer_label.Text = oscillate_time.ToString();
                
                sw.WriteLine(oscillate_time + "," + PotentiometerL11.Text + ","
                + PotentiometerL12.Text + "," + PotentiometerL21.Text + ","
                + PotentiometerL22.Text + "," + PotentiometerR11.Text + ","
                + PotentiometerR12.Text + "," + PotentiometerR21.Text + ","
                + PotentiometerR22.Text + "," + L1_Normal_Force_label.Text + ","
                + L2_Normal_Force_label.Text + "," + R1_Normal_Force_label.Text + ","
                + R2_Normal_Force_label.Text + "," + angle_velocity_L1 + ","
                + angle_velocity_L2 + "," + angle_velocity_R1 + ","
                + angle_velocity_R2 + "," + PHASE_L1 + ","
                + PHASE_L2 + "," + PHASE_R1 + ","
                + PHASE_R2);
                


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
                    L1phase_chart.Series["phaseL1"].Points.AddXY(ToDegrees(PHASE_L1), 1);
                    L2phase_chart.Series["phaseL2"].Points.AddXY(ToDegrees(PHASE_L2), 1);
                    R1phase_chart.Series["phaseR1"].Points.AddXY(ToDegrees(PHASE_R1), 1);
                    R2phase_chart.Series["phaseR2"].Points.AddXY(ToDegrees(PHASE_R2), 1);

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
                Console.Write("angle_velocity_L1: "); Console.Write(angle_velocity_L1); Console.Write("  ");
                /*位相*/
                Console.Write("phase: "); Console.Write(ToDegrees(PHASE_L1)); Console.Write("  ");
                /*attitude*/
                Console.Write("attitude: "); Console.Write(attitude_L1); Console.Write("  ");
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
            if (serialPort1.IsOpen)
            {
                oscillator_flag = 0;
                oscillator_time = 0;
                serialPort1.Close();
            }
        }

     
    }

}

//arduinoへ移行するプログラム

/*
 
   //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        ///////////////位相の角速度を求める処理///////////////
        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        public static double calculate(double omega, double sigma, double normal_force, double phase)
        {
            double diff_phase;
            return diff_phase = omega - sigma * normal_force * Math.Cos(phase);//[rad]
        }



         private double ToNewton(double kg)
    {
        return kg * 9.81;
    }

    private double FB_sigma(double natural_omega, double minimam_omega)
    {
        double sigma;
        double weight = ToNewton(8 / 3);
        double omega_ratio = minimam_omega / natural_omega;
        sigma = (natural_omega / weight) * Math.Sqrt(1 - Math.Pow(omega_ratio, 2));

        return sigma;
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
            ans += ((initial_Integrable_function + final_Initegrable_function) * dt / 2);//台形積分による計算
        }
        return ans;
    }


    private static int flont_feet_attitude(double central_point1,double central_NNpoint,double range,double oscillate_phase)
    {
        if (central_point1 - range < oscillate_phase && oscillate_phase < central_point1 + range)
        {
            return 1;//FF
        }
        else if (central_NNpoint - range < oscillate_phase && oscillate_phase < central_NNpoint + range)
        {
            return 0;//NN
        }
        else
        {
            return 0;//NN
        }
    }
    private static int back_feet_attitude(double central_point1, double central_NNpoint, double range, double oscillate_phase)
    {
        if (central_point1 - range < oscillate_phase && oscillate_phase < central_point1 + range)
        {
            return 1;//RR
        }
        else if (central_NNpoint - range < oscillate_phase && oscillate_phase < central_NNpoint + range)
        {
            return 0;//NN
        }
        else
        {
            return 0;//NN
        }
    }

    private static double saw_function_converter(double radians)
    {
        double phai;
        return phai = radians % (2 * Math.PI);
    }

 */







/*

                //for (int count = 0; count < 4; count++)//アルゴリズム
                //{

                
                    diff_phase[0] = calculate(omega, sigma, Normal_Force_Double[0] * (1 / 2.5), oscillator_phase[0]);//[rad/s]
                    diff_phase[1] = calculate(omega, sigma, Normal_Force_Double[1] * (1 / 2.5), oscillator_phase[1]);//[rad/s]
                    diff_phase[2] = calculate(omega, sigma, Normal_Force_Double[2] * (1 / 2.5), oscillator_phase[2]);//[rad/s]
                    diff_phase[3] = calculate(omega, sigma, Normal_Force_Double[3] * (1 / 2.5), oscillator_phase[3]);//[rad/s]

                    oscillator_phase[0] = saw_function_converter(INTEGRAL(diff_phase[0], diff_phase_old[0], 0, oscillator_time, 0.1));//[rad]
                    oscillator_phase[1] = saw_function_converter(INTEGRAL(diff_phase[1], diff_phase_old[1], 0, oscillator_time, 0.1));//[rad]
                    oscillator_phase[2] = saw_function_converter(INTEGRAL(diff_phase[2], diff_phase_old[2], 0, oscillator_time, 0.1));//[rad]
                    oscillator_phase[3] = saw_function_converter(INTEGRAL(diff_phase[3], diff_phase_old[3], 0, oscillator_time, 0.1));//[rad]

                    diff_phase_old[0] = diff_phase[0];                    
                    diff_phase_old[1] = diff_phase[1];                
                    diff_phase_old[2] = diff_phase[2];
                    diff_phase_old[3] = diff_phase[3];

               
                for(int count=0;count<4;count++){
                diff_phase[count] = omega - sigma * Normal_Force_Double[count] * (1 / 2.5) * Math.Cos(oscillator_phase[count]);
                }
                for(int count=0;count<4;count++){
                oscillator_phase[count] =INTEGRAL(diff_phase[count],diff_phase_old[count],0,oscillator_time,0.1)% (2 * Math.PI);
                }
                for(int count=0;count<4;count++){
                 diff_phase_old[count] = diff_phase[count];
                }
                
                
                

                //}


                attitude[0] = flont_feet_attitude(central_point_FF, central_point_NN, phase_pattern_range, oscillator_phase[0]);
                attitude[1] = back_feet_attitude(central_point_RR, central_point_NN, phase_pattern_range, oscillator_phase[1]);
                attitude[2] = flont_feet_attitude(central_point_FF, central_point_NN, phase_pattern_range, oscillator_phase[2]);
                attitude[3] = back_feet_attitude(central_point_RR, central_point_NN, phase_pattern_range, oscillator_phase[3]);

                for (int subcount=0;subcount<4;subcount++)
                { 
                    attitude_string[subcount] = attitude[subcount].ToString();
                    serialPort1.Write(attitude_string[subcount]);
                    
                }
                */
