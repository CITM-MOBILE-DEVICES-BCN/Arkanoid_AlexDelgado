using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    private void Start()
    {
        // Obtener la referencia de la c�mara principal
        Camera mainCamera = Camera.main;

        // Si la c�mara es ortogr�fica, usamos orthographicSize para obtener el tama�o de la vista
        if (mainCamera.orthographic)
        {
            // El alto de la pantalla en unidades del mundo (2 * el tama�o ortogr�fico de la c�mara)
            float screenHeight = 2f * mainCamera.orthographicSize;

            // Ancho en funci�n de la relaci�n de aspecto
            float screenWidth = screenHeight * mainCamera.aspect;

            // Convierte el tama�o en unidades de mundo a p�xeles usando la resoluci�n deseada (por ejemplo, 100 p�xeles por unidad)
            int pixelHeight = Mathf.RoundToInt(screenHeight * 100f); // 100 p�xeles por unidad
            int pixelWidth = Mathf.RoundToInt(screenWidth * 100f);

            // Ajustar la resoluci�n de la ventana
            Screen.SetResolution(pixelWidth, pixelHeight, false); // 'false' para que no sea en pantalla completa
        }
    }
}
