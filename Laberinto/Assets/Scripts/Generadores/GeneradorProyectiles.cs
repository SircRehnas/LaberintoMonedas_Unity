/*
using UnityEngine;

public class GeneradorProyectiles : MonoBehaviour
{
    public GameObject proyectilPrefab;
    public float velocidadProyectil = 5f;
    public float tiempoEntreProyectiles = 1f;
    private float tiempoSiguienteProyectil = 0f;

    // Variables para habilitar/deshabilitar direcciones
    public bool generarDerecha = true;
    public bool generarIzquierda = true;
    public bool generarFrente = true;
    public bool generarAtras = true;

    void Update()
    {
        if (Time.time >= tiempoSiguienteProyectil)
        {
            GenerarProyectilesDesdeLaterales();
            tiempoSiguienteProyectil = Time.time + tiempoEntreProyectiles;
        }
    }

    void GenerarProyectilesDesdeLaterales()
    {
        // Generar proyectiles desde los laterales del cubo
        Vector3[] direcciones = {
            generarDerecha ? transform.right : Vector3.zero,
            generarIzquierda ? -transform.right : Vector3.zero,
            generarFrente ? transform.forward : Vector3.zero,
            generarAtras ? -transform.forward : Vector3.zero
        };

        foreach (Vector3 direccion in direcciones)
        {
            if (direccion != Vector3.zero)
            {
                Vector3 puntoDeGeneracion = transform.position + direccion * transform.localScale.x / 2f;
                GameObject proyectil = Instantiate(proyectilPrefab, puntoDeGeneracion, transform.rotation);
                Rigidbody rb = proyectil.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.linearVelocity = direccion * velocidadProyectil;
                }
                else
                {
                    Debug.LogError("El proyectil no tiene un componente Rigidbody.");
                }
            }
        }
    }
}
*/
using UnityEngine;

public class GeneradorProyectiles : MonoBehaviour
{
    public GameObject proyectilPrefab;
    public float velocidadProyectil = 5f;
    public float tiempoEntreProyectiles = 1f;

    public bool generarDerecha = true;
    public bool generarIzquierda = true;
    public bool generarFrente = true;
    public bool generarAtras = true;

    private float tiempoSiguienteProyectil = 0f;

    void Update()
    {
        if (Time.time >= tiempoSiguienteProyectil)
        {
            GenerarProyectilesDesdeLaterales();
            tiempoSiguienteProyectil = Time.time + tiempoEntreProyectiles;
        }
    }

    void GenerarProyectilesDesdeLaterales()
    {
        Vector3[] direcciones = {
            generarDerecha ? transform.right : Vector3.zero,
            generarIzquierda ? -transform.right : Vector3.zero,
            generarFrente ? transform.forward : Vector3.zero,
            generarAtras ? -transform.forward : Vector3.zero
        };

        foreach (Vector3 direccion in direcciones)
        {
            if (direccion != Vector3.zero)
            {
                Vector3 puntoDeGeneracion = transform.position + direccion * transform.localScale.x / 2f;
                GameObject proyectil = Instantiate(proyectilPrefab, puntoDeGeneracion, transform.rotation);
                Rigidbody rb = proyectil.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    rb.linearVelocity = direccion * velocidadProyectil;
                }
                else
                {
                    Debug.LogError("El proyectil no tiene un componente Rigidbody.");
                }
            }
        }
    }
}