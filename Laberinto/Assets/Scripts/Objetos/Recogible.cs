/*New*/
using UnityEngine;

public abstract class Recogible : MonoBehaviour
{
    public int valor;
    public float tiempoDestruccion = 0.8f;
    private AudioSource sonidoRecogida;

    private void Start()
    {
        sonidoRecogida = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            sonidoRecogida.Play();
            transform.localScale = Vector3.zero;
            GetComponent<Collider>().enabled = false;
            Recoger();
            Destroy(gameObject, tiempoDestruccion);
        }
    }

    protected abstract void Recoger();
}