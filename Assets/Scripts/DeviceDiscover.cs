using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.Networking;

public class DeviceDiscover : MonoBehaviour
{
    private conectionArduino conectionArduino;
    public string esp8266Path = "/check-esp8266";
    private string ipAdress = "192.168.101.110";
    // Start is called before the first frame update
    void Start()
    {
        string ipAddress = GetLocalIPAddress();
        Debug.Log("Direccion IP local: "+ipAddress);
        //StartCoroutine(ScanForESP8266(ipAddress));
        StartCoroutine(CheckDevice(ipAdress));
    }

    string GetLocalIPAddress()
    {
        string ipAddress = "";

        // Obtener todas las interfaces de red disponibles
        NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
        foreach (NetworkInterface intf in interfaces)
        {
            // Filtrar las interfaces que estén conectadas y sean de tipo Ethernet o Wi-Fi
            if (intf.OperationalStatus == OperationalStatus.Up &&
                 intf.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
            {
                // Obtener las direcciones IP asociadas con esta interfaz
                IPInterfaceProperties ipProps = intf.GetIPProperties();
                foreach (UnicastIPAddressInformation addr in ipProps.UnicastAddresses)
                {
                    // Filtrar las direcciones IPv4 y obtener la dirección IP
                    if (addr.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        ipAddress = addr.Address.ToString();
                        break;
                    }
                }
            }
        }

        return ipAddress;
    }

    IEnumerator ScanForESP8266(string baseIpAddress)
    {
        int timeout = 100;

        if (string.IsNullOrEmpty(baseIpAddress))
        {
            Debug.LogError("No se pudo obtener la dirección IP local.");
            yield break; // Salir de la función si no se puede obtener la dirección IP local
        }

        // Obtener los componentes de la dirección IP base
        string[] ipParts = baseIpAddress.Split('.');
        if (ipParts.Length != 4)
        {
            Debug.LogError("La dirección IP base no es válida.");
            yield break; // Salir de la función si la dirección IP base no es válida
        }

        // Obtener la parte de la red (los primeros tres octetos)
        string networkPart = string.Join(".", ipParts.Take(3));

        new Thread(() =>
        {
            Parallel.For(1, 255, i =>
            {
                Debug.Log("Escaneando dirección IP: " + networkPart + "." + i + "...");
                string ipAddress = $"{networkPart}.{i}";

                System.Net.NetworkInformation.Ping pingSender = new System.Net.NetworkInformation.Ping();
                pingSender.PingCompleted += (sender, e) => PingCompletedCallback(sender, e, ipAddress);
                pingSender.SendAsync(ipAddress, timeout, null);
            });
        }).Start();
    }
     private void PingCompletedCallback(object sender, PingCompletedEventArgs e, string ipAddress)
    {
        if (e.Reply.Status == IPStatus.Success)
        {
            StartCoroutine(CheckDevice(ipAddress));
        }
    }

    IEnumerator CheckDevice(string ipAddress)
    {
        string url = "http://" + ipAddress + esp8266Path;

        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            www.certificateHandler = new CustomCertificateHandler();
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                // El dispositivo en la dirección IP especificada es un módulo ESP8266
                Debug.Log("Encontrado ESP8266 en: " + ipAddress);
                // Puedes realizar cualquier acción adicional que necesites aquí, como conectarte al dispositivo
                conectionArduino = new conectionArduino();

                // Conéctate al módulo WiFi ESP8266
                conectionArduino.Connect(ipAddress, 80);
                // mensaje de coneccion exitosa
                Debug.Log("Conectado a " + ipAddress);
            }
            else
            {
                // No se pudo conectar al dispositivo en la dirección IP especificada
                Debug.Log("No se pudo conectar a " + ipAddress + ": " + www.error);
            }
        }
    }
    void OnApplicationQuit()
    {
        // Cierra la conexión cuando la aplicación se cierra
        conectionArduino.CloseConnection();
    }

    public void SendData(string s)
    {
        if (conectionArduino != null)
        {
            conectionArduino.SendData(s);
            Debug.Log(s);
        }
    }
}