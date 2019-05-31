using System;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Windows.Forms.DataVisualization.Charting;
using System.Collections.Generic;

namespace Phase_oscillator
{
    public partial class POform : Form
    {
        StreamWriter sw;
        int setTimer=0;
        static double countTimer = 0.0;
        Double L1_Normal_Force_Double;
        Double L2_Normal_Force_Double;
        Double R1_Normal_Force_Double;
        Double R2_Normal_Force_Double;

        double[] diff_phase = new double[4];

        double diff_phase_L1;
        double diff_phase_L2;
        double diff_phase_R1;
        double diff_phase_R2;

        int oscillator_flag = 0;
        static double oscillater_timer=0.0;

        public POform()
        {
            InitializeComponent();
            PortNumberBox.DropDownStyle = ComboBoxStyle.DropDownList;
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
                timer1.Enabled = true;
                state_label.Text = "シリアルポート接続";
            }
            else
            {
                SerialPortCLOSE();
                Serialport_openandclose_button.Text = "OPEN";            
                timer1.Enabled = false;
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
        ///////////////////////タイマー///////////////////////
        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        private void timer_Tick(object sender, EventArgs e)
        {
            if (setTimer == 0)
            {            
            }
            else if (setTimer == 1)
            {
                countTimer++;
                sw.WriteLine((countTimer / 10) + "," + PotentiometerL11.Text + "," 
                    + PotentiometerL12.Text + "," + PotentiometerL21.Text + "," 
                    + PotentiometerL22.Text + "," + PotentiometerR11.Text + "," 
                    + PotentiometerR12.Text + "," + PotentiometerR21.Text + "," 
                    + PotentiometerR22.Text + "," + L1_Normal_Force_label.Text + "," 
                    + L2_Normal_Force_label.Text + "," + R1_Normal_Force_label.Text + "," 
                    + R2_Normal_Force_label.Text);

                Timecounter.Text=(countTimer/10).ToString();
            }
        }


        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        //////////////データを受信するための関数//////////////
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
                if (Double.TryParse(Mail, out L1_Normal_Force_Double))
                {
                    if (L1_Normal_Force_Double < 1.0) L1_Normal_Force_Double = 0;
                    else if (L1_Normal_Force_Double >= 1.0&&L1_Normal_Force_Double<250) L1_Normal_Force_Double = 12.5;//特
                    else if (L1_Normal_Force_Double >= 250) L1_Normal_Force_Double = 25;//特
                    L1_Normal_Force_label.Text = L1_Normal_Force_Double.ToString() + "[N]";
                    if (L1_Normal_Force_Double > 0) L1_state.Text = "STANCE";
                    else if (L1_Normal_Force_Double <= 0) L1_state.Text = "SWING";
                }
            }
            else if (PostOffice == "L2_Normal_Force")
            {
                if (Double.TryParse(Mail, out L2_Normal_Force_Double))
                {
                    if (L2_Normal_Force_Double < 1.0) L2_Normal_Force_Double = 0;
                    else if (L2_Normal_Force_Double >= 1.0) L2_Normal_Force_Double = 25;
                    L2_Normal_Force_label.Text = L2_Normal_Force_Double.ToString() + "[N]";
                    if (L2_Normal_Force_Double > 0) L2_state.Text = "STANCE";
                    else if (L2_Normal_Force_Double <= 0) L2_state.Text = "SWING";
                }
            }
            else if (PostOffice == "R1_Normal_Force")
            {
                if (Double.TryParse(Mail, out R1_Normal_Force_Double))
                {
                    if (R1_Normal_Force_Double < 1.0) R1_Normal_Force_Double = 0;
                    else if (R1_Normal_Force_Double >= 1.0) R1_Normal_Force_Double = 25;
                    R1_Normal_Force_label.Text = R1_Normal_Force_Double.ToString() + "[N]";
                    if (R1_Normal_Force_Double > 0) R1_state.Text = "STANCE";
                    else if (R1_Normal_Force_Double <= 0) R1_state.Text = "SWING";
                }
            }
            else if (PostOffice == "R2_Normal_Force")
            {
                if (Double.TryParse(Mail, out R2_Normal_Force_Double))
                {
                    if (R2_Normal_Force_Double < 1.0) R2_Normal_Force_Double = 0;
                    else if (R2_Normal_Force_Double >= 1.0) R2_Normal_Force_Double = 25;
                    R2_Normal_Force_label.Text = R2_Normal_Force_Double.ToString() + "[N]";
                    if (R2_Normal_Force_Double > 0) R2_state.Text = "STANCE";
                    else if (R2_Normal_Force_Double <= 0) R2_state.Text = "SWING";
                }
            }

        }


        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        /////タイマーボタンがクリックされた時に起こる処理/////
        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        private void timerbutton_Click(object sender, EventArgs e)
        {
            if(timerbutton.Text=="TIMER ON")
            { 
                setTimer = 1;
                sw = new StreamWriter(@"C:\Users\tomor\Desktop\修士研究(西方)\Result_Box\TEST.csv");
                //ここでデータ保存のファイル名，場所を指定してください．
                sw.WriteLine("TIME,L11,L12,L21,L22,R11,R12,R21,R22,F_L1,F_L2,F_R1,F_R2,L1_P,L2_P,R1_P,R2_P");
                state_label.Text = "試験 記録中";
                timerbutton.Text = "TIMER OFF";
                
            }
            else if(timerbutton.Text=="TIMER OFF")
            {
                setTimer = 0;
                countTimer = 0;
                sw.Close();
                state_label.Text = "試験 記録停止中";
                timerbutton.Text = "TIMER ON";
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
            String DebugString;
            String SendDataString;

            SendDataString = "GFGA:PointDrive:" + PointPatternDelay.Value.ToString() + ":LegL1:";
            SendDataString = SendDataString + pattern_comoBox1.SelectedItem.ToString() + ":";
            SendDataString = SendDataString + pattern_comboBox2.SelectedItem.ToString() + ":";
            serialPort1.Write(SendDataString);
            DebugString = SendDataString + "\n";

  

          

            DebugLabel2.Text = DebugString;
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


        private void phase_chart_button_click(object sender, EventArgs e)
        {

        }

        
        public static double calculate(double omega, double sigma, double normal_force, double phase)
        {
            double diff_phase;
            return diff_phase = omega - sigma * normal_force * Math.Cos(phase * Math.PI / 180);
        }

        private void start_button_click(object sender, EventArgs e)
        {
            if (START_button.Text=="START")
            {
                START_button.Text = "STOP";
                oscillator_flag = 1;
                state_label.Text = "試験中";
                oscillator_timer.Enabled = true;
            }
            else if (START_button.Text == "STOP")
            {
                START_button.Text = "START";
                oscillator_flag = 0;
                state_label.Text = "試験停止";
                oscillator_timer.Enabled = false;
            }
        }

        private void oscillator_timer_Tick(object sender, EventArgs e)
        {
            double phase_L1;//[rad]
            double phase_L2;//[rad]
            double phase_R1;//[rad]
            double phase_R2;//[rad]
            double omega = 0.04;//[rad/s]
            double sigma = 0.0052;//[rad/(s*N)]
            double normal_force_L1 = L1_Normal_Force_Double;//[N]
            double normal_force_L2 = L2_Normal_Force_Double;//[N]
            double normal_force_R1 = R1_Normal_Force_Double;//[N]
            double normal_force_R2 = R2_Normal_Force_Double;//[N]
            double PHAI_L1 = 0.0;
            double PHAI_L2 = 0.0;
            double PHAI_R1 = 0.0;
            double PHAI_R2 = 0.0;
            double L1_X_value;
            double L1_Y_value;
            double L2_X_value;
            double L2_Y_value;
            double R1_X_value;
            double R1_Y_value;
            double R2_X_value;
            double R2_Y_value;
            double oscillator_phase_L1;
            double oscillator_phase_L2;
            double oscillator_phase_R1;
            double oscillator_phase_R2;
            double oscillate_time = oscillater_timer / 10;
            //double phase_pattern_range=0.0;

            /*
            double[] phase = new double[4];
            double[] normal_force = new double[4] { L1_Normal_Force_Double, L2_Normal_Force_Double, R1_Normal_Force_Double, R2_Normal_Force_Double };
            double[] phai = new double[4] { 0, 0, 0, 0 };
            double[] X_value = new double[4];
            double[] Y_value = new double[4];
            double[] oscillator_phase = new double[4];
            */




            if (oscillator_flag==1)
            {
                oscillater_timer++;
                oscillate_timer_label.Text = oscillate_time.ToString();

                /*
                for (int count = 0; count < 5; count++)
                {
                    oscillator_phase[count] = diff_phase[count] * oscillate_time * 1000;
                    phase[count] = (oscillator_phase[count] + phai[count]);
                    diff_phase[count] = calculate(omega, sigma, normal_force[count], phase[count]);

                    X_value[count] = Math.Cos(oscillator_phase[count]);
                    Y_value[count] = Math.Sin(oscillator_phase[count]);
                }
                oscillator_phase_L1 = oscillator_phase[0];
                oscillator_phase_L2 = oscillator_phase[1];
                oscillator_phase_R1 = oscillator_phase[2];
                oscillator_phase_R2 = oscillator_phase[3];
                */

                
                //L1脚パターン
                oscillator_phase_L1 = (diff_phase_L1 * oscillate_time * 1000);//[deg]
                phase_L1 = (oscillator_phase_L1 +PHAI_L1);//phase_L1 = (diff_phase_L1 * oscillate_time * 1000 + PHAI_L1);//[deg]
                diff_phase_L1 = calculate(omega, sigma, normal_force_L1, phase_L1);//[rad/s]

                //L2脚パターン
                oscillator_phase_L2 = (diff_phase_L2 * oscillate_time * 1000);//[deg]
                phase_L2 = (oscillator_phase_L2 + PHAI_L2);//phase_L2 = (diff_phase_L2 * oscillate_time * 1000 + PHAI_L2);//[deg]
                diff_phase_L2 = calculate(omega, sigma, normal_force_L2, phase_L2);//[rad/s]

                //R1脚パターン
                oscillator_phase_R1 = (diff_phase_R1 * oscillate_time * 1000);//[deg]
                phase_R1 = (oscillator_phase_R1 + PHAI_R1);//phase_R1 = (diff_phase_R1 * oscillate_time * 1000 + PHAI_R1);//[deg]
                diff_phase_R1 = calculate(omega, sigma, normal_force_R1, phase_R1);//[rad/s]

                //R2脚パターン
                oscillator_phase_R2 = (diff_phase_R2 * oscillate_time * 1000);//[deg]
                phase_R2 = (oscillator_phase_R2 + PHAI_R2);//phase_R2 = (diff_phase_R2 * oscillate_time * 1000 + PHAI_R2);//[deg]
                diff_phase_R2 = calculate(omega, sigma, normal_force_R2, phase_R2);//[rad/s]
                





                //ここからグラフ描画処理------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


                
                L1_X_value = Math.Cos(oscillator_phase_L1);
                L1_Y_value = Math.Sin(oscillator_phase_L1);
                L2_X_value = Math.Cos(oscillator_phase_L2);
                L2_Y_value = Math.Sin(oscillator_phase_L2);
                R1_X_value = Math.Cos(oscillator_phase_R1);
                R1_Y_value = Math.Sin(oscillator_phase_R1);
                R2_X_value = Math.Cos(oscillator_phase_R2);
                R2_Y_value = Math.Sin(oscillator_phase_R2);
                

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
                L1phase_chart.Series["phaseL1"].Points.AddXY(oscillator_phase_L1, 1);
                L2phase_chart.Series["phaseL2"].Points.AddXY(oscillator_phase_L2, 1);
                R1phase_chart.Series["phaseR1"].Points.AddXY(oscillator_phase_R1, 1);
                R2phase_chart.Series["phaseR2"].Points.AddXY(oscillator_phase_R2, 1);
                //L1phase_chart.Series["phaseL1"].Points.AddXY(oscillator_phase[0], 1);
                //L2phase_chart.Series["phaseL2"].Points.AddXY(oscillator_phase[1], 1);
                //R1phase_chart.Series["phaseR1"].Points.AddXY(oscillator_phase[2], 1);
                //R2phase_chart.Series["phaseR2"].Points.AddXY(oscillator_phase[3], 1);
                //L1phase_chart.Series["phaseL1"].Points.AddXY(oscillator_phase_L1, Math.Sqrt((L1_X_value * L1_X_value) + (L1_Y_value * L1_Y_value))); 
                //L2phase_chart.Series["phaseL2"].Points.AddXY(oscillator_phase_L2, Math.Sqrt((L2_X_value * L2_X_value) + (L2_Y_value * L2_Y_value))); 
                //R1phase_chart.Series["phaseR1"].Points.AddXY(oscillator_phase_R1, Math.Sqrt((R1_X_value * R1_X_value) + (R1_Y_value * R1_Y_value))); 
                //R2phase_chart.Series["phaseR2"].Points.AddXY(oscillator_phase_R2, Math.Sqrt((R2_X_value * R2_X_value) + (R2_Y_value * R2_Y_value))); 

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

                //Console.Write(" time: "); Console.Write(oscillate_time); Console.Write("  ");
                //Console.Write("phase_L1: "); Console.Write(phase_L1); Console.Write("  ");
                //Console.Write("cos(phai*PI/180)_L1: "); Console.Write(Math.Cos(phase_L1*Math.PI/180)); Console.Write("  ");
                //Console.Write("NF_L1: "); Console.Write(normal_force_L1);Console.Write("  ");
                //Console.Write(" L1_diff_phase: "); Console.Write(diff_phase_L1); Console.Write("  ");
                //Console.Write("gp_L1: "); Console.Write(oscillator_phase[0]); Console.WriteLine("  ");
                //Console.Write("gp_L1: "); Console.Write(oscillator_phase_L1); Console.WriteLine("  ");


                //Arduinoに位相を送信
                //serialPort1.Write("PHASE:L1:" + oscillator_phase_L1 + ":" + "L2:" + oscillator_phase_L2 + ":" + "R1" + oscillator_phase_R1 + ":" + "R2" + oscillator_phase_R2 + ":");
                //serialPort1.Write("PHASE:L1:" + oscillator_phase[0] + ":" + "L2:" + oscillator_phase[1] + ":" + "R1" + oscillator_phase[2] + ":" + "R2" + oscillator_phase[3] + ":");

            }
            else if (oscillator_flag==0)
            {
                /**/
            }
        }
    }

}