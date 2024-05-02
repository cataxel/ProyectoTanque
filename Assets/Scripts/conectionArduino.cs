using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Text;

public class conectionArduino
{
    // conectar al modulowifi ESP8266
    private TcpClient client;
    private NetworkStream stream;

    public void Connect(string host, int port)
    {
        client = new TcpClient(host, port);
        stream = client.GetStream();
    }

    public void SendData(string data)
    {
        if (client != null)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(data);
            stream.Write(bytes, 0, bytes.Length);
        }
    }

    public void CloseConnection()
    {
        stream.Close();
        client.Close();
    }
}
