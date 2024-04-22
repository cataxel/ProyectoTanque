using UnityEngine;
using UnityEngine.UI;

public class Camara : MonoBehaviour
{
	RawImage rawImage;
	WebCamTexture webCamTexture;

	// Variable para controlar si el zoom está activado o desactivado
	bool isZoomed = false;

	// Tamaño de zoom
	Vector2 zoomedSize;

	// Textura para la máscara circular
	Texture2D circleMaskTexture;

	void Start()
	{
		rawImage = GetComponent<RawImage>();
		webCamTexture = new WebCamTexture();

		rawImage.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
		rawImage.rectTransform.localScale = new Vector3(-1, 1, 1);

		rawImage.texture = webCamTexture;
		webCamTexture.Play();

		// Inicializa el tamaño de zoom
		zoomedSize = new Vector2(Screen.width * 1.5f, Screen.height * 1.5f);

		// Crea una textura circular para recortar la imagen
		circleMaskTexture = CreateCircleMaskTexture(Screen.width, Screen.height);
	}

	void Update()
	{
		// Verificar si se ha hecho clic con el botón derecho del mouse
		if (Input.GetMouseButtonDown(1))
		{
			ToggleZoom();
		}
	}

	void ToggleZoom()
	{
		// Cambiar el estado del zoom
		isZoomed = !isZoomed;

		// Aplicar zoom si está activado
		if (isZoomed)
		{
			// Modificar el tamaño del RawImage para simular el zoom
			rawImage.rectTransform.sizeDelta = zoomedSize;
		}
		else
		{
			// Restaurar el tamaño original
			rawImage.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
		}
	}

	// Función para crear una textura circular para recortar la imagen
	Texture2D CreateCircleMaskTexture(int width, int height)
	{
		Texture2D texture = new Texture2D(width, height);
		Color[] colors = new Color[width * height];

		float centerX = width / 2f;
		float centerY = height / 2f;
		float radius = Mathf.Min(centerX, centerY);

		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
				float distance = Mathf.Sqrt((x - centerX) * (x - centerX) + (y - centerY) * (y - centerY));
				if (distance <= radius)
				{
					colors[y * width + x] = Color.clear; // Hace transparente el círculo interior
				}
				else
				{
					colors[y * width + x] = Color.black; // Hace negro el área exterior
				}
			}
		}

		texture.SetPixels(colors);
		texture.Apply();

		return texture;
	}

	void OnGUI()
	{
		// Dibuja la máscara circular solo cuando el zoom está activado
		if (isZoomed)
		{
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), circleMaskTexture, ScaleMode.StretchToFill);
		}
	}
}
