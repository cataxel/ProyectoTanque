#include <AFMotor.h>
#include <Servo.h>

#define pin_esc1 34
#define pin_esc2 35

Servo esc1; 
Servo esc2; 

// Crear un objeto Servo
Servo myServoUJ;
Servo myServoD;

// Inicializa los motores en los puertos M1 y M4 de la placa L293D Motor Shield
AF_DCMotor motor1(1); // motor izquierdo
AF_DCMotor motor2(4); // motor derecho
//AF_DCMotor motor3(2); // motor derecho
//AF_DCMotor motor4(2); // motor derecho

String inputString = "";         // una cadena para almacenar los datos de entrada
boolean stringComplete = false;  // si se ha recibido una cadena completa

int cont = 179;

void setup() {
  // Inicia la comunicación serie en el puerto Serial1 a una velocidad de 9600 baudios
  Serial1.begin(9600);
  Serial.begin(9600);

  inputString.reserve(200);  // reserva espacio para la cadena

  // Adjuntar el servo al pin digital
  myServoUJ.attach(10);
  myServoD.attach(9);

  // Inicialmente mover el servo a 0 grados
  myServoUJ.write(179);
  myServoD.write(0);

  // Adjuntar el servo al pin 
  esc1.attach(pin_esc1);
  esc2.attach(pin_esc2);

  //Es necesario iniciar con 1 ms para activar el ESC
  esc1.writeMicroseconds(1000);
  esc2.writeMicroseconds(1000);

  delay(1000);//Esperar 1 segundo     
}

void loop() {
  // cuando se recibe una cadena completa, se procesa
  if (stringComplete) {
    Serial.println(inputString); // Imprime el comando recibido para depuración

    // Comprueba el comando recibido y controla el motor en consecuencia
    if (inputString.length() == 1) { // si el comando es un solo carácter
      char cmd = inputString[0]; // convierte el comando a char
      switch (cmd) {
        case 'S': // Avanza
          motor1.setSpeed(255); // Velocidad máxima
          motor1.run(FORWARD); // Avanza
          motor2.setSpeed(255); // Velocidad máxima
          motor2.run(FORWARD); // Avanza
          break;
        case 'W': // Retrocede
          motor1.setSpeed(255); // Velocidad máxima
          motor1.run(BACKWARD); // Retrocede
          motor2.setSpeed(255); // Velocidad máxima
          motor2.run(BACKWARD); // Retrocede
          break;
        case 'A': // Gira a la izquierda
          motor1.setSpeed(100); // Velocidad máxima
          motor1.run(FORWARD); // Avanza
          motor2.setSpeed(255); // Velocidad máxima
          motor2.run(FORWARD); // Avanza
          break;
        case 'D': // Gira a la derecha
          motor1.setSpeed(255); // Velocidad máxima
          motor1.run(FORWARD); // Avanza
          motor2.setSpeed(100); // Velocidad máxima
          motor2.run(FORWARD); // Avanza
          break;
        case 'Q': // Gira a la derecha
          motor1.setSpeed(200); // Velocidad máxima
          motor1.run(BACKWARD); // Avanza
          motor2.setSpeed(200); // Velocidad máxima
          motor2.run(FORWARD); // Avanza
          break;
        case 'E': // Gira a la derecha
          motor1.setSpeed(200); // Velocidad máxima
          motor1.run(FORWARD); // Avanza
          motor2.setSpeed(200); // Velocidad máxima
          motor2.run(BACKWARD); // Avanza
          break;
        case 'P': // Detiene los motores
          motor1.run(RELEASE);
          motor2.run(RELEASE);
          break;
        case 'U': // Detiene los motores
          cont = cont - 40;
          if(cont < 99){
            cont = 99;
          }
          myServoUJ.write(cont);
          break;
        case 'J': // Detiene los motores
          contJ();
          if(cont > 179){
            cont = 179;
            myServoUJ.write(cont);
          }
          break;
        case '0': // Detiene los motores
          esc1.writeMicroseconds(2000);
          esc2.writeMicroseconds(2000);
          delay(500);
          myServoD.write(160);
          delay(1000);
          myServoD.write(0);
          delay(1000);
          esc1.writeMicroseconds(1000);
          esc2.writeMicroseconds(1000);
          break;
      }
    } 

    // limpia la cadena y reinicia la bandera
    inputString = "";
    stringComplete = false;
  }

  //metodo para que funcione con los datos que se introducen a traves del monitor serial
  monitor();

}

// controlar la velocidad de bajada
void contJ(){ 
  for(int i = 0; i<40; i++){
    cont = cont + 1;
    myServoUJ.write(cont);
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

void monitor(){
  if (Serial.available()) {
    char cmd = (char)Serial.read();// convierte el comando a char
    Serial.print(cmd);
      switch (cmd) {
        case 'W': // Avanza
          motor1.setSpeed(255); // Velocidad máxima
          motor1.run(FORWARD); // Avanza
          motor2.setSpeed(255); // Velocidad máxima
          motor2.run(FORWARD); // Avanza
          break;
        case 'S': // Retrocede
          motor1.setSpeed(255); // Velocidad máxima
          motor1.run(BACKWARD); // Retrocede
          motor2.setSpeed(255); // Velocidad máxima
          motor2.run(BACKWARD); // Retrocede
          break;
        case 'A': // Gira a la izquierda
          motor1.setSpeed(150); // Velocidad máxima
          motor1.run(FORWARD); // Avanza
          motor2.setSpeed(255); // Velocidad máxima
          motor2.run(FORWARD); // Avanza
          break;
        case 'D': // Gira a la derecha
          motor1.setSpeed(255); // Velocidad máxima
          motor1.run(FORWARD); // Avanza
          motor2.setSpeed(150); // Velocidad máxima
          motor2.run(FORWARD); // Avanza
          break;
        case 'P': // Detiene los motores
          motor1.run(RELEASE);
          motor2.run(RELEASE);
          break;
        case 'U': // Detiene los motores
          cont = cont - 40;
          if(cont < 99){
            cont = 99;
          }
          myServoUJ.write(cont);
          break;
        case 'J': // Detiene los motores
          contJ();
          if(cont > 179){
            cont = 179;
            myServoUJ.write(cont);
          }
          break;
        case '0': // Detiene los motores
          esc1.writeMicroseconds(2000);
          esc2.writeMicroseconds(2000);
          delay(500);
          myServoD.write(160);
          delay(1000);
          myServoD.write(0);
          delay(1000);
          esc1.writeMicroseconds(1000);
          esc2.writeMicroseconds(1000);
          break;
      }
    } 
}
