using UnityEngine;

public class RellenarPerimetroPlano : MonoBehaviour
{
    public GameObject cuboPrefab; // Prefab del cubo 1x1x1
    public GameObject plano; // GameObject del plano

    void Start()
    {
        GenerarPerimetro();
    }

    void GenerarPerimetro()
    {
        if (cuboPrefab == null || plano == null)
        {
            Debug.LogError("¡Asigna el prefab del cubo y el GameObject del plano en el Inspector!");
            return;
        }

        Renderer planoRenderer = plano.GetComponent<Renderer>();
        if (planoRenderer == null)
        {
            Debug.LogError("El GameObject del plano no tiene un componente Renderer.");
            return;
        }

        Bounds planoBounds = planoRenderer.bounds;
        float tamañoCubo = 1f; // Asumiendo que el cubo es 1x1x1

        // Generar cubos en el perímetro del plano
        for (float x = planoBounds.min.x; x <= planoBounds.max.x; x += tamañoCubo)
        {
            for (float z = planoBounds.min.z; z <= planoBounds.max.z; z += tamañoCubo)
            {
                // Verificar si la posición está en el perímetro
                if (Mathf.Approximately(x, planoBounds.min.x) || Mathf.Approximately(x, planoBounds.max.x) ||
                    Mathf.Approximately(z, planoBounds.min.z) || Mathf.Approximately(z, planoBounds.max.z))
                {
                    // Instanciar el cubo en la posición correspondiente
                    Vector3 posicion = new Vector3(x, planoBounds.min.y, z);
                    Instantiate(cuboPrefab, posicion, Quaternion.identity, transform);
                }
            }
        }
    }
}