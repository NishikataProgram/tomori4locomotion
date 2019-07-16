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

int serial_baudrate=19200;//9600,19200,38400,57600,74880,115200,230400,250000

//unsigned long start;
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
String DRIVE_TYPE;
double omega;
double sigma;
double force[4];
double PHASE[4]={0.0,0.0,0.0,0.0};
double angle_velocity[4]={0.0,0.0,0.0,0.0};
double FORCE[4];
String SETTING_SWITCH;
String CONST_VALUE_SWITCH;
String CONST_VALUE_OMEGA;
String CONST_VALUE_SIGMA;
String LEG_NUM;
String Pattern[4][4];//配列前半：脚番号，配列後半：動作順番（位相順方向）
int DRIVE_FLAG=0;
int timinger[4]={0,0,0,0};
double DeltaPhase[4]={0.0,0.0,0.0,0.0};
double PrePhase[4]={0.0,0.0,0.0,0.0};
double TIMER=0.0;
int timerflag=0;
int loopcounter;
double starttimebox;
double PHASEsaw[4]={0.0,0.0,0.0,0.0};
double GIGA=pow(10,9);
double MEGA=pow(10,6);
double KILLO=pow(10,3);
double preTime;
double dt;
double nowTIME;

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
  }//for end
}//DACout end


