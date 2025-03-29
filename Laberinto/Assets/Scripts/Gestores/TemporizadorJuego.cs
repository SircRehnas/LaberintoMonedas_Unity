/*
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TemporizadorJuego : MonoBehaviour
{
    public float tiempoLimite = 120f; // Tiempo límite en segundos (2 minutos por defecto)
    private float tiempoRestante;
    public TextMeshProUGUI temporizadorTexto; // Referencia al Text del temporizador

    void Start()
    {
        tiempoRestante = tiempoLimite;
    }

    void Update()
    {
        if (tiempoRestante > 0)
        {
            tiempoRestante -= Time.deltaTime;
            ActualizarTemporizadorUI();
        }
        else
        {
            tiempoRestante = 0;            
            TemporizadorTerminado();
        }
    }

    void ActualizarTemporizadorUI()
    {
        if (temporizadorTexto != null)
        {
            int minutos = Mathf.FloorToInt(tiempoRestante / 60);
            int segundos = Mathf.FloorToInt(tiempoRestante % 60);
            temporizadorTexto.text = string.Format("{0:00}:{1:00}", minutos, segundos);
             if (tiempoRestante < tiempoLimite / 2)
            {
                temporizadorTexto.color = Color.red;
            }
        }
        else
        {
            Debug.LogError("¡No se ha asignado el Text del temporizador!");
        }
    }

    void TemporizadorTerminado()
    {
        Debug.Log("¡Tiempo terminado!");
        GameManager.MensajeFinal = "Tiempo terminado! \n Fin del juego";
        //GameManager.currentNumberCollectedPoints = 0;
        //GameManager.currentNumberCollectedCoins = 0;
        SceneManager.LoadScene("Final");
        // Aquí puedes añadir la lógica para finalizar el juego
        // Por ejemplo, mostrar un mensaje de fin de juego o cargar una escena diferente
    }
}
*/
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TemporizadorJuego : MonoBehaviour
{
    public float tiempoLimite = 120f; // Tiempo límite base (dificultad normal)
    private float tiempoRestante;
    public TextMeshProUGUI temporizadorTexto;

    void Start()
    {
        AjustarTiempoPorDificultad();
        tiempoRestante = tiempoLimite;
    }

    void Update()
    {
        if (tiempoRestante > 0)
        {
            tiempoRestante -= Time.deltaTime;
            ActualizarTemporizadorUI();
        }
        else
        {
            tiempoRestante = 0;
            TemporizadorTerminado();
        }
    }

    void ActualizarTemporizadorUI()
    {
        if (temporizadorTexto != null)
        {
            int minutos = Mathf.FloorToInt(tiempoRestante / 60);
            int segundos = Mathf.FloorToInt(tiempoRestante % 60);
            temporizadorTexto.text = string.Format("{0:00}:{1:00}", minutos, segundos);
            if (tiempoRestante < tiempoLimite / 2)
            {
                temporizadorTexto.color = Color.red;
            }
        }
        else
        {
            Debug.LogError("¡No se ha asignado el Text del temporizador!");
        }
    }

    void TemporizadorTerminado()
    {
        GameManager.Instancia.TiempoTerminado();
    }

    void AjustarTiempoPorDificultad()
    {
        if (GestorProgreso.Instancia != null && GestorProgreso.Instancia.datosJugador != null)
        {
            int nivelActual = GameManager.Instancia.nivelActual; // Obtener el nivel actual
            if (GestorProgreso.Instancia.datosJugador.dificultadesCompletadas != null && nivelActual < GestorProgreso.Instancia.datosJugador.dificultadesCompletadas.Length)
            {
                string dificultad = GestorProgreso.Instancia.datosJugador.dificultadesCompletadas[nivelActual];

                switch (dificultad)
                {
                    case "Facil":
                        tiempoLimite += 30f;
                        break;
                    case "Dificil":
                        tiempoLimite -= 30f;
                        if (tiempoLimite < 0) tiempoLimite = 0;
                        break;
                    default: // Normal
                        break;
                }
            }
            else
            {
                Debug.LogWarning("Dificultad no encontrada para el nivel actual, usando dificultad normal.");
            }
        }
        else
        {
            Debug.LogWarning("GestorProgreso no encontrado, usando dificultad normal.");
        }
    }
}