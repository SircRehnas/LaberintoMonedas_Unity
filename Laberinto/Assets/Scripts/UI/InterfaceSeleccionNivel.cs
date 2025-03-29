/*
using UnityEngine;
using UnityEngine.SceneManagement;

public class InterfaceSeleccionNivel : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void botonVolver(){
        SceneManager.LoadScene("Awake");//
    }

    public void botonNivelUno(){
        SceneManager.LoadScene("MainScene");
    }
}
*/
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InterfaceSeleccionNivel : MonoBehaviour
{
    public Button[] botonesNiveles;
    public TextMeshProUGUI[] textosPuntuaciones;
    public float velocidadRotacion;
    // Colores para las dificultades
    public Color bronceColor = new Color(0.8f, 0.5f, 0.2f); // Color bronce
    public Color plataColor = new Color(0.7f, 0.7f, 0.7f); // Color plata
    public Color oroColor = new Color(1f, 0.8f, 0f); // Color oro
    void Start()
    {
        ActualizarInterfaz();
    }

    void LateUpdate()
    {
        float rotacionActual = RenderSettings.skybox.GetFloat("_Rotation");
        rotacionActual += velocidadRotacion * Time.deltaTime;
        RenderSettings.skybox.SetFloat("_Rotation", rotacionActual);
    }

    public void BotonVolver()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }

    public void BotonNivelUno()
    {
        GameManager.Instancia.AsignarNivelActual(0); // Nivel 1 (índice 0)ç
        GestorProgreso.Instancia.datosJugador.dificultadesCompletadas[0] = GameObject.FindFirstObjectByType<ControlDificultadDropdown>().ObtenerDificultadSeleccionada(); 
        SceneManager.LoadScene("Nivel1");
    }

    public void BotonNivelDos()
    {
        GameManager.Instancia.AsignarNivelActual(1); // Nivel 2 (índice 1)
        GestorProgreso.Instancia.datosJugador.dificultadesCompletadas[1] = GameObject.FindFirstObjectByType<ControlDificultadDropdown>().ObtenerDificultadSeleccionada(); // Obtener la dificultad seleccionada;
        SceneManager.LoadScene("Nivel2");
    }

    public void BotonNivelTres()
    {
        GameManager.Instancia.AsignarNivelActual(2); // Nivel 3
        GestorProgreso.Instancia.datosJugador.dificultadesCompletadas[2] = GameObject.FindFirstObjectByType<ControlDificultadDropdown>().ObtenerDificultadSeleccionada(); 
        SceneManager.LoadScene("Nivel3");
    }

    public void BotonNivelCuatro()
    {
        GameManager.Instancia.AsignarNivelActual(3); // Nivel 4
        GestorProgreso.Instancia.datosJugador.dificultadesCompletadas[3] = GameObject.FindFirstObjectByType<ControlDificultadDropdown>().ObtenerDificultadSeleccionada(); 
        SceneManager.LoadScene("Nivel4");
    }

    public void BotonNivelCinco()
    {
        GameManager.Instancia.AsignarNivelActual(4); // Nivel 5 
    }

    public void BotonNivelSeis()
    {
        GameManager.Instancia.AsignarNivelActual(5); // Nivel 6
    }

    public void BotonNivelSiete()
    {
        GameManager.Instancia.AsignarNivelActual(6); // Nivel 7 
        SceneManager.LoadScene("Nivel7");
    }

    public void BotonNivelOcho()
    {
        GameManager.Instancia.AsignarNivelActual(7); // Nivel 8 
        SceneManager.LoadScene("Nivel8");
    }

    void ActualizarInterfaz()
{
    if (GestorProgreso.Instancia != null && GestorProgreso.Instancia.datosJugador != null)
    {
        DatosJugador datos = GestorProgreso.Instancia.datosJugador;

        for (int i = 0; i < botonesNiveles.Length; i++)
        {
            if (i < datos.nivelesCompletados.Length && i < datos.puntuacionesMaximas.Length)
            {
                if (i == 0 || datos.nivelesCompletados[i - 1])
                {
                    botonesNiveles[i].interactable = true;
                }
                else
                {
                    botonesNiveles[i].interactable = false;
                }

                textosPuntuaciones[i].text = "MP: " + datos.puntuacionesMaximas[i];
                // Cambiar el color del botón según la dificultad
                Image buttonImage = botonesNiveles[i].GetComponent<Image>();
                if (datos.dificultadesCompletadas != null && i < datos.dificultadesCompletadas.Length)
                    {
                    string dificultad = datos.dificultadesCompletadas[i]; // Asumiendo que tienes un array de dificultades en DatosJugador
                        if (dificultad == "Facil")
                        {
                            buttonImage.color = bronceColor;
                        }
                        else if (dificultad == "Normal")
                        {
                            buttonImage.color = plataColor;
                        }
                        else if (dificultad == "Dificil")
                        {
                            buttonImage.color = oroColor;
                        }
                        else
                        {
                            buttonImage.color = Color.white; // Color blanco por defecto
                        }
                    }
                    else
                    {
                        buttonImage.color = Color.white; // Color blanco por defecto si no hay dificultad
                    }
            }
            else
            {
                Debug.LogError("Arrays de progreso y botones/textos no coinciden en longitud.");
                break;
            }
        }
    }
}
}
