#include <AFMotor.h>

// Inicializa el motor en el puerto M1 de la placa L293D Motor Shield
AF_DCMotor motor(1);

void setup() {
  // Inicia la comunicación serie en el puerto Serial1 a una velocidad de 9600 baudios
  Serial1.begin(9600);
  Serial.begin(9600);
}

void loop() {
  // Comprueba si hay datos disponibles para leer en el puerto Serial1
  if (Serial1.available() > 0) {
    // Lee el dato del puerto serie
    char command = Serial1.read();
    Serial.println(command);

    // Comprueba el comando recibido y controla el motor en consecuencia
    switch (command) {
      case 'W': // Avanza
        motor.setSpeed(255); // Velocidad máxima
        motor.run(FORWARD); // Avanza
        break;
      case 'S': // Retrocede
        motor.setSpeed(255); // Velocidad máxima
        motor.run(BACKWARD); // Retrocede
        break;
    }
  }
}
