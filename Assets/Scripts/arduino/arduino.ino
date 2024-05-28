#include <AFMotor.h>
#include <Servo.h>
#define pin_esc 35
Servo esc; 

Servo myServo;  // Crear un objeto Servo
Servo myServo1;
Servo myServo2;

// Inicializa los motores en los puertos M1 y M4 de la placa L293D Motor Shield
AF_DCMotor motor(1); // motor izquierdo
AF_DCMotor motor1(2); // motor derecho

Servo servoX; // Crea un objeto de tipo Servo para controlar el servo motor en el eje X

String inputString = "";         // una cadena para almacenar los datos de entrada
boolean stringComplete = false;  // si se ha recibido una cadena completa

void setup() {
  // Inicia la comunicación serie en el puerto Serial1 a una velocidad de 9600 baudios
  Serial1.begin(9600);
  Serial.begin(9600);

  servoX.attach(10); // Conecta el servo motor en el eje X al pin 10
  inputString.reserve(200);  // reserva espacio para la cadena


  myServo.attach(30);  // Adjuntar el servo al pin digital 9
  myServo1.attach(31);
  myServo2.attach(33); 
  myServo.write(180);   // Inicialmente mover el servo a 0 grados
  myServo1.write(100);
  myServo2.write(0);

  esc.attach(pin_esc);
  esc.writeMicroseconds(1000);//Es necesario iniciar con 1 ms para activar el ESC
  delay(1000);//Esperar 1 segundo     
}

int cont1 = 180;
int cont2 = 100;

void loop() {
  // cuando se recibe una cadena completa, se procesa
  
  if (stringComplete) {
    Serial.println(inputString); // Imprime el comando recibido para depuración

    // Comprueba el comando recibido y controla el motor en consecuencia
    if (inputString.length() == 1) { // si el comando es un solo carácter
      char cmd = inputString[0]; // convierte el comando a char
      switch (cmd) {
        case 'W': // Avanza
          motor.setSpeed(255); // Velocidad máxima
          motor.run(FORWARD); // Avanza
          motor1.setSpeed(255); // Velocidad máxima
          motor1.run(FORWARD); // Avanza
          break;
        case 'S': // Retrocede
          motor.setSpeed(255); // Velocidad máxima
          motor.run(BACKWARD); // Retrocede
          motor1.setSpeed(255); // Velocidad máxima
          motor1.run(BACKWARD); // Retrocede
          break;
        case 'A': // Gira a la izquierda
          motor.setSpeed(150); // Velocidad máxima
          motor.run(FORWARD); // Avanza
          motor1.setSpeed(255); // Velocidad máxima
          motor1.run(FORWARD); // Avanza
          break;
        case 'D': // Gira a la derecha
          motor.setSpeed(255); // Velocidad máxima
          motor.run(FORWARD); // Avanza
          motor1.setSpeed(150); // Velocidad máxima
          motor1.run(FORWARD); // Avanza
          break;
        case 'P': // Detiene los motores
          motor.run(RELEASE);
          motor1.run(RELEASE);
          break;
        case 'U': // Detiene los motores
          cont1 = cont1 - 40;
          cont2 = cont2 + 40;
          if(cont1 < 100){
            cont1 = 100;
            cont2 = 180;
          }
          myServo.write(cont1);
          myServo1.write(cont2);
          break;
        case 'J': // Detiene los motores
          contJ();
          if(cont2 < 100){
            cont1 = 180;
            cont2 = 100;
            myServo.write(cont1);
            myServo1.write(cont2);
          }
          break;
        case '0': // Detiene los motores
          esc.writeMicroseconds(1500);
          delay(500);
          myServo2.write(160);
          delay(1000);
          myServo2.write(0);
          delay(1000);
          esc.writeMicroseconds(1000);
          break;
      }
    } 

    // limpia la cadena y reinicia la bandera
    inputString = "";
    stringComplete = false;
  }
}

void contJ(){
  for(int i = 0; i<40; i++){
    cont1 = cont1 + 1;
    cont2 = cont2 - 1;
    myServo.write(cont1);
    myServo1.write(cont2);
    delay(10);
  }
}


// función llamada cuando un nuevo dato llega por el puerto serial
void serialEvent1() {
  while (Serial1.available()) {
    char inChar = (char)Serial1.read();
    if (inChar == '\n') {
      stringComplete = true;
    } else {
      inputString += inChar;
    }
  }
}













