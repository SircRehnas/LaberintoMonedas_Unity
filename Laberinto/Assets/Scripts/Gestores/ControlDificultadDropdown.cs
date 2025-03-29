using UnityEngine;
using UnityEngine.UI;

public class ControlDificultadDropdown : MonoBehaviour
{
    public Dropdown dropdownDificultad;
    private string dificultadSeleccionada; // Variable para almacenar la dificultad seleccionada

    void Start()
    {
        dropdownDificultad.onValueChanged.AddListener(delegate {
            dificultadSeleccionada = dropdownDificultad.options[dropdownDificultad.value].text; // Guardar la dificultad seleccionada
        });
        CargarDificultad();
    }

    void SeleccionarDificultad(string dificultad)
    {
        dificultadSeleccionada = dificultad; // Guardar la dificultad seleccionada
    }

    void CargarDificultad()
    {
        if (GestorProgreso.Instancia != null && GestorProgreso.Instancia.datosJugador != null && GestorProgreso.Instancia.datosJugador.dificultadesCompletadas != null)
        {
            string dificultad = GestorProgreso.Instancia.datosJugador.dificultadesCompletadas[0]; // Cargar la dificultad global (o del primer nivel)
            if (dificultad == "Facil")
            {
                dropdownDificultad.value = 1;
            }
            else if (dificultad == "Dificil")
            {
                dropdownDificultad.value = 2;
            }
            else // Normal o ninguna
            {
                dropdownDificultad.value = 0;
            }
        }
        else
        {
            Debug.LogError("GestorProgreso no inicializado correctamente.");
        }
    }

    public string ObtenerDificultadSeleccionada()
    {
        return dificultadSeleccionada;
    }
}