using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    public DeviceDiscover deviceDiscover;
    private Vector3 lastMousePosition;

    // Update is called once per frame
    void Update()
    {
        // Si se presiona la tecla ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Si el cursor está actualmente oculto
            if (!Cursor.visible)
            {
                // Muestra el cursor
                Cursor.visible = true;
                // Desbloquea el cursor para que pueda moverse libremente
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                // Oculta el cursor
                Cursor.visible = false;
                // Bloquea el cursor en el centro de la ventana del juego
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // mandar el comando al modulo wifi
            deviceDiscover.SendData("Q");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            // mandar el comando al modulo wifi
            deviceDiscover.SendData("E");
        }



        // mover hacia abajo
        if (Input.GetKeyDown(KeyCode.S))
        {
            // mandar el comando al modulo wifi
            deviceDiscover.SendData("S");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // mandar el comando al modulo wifi
            deviceDiscover.SendData("P");
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // mandar el comando al modulo wifi
            deviceDiscover.SendData("U");
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // mandar el comando al modulo wifi
            deviceDiscover.SendData("J");
        }
        if (Input.GetMouseButtonDown(0))
        {
            // mandar el comando al modulo wifi
            deviceDiscover.SendData("0");
        }

        // Obtén la posición actual del mouse
        Vector3 mousePosition = Input.mousePosition;

        // Si la posición del mouse ha cambiado
        if (mousePosition != lastMousePosition)
        {
            // Mapea la posición del mouse a un rango adecuado para los servomotores
            //int servoX = (int)Mathf.Lerp(0, 180, mousePosition.x / Screen.width);
            int servoY = (int)Mathf.Lerp(0, 180, mousePosition.y / Screen.height);

            // Convierte los valores a una cadena de texto
            string mouseMovement = servoY.ToString();

            // Envía la cadena de texto al Arduino
            //deviceDiscover.SendData(mouseMovement+ "\n");

            // Guarda la posición actual del mouse
            lastMousePosition = mousePosition;
        }
    }
}
