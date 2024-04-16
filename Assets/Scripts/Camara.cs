using UnityEngine;
using UnityEngine.UI;

public class Camara : MonoBehaviour
{
    // Mostrar video de webcam a la camara de unity
    RawImage rawImage;
    WebCamTexture webCamTexture;

    // Referencias a los objetos de imagen de la UI que forman el retículo
    public Image reticlePart1;
    public Image reticlePart2;

    void Start()
    {
        rawImage = GetComponent<RawImage>();
        webCamTexture = new WebCamTexture();

        // Ajustar el tamaño del RawImage para que coincida con el tamaño de la WebCamTexture
        rawImage.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);

        // Invertir la imagen (efecto espejo)
        rawImage.rectTransform.localScale = new Vector3(-1, 1, 1);

        rawImage.texture = webCamTexture;
        webCamTexture.Play();
        // Rotar reticlePart2 90 grados en el eje Z para crear una cruz
        reticlePart2.rectTransform.rotation = Quaternion.Euler(0, 0, 90);
    }

    void Update()
    {
        // Ajustar el tamaño y la posición del retículo en función del tamaño de la pantalla
        Vector2 reticleSize = new Vector2(Screen.width/5, Screen.height/5);

        reticlePart1.rectTransform.sizeDelta = reticleSize;

        reticlePart2.rectTransform.sizeDelta = reticleSize;
        // Aquí puedes mostrar u ocultar tu retículo según sea necesario
        // Por ejemplo, puedes mostrar el retículo solo cuando el botón derecho del ratón esté presionado:
        if (Input.GetMouseButton(1))
        {
            ShowReticle(true);
        }
        else
        {
            ShowReticle(false);
        }
    }

    void ShowReticle(bool show)
    {
        // Mostrar u ocultar cada parte del retículo
        reticlePart1.enabled = show;
        reticlePart2.enabled = show;
    }
}
