/*
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    public float tiempoDeVida = 3f;
    public Vector3 velocidadRotacion = new Vector3(100f, 100f, 100f); // Velocidad de rotación en grados por segundo
    public float dañoProyectil;
    private AudioSource sonido;
    void Start()
    {
        Destroy(gameObject, tiempoDeVida);
        sonido = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Rotar el proyectil
        transform.Rotate(velocidadRotacion * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("¡El jugador ha sido alcanzado por un proyectil!");
            sonido.Play();
            GameManager.currentLifePoints -= dañoProyectil;
            Destroy(gameObject, 0.8f);
            // Aquí puedes agregar la lógica para aplicar daño al jugador
            // Por ejemplo:
            // collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(daño);
        }
        else
        {
            // Si colisiona con cualquier otro objeto, destruye el proyectil
            Destroy(gameObject, 0.8f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("¡El jugador ha sido alcanzado por un proyectil!");
            sonido.Play();
            GameManager.currentLifePoints -= dañoProyectil;
            Destroy(gameObject, 0.8f);
            // Aquí puedes agregar la lógica para aplicar daño al jugador
            // Por ejemplo:
            // collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(daño);
        }
        else
        {
            // Si colisiona con cualquier otro objeto, destruye el proyectil
            Destroy(gameObject, 0.8f);
        }
    }
}
*/

using UnityEngine;

public class Proyectil : MonoBehaviour
{
    public float tiempoDeVida = 3f;
    public Vector3 velocidadRotacion = new Vector3(100f, 100f, 100f);
    public float dañoProyectil;
    private AudioSource sonido;

    void Start()
    {
        Destroy(gameObject, tiempoDeVida);
        sonido = GetComponent<AudioSource>();
    }

    void Update()
    {
        transform.Rotate(velocidadRotacion * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("¡El jugador ha sido alcanzado por un proyectil!");
            sonido.Play();
            GameManager.Instancia.RecibirDaño(dañoProyectil);
            Destroy(gameObject, 0.8f);
        }
        else
        {
            Destroy(gameObject, 0.8f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("¡El jugador ha sido alcanzado por un proyectil!");
            sonido.Play();
            GameManager.Instancia.RecibirDaño(dañoProyectil);
            Destroy(gameObject, 0.8f);
        }
        else
        {
            Destroy(gameObject, 0.8f);
        }
    }
}