/*
using System;
using UnityEngine;

public static class GameManager {
    public static int currentNumberCollectedPoints = 0;
    public static int currentNumberCollectedCoins = 0;
    public static String MensajeFinal;
    public static float currentLifePoints = 3f; // Vida inicial del jugador
}

*/
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instancia { get; private set; }

    public int puntosRecogidos { get; private set; } = 0;
    public int monedasRecogidas { get; private set; } = 0;
    public float vidaActual { get; private set; }
    public float vidaMaxima = 6f;
    public string mensajeFinal { get; private set; }
    public string nivelAnterior;
    public delegate void PuntosActualizados(int puntos);
    public event PuntosActualizados OnPuntosActualizados;

    public delegate void MonedasActualizadas(int monedas);
    public event MonedasActualizadas OnMonedasActualizadas;

    public delegate void VidaActualizada(float vida);
    public event VidaActualizada OnVidaActualizada;

    public delegate void JuegoTerminado(string mensaje);
    public event JuegoTerminado OnJuegoTerminado;

    public int nivelActual = 0; // Variable para almacenar el nivel actual

    private void Awake()
    {
        if (Instancia == null)
        {
            Instancia = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("GameManager inicializado.");
        }
        else
        {
            Destroy(gameObject);
        }

        ReiniciarJuego();
    }

    public void ReiniciarJuego()
    {
        puntosRecogidos = 0;
        monedasRecogidas = 0;
        vidaActual = vidaMaxima;
        mensajeFinal = "";

        OnPuntosActualizados?.Invoke(puntosRecogidos);
        OnMonedasActualizadas?.Invoke(monedasRecogidas);
        OnVidaActualizada?.Invoke(vidaActual);
    }

    public void RecogerMoneda(int valor)
    {
        Debug.Log("Moneda recogida: Puntos=" + puntosRecogidos + ", Monedas=" + monedasRecogidas);
        monedasRecogidas++;
        puntosRecogidos += valor;

        OnPuntosActualizados?.Invoke(puntosRecogidos);
        OnMonedasActualizadas?.Invoke(monedasRecogidas);
    }

    public void RecibirDaño(float daño)
    {
        vidaActual -= daño;
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima);

        OnVidaActualizada?.Invoke(vidaActual);

        if (vidaActual <= 0)
        {
            TerminarJuego("Te quedaste sin vida! \n Fin del juego");
        }
    }

    public void Curar(float curacion)
    {
        vidaActual += curacion;
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima);

        OnVidaActualizada?.Invoke(vidaActual);
    }

    public void ComprobarVictoria(int monedasNecesarias, string dificultad)
    {
        if (monedasRecogidas >= monedasNecesarias)
        {
            TerminarJuego("Enhorabuena! \n Nivel Completado");
            ActualizarProgresoJugador(dificultad); // Llamar a la función para actualizar el progreso
        }
    }

    public void TerminarJuego(string mensaje)
    {
        nivelAnterior = SceneManager.GetActiveScene().name;
        mensajeFinal = mensaje;
        OnJuegoTerminado?.Invoke(mensajeFinal);
        SceneManager.LoadScene("Final");
    }

    public void CaerAlVacio()
    {
        TerminarJuego("Te Caiste! \n Fin del juego");
    }

    public void TiempoTerminado()
    {
        TerminarJuego("Tiempo terminado! \n Fin del juego");
    }

    public void AsignarNivelActual(int nivel)
    {
        nivelActual = nivel;
    }

    private void ActualizarProgresoJugador(string dificultad)
    {
        DatosJugador datos = GestorProgreso.Instancia.datosJugador;

        if (puntosRecogidos > datos.puntuacionesMaximas[nivelActual])
        {
            datos.puntuacionesMaximas[nivelActual] = puntosRecogidos;
        }

        datos.nivelesCompletados[nivelActual] = true;
        GestorProgreso.Instancia.GuardarDificultadNivel(nivelActual, dificultad);
        GestorProgreso.Instancia.GuardarDatos();
    }

    public void ReiniciarMonedas()
    {
        monedasRecogidas = 0;
    }

    public void ReiniciarVida()
    {
        vidaActual = vidaMaxima;
    }
}