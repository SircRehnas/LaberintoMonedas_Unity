/*
using UnityEngine;

public class GeneradorMonedas : MonoBehaviour
{
    [System.Serializable]
    public class MonedaConfiguracion
    {
        public GameObject monedaPrefab;
        public int numeroMonedas;
    }

    public MonedaConfiguracion[] monedasConfiguraciones;
    public GameObject plano;
    public float alturaInicial;
    public float alturaFinal;

    public int TotalMonedas { get; private set; } // Propiedad para obtener el total

    void Start()
    {
        GenerarMonedas();
    }

    void GenerarMonedas()
    {
        if (plano == null)
        {
            Debug.LogError("Asigna el GameObject del plano a la variable 'plano' en el Inspector.");
            return;
        }

        Renderer planoRenderer = plano.GetComponent<Renderer>();
        if (planoRenderer == null)
        {
            Debug.LogError("El GameObject del plano no tiene un componente Renderer.");
            return;
        }

        Bounds planoBounds = planoRenderer.bounds;

        TotalMonedas = 0; // Inicializa el total

        foreach (MonedaConfiguracion configuracion in monedasConfiguraciones)
        {
            for (int i = 0; i < configuracion.numeroMonedas; i++)
            {
                float x = Random.Range(planoBounds.min.x + 1f, planoBounds.max.x - 1f);
                float z = Random.Range(planoBounds.min.z + 1f, planoBounds.max.z - 1f);

                Vector3 posicionInicial = new Vector3(x, alturaInicial, z);
                GameObject moneda = Instantiate(configuracion.monedaPrefab, posicionInicial, Quaternion.identity);

                MonedaCaida monedaCaida = moneda.AddComponent<MonedaCaida>();
                monedaCaida.alturaFinal = alturaFinal;

                TotalMonedas++; // Incrementa el total
            }
        }
    }
}
*/
using UnityEngine;

public class GeneradorMonedas : MonoBehaviour
{
    [System.Serializable]
    public class MonedaConfiguracion
    {
        public GameObject monedaPrefab;
        public int numeroMonedas;
    }

    public MonedaConfiguracion[] monedasConfiguraciones;
    public GameObject plano;
    public float alturaInicial;
    public float alturaFinal;

    public int TotalMonedas { get; private set; }

    void Start()
    {
        GenerarMonedas();
        // Guardar el total de monedas generadas en DatosNivel
        GestorProgreso.Instancia.datosNivel[GameManager.Instancia.nivelActual].totalMonedas = TotalMonedas;
    }

    void GenerarMonedas()
    {
        if (plano == null)
        {
            throw new System.Exception("Asigna el GameObject del plano a la variable 'plano' en el Inspector.");
        }

        Renderer planoRenderer = plano.GetComponent<Renderer>();
        if (planoRenderer == null)
        {
            throw new System.Exception("El GameObject del plano no tiene un componente Renderer.");
        }

        Bounds planoBounds = planoRenderer.bounds;
        TotalMonedas = 0;

        foreach (MonedaConfiguracion configuracion in monedasConfiguraciones)
        {
            for (int i = 0; i < configuracion.numeroMonedas; i++)
            {
                float x = Random.Range(planoBounds.min.x + 1f, planoBounds.max.x - 1f);
                float z = Random.Range(planoBounds.min.z + 1f, planoBounds.max.z - 1f);

                Vector3 posicionInicial = new Vector3(x, alturaInicial, z);
                GameObject moneda = Instantiate(configuracion.monedaPrefab, posicionInicial, Quaternion.identity);

                MonedaCaida monedaCaida = moneda.AddComponent<MonedaCaida>();
                monedaCaida.alturaFinal = alturaFinal;

                TotalMonedas++;
            }
        }
    }
    
}