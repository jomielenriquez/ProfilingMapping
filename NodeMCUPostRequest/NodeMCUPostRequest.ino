#include <ESP8266WiFi.h>
#include <ESP8266HTTPClient.h>
#include <TinyGPS++.h>
#include <SoftwareSerial.h>
TinyGPSPlus gps;  // The TinyGPS++ object
SoftwareSerial ss(4, 5); // The serial connection to the GPS device
float latitude , longitude;
int year , month , date, hour , minute , second;
String date_str , time_str , lat_str="14.083027", lng_str="121.125298";
int pm;

const char *ssid =  "JomielPogi_2.4";     // replace with your wifi ssid and wpa2 key
const char *pass =  "Irtcitmahs121625";
const char *DeviceName =  "Testing 5/25";
String MobileNumber =  "AT+CMGS=\"+639953637231\"\r";
const char* server = "http://profilingmapping.somee.com/RequestData/InsertLog";
String nameid = "79D7658F-B190-496E-86FE-20CDB7F07588";

SoftwareSerial mySerial(3,1);
const int Push_Button1 =  2;
const int Push_Button2 =  0;
int Button_State_SMS = 0;
int Button_State_HTTPS = 0;

// GPS Connection
// GPS     ESP32
// VCC  => 3v    ESP32
// GND  => GND   ESP32
// TX   => D1    ESP32 (5)
// RX   => D2    ESP32 (4)

//GSM Connection
//GSM
//VCC => ARDUINO 5V
//GND => ARDUINO GND
//RX  => NODEMCU TX (GPIO1)
//RX  => NODEMCU RX (GPIO3)

//Push button 1
//Line 1 => ESP32 GND
//Line 2 => D4 (GPIO2)

//Push button 2
//Line 1 => ESP32 GND
//Line 2 => D3 (GPIO0)

WiFiClient client;
 
void setup() 
{
  pinMode (LED_BUILTIN, OUTPUT);

  pinMode(Push_Button1, INPUT); 
  pinMode(Push_Button2, INPUT); 
  mySerial.begin(9600);

  ss.begin(9600);
  delay(10);
          
  mySerial.println("Connecting to ");
  mySerial.println(ssid); 

  WiFi.begin(ssid, pass); 
  while (WiFi.status() != WL_CONNECTED) 
  {
    delay(500);
    Serial.print(".");
  }
  mySerial.println("");
  mySerial.println("WiFi connected"); 

  if(WiFi.status()== WL_CONNECTED){
    digitalWrite(LED_BUILTIN, HIGH);
    delay(500);
    //sendPostRequest();
    digitalWrite(LED_BUILTIN, LOW);
  }
}
 
void loop() 
{      
  while (ss.available() > 0) //while data is available
    if (gps.encode(ss.read())) //read gps data
    {
      if (gps.location.isValid()) //check whether gps location is valid
      {
        latitude = gps.location.lat();
        lat_str = String(latitude , 6); // latitude location is stored in a string
        longitude = gps.location.lng();
        lng_str = String(longitude , 6); //longitude location is stored in a string
      }
      if (gps.date.isValid()) //check whether gps date is valid
      {
        date_str = "";
        date = gps.date.day();
        month = gps.date.month();
        year = gps.date.year();
        if (date < 10)
          date_str = '0';
        date_str += String(date);// values of date,month and year are stored in a string
        date_str += " / ";

        if (month < 10)
          date_str += '0';
        date_str += String(month); // values of date,month and year are stored in a string
        date_str += " / ";
        if (year < 10)
          date_str += '0';
        date_str += String(year); // values of date,month and year are stored in a string
      }
      if (gps.time.isValid())  //check whether gps time is valid
      {
        time_str = "";
        hour = gps.time.hour();
        minute = gps.time.minute();
        second = gps.time.second();
        minute = (minute + 30); // converting to IST
        if (minute > 59)
        {
          minute = minute - 60;
          hour = hour + 1;
        }
        hour = (hour + 5) ;
        if (hour > 23)
          hour = hour - 24;   // converting to IST
        if (hour >= 12)  // checking whether AM or PM
          pm = 1;
        else
          pm = 0;
        hour = hour % 12;
        if (hour < 10)
          time_str = '0';
        time_str += String(hour); //values of hour,minute and time are stored in a string
        time_str += " : ";
        if (minute < 10)
          time_str += '0';
        time_str += String(minute); //values of hour,minute and time are stored in a string
        time_str += " : ";
        if (second < 10)
          time_str += '0';
        time_str += String(second); //values of hour,minute and time are stored in a string
        if (pm == 1)
          time_str += " PM ";
        else
          time_str += " AM ";
      }
      // Serial.print("Latitude ");
      // Serial.print(lat_str);
      // Serial.print(" Longitude ");
      // Serial.print(lng_str);
      // Serial.print(" Date ");
      // Serial.print(date_str);
      // Serial.print(" Time ");
      // Serial.println(time_str);
      
    }

  Button_State_SMS = digitalRead(Push_Button1);  /*Check pushbutton state*/
  Button_State_HTTPS = digitalRead(Push_Button2);  /*Check pushbutton state*/
  if (Button_State_SMS == LOW) {       /*if condition to check button status*/
    //Send Text Message
    SendMessage();
  }
  if (Button_State_HTTPS == LOW) {       /*if condition to check button status*/
    // Send Https post reqest
    sendPostRequest();
  }
  delay(100);
}

void sendPostRequest(){
  HTTPClient http;
  http.begin(client,server);
  http.addHeader("Content-Type", "application/x-www-form-urlencoded");
  // Data to send with HTTP POST
  String httpRequestData = "nameid="+nameid+"&loc_lat="+lat_str+"&loc_long="+lng_str+"&devicename="+DeviceName;           
  // Send HTTP POST request
  int httpResponseCode = http.POST(httpRequestData);
  
  /*
  
  http.addHeader("Content-Type", "application/json");
  // JSON data to send with HTTP POST
  String httpRequestData = "{\"api_key\":\"" + my_Api_Key + "\",\"field1\":\"" + String(random(50)) + "\"}";           
  // Send HTTP POST request
  int httpResponseCode = http.POST(httpRequestData);*/
  
  mySerial.print("HTTP Response code is: ");
  mySerial.println(httpResponseCode);
  http.end();
}

void SendMessage(){
  mySerial.println("AT+CMGF=1");    //Sets the GSM Module in Text Mode
  delay(1000);  // Delay of 1 second
  mySerial.println(MobileNumber); // Replace x with mobile number
  delay(1000);
  String loclink="maps/place/",comma=",";
  // String body = "maps.google.com/?q= "+lat_str+","+lng_str;
  mySerial.print(loclink);
  mySerial.print(lat_str);
  mySerial.print(comma);
  mySerial.println(lng_str);
  // mySerial.println(body);// The SMS text you want to send
  // mySerial.println("https://maps.google.com/?q="+lat_str+","+lng_str);// The SMS text you want to send
  delay(100);
  mySerial.println((char)26);// ASCII code of CTRL+Z for saying the end of sms to  the module 
  delay(1000);
}
