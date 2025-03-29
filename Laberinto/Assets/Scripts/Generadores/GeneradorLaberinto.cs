using UnityEngine;

public class GeneradorLaberinto : MonoBehaviour
{
    // Variables públicas para configurar el laberinto desde el Inspector
    public int ancho = 20; // Ancho del laberinto
    public int alto = 20;  // Alto del laberinto
    public GameObject cuboPrefab; // Prefab del cubo que representa las paredes
    public float tamañoCubo = 1.0f; // Tamaño de cada cubo

    private int[,] laberinto; // Matriz que representa el laberinto (0: pared, 1: camino)

    void Start()
    {
        // Llamamos a los métodos para generar y dibujar el laberinto al inicio del juego
        GenerarLaberintoAlgoritmo();
        DibujarLaberinto();
    }

    void GenerarLaberintoAlgoritmo()
    {
        // Inicializamos la matriz del laberinto con todas las celdas como paredes (0)
        laberinto = new int[ancho, alto];
        // Llamamos al método recursivo para generar el laberinto, comenzando en la celda (1, 1)
        GenerarLaberintoRecursivo(1, 1);
    }

    void GenerarLaberintoRecursivo(int x, int y)
    {
        // Marcamos la celda actual como un camino (1)
        laberinto[x, y] = 1;

        // Creamos un array con las direcciones posibles (1: arriba, 2: derecha, 3: abajo, 4: izquierda)
        int[] direcciones = { 1, 2, 3, 4 };
        // Mezclamos el array de direcciones para generar laberintos aleatorios
        MezclarArray(direcciones);

        // Iteramos sobre las direcciones mezcladas
        foreach (int direccion in direcciones)
        {
            // Calculamos las coordenadas de la siguiente celda
            int nuevoX = x;
            int nuevoY = y;

            switch (direccion)
            {
                case 1: nuevoY += 2; break; // Arriba
                case 2: nuevoX += 2; break; // Derecha
                case 3: nuevoY -= 2; break; // Abajo
                case 4: nuevoX -= 2; break; // Izquierda
            }

            // Verificamos si la siguiente celda está dentro de los límites del laberinto y no ha sido visitada
            if (nuevoX > 0 && nuevoX < ancho - 1 && nuevoY > 0 && nuevoY < alto - 1 && laberinto[nuevoX, nuevoY] == 0)
            {
                // Marcamos la celda intermedia como un camino (1) para conectar las celdas
                laberinto[(x + nuevoX) / 2, (y + nuevoY) / 2] = 1;
                // Llamamos recursivamente al método para continuar generando el laberinto desde la siguiente celda
                GenerarLaberintoRecursivo(nuevoX, nuevoY);
            }
        }
    }

    void DibujarLaberinto()
    {
        // Iteramos sobre todas las celdas del laberinto
        for (int x = 0; x < ancho; x++)
        {
            for (int y = 0; y < alto; y++)
            {
                // Si la celda es una pared (0), instanciamos un cubo en esa posición
                if (laberinto[x, y] == 0)
                {
                    Vector3 posicion = new Vector3(x * tamañoCubo, 0, y * tamañoCubo);
                    Instantiate(cuboPrefab, posicion, Quaternion.identity);
                }
            }
        }
    }

    void MezclarArray(int[] array)
    {
        // Mezclamos un array utilizando el algoritmo de Fisher-Yates
        for (int i = 0; i < array.Length; i++)
        {
            int temp = array[i];
            int randomIndex = Random.Range(i, array.Length);
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }
}