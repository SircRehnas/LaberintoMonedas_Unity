/*
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InterfaceFinal : MonoBehaviour
{
    public Text textFinal;
    public Text textCollectedCoins;
    public Text textCollectedPoints;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textFinal.text = GameManager.MensajeFinal;
        textCollectedPoints.text = "Puntuación: " + GameManager.currentNumberCollectedPoints;
        textCollectedCoins.text = "Monedas recolectadas: " + GameManager.currentNumberCollectedCoins;
    }

    public void Click(){
        SceneManager.LoadScene("Awake");
    }
    
}
*/
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class InterfaceFinal : MonoBehaviour
{
    public TextMeshProUGUI textoFinal;
    public TextMeshProUGUI textoMonedas;
    public TextMeshProUGUI textoPuntos;
    public float velocidadRotacion;
    void Start()
    {
        if (GameManager.Instancia != null)
        {
            textoFinal.text = GameManager.Instancia.mensajeFinal;
            textoPuntos.text = "Puntuación: " + GameManager.Instancia.puntosRecogidos;
            textoMonedas.text = "Monedas recolectadas: " + GameManager.Instancia.monedasRecogidas;
        }
        else
        {
            Debug.LogError("GameManager no inicializado en la escena Final.");
        }
    }
    
    void LateUpdate()
    {
        float rotacionActual = RenderSettings.skybox.GetFloat("_Rotation");
        rotacionActual += velocidadRotacion * Time.deltaTime;
        RenderSettings.skybox.SetFloat("_Rotation", rotacionActual);
    }
    
    public void Clic()
    {
        GameManager.Instancia.ReiniciarJuego();
        SceneManager.LoadScene("MenuPrincipal");
    }
}