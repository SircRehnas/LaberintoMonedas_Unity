/*
using UnityEngine;
using UnityEngine.SceneManagement;

public class InterfaceSettings : MonoBehaviour
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
}
*/
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InterfazSettings : MonoBehaviour
{
    public Slider sliderVolumen;
    public Toggle toggleSonido;
    //public AudioSource sonido;

    void Start()
    {   
        //sonido.Play();
        // Cargar el estado del sonido desde PlayerPrefs
        int sonidoActivado = PlayerPrefs.GetInt("SonidoActivado", 1);
        toggleSonido.isOn = sonidoActivado == 1;

        // Cargar el valor del volumen desde PlayerPrefs
        float volumen = PlayerPrefs.GetFloat("Volumen", 1f);
        sliderVolumen.value = volumen;

        // Actualizar el volumen inicial
        ActualizarVolumen(volumen, sonidoActivado == 1);

        // Añadir listeners para el cambio de estado del ToggleButton y el Slider
        toggleSonido.onValueChanged.AddListener(delegate {
            ActualizarVolumen(sliderVolumen.value, toggleSonido.isOn);
        });

        sliderVolumen.onValueChanged.AddListener(delegate {
            ActualizarVolumen(sliderVolumen.value, toggleSonido.isOn);
        });
    }

    void ActualizarVolumen(float volumen, bool sonidoActivado)
    {
        if (sonidoActivado)
        {
            AudioListener.volume = volumen;
            PlayerPrefs.SetFloat("Volumen", volumen);
            PlayerPrefs.SetInt("SonidoActivado", 1);
        }
        else
        {
            AudioListener.volume = 0f;
            PlayerPrefs.SetInt("SonidoActivado", 0);
        }
    }

    public void BotonVolver()
    {
        SceneManager.LoadScene("Awake");
    }
}
