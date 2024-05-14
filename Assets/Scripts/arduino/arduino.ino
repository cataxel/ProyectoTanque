
// ping del motor derecho
int motorRightPing1 = 11;
int motorRightPing2 = 10;
int ping = 22;
// ping del motor izquierdo
int motorLeftPing = 3;



void setup() {
  // Inicia la comunicación serie a una velocidad de 9600 baudios
  Serial.begin(9600);
  pinMode(motorRightPing1, OUTPUT);
  pinMode(motorRightPing2, OUTPUT);
  pinMode(ping,OUTPUT);
}

void loop() {
  // Comprueba si hay datos disponibles para leer
  if (Serial.available() > 0) {
    // Lee la cadena de texto desde el puerto serie
    String mouseMovement = Serial.readStringUntil('\n');

    // Separa la cadena de texto en los valores de X e Y
    int commaIndex = mouseMovement.indexOf(',');
    String mouseX = mouseMovement.substring(0, commaIndex);
    String mouseY = mouseMovement.substring(commaIndex + 1);

    // Convierte los valores de X e Y a números
    float x = mouseX.toFloat();
    float y = mouseY.toFloat();

    // Usa los valores de X e Y para controlar los motores

    // si el serial es 'W' avanza hacia adelante
    if (mouseMovement == "W") {
      //activar el motor
      digitalWrite(motorRightPing1, HIGH);
      digitalWrite(motorRightPing2, HIGH);
    }
  }
}
