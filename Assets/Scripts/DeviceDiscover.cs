using System.Collections;
using UnityEngine;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using UnityEngine.Networking;

/// <summary>
/// Clase DeviceDiscover para descubrir y manejar la conexión con el módulo WiFi ESP8266.
/// </summary>
public class DeviceDiscover : MonoBehaviour
{
    // Instancia de la clase conectionArduino para manejar la conexión con el módulo WiFi ESP8266
    private conectionArduino conectionArduino;
    // Ruta para verificar la presencia del módulo ESP8266
    public string esp8266Path = "/check-esp8266";
    // Dirección IP del módulo ESP8266
    private string ipAdress = "192.168.1.148";

    /// <summary>
    /// Método Start que se llama antes de la primera actualización del frame.
    /// </summary>
    void Start()
    {
        string ipAddress = GetLocalIPAddress();
        Debug.Log("Direccion IP local: "+ipAddress);
        StartCoroutine(CheckDevice(ipAdress));
    }

    /// <summary>
    /// Método GetLocalIPAddress para obtener la dirección IP local del dispositivo.
    /// </summary>
    /// <returns>La dirección IP local del dispositivo.</returns>
    string GetLocalIPAddress()
    {
        string ipAddress = "";

        NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
        foreach (NetworkInterface intf in interfaces)
        {
            if (intf.OperationalStatus == OperationalStatus.Up &&
                 intf.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
            {
                IPInterfaceProperties ipProps = intf.GetIPProperties();
                foreach (UnicastIPAddressInformation addr in ipProps.UnicastAddresses)
                {
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

    /// <summary>
    /// Método CheckDevice para verificar la presencia del módulo ESP8266 en la dirección IP especificada.
    /// </summary>
    /// <param name="ipAddress">La dirección IP donde se verificará la presencia del módulo ESP8266.</param>
    IEnumerator CheckDevice(string ipAddress)
    {
        Debug.Log(ipAddress);
        string url = "http://" + ipAddress + esp8266Path;

        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            www.certificateHandler = new CustomCertificateHandler();
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Encontrado ESP8266 en: " + ipAddress);
                conectionArduino = new conectionArduino();
                conectionArduino.Connect(ipAddress, 80);
                Debug.Log("Conectado a " + ipAddress);
            }
            else
            {
                Debug.Log("No se pudo conectar a " + ipAddress + ": " + www.error);
                Start();
            }
        }
    }

    /// <summary>
    /// Método OnApplicationQuit para manejar el cierre de la aplicación.
    /// </summary>
    void OnApplicationQuit()
    {
        conectionArduino.CloseConnection();
    }

    /// <summary>
    /// Método SendData para enviar datos al módulo WiFi ESP8266.
    /// </summary>
    /// <param name="s">Los datos que se enviarán al módulo WiFi ESP8266.</param>
    public void SendData(string s)
    {
        if (conectionArduino != null)
        {
            conectionArduino.SendData(s);
            Debug.Log(s);
        }
    }
}