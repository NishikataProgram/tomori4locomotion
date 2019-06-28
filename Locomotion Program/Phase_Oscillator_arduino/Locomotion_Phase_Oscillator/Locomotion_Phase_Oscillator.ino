#include <MsTimer2.h>

//ピン番号設定
#define DA_SCK 13 //クロック信号出力ピン
#define DA_SDI 11 //データ信号出力ピン
#define DA_CS  10 //選択動作出力ピン
#define DA_LDAC 9 //ラッチ動作出力ピン

//数値設定
#define PI_VAL 3.1415926535897932384626433832795
#define DEG_TO_RAD 0.017453292519943295769236907684886
#define RAD_TO_DEG 57.295779513082320876798154814105
#define ErrorFloat 1.0E30
#define FREQUENCY_LEG 0.2

int serial_baudrate=19200;

float StringToFloat(String s);
int PotentiometerFlag=0;
int Normal_Force_Flag=0;
String valve_type;
String valve_direction;
String valve_serial;
float valve_pressure = 0.0;
int digit_valve = 0;
int VOUT = 2;
int valve_flag = 0;
String sampleDrive="OFF";
int PinNum=0;
int drive_flag=0;
String POTandFORCE_SWITCH;
String PRESSER_SETTING;
String DRIVE_TYPE;
double omega;
double sigma;
double force[4];
double PHASE_L1;
double PHASE_L2;
double PHASE_R1;
double PHASE_R2;
double angle_velocity_L1;
double angle_velocity_L2;
double angle_velocity_R1;
double angle_velocity_R2;
double FORCE[4];
String SETTING_SWITCH;
String CONST_VALUE_SWITCH;
String CONST_VALUE_OMEGA;
String CONST_VALUE_SIGMA;
String LEG_NUM;
String Pattern[4][4];
int attitude[4][4];
int DRIVE_FLAG=0;


//DACに出力を行う処理の関数
void DACout(int dataPin, int clockPin, int destination, int value){
  int i;
  //コマンドデータの出力
  digitalWrite(dataPin, destination);//出力するピンを選択する
  digitalWrite(clockPin, HIGH);
  digitalWrite(clockPin, LOW);
  digitalWrite(dataPin, LOW);//VREFバッファは使用しない
  digitalWrite(clockPin, HIGH);
  digitalWrite(clockPin, LOW);
  digitalWrite(dataPin, HIGH);//出力ゲインは１倍とする
  digitalWrite(clockPin, HIGH);
  digitalWrite(clockPin, LOW);
  digitalWrite(dataPin, HIGH);//アナログ出力は有効とする
  digitalWrite(clockPin, HIGH);
  digitalWrite(clockPin, LOW);
  //DACデータビット出力
  for (i = 11; i >= 0; i--) {
    if (((value >> i) & 0x1) == 1) digitalWrite(dataPin, HIGH);
    else                    digitalWrite(dataPin, LOW);
    digitalWrite(clockPin, HIGH);
    digitalWrite(clockPin, LOW);
  }
}



