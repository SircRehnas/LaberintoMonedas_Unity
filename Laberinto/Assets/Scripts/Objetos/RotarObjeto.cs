using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotarObjeto : MonoBehaviour
{
    public Vector3 ejeRotacion; // Eje de rotación elegido (e.g., Vector3.right, Vector3.up, Vector3.forward)
    public float velocidadRotacion = 50.0f; // Velocidad de rotación en grados por segundo

    void Update()
    {
        // Rotar el objeto alrededor del eje elegido
        transform.Rotate(ejeRotacion * velocidadRotacion * Time.deltaTime);
    }
}