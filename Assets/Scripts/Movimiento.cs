using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    public DeviceDiscover deviceDiscover;

    // Update is called once per frame
    void Update()
    {
        // mover hacia la izquierda
        if (Input.GetKeyDown(KeyCode.A))
        {
            // mandar el comando al modulo wifi
            deviceDiscover.SendData("A");            
        }   
        // mover hacia la derecha
        if (Input.GetKeyDown(KeyCode.D))
        {
            // mandar el comando al modulo wifi
            deviceDiscover.SendData("D");            
        }
        // mover hacia arriba
        if (Input.GetKeyDown(KeyCode.W))
        {
            // mandar el comando al modulo wifi
            deviceDiscover.SendData("W");            
        }
        // mover hacia abajo
        if (Input.GetKeyDown(KeyCode.S))
        {
            // mandar el comando al modulo wifi
            deviceDiscover.SendData("S");            
        }
        // Obtén el movimiento del mouse
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Convierte los valores a una cadena de texto
        string mouseMovement = mouseX.ToString() + "," + mouseY.ToString();

        // Envía la cadena de texto al Arduino
        //deviceDiscover.SendData(mouseMovement);
    }
}