void Pin_All_OFF(){
    for(PinNum=22;PinNum<54;PinNum++){
      digitalWrite(PinNum,LOW);
    }
}


  
void setup() {
  Serial.begin(serial_baudrate);

  pinMode(DA_SCK, OUTPUT);
  pinMode(DA_SDI, OUTPUT);
  pinMode(DA_CS, OUTPUT);
  pinMode(DA_LDAC, OUTPUT);
  //出力ピンの初期化
  digitalWrite(DA_SCK, LOW);

  for(PinNum=22;PinNum<54;PinNum++){
   pinMode(PinNum,OUTPUT);
  }

  for(PinNum=22;PinNum<54;PinNum++){
   digitalWrite(PinNum,LOW);
  }
}

          
void loop() {
    if(Serial.available()>0){//PCからの受信
      
        SETTING_SWITCH=Serial.readStringUntil(':');
        if(SETTING_SWITCH=="POT ON")PotentiometerFlag=1;
        if(SETTING_SWITCH=="POT OFF")PotentiometerFlag=0;
        if(SETTING_SWITCH=="Normal_force_ON")Normal_Force_Flag=1;
        if(SETTING_SWITCH=="Normal_force_OFF")Normal_Force_Flag=0;
        if(SETTING_SWITCH=="ITV")ITV_function();         
        if(SETTING_SWITCH=="DRIVE")DRIVE_FLAG=1; 
        if(SETTING_SWITCH=="DRIVE_STOP")DRIVE_FLAG=0;
        if (SETTING_SWITCH == "OMEGA_AND_FBW") {
          CONST_VALUE_SWITCH = Serial.readStringUntil(':');
          if (CONST_VALUE_SWITCH=="OMEGA") {
            CONST_VALUE_OMEGA = Serial.readStringUntil(':');
            omega = (double)StringToFloat(CONST_VALUE_OMEGA);
          }
          else if (CONST_VALUE_SWITCH == "FB_weight") {
            CONST_VALUE_SIGMA = Serial.readStringUntil(':');
            sigma = (double)StringToFloat(CONST_VALUE_SIGMA);
          }
        }
        if(SETTING_SWITCH=="PHASE_DRIVE"){
          
          LEG_NUM=Serial.readStringUntil(':');
          if(LEG_NUM=="L1"){
            for(int COUNT1=0;COUNT1<4;COUNT1++){
             Pattern[1][COUNT1]=Serial.readStringUntil(':');
             if(Pattern[1][COUNT1]=="No")attitude[1][COUNT1]=1;
             //else if(Pattern[1][COUNT1]=="NN")attitude[1][COUNT1]=0;
             //Serial.println("test:1"+(String)COUNT1+(String)attitude[1][COUNT1]);
            }
          }
          else if(LEG_NUM=="L2"){
            for(int COUNT2=0;COUNT2<4;COUNT2++){
             Pattern[2][COUNT2]=Serial.readStringUntil(':');
             if(Pattern[2][COUNT2]=="No")attitude[2][COUNT2]=1;
             //else if(Pattern[2][COUNT2]=="NN")attitude[2][COUNT2]=0;
             //Serial.println("test:2"+(String)COUNT2+(String)attitude[2][COUNT2]);
            }
          }
          else if(LEG_NUM=="R1"){
            for(int COUNT3=0;COUNT3<4;COUNT3++){
             Pattern[3][COUNT3]=Serial.readStringUntil(':');
             if(Pattern[3][COUNT3]=="No")attitude[3][COUNT3]=1;
             //else if(Pattern[3][COUNT3]=="NN")attitude[3][COUNT3]=0;
             //Serial.println("test:3"+(String)COUNT3+(String)attitude[3][COUNT3]);
            }
          }
          else if(LEG_NUM=="R2"){
            for(int COUNT4=0;COUNT4<4;COUNT4++){
             Pattern[4][COUNT4]=Serial.readStringUntil(':');
             if(Pattern[4][COUNT4]=="No")attitude[4][COUNT4]=1;
             //else if(Pattern[4][COUNT4]=="NN")attitude[4][COUNT4]=0;
             //Serial.println("test:4"+(String)COUNT4+(String)attitude[4][COUNT4]);
            }
          }
        }
    }
    
    if(PotentiometerFlag==1)Output_Function_Angle();  //ポテンショメータの角度データを出力  
    if(Normal_Force_Flag==1)Output_function_NormalForce();//圧力センサ出力
    if(DRIVE_FLAG==1){
      Output_Function_PHASE_angle_velocity();
      Output_Function_Angle();
      Output_function_NormalForce();
    }
    
    
    
}//loop関数終わり



//Degrees to Radians
double To_Radians(double Degrees){
  return Degrees*(PI_VAL/180);
}
//Radians to Degrees
double To_Degrees(double Radians){
  return Radians*(180/PI_VAL);
}

