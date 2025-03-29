/*New*/
using UnityEngine;

public class LaserBeamBase : MonoBehaviour
{
    public Transform puntoOrigen;
    public Transform puntoDestino;
    public LineRenderer lineRenderer;
    public float daño = 10f;
    public float intensidadLuz = 2f;
    public Color colorLuz = Color.red;
    public float rangoLuz = 5f;

    protected Light luzPunto;

    protected virtual void Start()
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

    protected virtual void Update()
    {
        Vector3 origen = puntoOrigen.position;
        Vector3 destino = puntoDestino.position;

        lineRenderer.SetPosition(0, origen);
        lineRenderer.SetPosition(1, destino);

        luzPunto.transform.position = Vector3.Lerp(origen, destino, 0.5f);

        RaycastHit hit;
        if (Physics.Raycast(origen, destino - origen, out hit, Vector3.Distance(origen, destino)))
        {
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("¡El jugador ha sido alcanzado!");
                GameManager.Instancia.RecibirDaño(daño);
            }
        }
    }
}