void Pin_All_OFF(){
    for(PinNum=22;PinNum<54;PinNum++){
      digitalWrite(PinNum,LOW);
    }
}//Pin_All_OFF end


  
void setup() {
  Serial.begin(serial_baudrate);
  //start=micros();
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
}//setup end

          
void loop() {
    if(Serial.available()>0){//PCからの受信
      
        SETTING_SWITCH=Serial.readStringUntil(':');
        if(SETTING_SWITCH=="POT ON")PotentiometerFlag=1;
        if(SETTING_SWITCH=="POT OFF")PotentiometerFlag=0;
        if(SETTING_SWITCH=="Normal_force_ON")Normal_Force_Flag=1;
        if(SETTING_SWITCH=="Normal_force_OFF")Normal_Force_Flag=0;
        if(SETTING_SWITCH=="ITV")ITV_function();         
        if(SETTING_SWITCH=="DRIVE"){
          DRIVE_FLAG=1; 
        }
        if(SETTING_SWITCH=="DRIVE_STOP"){
          DRIVE_FLAG=0;
        }
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
        
        if(SETTING_SWITCH=="PHASE_DRIVE"){//脚の駆動ぱたーん受信
          LEG_NUM=Serial.readStringUntil(':');
          if(LEG_NUM=="L1"){
            for(int COUNT1=0;COUNT1<4;COUNT1++){
             Pattern[0][COUNT1]=Serial.readStringUntil(':');
            }
          }
          else if(LEG_NUM=="L2"){
            for(int COUNT2=0;COUNT2<4;COUNT2++){
             Pattern[1][COUNT2]=Serial.readStringUntil(':');
            }
          }
          else if(LEG_NUM=="R1"){
            for(int COUNT3=0;COUNT3<4;COUNT3++){
             Pattern[2][COUNT3]=Serial.readStringUntil(':');
            }
          }
          else if(LEG_NUM=="R2"){
            for(int COUNT4=0;COUNT4<4;COUNT4++){
             Pattern[3][COUNT4]=Serial.readStringUntil(':');
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
      dt=(micros()-preTime)/MEGA;
      preTime=micros();
      TIMER+=dt;
      if(loopcounter==0){starttimebox=TIMER;loopcounter+=1;}
      else if(loopcounter>0){loopcounter+=1;}
      else {loopcounter=0;}
      nowTIME=TIMER-starttimebox;
      
    }
    else if(DRIVE_FLAG==0){
      for(int c=0;c<4;c++){
        PHASE[c]=0.0;
      }
      TIMER=0.0;
    }


}//loop end

//Degrees to Radians
double To_Radians(double Degrees){
  return Degrees*(PI_VAL/180);
}
//Radians to Degrees
double To_Degrees(double Radians){
  return Radians*(180/PI_VAL);
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
}//LegPointImput end 

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

  //unsigned long preTime;
  //unsigned long dt;
  double saw[4];
  static int ComboBoxValue[4]={0,1,2,3};
  //unsigned long nowTIME;

  
  //静的変数の初期化処理
  if(DRIVE_FLAG==0){
    for(int c=0;c<4;c++){
      PHASE[c]=0.0;
      ComboBoxValue[c]=0;
    }
  }
  
    for(int count=0;count<4;count++){
      angle_velocity[count]=omega-sigma*FORCE[count]*cos(PHASE[count]);
      saw[count]+=angle_velocity[count]*dt;
      PHASE[count]=fmod(saw[count],2*PI_VAL);
      PHASEsaw[count]=fmod(PHASE[count],PI_VAL);
      DeltaPhase[count]=PHASEsaw[count]-PrePhase[count];
      if(DeltaPhase[count]<0)timinger[count]=1;
      else if(DeltaPhase[count]>0)timinger[count]=0;
      PrePhase[count]=PHASEsaw[count];
  
      if(Pattern[count][ComboBoxValue[count]] != "No"){
        //ComboBox内の値がNoneでなかった場合DelayTimeだけ以下の処理を行う
        LegPointInput(ComboBoxValue[count],1,Pattern[count][ComboBoxValue[count]].charAt(0),Pattern[count][ComboBoxValue[count]].charAt(1));
        if(timinger[count]==1){
            ComboBoxValue[count] = ComboBoxValue[count] + 1;
            if( ComboBoxValue[count] == 4 ) ComboBoxValue[count] = 0;
        }
      }  
      else {
        //ComboBoxの値がNoneだった場合次のComboBoxの処理に移る
        ComboBoxValue[count] = ComboBoxValue[count] + 1;
        if( ComboBoxValue[count] == 10 ) ComboBoxValue[count] = 0;
      }
    }
    
  /*
  if(DRIVE_FLAG==1&&loopcounter==0){starttimebox=TIMER;loopcounter++;}
  else if(DRIVE_FLAG==1&&loopcounter>0){loopcounter++;}
  else {loopcounter=0;}
  nowTIME=TIMER-starttimebox;
  */
  Serial.println("TIMER:"+String(nowTIME));
  Serial.println("AV_L1:"+String(angle_velocity[0]));
  Serial.println("AV_L2:"+String(angle_velocity[1]));
  Serial.println("AV_R1:"+String(angle_velocity[2]));
  Serial.println("AV_R2:"+String(angle_velocity[3]));
  Serial.println("PHASE_L1:"+String(PHASE[0]));
  Serial.println("PHASE_L2:"+String(PHASE[1]));
  Serial.println("PHASE_R1:"+String(PHASE[2]));
  Serial.println("PHASE_R2:"+String(PHASE[3]));
  Serial.println("ATTITUDE_L1:"+Pattern[0][ComboBoxValue[0]]);
  Serial.println("ATTITUDE_L2:"+Pattern[1][ComboBoxValue[1]]);
  Serial.println("ATTITUDE_R1:"+Pattern[2][ComboBoxValue[2]]);
  Serial.println("ATTITUDE_R2:"+Pattern[3][ComboBoxValue[3]]);
  Serial.println("dt:"+String(dt));
}


  double Angle[4][2]={0.0};

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

String Force_Info_L1;
String Force_Info_L2;
String Force_Info_R1;
String Force_Info_R2;

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
    FORCE[i]=filtered_fg[i];
  }

  Force_Info_L1=String(FORCE[0]);
  Force_Info_L2=String(FORCE[1]);
  Force_Info_R1=String(FORCE[2]);
  Force_Info_R2=String(FORCE[3]);
  
  Serial.println("L1_Normal_Force:"+Force_Info_L1);
  Serial.println("L2_Normal_Force:"+Force_Info_L2);
  Serial.println("R1_Normal_Force:"+Force_Info_R1);
  Serial.println("R2_Normal_Force:"+Force_Info_R2);
  Serial.flush();
  
}


  /*
  //シングルスレッド（絶対消すな！）
  /////////////
  //パターン１//
  /////////////
  dt=(micros()-preTime)/MEGA;
  preTime=micros();
  TIMER=TIMER+dt;
  angle_velocity[0]=omega-sigma*FORCE[0]*cos(PHASE[0]);
  angle_velocity[1]=omega-sigma*FORCE[1]*cos(PHASE[1]);
  angle_velocity[2]=omega-sigma*FORCE[2]*cos(PHASE[2]);
  angle_velocity[3]=omega-sigma*FORCE[3]*cos(PHASE[3]);

  saw[0]+=angle_velocity[0]*dt;
  saw[1]+=angle_velocity[1]*dt;
  saw[2]+=angle_velocity[2]*dt;
  saw[3]+=angle_velocity[3]*dt;

  PHASE[0]=fmod(saw[0],2*PI_VAL);
  PHASE[1]=fmod(saw[1],2*PI_VAL);
  PHASE[2]=fmod(saw[2],2*PI_VAL);
  PHASE[3]=fmod(saw[3],2*PI_VAL);

  PHASEsaw[0]=fmod(PHASE[0],PI_VAL);
  PHASEsaw[1]=fmod(PHASE[1],PI_VAL);
  PHASEsaw[2]=fmod(PHASE[2],PI_VAL);
  PHASEsaw[3]=fmod(PHASE[3],PI_VAL);


  DeltaPhase[0]=PHASEsaw[0]-PrePhase[0];
  DeltaPhase[1]=PHASEsaw[1]-PrePhase[1];
  DeltaPhase[2]=PHASEsaw[2]-PrePhase[2];
  DeltaPhase[3]=PHASEsaw[3]-PrePhase[3];
  
  if(DeltaPhase[0]<0)timinger[0]=1;
  else if(DeltaPhase[0]>0)timinger[0]=0;
  if(DeltaPhase[1]<0)timinger[1]=1;
  else if(DeltaPhase[1]>0)timinger[1]=0;
  if(DeltaPhase[2]<0)timinger[2]=1;
  else if(DeltaPhase[2]>0)timinger[2]=0;
  if(DeltaPhase[3]<0)timinger[3]=1;
  else if(DeltaPhase[3]>0)timinger[3]=0;
  
  PrePhase[0]=PHASEsaw[0];
  PrePhase[1]=PHASEsaw[1];
  PrePhase[2]=PHASEsaw[2];
  PrePhase[3]=PHASEsaw[3];
  

  //////////////////Leg1の処理//////////////////
    
  if(Pattern[0][ComboBoxValue[0]] != "No"){
      //ComboBox内の値がNoneでなかった場合DelayTimeだけ以下の処理を行う
      LegPointInput(ComboBoxValue[0],1,Pattern[0][ComboBoxValue[0]].charAt(0),Pattern[0][ComboBoxValue[0]].charAt(1));
      if(timinger[0]==1){
          ComboBoxValue[0] = ComboBoxValue[0] + 1;
          if( ComboBoxValue[0] == 4 ) ComboBoxValue[0] = 0;
      }
   }  
   else {
      //ComboBoxの値がNoneだった場合次のComboBoxの処理に移る
      ComboBoxValue[0] = ComboBoxValue[0] + 1;
      if( ComboBoxValue[0] == 4 ) ComboBoxValue[0] = 0;
   }
   /////////////////////////////////////////////////

   ////////////////////Leg2の処理//////////////////
    
   if(Pattern[1][ComboBoxValue[1]] != "No"){
      //ComboBox内の値がNoneでなかった場合DelayTimeだけ以下の処理を行う
      LegPointInput(ComboBoxValue[1],2,Pattern[1][ComboBoxValue[1]].charAt(0),Pattern[1][ComboBoxValue[1]].charAt(1));
      if(timinger[1]==1){
               ComboBoxValue[1] = ComboBoxValue[1] + 1;
               if( ComboBoxValue[1] == 4 ) ComboBoxValue[1] = 0;
      }
   }  
   else {
      //ComboBoxの値がNoneだった場合次のComboBoxの処理に移る
      ComboBoxValue[1] = ComboBoxValue[1] + 1;
      if( ComboBoxValue[1] == 4 ) ComboBoxValue[1] = 0;
   }
   ///////////////////////////////////////////////////
        
   ////////////////////Leg3の処理//////////////////
    
       if(Pattern[2][ComboBoxValue[2]] != "No"){
           //ComboBox内の値がNoneでなかった場合DelayTimeだけ以下の処理を行う
           LegPointInput(ComboBoxValue[2],3,Pattern[2][ComboBoxValue[2]].charAt(0),Pattern[2][ComboBoxValue[2]].charAt(1));
           if(timinger[2]==1){
               ComboBoxValue[2] = ComboBoxValue[2] + 1;
               if( ComboBoxValue[2] == 4 ) ComboBoxValue[2] = 0;
           }
        }  
        else {
       //ComboBoxの値がNoneだった場合次のComboBoxの処理に移る
           ComboBoxValue[2] = ComboBoxValue[2] + 1;
           if( ComboBoxValue[2] == 4 ) ComboBoxValue[2] = 0;
        }
   ///////////////////////////////////////////////////

   ////////////////////Leg4の処理//////////////////
    
       if(Pattern[3][ComboBoxValue[3]] != "No"){
           //ComboBox内の値がNoneでなかった場合DelayTimeだけ以下の処理を行う
           LegPointInput(ComboBoxValue[3],4,Pattern[3][ComboBoxValue[3]].charAt(0),Pattern[3][ComboBoxValue[3]].charAt(1));
           if(timinger[3]==1){
               ComboBoxValue[3] = ComboBoxValue[3] + 1;
               if( ComboBoxValue[3] == 4 ) ComboBoxValue[3] = 0;
           }
        }  
        else {
       //ComboBoxの値がNoneだった場合次のComboBoxの処理に移る
           ComboBoxValue[3] = ComboBoxValue[3] + 1;
           if( ComboBoxValue[3] == 4 ) ComboBoxValue[3] = 0;
        }
   ///////////////////////////////////////////////////

////////////
//パターン2//
////////////

angle_velocity_L1=omega-sigma* FORCE_L1* cos(PHASE_L1);
angle_velocity_L2=omega-sigma* FORCE_L2* cos(PHASE_L2);
angle_velocity_R1=omega-sigma* FORCE_R1* cos(PHASE_R1);
angle_velocity_R2=omega-sigma* FORCE_R2* cos(PHASE_R2);

saw_L1+=angle_velocity_L1* dt;
saw_L2+=angle_velocity_L2* dt;
saw_R1+=angle_velocity_R1* dt;
saw_R2+=angle_velocity_R2* dt;

PHASE_L1=fmod(saw_L1,2*PI_VAL);
PHASE_L2=fmod(saw_L2,2*PI_VAL);
PHASE_R1=fmod(saw_R1,2*PI_VAL);
PHASE_R2=fmod(saw_R2,2*PI_VAL);

PHASEsaw_L1=fmod(PHASE_L1, PI_VAL);
PHASEsaw_L2=fmod(PHASE_L2, PI_VAL);
PHASEsaw_R1=fmod(PHASE_R1, PI_VAL);
PHASEsaw_R2=fmod(PHASE_R2, PI_VAL);


DeltaPhase_L1=PHASEsaw_L1-PrePhase_L1;
  DeltaPhase_L2=PHASEsaw_L2-PrePhase_L2;
  DeltaPhase_R1=PHASEsaw_R1-PrePhase_R1;
  DeltaPhase_R2=PHASEsaw_R2-PrePhase_R2;
  
  if(DeltaPhase_L1<0)timinger_L1=1;
  else if(DeltaPhase_L1>0)timinger_L1=0;
  if(DeltaPhase_L2<0)timinger_L2=1;
  else if(DeltaPhase_L2>0)timinger_L2=0;
  if(DeltaPhase_R1<0)timinger_R1=1;
  else if(DeltaPhase_R1>0)timinger_R1=0;
  if(DeltaPhase_R2<0)timinger_R2=1;
  else if(DeltaPhase_R2>0)timinger_R2=0;
  
  PrePhase_L1=PHASEsaw_L1;
  PrePhase_L2=PHASEsaw_L2;
  PrePhase_R1=PHASEsaw_R1;
  PrePhase_R2=PHASEsaw_R2;
  

  //////////////////Leg1の処理//////////////////
    
  if(Pattern_L1[ComboBoxValue_L1] != "No"){
      //ComboBox内の値がNoneでなかった場合DelayTimeだけ以下の処理を行う
      LegPointInput(ComboBoxValue_L1,1, Pattern_L1[ComboBoxValue_L1].charAt(0),Pattern_L1[ComboBoxValue_L1].charAt(1));
      if(timinger_L1==1){
          ComboBoxValue_L1 = ComboBoxValue_L1 + 1;
          if(ComboBoxValue_L1 == 4 ) ComboBoxValue_L1 = 0;
      }
   }  
   else {
      //ComboBoxの値がNoneだった場合次のComboBoxの処理に移る
      ComboBoxValue_L1 = ComboBoxValue_L1 + 1;
      if(ComboBoxValue_L1 == 4 ) ComboBoxValue_L1 = 0;
   }
   /////////////////////////////////////////////////

   ////////////////////Leg2の処理//////////////////
    
   if(Pattern_L2[ComboBoxValue_L2] != "No"){
      //ComboBox内の値がNoneでなかった場合DelayTimeだけ以下の処理を行う
      LegPointInput(ComboBoxValue_L2,2, Pattern_L2[ComboBoxValue_L2].charAt(0),Pattern_L2[ComboBoxValue_L2].charAt(1));
      if(timinger_L2==1){
               ComboBoxValue_L2 = ComboBoxValue_L2 + 1;
               if(ComboBoxValue_L2 == 4 ) ComboBoxValue_L2 = 0;
      }
   }  
   else {
      //ComboBoxの値がNoneだった場合次のComboBoxの処理に移る
      ComboBoxValue_L2 = ComboBoxValue_L2 + 1;
      if(ComboBoxValue_L2 == 4 ) ComboBoxValue_L2 = 0;
   }
   ///////////////////////////////////////////////////
        
   ////////////////////Leg3の処理//////////////////
    
       if(Pattern_R1[ComboBoxValue_R1] != "No"){
           //ComboBox内の値がNoneでなかった場合DelayTimeだけ以下の処理を行う
           LegPointInput(ComboBoxValue_R1,3, Pattern_R1[ComboBoxValue_R1].charAt(0),Pattern_R1[ComboBoxValue_R1].charAt(1));
           if(timinger_R1==1){
               ComboBoxValue_R1 = ComboBoxValue_R1 + 1;
               if(ComboBoxValue_R1 == 4 ) ComboBoxValue_R1 = 0;
           }
        }  
        else {
       //ComboBoxの値がNoneだった場合次のComboBoxの処理に移る
           ComboBoxValue_R1 = ComboBoxValue_R1 + 1;
           if(ComboBoxValue_R1 == 4 ) ComboBoxValue_R1 = 0;
        }
   ///////////////////////////////////////////////////

   ////////////////////Leg4の処理//////////////////
    
       if(Pattern_R2[ComboBoxValue_R2] != "No"){
           //ComboBox内の値がNoneでなかった場合DelayTimeだけ以下の処理を行う
           LegPointInput(ComboBoxValue_R2,4, Pattern_R2[ComboBoxValue_R2].charAt(0),Pattern_R2[ComboBoxValue_R2].charAt(1));
           if(timinger_R2==1){
               ComboBoxValue_R2 = ComboBoxValue_R2 + 1;
               if(ComboBoxValue_R2 == 4 ) ComboBoxValue_R2 = 0;
           }
        }  
        else {
       //ComboBoxの値がNoneだった場合次のComboBoxの処理に移る
           ComboBoxValue_R2 = ComboBoxValue_R2 + 1;
           if(ComboBoxValue_R2 == 4 ) ComboBoxValue_R2 = 0;
        }
   ///////////////////////////////////////////////////

//シングルスレッド終わり
  */
