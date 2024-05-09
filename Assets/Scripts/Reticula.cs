using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Clase Reticula para manejar la visualización de una retícula en la interfaz de usuario.
/// </summary>
public class Reticula : MonoBehaviour
{
    // Imágenes que representan las líneas horizontales de la retícula
	public Image h0;
	public Image h1;
	public Image h2;
    // Imágenes que representan las líneas verticales de la retícula
	public Image v0;
	public Image v1;
	public Image v2;
	public Image v3;
	public Image v4;
	public Image v5;
    // Bandera para controlar la visualización de la retícula
	private bool ban = false;

    /// <summary>
    /// Método Start que se llama antes de la primera actualización del frame.
    /// </summary>
	void Start()
    {
		ShowReticle(ban);
	}

    /// <summary>
    /// Método Update que se llama una vez por frame.
    /// </summary>
    void Update()
    {
        // Si se presiona el botón derecho del ratón
		if (Input.GetMouseButtonDown(1))
		{
            // Cambia el estado de la bandera
			if (ban == false)
			{
				ban = true;
			}
			else
			{
				ban = false;
			}
            // Actualiza la visualización de la retícula
			ShowReticle(ban);
		}
    }

    /// <summary>
    /// Método ShowReticle para controlar la visualización de la retícula.
    /// </summary>
    /// <param name="show">Si es verdadero, la retícula se muestra. Si es falso, la retícula se oculta.</param>
    void ShowReticle(bool show)
    {
        h0.enabled = show;
        h1.enabled = show;
		h2.enabled = show;
		v0.enabled = show;
		v1.enabled = show;
		v2.enabled = show;
		v3.enabled = show;
		v4.enabled = show;
		v5.enabled = show;
	}
}
