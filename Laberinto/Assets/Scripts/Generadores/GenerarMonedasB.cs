using UnityEngine;

public class GeneradorMonedasB : MonoBehaviour
{
    [System.Serializable]
    public class MonedaConfiguracion
    {
        public GameObject monedaPrefab;
        public int numeroMonedas;
    }

    public MonedaConfiguracion[] monedasConfiguraciones;
    public float alturaInicial;
    public float alturaFinal;

    public void GenerarMoneda(Vector3 posicion, MonedaConfiguracion configuracion) // Añadido MonedaConfiguracion como parámetro
    {
        Vector3 posicionInicial = new Vector3(posicion.x, alturaInicial, posicion.z);
        GameObject moneda = Instantiate(configuracion.monedaPrefab, posicionInicial, Quaternion.identity);

        MonedaCaida monedaCaida = moneda.AddComponent<MonedaCaida>();
        monedaCaida.alturaFinal = alturaFinal;
    }
}