//位相を受信してスイッチを切り替える
void LegphaseInput(char legnum,double leg_switch){
  int LegNumberOfPoint;
  if(legnum=="L1"){
    LegNumberOfPoint=1;
        if(leg_switch==0){//NN
          digitalWrite((22+4*LegNumberOfPoint-4),LOW);
          digitalWrite((23+4*LegNumberOfPoint-4),LOW);
          digitalWrite((24+4*LegNumberOfPoint-4),LOW);
          digitalWrite((25+4*LegNumberOfPoint-4),LOW);  
        }
        else if(leg_switch==1){//FF
          digitalWrite((22+4*LegNumberOfPoint-4),HIGH);
          digitalWrite((23+4*LegNumberOfPoint-4),LOW);
          digitalWrite((24+4*LegNumberOfPoint-4),HIGH);
          digitalWrite((25+4*LegNumberOfPoint-4),LOW);  
        }
  }
  else if(legnum=="L2"){
    LegNumberOfPoint=2;
        if(leg_switch==0){//NN
          digitalWrite((22+4*LegNumberOfPoint-4),LOW);
          digitalWrite((23+4*LegNumberOfPoint-4),LOW);
          digitalWrite((24+4*LegNumberOfPoint-4),LOW);
          digitalWrite((25+4*LegNumberOfPoint-4),LOW);  
        }
        else if(leg_switch==1){//RR
          digitalWrite((22+4*LegNumberOfPoint-4),LOW);
          digitalWrite((23+4*LegNumberOfPoint-4),HIGH);
          digitalWrite((24+4*LegNumberOfPoint-4),LOW);
          digitalWrite((25+4*LegNumberOfPoint-4),HIGH);  
        }
  }
  else if(legnum=="R1"){
    LegNumberOfPoint=3;
        if(leg_switch==0){//NN
          digitalWrite((22+4*LegNumberOfPoint-4),LOW);
          digitalWrite((23+4*LegNumberOfPoint-4),LOW);
          digitalWrite((24+4*LegNumberOfPoint-4),LOW);
          digitalWrite((25+4*LegNumberOfPoint-4),LOW);  
        }
        else if(leg_switch==1){//FF
          digitalWrite((22+4*LegNumberOfPoint-4),HIGH);
          digitalWrite((23+4*LegNumberOfPoint-4),LOW);
          digitalWrite((24+4*LegNumberOfPoint-4),HIGH);
          digitalWrite((25+4*LegNumberOfPoint-4),LOW);  
        }
  }
  else if(legnum=="R2"){
    LegNumberOfPoint=4;
        if(leg_switch==0){//NN
          digitalWrite((22+4*LegNumberOfPoint-4),LOW);
          digitalWrite((23+4*LegNumberOfPoint-4),LOW);
          digitalWrite((24+4*LegNumberOfPoint-4),LOW);
          digitalWrite((25+4*LegNumberOfPoint-4),LOW);  
        }
        else if(leg_switch==1){//RR
          digitalWrite((22+4*LegNumberOfPoint-4),LOW);
          digitalWrite((23+4*LegNumberOfPoint-4),HIGH);
          digitalWrite((24+4*LegNumberOfPoint-4),LOW);
          digitalWrite((25+4*LegNumberOfPoint-4),HIGH);    
        }
  }
}





//コンボボックス値と脚番号によってスイッチを切り替える
void LegPointInput(int ComboBoxNumber,int LegNumberOfPoint,char JointPoint1,char JointPoint2){
       
            switch(JointPoint1){
                case 'N':
                    digitalWrite((22+4*LegNumberOfPoint-4),LOW);
                    digitalWrite((23+4*LegNumberOfPoint-4),LOW);                   
                    break;
                case 'F':
                    digitalWrite((22+4*LegNumberOfPoint-4),HIGH);
                    digitalWrite((23+4*LegNumberOfPoint-4),LOW); 
                    break;
                case 'B':
                    digitalWrite((22+4*LegNumberOfPoint-4),LOW);
                    digitalWrite((23+4*LegNumberOfPoint-4),HIGH);  
                    break;
                default:
                    break;
            }
            
            switch(JointPoint2){
                case 'N':
                    digitalWrite((24+4*LegNumberOfPoint-4),LOW);
                    digitalWrite((25+4*LegNumberOfPoint-4),LOW);
                    break;
                case 'F':
                    digitalWrite((24+4*LegNumberOfPoint-4),HIGH);
                    digitalWrite((25+4*LegNumberOfPoint-4),LOW);  
                    break;
                case 'B':
                    digitalWrite((24+4*LegNumberOfPoint-4),LOW);
                    digitalWrite((25+4*LegNumberOfPoint-4),HIGH);
                    break;
                default:
                    break;
            }
  }

//電磁弁の関数
void GFGA_function(){
  if(sampleDrive=="OFF"){  
    for(PinNum=22;PinNum<54;PinNum++){
      digitalWrite(PinNum,LOW);
    } 
  }else if(sampleDrive=="ON"){
  //digitalWrite(LED_BUILTIN, HIGH); 
    for(PinNum=22;PinNum<38;PinNum++){
      digitalWrite(PinNum,HIGH);
      delay(100);
      digitalWrite(PinNum,LOW);
      delay(100);
    }
  }
}



//電空レギュレータの関数
void ITV_function(){
  valve_direction = Serial.readStringUntil(':');
  valve_serial = Serial.readStringUntil(':');
  valve_pressure = valve_serial.toFloat();
  digit_valve = valve_pressure / 0.9 * 4095;
  if(valve_direction=="MAXP")
  VOUT = 0;
  else if(valve_direction=="minP")
  VOUT = 1;
  valve_flag = 1;
   
  if(valve_flag == 1)
  {
    digitalWrite(DA_LDAC, HIGH);
    digitalWrite(DA_CS, LOW);
    DACout(DA_SDI, DA_SCK, VOUT, digit_valve);
    digitalWrite(DA_CS, HIGH);
    digitalWrite(DA_LDAC, LOW);//ラッチ信号を出す（ここで実際に出力するように指示）
    valve_flag = 0;
    //Serial.println(valve_direction + '=' + valve_pressure + ':');
  }    
}


