using UnityEngine;

public class GenerarPerimetroCuadrado : MonoBehaviour
{
    public GameObject cuboPrefab; // Prefab del cubo 1x1x1
    public int tamañoPlano = 4; // Tamaño del plano (4x4 por defecto)

    void Start()
    {
        GenerarPerimetro();
    }

    void GenerarPerimetro()
    {
        if (cuboPrefab == null)
        {
            Debug.LogError("¡Asigna el prefab del cubo en el Inspector!");
            return;
        }

        // Generar cubos en el perímetro del plano
        for (int x = 0; x < tamañoPlano; x++)
        {
            for (int z = 0; z < tamañoPlano; z++)
            {
                // Verificar si la posición está en el perímetro
                if (x == 0 || x == tamañoPlano - 1 || z == 0 || z == tamañoPlano - 1)
                {
                    // Instanciar el cubo en la posición correspondiente
                    Vector3 posicion = new Vector3(x, 0, z);
                    Instantiate(cuboPrefab, posicion, Quaternion.identity, transform);
                }
            }
        }
    }
}