using UnityEngine;

public class LaserBeam : LaserBeamBase
{
    public float alturaOrigen = 0f;
    public float alturaDestino = 0f;

    public float encendidoTiempo = 2f; // Tiempo que el láser está encendido
    public float apagadoTiempo = 3f;  // Tiempo que el láser está apagado

    private float timer;
    private bool estaEncendido = true;

    private AudioSource audioSource; // Referencia al AudioSource
    public Light puntoLuz; // Referencia al Point Light

    protected override void Start()
    {
        base.Start(); // Llama al Start() de la clase base
        timer = encendidoTiempo;

        audioSource = GetComponent<AudioSource>(); // Obtener el AudioSource
        if (audioSource != null)
        {
            audioSource.loop = true; // Reproducir en bucle mientras esté encendido
            if (estaEncendido)
            {
                audioSource.Play(); // Iniciar reproducción si está encendido al inicio
            }
        }

        if (puntoLuz != null)
        {
            puntoLuz.enabled = estaEncendido; // Establecer el estado inicial del Point Light
        }
    }

    protected override void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            estaEncendido = !estaEncendido;
            timer = estaEncendido ? encendidoTiempo : apagadoTiempo;
            lineRenderer.enabled = estaEncendido;

            if (audioSource != null)
            {
                if (estaEncendido)
                {
                    audioSource.Play(); // Iniciar reproducción al encender
                }
                else
                {
                    audioSource.Stop(); // Detener reproducción al apagar
                }
            }

            if (puntoLuz != null)
            {
                puntoLuz.enabled = estaEncendido; // Activar/desactivar el Point Light
            }
        }

        if (estaEncendido)
        {
            ActualizarLaser();
        }
    }

    void ActualizarLaser()
    {
        Vector3 origenConAltura = puntoOrigen.position + Vector3.up * alturaOrigen;
        Vector3 destinoConAltura = puntoDestino.position + Vector3.up * alturaDestino;

        lineRenderer.SetPosition(0, origenConAltura);
        lineRenderer.SetPosition(1, destinoConAltura);

        luzPunto.transform.position = Vector3.Lerp(origenConAltura, destinoConAltura, 0.5f);

        RaycastHit hit;
        if (Physics.Raycast(origenConAltura, destinoConAltura - origenConAltura, out hit, Vector3.Distance(origenConAltura, destinoConAltura)))
        {
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("¡El jugador ha sido alcanzado!");
                GameManager.Instancia.RecibirDaño(daño);
            }
        }
    }
}