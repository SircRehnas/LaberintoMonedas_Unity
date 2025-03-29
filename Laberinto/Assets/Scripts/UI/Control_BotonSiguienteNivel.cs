using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Control_BotonSiguienteNivel : MonoBehaviour
{
    void Start()
    {
        VerificarNivelCompletado();
        GetComponent<Button>().onClick.AddListener(CargarSiguienteNivel);
    }

    void VerificarNivelCompletado()
    {
        string escenaAnterior = GameManager.Instancia.nivelAnterior; // Obtener el nombre del nivel anterior de GameManager
        int nivelAnteriorNumero = ObtenerNumeroNivel(escenaAnterior);

        if (nivelAnteriorNumero > 0 && nivelAnteriorNumero < 8)
        {
            if (GestorProgreso.Instancia != null && GestorProgreso.Instancia.datosJugador != null)
            {
                if (GestorProgreso.Instancia.datosJugador.nivelesCompletados[nivelAnteriorNumero - 1])
                {
                    gameObject.SetActive(true);
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
            else
            {
                Debug.LogError("GestorProgreso no encontrado.");
                gameObject.SetActive(false);
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    void CargarSiguienteNivel()
    {
        string escenaAnterior = GameManager.Instancia.nivelAnterior; // Obtener el nombre del nivel anterior de GameManager
        int nivelAnteriorNumero = ObtenerNumeroNivel(escenaAnterior);

        if (nivelAnteriorNumero < 8)
        {
            string siguienteNivel = "Nivel" + (nivelAnteriorNumero + 1);
            // Reiniciar monedas recogidas y vida
            if (GameManager.Instancia != null)
            {
                GameManager.Instancia.ReiniciarMonedas(); // Reiniciar la variable de monedas
                GameManager.Instancia.ReiniciarVida(); 
                GameManager.Instancia.AsignarNivelActual(nivelAnteriorNumero);//Aumenta el nivelActual en el GameManager
            }
            SceneManager.LoadScene(siguienteNivel);
        }
    }

    int ObtenerNumeroNivel(string nombreEscena)
    {   

        Debug.Log("Nombre escena actual: " + nombreEscena );
        if (nombreEscena.StartsWith("Nivel"))
        {
            int numeroNivel;
            if (int.TryParse(nombreEscena.Substring(5), out numeroNivel))
            {
                Debug.Log("Nivel actual: " + numeroNivel);
                return numeroNivel;
            }
        }
        return 0;
    }
}