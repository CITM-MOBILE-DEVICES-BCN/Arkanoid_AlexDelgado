using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    private void Start()
    {
        // Obtener la referencia de la cámara principal
        Camera mainCamera = Camera.main;

        // Si la cámara es ortográfica, usamos orthographicSize para obtener el tamaño de la vista
        if (mainCamera.orthographic)
        {
            // El alto de la pantalla en unidades del mundo (2 * el tamaño ortográfico de la cámara)
            float screenHeight = 2f * mainCamera.orthographicSize;

            // Ancho en función de la relación de aspecto
            float screenWidth = screenHeight * mainCamera.aspect;

            // Convierte el tamaño en unidades de mundo a píxeles usando la resolución deseada (por ejemplo, 100 píxeles por unidad)
            int pixelHeight = Mathf.RoundToInt(screenHeight * 100f); // 100 píxeles por unidad
            int pixelWidth = Mathf.RoundToInt(screenWidth * 100f);

            // Ajustar la resolución de la ventana
            Screen.SetResolution(pixelWidth, pixelHeight, false); // 'false' para que no sea en pantalla completa
        }
    }
}
