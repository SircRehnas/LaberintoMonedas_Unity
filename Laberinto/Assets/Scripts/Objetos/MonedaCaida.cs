using UnityEngine;

public class MonedaCaida : MonoBehaviour
{
    public float alturaFinal;
    private float velocidadCaida = 50f; // Ajusta la velocidad de caída

    void Update()
    {
        if (transform.position.y > alturaFinal)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, alturaFinal, transform.position.z), velocidadCaida * Time.deltaTime);
        }
    }
}