//ローパスフィルター
double LOW_PASS_FILTER(double now_signal,double last_signal,double constant1,double constant2,double difference)
   {
       double variable;
       double LPF=0.0;
       if (abs(last_signal - now_signal) > difference) variable = constant1;
       else variable = constant2;
       return LPF += variable * (now_signal - LPF);
   }




//String型をFloat型に変換する関数
float StringToFloat(String s)
{
  float result;
  char *ErrPtr;
  
  if(s=="") return ErrorFloat;
  result=strtod(s.c_str(),&ErrPtr);
  if(*ErrPtr=='\0') { // 正常に変換できた
    return result;
  } else { // 変換できなかった
    return ErrorFloat;
  } // if
} // StringToFloat




//位相の角速度と位相を出力する関数
void Output_Function_PHASE_angle_velocity(){

  double preTime;
  double dt=(micros()-preTime)/1000000;
  double saw_L1;
  double saw_L2;
  double saw_R1;
  double saw_R2;
  int attitude_L1;
  int attitude_L2;
  int attitude_R1;
  int attitude_R2;

  angle_velocity_L1=omega-sigma*FORCE[0]*cos(PHASE_L1);
  angle_velocity_L2=omega-sigma*FORCE[1]*cos(PHASE_L2);
  angle_velocity_R1=omega-sigma*FORCE[2]*cos(PHASE_R1);
  angle_velocity_R2=omega-sigma*FORCE[3]*cos(PHASE_R2);

  saw_L1+=angle_velocity_L1*dt;
  saw_L2+=angle_velocity_L2*dt;
  saw_R1+=angle_velocity_R1*dt;
  saw_R2+=angle_velocity_R2*dt;

  PHASE_L1=fmod(saw_L1,2*PI_VAL);
  PHASE_L2=fmod(saw_L2,2*PI_VAL);
  PHASE_R1=fmod(saw_R1,2*PI_VAL);
  PHASE_R2=fmod(saw_R2,2*PI_VAL);

  if(0<=PHASE_L1&&PHASE_L1<180)attitude_L1=1;
  else if(180<=PHASE_L1&&PHASE_L1<360)attitude_L1=0;
  if(0<=PHASE_L2&&PHASE_L2<180)attitude_L2=1;
  else if(180<=PHASE_L2&&PHASE_L2<360)attitude_L2=0;
  if(0<=PHASE_R1&&PHASE_R1<180)attitude_R1=1;
  else if(180<=PHASE_R1&&PHASE_R1<360)attitude_R1=0;
  if(0<=PHASE_R2&&PHASE_R2<180)attitude_R2=1;
  else if(180<=PHASE_R2&&PHASE_R2<360)attitude_R2=0;
  
  
  Serial.println("AV_L1:"+String(angle_velocity_L1));
  Serial.println("AV_L2:"+String(angle_velocity_L2));
  Serial.println("AV_R1:"+String(angle_velocity_R1));
  Serial.println("AV_R2:"+String(angle_velocity_R2));
  Serial.println("PHASE_L1:"+String(PHASE_L1));
  Serial.println("PHASE_L2:"+String(PHASE_L2));
  Serial.println("PHASE_R1:"+String(PHASE_R1));
  Serial.println("PHASE_R2:"+String(PHASE_R2));
  Serial.println("ATTITUDE_L1:"+String(attitude_L1));
  Serial.println("ATTITUDE_L2:"+String(attitude_L2));
  Serial.println("ATTITUDE_R1:"+String(attitude_R1));
  Serial.println("ATTITUDE_R2:"+String(attitude_R2));
}





//ポテンショメータ生データを出力する関数
void Output_Function_Analogdata(){
  
  String Analog_data_L11;
  String Analog_data_L12;
  String Analog_data_L21;
  String Analog_data_L22;
  String Analog_data_R11;
  String Analog_data_R12;
  String Analog_data_R21;
  String Analog_data_R22;

  //ポテンショメータからのデータ受信，型をStringにキャスティング
  Analog_data_L11=String(analogRead(A0));
  Analog_data_L12=String(analogRead(A1));
  Analog_data_L21=String(analogRead(A2));
  Analog_data_L22=String(analogRead(A3));
  Analog_data_R11=String(analogRead(A4));
  Analog_data_R12=String(analogRead(A5));
  Analog_data_R21=String(analogRead(A6));
  Analog_data_R22=String(analogRead(A7));

  //PCへのデータ送信（生データ）
  Serial.println("Potentiometer_L11:"+Analog_data_L11);
  Serial.println("Potentiometer_L12:"+Analog_data_L12);
  Serial.println("Potentiometer_L21:"+Analog_data_L21);
  Serial.println("Potentiometer_L22:"+Analog_data_L22);
  Serial.println("Potentiometer_R11:"+Analog_data_R11);
  Serial.println("Potentiometer_R12:"+Analog_data_R12);
  Serial.println("Potentiometer_R21:"+Analog_data_R21);
  Serial.println("Potentiometer_R22:"+Analog_data_R22);
  
}

