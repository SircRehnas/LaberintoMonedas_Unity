using UnityEngine;

public class GeneradorLaberintoB : MonoBehaviour
{
    public int ancho = 20;
    public int alto = 20;
    public GameObject cuboPrefab;
    public float tamañoCubo = 1.0f;

    public int anchoPasillo = 1; // Ancho del pasillo (en cubos)

    private int[,] laberinto;

    public int[,] GenerarLaberinto()
    {
        laberinto = new int[ancho, alto];
        GenerarLaberintoRecursivo(1, 1);
        CrearEntrada(); // Añadido: Crear la entrada
        return laberinto;
    }

    private void GenerarLaberintoRecursivo(int x, int y)
    {
        for (int i = 0; i < anchoPasillo; i++)
        {
            for (int j = 0; j < anchoPasillo; j++)
            {
                if (x + i < ancho && y + j < alto)
                {
                    laberinto[x + i, y + j] = 1;
                }
            }
        }

        int[] direcciones = { 1, 2, 3, 4 };
        MezclarArray(direcciones);

        foreach (int direccion in direcciones)
        {
            int nuevoX = x;
            int nuevoY = y;

            switch (direccion)
            {
                case 1: nuevoY += 2 * anchoPasillo; break;
                case 2: nuevoX += 2 * anchoPasillo; break;
                case 3: nuevoY -= 2 * anchoPasillo; break;
                case 4: nuevoX -= 2 * anchoPasillo; break;
            }

            if (nuevoX > 0 && nuevoX < ancho - anchoPasillo && nuevoY > 0 && nuevoY < alto - anchoPasillo && laberinto[nuevoX, nuevoY] == 0)
            {
                for (int i = 0; i < anchoPasillo; i++)
                {
                    for (int j = 0; j < anchoPasillo; j++)
                    {
                        laberinto[(x + nuevoX) / 2 + i, (y + nuevoY) / 2 + j] = 1;
                    }
                }
                GenerarLaberintoRecursivo(nuevoX, nuevoY);
            }
        }
    }

    private void CrearEntrada()
    {
        // Crear una entrada en la parte inferior del laberinto
        for (int i = 0; i < anchoPasillo; i++)
        {
            laberinto[i, 0] = 1;
        }
    }
    public void DibujarLaberinto()
    {
        for (int x = 0; x < ancho; x++)
        {
            for (int y = 0; y < alto; y++)
            {
                if (laberinto[x, y] == 0)
                {
                    Vector3 posicion = new Vector3(x * tamañoCubo, 0, y * tamañoCubo);
                    Instantiate(cuboPrefab, posicion, Quaternion.identity);
                }
            }
        }
    }

    private void MezclarArray(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int temp = array[i];
            int randomIndex = Random.Range(i, array.Length);
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }
}