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
  Serial.begin(9600);

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
      
          valve_type=Serial.readStringUntil(':');   
              
          if(valve_type=="POT ON"){
            PotentiometerFlag=1;
          }
          else if(valve_type=="POT OFF"){
            PotentiometerFlag=0;
          }
          else if(valve_type=="ITV"){          //電空レギュレータによる制御
            ITV_function();
            //Serial.println("ITV");
          }
          else if(valve_type=="PHASE"){
            cycle_func();
          }
  
          if(valve_type=="Normal_force_ON"){
            Normal_Force_Flag=1;
          }
          else if(valve_type=="Normal_force_OFF"){
            Normal_Force_Flag=0;
          }
    }



    
    if(PotentiometerFlag==1){
      Output_Function_Angle();  //ポテンショメータの角度データを出力
    }

    if(Normal_Force_Flag==1){
      Output_function_NormalForce();
    }
    
}//loop関数終わり
/*
//位相を受信してスイッチを切り替える
void LegphaseInput(char legnum,double phase,char joint1,char joint2){
  switch(legnum){
    case 'L1':
    
    case 'L2':
    case 'R1':
    case 'R2':
    default:
           break;
    
  }
}
*/
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

void cycle_func(){
  String foot_sign=Serial.readStringUntil(':');
  String sygnal_L1;
  String sygnal_L2;
  String sygnal_R1;
  String sygnal_R2;
  double L1_phase;
  double L2_phase;
  double R1_phase;
  double R2_phase;
  
  if(foot_sign=="L1"){
    sygnal_L1=Serial.readStringUntil(':');
    L1_phase=sygnal_L1.toFloat();
    Serial.println("L1_phase_receive:"+String(L1_phase));
  }
  else if(foot_sign=="L2"){
    sygnal_L2=Serial.readStringUntil(':');
    L2_phase=sygnal_L2.toFloat();
    Serial.println("L2_phase_receive:"+String(L2_phase));
  }
  else if(foot_sign=="R1"){
    sygnal_R1=Serial.readStringUntil(':');
    R1_phase=sygnal_R1.toFloat();
    Serial.println("R1_phase_receive:"+String(R1_phase));
  }
  else if(foot_sign=="R2"){
    sygnal_R2=Serial.readStringUntil(':');
    R2_phase=sygnal_R2.toFloat();
    Serial.println("R2_phase_receive:"+String(R2_phase));
  }
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
  
}

void Output_function_NormalForce(){

  double Vo[4], Rf[4], fg[4];
  double R1 = 1.0;
  int Force[4];

  Force[0]=analogRead(A8);
  Force[1]=analogRead(A9);
  Force[2]=analogRead(A10);
  Force[3]=analogRead(A11);
  for(int i=0;i<4;i++){
    Vo[i] = Force[i] * 5.0 / 1024;
    Rf[i] = R1*Vo[i] / (5.0 - Vo[i]);
    fg[i] = 880.79/Rf[i];
    //fg[i] = 880.79/Rf[i] + 47.96;
  }

  String Force_Info_L1=String(fg[0]);
  String Force_Info_L2=String(fg[1]);
  String Force_Info_R1=String(fg[2]);
  String Force_Info_R2=String(fg[3]);
  
  Serial.println("L1_Normal_Force:"+Force_Info_L1);
  Serial.println("L2_Normal_Force:"+Force_Info_L2);
  Serial.println("R1_Normal_Force:"+Force_Info_R1);
  Serial.println("R2_Normal_Force:"+Force_Info_R2);
  
}
