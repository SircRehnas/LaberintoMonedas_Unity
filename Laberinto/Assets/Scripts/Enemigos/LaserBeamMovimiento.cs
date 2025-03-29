/*
using UnityEngine;

public class LaserBeamMovimiento : MonoBehaviour
{
    public Transform puntoOrigen;
    public Transform puntoDestino;
    public LineRenderer lineRenderer;
    public float daño = 10f;
    public float velocidadMovimiento = 2f;
    public float amplitudMovimiento = 1f;
    public float intensidadLuz = 2f;
    public Color colorLuz = Color.red;
    public float rangoLuz = 5f;

    private Light luzPunto;
    private float tiempo = 0f;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;

        luzPunto = new GameObject("LuzLaser").AddComponent<Light>();
        luzPunto.type = LightType.Point;
        luzPunto.intensity = intensidadLuz;
        luzPunto.color = colorLuz;
        luzPunto.range = rangoLuz;
        luzPunto.shadows = LightShadows.Hard;
    }

    void Update()
    {
        tiempo += Time.deltaTime * velocidadMovimiento;

        // Movimiento vertical sinusoidal para ambos puntos
        float desplazamiento = Mathf.Sin(tiempo) * amplitudMovimiento;

        Vector3 origenConMovimiento = puntoOrigen.position + Vector3.up * desplazamiento;
        Vector3 destinoConMovimiento = puntoDestino.position + Vector3.up * desplazamiento;

        lineRenderer.SetPosition(0, origenConMovimiento);
        lineRenderer.SetPosition(1, destinoConMovimiento);

        luzPunto.transform.position = Vector3.Lerp(origenConMovimiento, destinoConMovimiento, 0.5f);
        luzPunto.range = Vector3.Distance(origenConMovimiento, destinoConMovimiento) / 2f;

        RaycastHit hit;
        if (Physics.Raycast(origenConMovimiento, destinoConMovimiento - origenConMovimiento, out hit, Vector3.Distance(origenConMovimiento, destinoConMovimiento)))
        {
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("¡El jugador ha sido alcanzado!");
                GameManager.currentLifePoints -= daño;
            }
        }
    }
}
*/
using UnityEngine;

public class LaserBeamMovimiento : LaserBeamBase
{
    public float velocidadMovimiento = 2f;
    public float amplitudMovimiento = 1f;

    private float tiempo = 0f;

    protected override void Update()
    {
        tiempo += Time.deltaTime * velocidadMovimiento;
        float desplazamiento = Mathf.Sin(tiempo) * amplitudMovimiento;

        Vector3 origenConMovimiento = puntoOrigen.position + Vector3.up * desplazamiento;
        Vector3 destinoConMovimiento = puntoDestino.position + Vector3.up * desplazamiento;

        lineRenderer.SetPosition(0, origenConMovimiento);
        lineRenderer.SetPosition(1, destinoConMovimiento);

        luzPunto.transform.position = Vector3.Lerp(origenConMovimiento, destinoConMovimiento, 0.5f);
        luzPunto.range = Vector3.Distance(origenConMovimiento, destinoConMovimiento) / 2f;

        RaycastHit hit;
        if (Physics.Raycast(origenConMovimiento, destinoConMovimiento - origenConMovimiento, out hit, Vector3.Distance(origenConMovimiento, destinoConMovimiento)))
        {
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("¡El jugador ha sido alcanzado!");
                GameManager.Instancia.RecibirDaño(daño);
            }
        }
    }
}