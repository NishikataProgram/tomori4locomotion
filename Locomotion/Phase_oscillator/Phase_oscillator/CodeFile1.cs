using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data;



namespace Phase_oscillator
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new POform());
        }
    }
}






/*
class CLASS:Form
{


    
    private Ball ball;

    public static void Main()
    {
        Application.Run(new CLASS());
    }


    public CLASS()
    {       
        this.Text = "PHASE_OSCILLATOR";
        this.ClientSize = new Size(500, 200);

        ball = new Ball();
        Point point= new Point(0, 0);
        Color color = Color.Blue;
        ball.Point = point;
        ball.Color = color;

        Timer tm = new Timer();
        //tm.Interval = 10000;
        tm.Start();

        this.Paint += new PaintEventHandler(graphic);
        tm.Tick += new EventHandler(tm_Tick);
    }

    public static double calculate(double omega,double sigma,double normal_force,double phase)
    {
        double diff_phase;
        return diff_phase = omega + sigma * normal_force * Math.Cos(phase);
    }

    public static double Normal_force_func(double phase)
    {

        double force=0;

        if (0 <= phase && phase < 180)
        {
            force = 0;
        }
        else if (180 <= phase && phase <= 360)
        {
            force = -Math.Sin(phase);
        }
        return force;
    }
    
    public static double TIME_func()
    {
        double TIME ;
        double time ;
        DateTime dt_pre = DateTime.Now;
        double pre_time = (dt_pre.Minute * 60) + (dt_pre.Second * 1) + (dt_pre.Millisecond * 0.001);

        DateTime dt = DateTime.Now;
        time = (dt.Minute * 60) + (dt.Second * 1) + (dt.Millisecond * 0.001);
        TIME=time - pre_time;
        return TIME;

    }

    public void graphic(object sender,PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        Point point = ball.Point;
        Color color = ball.Color;
        SolidBrush br = new SolidBrush(color);
        int x = point.X;
        int y = point.Y;
        g.FillEllipse(br, x, y, 10, 10);
    }
    
    public void tm_Tick(object sender,EventArgs e)
    {
        double diff_phase;
        double phase;//[rad]
        double omega = 0.04;//[rad/s]
        double sigma = 0.0052;//[rad/(s*N)]
        double x;
        double y;
        double TIME;
        double normal_force;//[N]


        for (phase = 0; phase < 361; phase++)
        {

            normal_force = Normal_force_func(phase);

            Point point = ball.Point;

            diff_phase = calculate(omega, sigma, normal_force, phase);
            TIME = TIME_func();

            Task.Delay(100);
            x = Math.Cos(diff_phase * TIME) * 10;
            y = Math.Sin(diff_phase * TIME) * 10;
                
            point.X = point.X + (int)(x * 100);
            point.Y = point.Y + (int)(y * 100);
            Console.Write(x); Console.Write(" "); Console.WriteLine(TIME);
                

            ball.Point = point;
            this.Invalidate();
            
        }

    }
    

}
*/

/*

class Ball
{
    public Color Color;
    public Point Point;
}
*/



