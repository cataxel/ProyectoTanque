using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Text;

/// <summary>
/// Clase conectionArduino para manejar la conexión con el módulo WiFi ESP8266.
/// </summary>
public class conectionArduino
{
    // Cliente TCP para la conexión con el módulo WiFi ESP8266
    private TcpClient client;
    // Flujo de red para enviar y recibir datos
    private NetworkStream stream;

    /// <summary>
    /// Método Connect para establecer la conexión con el módulo WiFi ESP8266.
    /// </summary>
    /// <param name="host">El nombre del host al que se conectará el cliente TCP.</param>
    /// <param name="port">El número de puerto en el host al que se conectará el cliente TCP.</param>
    public void Connect(string host, int port)
    {
        client = new TcpClient(host, port);
        stream = client.GetStream();
    }

    /// <summary>
    /// Método SendData para enviar datos al módulo WiFi ESP8266.
    /// </summary>
    /// <param name="data">Los datos que se enviarán al módulo WiFi ESP8266.</param>
    public void SendData(string data)
    {
        if (client != null)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(data);
            stream.Write(bytes, 0, bytes.Length);
        }
    }

    /// <summary>
    /// Método CloseConnection para cerrar la conexión con el módulo WiFi ESP8266.
    /// </summary>
    public void CloseConnection()
    {
        stream.Close();
        client.Close();
    }
}
