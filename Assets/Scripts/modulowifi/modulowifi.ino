#include <ESP8266WiFi.h>

const char* ssid = "ALUMNOSEDX";
const char* password = "TNMjiqA2024:>!";

WiFiServer server(80);  // Servidor se ejecutará en el puerto 80

void setup(){
  Serial.begin(9600);
  Serial.println("nigga");
  WiFi.begin(ssid,password);
  while(WiFi.status() != WL_CONNECTED){
    delay(500);
    Serial.print(".");
  }
  Serial.println("WiFi connected");
  Serial.println("IP address: ");
  Serial.println(WiFi.localIP());

  server.begin();  // Inicia el servidor
}

void loop(){
  WiFiClient client = server.available();  // Escucha a los clientes entrantes

  if (client) {  // Si un nuevo cliente se conecta,
    Serial.println("New Client.");  // Imprime un mensaje en el puerto serie
    String currentLine = "";        // Haz una cadena para guardar los datos del cliente entrante
    while (client.connected()) {    // Mientras el cliente esté conectado,
      if (client.available()) {     // Si hay bytes para leer desde el cliente,
        char c = client.read();     // Lee un byte, luego
        Serial.write(c+'\n');            // Imprime ese byte en el puerto serie
        if (c == '\n') {            // Si el byte es un carácter de nueva línea,

          // Si la línea actual está vacía, esto significa que el cliente http
          // ha enviado una solicitud final. Si un cliente http envía una solicitud,
          // una línea en blanco indica el final de la solicitud del cliente,
          // así que puedes enviar una respuesta:
          if (currentLine.length() == 0) {
            // Envía una respuesta estándar http
            client.println("HTTP/1.1 200 OK");
            client.println("Content-type:text/html");
            client.println("Connection: close");
            client.println();

            // Aquí es donde puedes agregar contenido a la respuesta http, como un sitio web HTML.
            client.println("<!DOCTYPE html><html><body><h1>Hola, mundo!</h1></body></html>");

            // La respuesta http termina con una línea en blanco
            client.println();
            break;
          } else {    // Si obtienes una nueva línea, entonces borra la línea actual
            currentLine = "";
          }
        } else if (c != '\r') {  // Si obtienes cualquier otro carácter,
          currentLine += c;      // Añádelo al final de la línea actual
        }
      }
    }
    // Cierra la conexión
    client.stop();
    Serial.println("Client Disconnected.");
  }
}
