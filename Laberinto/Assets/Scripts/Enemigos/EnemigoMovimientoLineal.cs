/*
using UnityEngine;

public class EnemigoMovimientoLineal : MonoBehaviour
{
    public float velocidad = 5f;
    public enum EjeDeMovimiento { X, Y, Z };
    public EjeDeMovimiento ejeDeMovimiento = EjeDeMovimiento.X;
    public float limiteMinimo = -5f;
    public float limiteMaximo = 5f;
    public float daño = 1f;

    private int direccion = 1;

    private AudioSource sonido;

    void Start()
    {
        sonido = GetComponent<AudioSource>();
    }
    void Update()
    {
        float nuevaPosicion = 0f;
        Vector3 posicionActual = transform.position;

        switch (ejeDeMovimiento)
        {
            case EjeDeMovimiento.X:
                nuevaPosicion = posicionActual.x + direccion * velocidad * Time.deltaTime;
                if (nuevaPosicion > limiteMaximo) { direccion = -1; nuevaPosicion = limiteMaximo; }
                else if (nuevaPosicion < limiteMinimo) { direccion = 1; nuevaPosicion = limiteMinimo; }
                transform.position = new Vector3(nuevaPosicion, posicionActual.y, posicionActual.z);
                break;
            case EjeDeMovimiento.Y:
                nuevaPosicion = posicionActual.y + direccion * velocidad * Time.deltaTime;
                if (nuevaPosicion > limiteMaximo) { direccion = -1; nuevaPosicion = limiteMaximo; }
                else if (nuevaPosicion < limiteMinimo) { direccion = 1; nuevaPosicion = limiteMinimo; }
                transform.position = new Vector3(posicionActual.x, nuevaPosicion, posicionActual.z);
                break;
            case EjeDeMovimiento.Z:
                nuevaPosicion = posicionActual.z + direccion * velocidad * Time.deltaTime;
                if (nuevaPosicion > limiteMaximo) { direccion = -1; nuevaPosicion = limiteMaximo; }
                else if (nuevaPosicion < limiteMinimo) { direccion = 1; nuevaPosicion = limiteMinimo; }
                transform.position = new Vector3(posicionActual.x, posicionActual.y, nuevaPosicion);
                break;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            sonido.Play();
            GameManager.currentLifePoints -= daño;
            Debug.Log("¡Jugador golpeado! Vida restante: " + GameManager.currentLifePoints);
        }
    }
}
*/
using UnityEngine;

public class EnemigoMovimientoLineal : MonoBehaviour
{
    public float velocidad = 5f;
    public enum EjeDeMovimiento { X, Y, Z };
    public EjeDeMovimiento ejeDeMovimiento = EjeDeMovimiento.X;
    public float limiteMinimo = -5f;
    public float limiteMaximo = 5f;
    public float daño = 1f;

    private int direccion = 1;
    private AudioSource sonido;

    void Start()
    {
        sonido = GetComponent<AudioSource>();
    }

    void Update()
    {
        float nuevaPosicion = 0f;
        Vector3 posicionActual = transform.position;

        switch (ejeDeMovimiento)
        {
            case EjeDeMovimiento.X:
                nuevaPosicion = posicionActual.x + direccion * velocidad * Time.deltaTime;
                if (nuevaPosicion > limiteMaximo) { direccion = -1; nuevaPosicion = limiteMaximo; }
                else if (nuevaPosicion < limiteMinimo) { direccion = 1; nuevaPosicion = limiteMinimo; }
                transform.position = new Vector3(nuevaPosicion, posicionActual.y, posicionActual.z);
                break;
            case EjeDeMovimiento.Y:
                nuevaPosicion = posicionActual.y + direccion * velocidad * Time.deltaTime;
                if (nuevaPosicion > limiteMaximo) { direccion = -1; nuevaPosicion = limiteMaximo; }
                else if (nuevaPosicion < limiteMinimo) { direccion = 1; nuevaPosicion = limiteMinimo; }
                transform.position = new Vector3(posicionActual.x, nuevaPosicion, posicionActual.z);
                break;
            case EjeDeMovimiento.Z:
                nuevaPosicion = posicionActual.z + direccion * velocidad * Time.deltaTime;
                if (nuevaPosicion > limiteMaximo) { direccion = -1; nuevaPosicion = limiteMaximo; }
                else if (nuevaPosicion < limiteMinimo) { direccion = 1; nuevaPosicion = limiteMinimo; }
                transform.position = new Vector3(posicionActual.x, posicionActual.y, nuevaPosicion);
                break;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            sonido.Play();
            GameManager.Instancia.RecibirDaño(daño);
            Debug.Log("¡Jugador golpeado! Vida restante: " + GameManager.Instancia.vidaActual);
        }
    }
}