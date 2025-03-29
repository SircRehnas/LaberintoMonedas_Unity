using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject target;
    private AudioSource sonidoAmbiente;
    private Vector3 offset;

    public float smoothSpeed = 0.125f; // Velocidad de suavizado
    public Vector3 offsetPosition; // Offset adicional para la posición

    void Start()
    {
        Application.targetFrameRate = 60;
        offset = transform.position - target.transform.position;
        sonidoAmbiente = GetComponent<AudioSource>();
        sonidoAmbiente.Play();
        offsetPosition = offset; // Inicializa offsetPosition con el offset original
    }

    void LateUpdate() // Usamos LateUpdate para asegurar que el movimiento del target se haya completado
    {
        Vector3 desiredPosition = target.transform.position + offsetPosition;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}