//角度を出力する関数
void Output_Function_Angle(){
  
  String Analog_data_L11;
  String Analog_data_L12;
  String Analog_data_L21;
  String Analog_data_L22;
  String Analog_data_R11;
  String Analog_data_R12;
  String Analog_data_R21;
  String Analog_data_R22;

  //ポテンショメータからのデータ受信，型をStringにキャスティング
  Analog_data_L11=String(analogRead(A0));
  Analog_data_L12=String(analogRead(A1));
  Analog_data_L21=String(analogRead(A2));
  Analog_data_L22=String(analogRead(A3));
  Analog_data_R11=String(analogRead(A4));
  Analog_data_R12=String(analogRead(A5));
  Analog_data_R21=String(analogRead(A6));
  Analog_data_R22=String(analogRead(A7));

  //関節角度を格納する行列
  double Angle[4][2]={0.0};

  Angle[0][0]=180-90*(StringToFloat(Analog_data_L11)-543)/(194-543);
  Angle[0][1]=180-90*(StringToFloat(Analog_data_L12)-553)/(194-553);
  Angle[1][0]=180-90*(StringToFloat(Analog_data_L21)-470)/(805-470);
  Angle[1][1]=180-90*(StringToFloat(Analog_data_L22)-428)/(757-428);
  Angle[2][0]=180-90*(StringToFloat(Analog_data_R11)-414)/(742-414);
  Angle[2][1]=180-90*(StringToFloat(Analog_data_R12)-423)/(761-423);
  Angle[3][0]=180-90*(StringToFloat(Analog_data_R21)-650)/(336-650);
  Angle[3][1]=180-90*(StringToFloat(Analog_data_R22)-553)/(211-553);

  //PCへのデータ送信（生データ）
  Serial.println("Potentiometer_L11:"+String(Angle[0][0]));
  Serial.println("Potentiometer_L12:"+String(Angle[0][1]));
  Serial.println("Potentiometer_L21:"+String(Angle[1][0]));
  Serial.println("Potentiometer_L22:"+String(Angle[1][1]));
  Serial.println("Potentiometer_R11:"+String(Angle[2][0]));
  Serial.println("Potentiometer_R12:"+String(Angle[2][1]));
  Serial.println("Potentiometer_R21:"+String(Angle[3][0]));
  Serial.println("Potentiometer_R22:"+String(Angle[3][1]));
  Serial.flush();
  
}

void Output_function_NormalForce(){

  double Vo[4], Rf[4], fg[4];
  double R1 = 1.0;
  double Force[4];
  double last_fg[4];
  double filtered_fg[4];
  //double FORCE[4];
  
  Force[0]=analogRead(A8);
  Force[1]=analogRead(A9);
  Force[2]=analogRead(A10);
  Force[3]=analogRead(A11);

  
  for(int i=0;i<4;i++){
    Vo[i] = Force[i] * 5.0 / 1024;
    Rf[i] = R1*Vo[i] / (5.0 - Vo[i]);
    fg[i] = (((880.79/Rf[i]+0)/4448)*10)*9.81;//[N]
    //fg[i] = 880.79/Rf[i] + 47.96;
    filtered_fg[i]=LOW_PASS_FILTER(fg[i],last_fg[i],0.1,0.01,0.03);
    last_fg[i]=fg[i];
    FORCE[i]=force[1];
  }

  String Force_Info_L1=String(FORCE[0]);
  String Force_Info_L2=String(FORCE[1]);
  String Force_Info_R1=String(FORCE[2]);
  String Force_Info_R2=String(FORCE[3]);


  //String Force_Info_L1_non=String(fg[0]);
  //Serial.print("L1_Normal_Force_non:"+Force_Info_L1_non);
  
  Serial.println("L1_Normal_Force:"+Force_Info_L1);
  Serial.println("L2_Normal_Force:"+Force_Info_L2);
  Serial.println("R1_Normal_Force:"+Force_Info_R1);
  Serial.println("R2_Normal_Force:"+Force_Info_R2);
  Serial.flush();
  
}
