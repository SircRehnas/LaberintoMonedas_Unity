using UnityEngine;
using System.Collections.Generic;

public class GeneradorObjetosLaberinto : MonoBehaviour
{
    public GeneradorLaberintoB generadorLaberinto;
    public GeneradorMonedasB generadorMonedas;
    public GameObject corazonPrefab;
    public int cantidadCorazones = 5;

    public int TotalMonedasCreadas { get; private set; }

    public float probabilidadMoneda = 0.5f;
    public float probabilidadCorazon = 0.2f;

    [System.Serializable]
    public class EnemigoConfiguracion
    {
        public GameObject enemigoPrefab;
        public int cantidadEnemigos;
    }

    public List<EnemigoConfiguracion> configuracionesEnemigos = new List<EnemigoConfiguracion>();

    void Start()
    {
        int[,] laberinto = generadorLaberinto.GenerarLaberinto();
        generadorLaberinto.DibujarLaberinto();
        ColocarObjetos(laberinto);
    }

    void ColocarObjetos(int[,] laberinto)
    {
        int ancho = laberinto.GetLength(0);
        int alto = laberinto.GetLength(1);

        TotalMonedasCreadas = 0;

        List<Vector2Int> posicionesDisponibles = new List<Vector2Int>();

        for (int x = 0; x < ancho; x++)
        {
            for (int y = 0; y < alto; y++)
            {
                if (laberinto[x, y] == 1)
                {
                    posicionesDisponibles.Add(new Vector2Int(x, y));
                }
            }
        }

        // Colocar monedas
        foreach (GeneradorMonedasB.MonedaConfiguracion configuracion in generadorMonedas.monedasConfiguraciones)
        {
            int monedasColocadas = 0;
            while (monedasColocadas < configuracion.numeroMonedas && posicionesDisponibles.Count > 0)
            {
                int randomIndex = Random.Range(0, posicionesDisponibles.Count);
                Vector2Int posicion = posicionesDisponibles[randomIndex];
                posicionesDisponibles.RemoveAt(randomIndex);

                if (Random.value <= probabilidadMoneda)
                {
                    generadorMonedas.GenerarMoneda(new Vector3(posicion.x * generadorLaberinto.tamañoCubo, 0.5f, posicion.y * generadorLaberinto.tamañoCubo), configuracion);
                    monedasColocadas++;
                    TotalMonedasCreadas++;
                }
            }
        }
         
        
        // Colocar corazones
        int corazonesColocados = 0;
        while (corazonesColocados < cantidadCorazones && posicionesDisponibles.Count > 0)
        {
            int randomIndex = Random.Range(0, posicionesDisponibles.Count);
            Vector2Int posicion = posicionesDisponibles[randomIndex];
            posicionesDisponibles.RemoveAt(randomIndex);

            if (Random.value <= probabilidadCorazon)
            {
                Instantiate(corazonPrefab, new Vector3(posicion.x * generadorLaberinto.tamañoCubo, 0.5f, posicion.y * generadorLaberinto.tamañoCubo), Quaternion.identity);
                corazonesColocados++;
            }
        }

        // Colocar enemigos
        foreach (EnemigoConfiguracion configuracion in configuracionesEnemigos)
        {
            int enemigosColocados = 0;
            while (enemigosColocados < configuracion.cantidadEnemigos && posicionesDisponibles.Count > 0)
            {
                int randomIndex = Random.Range(0, posicionesDisponibles.Count);
                Vector2Int posicion = posicionesDisponibles[randomIndex];
                posicionesDisponibles.RemoveAt(randomIndex);

                Instantiate(configuracion.enemigoPrefab, new Vector3(posicion.x * generadorLaberinto.tamañoCubo, 0.5f, posicion.y * generadorLaberinto.tamañoCubo), Quaternion.identity);
                enemigosColocados++;
            }
        }

        // Guardar el total de monedas generadas en DatosNivel
        GestorProgreso.Instancia.datosNivel[GameManager.Instancia.nivelActual].totalMonedas = TotalMonedasCreadas;
    }
}