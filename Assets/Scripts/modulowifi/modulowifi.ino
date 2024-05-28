#include <WiFi.h>
#include <HardwareSerial.h>

// Nombre de la red WiFi a la que se conectará el ESP32
const char* ssid = "ALUMNOSEDX";
// Contraseña de la red WiFi a la que se conectará el ESP32
const char* password = "TNMjiqA2024:>!";

// Crea un servidor WiFi que se ejecutará en el puerto 80
WiFiServer server(80);

void setup() {
  // Inicia la comunicación serie a una velocidad de 9600 baudios
  Serial.begin(9600);

  // Conectarse a la red WiFi con el SSID y la contraseña especificados
  WiFi.begin(ssid, password);
  // Espera hasta que la conexión WiFi esté establecida
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }

  // Imprime un mensaje indicando que la conexión WiFi se ha establecido correctamente
  Serial.println("WiFi connected");
  // Imprime la dirección IP asignada al ESP32 en la red WiFi
  Serial.println("IP address: ");
  Serial.println(WiFi.localIP());

  // Inicia el servidor WiFi
  server.begin();
}

void loop() {
  // Escucha a los clientes entrantes
  WiFiClient client = server.available();

  // Si un nuevo cliente se conecta,
  if (client) {
    // Imprime un mensaje en el puerto serie
    Serial.println("New Client.");
    // Haz una cadena para guardar los datos del cliente entrante
    String currentLine = "";
    // Mientras el cliente esté conectado,
    while (client.connected()) {
      // Si hay bytes para leer desde el cliente,
      if (client.available()) {
        // Lee un byte
        char c = client.read();
        // Imprime el byte leído en el puerto serie
        Serial.write(c);
        Serial.println("\n");

        // Envía el byte leído al Arduino
        //Serial.write(c);

        // Si el byte es un carácter de nueva línea,
        if (c == '\n') {
          // Si la línea actual está vacía, esto significa que el cliente HTTP
          // ha enviado una solicitud final. Si un cliente HTTP envía una solicitud,
          // una línea en blanco indica el final de la solicitud del cliente,
          // así que puedes enviar una respuesta:
          if (currentLine.length() == 0) {
            // Envía una respuesta estándar HTTP
            client.println("HTTP/1.1 200 OK");
            client.println("Content-type:text/html");
            client.println("Connection: close");
            client.println();

            // Aquí es donde puedes agregar contenido a la respuesta HTTP, como un sitio web HTML.
            client.println("<!DOCTYPE html><html><body><h1>Hola, mundo!</h1></body></html>");

            // La respuesta HTTP termina con una línea en blanco
            client.println();
            break;
          } else {    // Si obtienes una nueva línea, entonces borra la línea actual
            currentLine = "";
          }
        } else if (c != '\r') {  // Si obtienes cualquier otro carácter,
          // Añádelo al final de la línea actual
          currentLine += c;
        }
      }
    }
    // Cierra la conexión con el cliente
    client.stop();
    // Imprime un mensaje indicando que el cliente se ha desconectado
    Serial.println("Client Disconnected.");
  }
}
