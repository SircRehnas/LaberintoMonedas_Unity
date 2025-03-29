/*
using UnityEngine;
using UnityEngine.SceneManagement;

public class InterfaceAwake : MonoBehaviour
{
    public AudioSource sonidoInterfaz;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //GameManager.currentNumberStonesThrow = 0;
        //GameManager.currentNumberDestroyedStones = 0;
        sonidoInterfaz = GetComponent<AudioSource>();
        sonidoInterfaz.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void botonPlay(){
        GameManager.currentNumberCollectedCoins = 0;
        GameManager.currentLifePoints = 3;
        SceneManager.LoadScene("SeleccionNivel");
    }

    public void botonCredits(){
        SceneManager.LoadScene("Creditos");
    }

    public void botonSettings(){
        SceneManager.LoadScene("Settings");
    }

    public void SalirJuego()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Detiene la ejecución en el editor
    #else
        Application.Quit(); // Cierra la aplicación en una compilación
    #endif
    }
}
*/
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InterfaceMenuPrincipal : MonoBehaviour
{
    public AudioSource sonidoInterfaz;
    public float velocidadRotacion; // Velocidad de rotación
    public TMP_Text versionTextTMP;
    void Start()
    {
        sonidoInterfaz = GetComponent<AudioSource>();
        sonidoInterfaz.Play();
        string version = Application.version;

        if (versionTextTMP != null)
        {
            versionTextTMP.text = "Version: " + version;
        }
    }

    void LateUpdate()
    {
        float rotacionActual = RenderSettings.skybox.GetFloat("_Rotation");
        rotacionActual += velocidadRotacion * Time.deltaTime;
        RenderSettings.skybox.SetFloat("_Rotation", rotacionActual);
    }
    
    public void BotonPlay()
{
    if (GameManager.Instancia != null)
    {
        GameManager.Instancia.ReiniciarJuego();
        SceneManager.LoadScene("SeleccionNivel");
    }
    else
    {
        Debug.LogError("GameManager no inicializado.");
    }
}

    public void BotonCreditos()
    {
        SceneManager.LoadScene("Creditos");
    }

    public void BotonAjustes()
    {
        SceneManager.LoadScene("Configuracion");
    }

    public void SalirJuego()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}