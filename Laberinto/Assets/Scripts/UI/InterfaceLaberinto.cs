/*
using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;
    using TMPro;

    public class InterfaceLaberinto : MonoBehaviour
    {
        public TextMeshProUGUI textoMonedas;
        public TextMeshProUGUI textoVida;
        public TextMeshProUGUI textoTotalPuntos;
        public GameObject[] corazonContainers; // Arrastra Corazon_1, Corazon_2, Corazon_3 aquí
        private Image[] hearts = new Image[3];
        private GeneradorMonedas generador;

        void Start()
        {
            generador = FindFirstObjectByType<GeneradorMonedas>();
            for (int i = 0; i < corazonContainers.Length; i++)
            {
                hearts[i] = corazonContainers[i].GetComponentInChildren<Image>();
            }
            UpdateHearts(); // Inicializa los corazones al inicio
        }

        void Update()
        {
            textoMonedas.text = "Monedas: " + GameManager.currentNumberCollectedCoins;
            textoTotalPuntos.text = GameManager.currentNumberCollectedPoints + " Puntos";
            textoVida.text = "Vidas: " + GameManager.currentLifePoints;
            UpdateHearts(); // Actualiza los corazones en cada frame

            if (GameManager.currentNumberCollectedCoins == generador.TotalMonedas)
            {
                GameManager.MensajeFinal = "Enhorabuena! \n Nivel Completado";
                SceneManager.LoadScene("Final");
            }
        }

       void UpdateHearts()
    {
        // Desactiva todos los Raw Images
        foreach (GameObject container in corazonContainers)
        {
            foreach (RawImage heart in container.GetComponentsInChildren<RawImage>())
            {
                heart.enabled = false;
            }
        }

        // Activa los Raw Images correctos según la vida restante
        switch (GameManager.currentLifePoints)
        {
            case 6: // 3 corazones llenos
                corazonContainers[0].transform.Find("Lleno").GetComponent<RawImage>().enabled = true;
                corazonContainers[1].transform.Find("Lleno").GetComponent<RawImage>().enabled = true;
                corazonContainers[2].transform.Find("Lleno").GetComponent<RawImage>().enabled = true;
                break;
            case 5: // 2 corazones llenos, 1 medio
                corazonContainers[0].transform.Find("Lleno").GetComponent<RawImage>().enabled = true;
                corazonContainers[1].transform.Find("Lleno").GetComponent<RawImage>().enabled = true;
                corazonContainers[2].transform.Find("Medio").GetComponent<RawImage>().enabled = true;
                break;
            case 4: // 2 corazones llenos, 1 vacío
                corazonContainers[0].transform.Find("Lleno").GetComponent<RawImage>().enabled = true;
                corazonContainers[1].transform.Find("Lleno").GetComponent<RawImage>().enabled = true;
                corazonContainers[2].transform.Find("Vacio").GetComponent<RawImage>().enabled = true;
                break;
            case 3: // 1 lleno, 1 medio, 1 vacío
                corazonContainers[0].transform.Find("Lleno").GetComponent<RawImage>().enabled = true;
                corazonContainers[1].transform.Find("Medio").GetComponent<RawImage>().enabled = true;
                corazonContainers[2].transform.Find("Vacio").GetComponent<RawImage>().enabled = true;
                break;
            case 2: // 1 medio, 2 vacíos
                corazonContainers[0].transform.Find("LLeno").GetComponent<RawImage>().enabled = true;
                corazonContainers[1].transform.Find("Vacio").GetComponent<RawImage>().enabled = true;
                corazonContainers[2].transform.Find("Vacio").GetComponent<RawImage>().enabled = true;
                break;
            case 1: // 1 medio, 2 vacíos
                corazonContainers[0].transform.Find("Medio").GetComponent<RawImage>().enabled = true;
                corazonContainers[1].transform.Find("Vacio").GetComponent<RawImage>().enabled = true;
                corazonContainers[2].transform.Find("Vacio").GetComponent<RawImage>().enabled = true;
                break;
            default: // 3 vacíos
                corazonContainers[0].transform.Find("Vacio").GetComponent<RawImage>().enabled = true;
                corazonContainers[1].transform.Find("Vacio").GetComponent<RawImage>().enabled = true;
                corazonContainers[2].transform.Find("Vacio").GetComponent<RawImage>().enabled = true;
                break;
        }
    }
    }
*/
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InterfaceLaberinto : MonoBehaviour
{
    public TextMeshProUGUI textoMonedas;
    public TextMeshProUGUI textoVida;
    public TextMeshProUGUI textoTotalPuntos;
    public GameObject[] corazonContainers;
    private Image[] hearts = new Image[3];
    public int totalMonedasNivel;
    public int velocidadRotacion = 1;
    void Start()
    {
        Debug.Log("InterfaceLaberinto.Start() llamado.");
        for (int i = 0; i < corazonContainers.Length; i++)
        {
            hearts[i] = corazonContainers[i].GetComponentInChildren<Image>();
        }

        GameManager.Instancia.OnPuntosActualizados += ActualizarPuntos;
        GameManager.Instancia.OnMonedasActualizadas += ActualizarMonedas;
        GameManager.Instancia.OnVidaActualizada += ActualizarVida;
        GameManager.Instancia.OnJuegoTerminado += MostrarMensajeFinal;

        textoMonedas.text = "Monedas: " + GameManager.Instancia.monedasRecogidas;
        textoTotalPuntos.text = GameManager.Instancia.puntosRecogidos + " Puntos";
        ActualizarVida(GameManager.Instancia.vidaActual);

        GestorProgreso.Instancia.datosNivel[GameManager.Instancia.nivelActual].totalMonedas = totalMonedasNivel;
    }

    void LateUpdate()
    {
        float rotacionActual = RenderSettings.skybox.GetFloat("_Rotation");
        rotacionActual += velocidadRotacion * Time.deltaTime;
        RenderSettings.skybox.SetFloat("_Rotation", rotacionActual);
    }

    private void OnDestroy()
    {
        GameManager.Instancia.OnPuntosActualizados -= ActualizarPuntos;
        GameManager.Instancia.OnMonedasActualizadas -= ActualizarMonedas;
        GameManager.Instancia.OnVidaActualizada -= ActualizarVida;
        GameManager.Instancia.OnJuegoTerminado -= MostrarMensajeFinal;
    }

    void ActualizarPuntos(int puntos)
    {
        Debug.Log("ActualizarPuntos llamado: puntos=" + puntos);
        textoTotalPuntos.text = puntos + " Puntos";
    }

    void ActualizarMonedas(int monedas)
    {
        Debug.Log("ActualizarMonedas llamado: monedas=" + monedas);
        textoMonedas.text = "Monedas: " + monedas;

        if (monedas == totalMonedasNivel)
        {
            GameManager.Instancia.ComprobarVictoria(totalMonedasNivel,GestorProgreso.Instancia.datosJugador.dificultadesCompletadas[GameManager.Instancia.nivelActual]);
        }
    }

    void ActualizarVida(float vida)
    {
        textoVida.text = "Vidas: " + vida;
        ActualizarCorazones(vida);
    }

    void MostrarMensajeFinal(string mensaje)
    {
        // Aquí puedes añadir la lógica para mostrar el mensaje en la interfaz final
        Debug.Log("Juego terminado: " + mensaje); // Ejemplo: mostrar el mensaje en la consola
    }

    void ActualizarCorazones(float vida)
    {
        // Desactiva todos los Raw Images
        foreach (GameObject container in corazonContainers)
        {
            foreach (RawImage heart in container.GetComponentsInChildren<RawImage>())
            {
                heart.enabled = false;
            }
        }

        // Activa los Raw Images correctos según la vida restante
        switch ((int)vida)
        {
            case 6: // 3 corazones llenos
                corazonContainers[0].transform.Find("Lleno").GetComponent<RawImage>().enabled = true;
                corazonContainers[1].transform.Find("Lleno").GetComponent<RawImage>().enabled = true;
                corazonContainers[2].transform.Find("Lleno").GetComponent<RawImage>().enabled = true;
                break;
            case 5: // 2 corazones llenos, 1 medio
                corazonContainers[0].transform.Find("Lleno").GetComponent<RawImage>().enabled = true;
                corazonContainers[1].transform.Find("Lleno").GetComponent<RawImage>().enabled = true;
                corazonContainers[2].transform.Find("Medio").GetComponent<RawImage>().enabled = true;
                break;
            case 4: // 2 corazones llenos, 1 vacío
                corazonContainers[0].transform.Find("Lleno").GetComponent<RawImage>().enabled = true;
                corazonContainers[1].transform.Find("Lleno").GetComponent<RawImage>().enabled = true;
                corazonContainers[2].transform.Find("Vacio").GetComponent<RawImage>().enabled = true;
                break;
            case 3: // 1 lleno, 1 medio, 1 vacío
                corazonContainers[0].transform.Find("Lleno").GetComponent<RawImage>().enabled = true;
                corazonContainers[1].transform.Find("Medio").GetComponent<RawImage>().enabled = true;
                corazonContainers[2].transform.Find("Vacio").GetComponent<RawImage>().enabled = true;
                break;
            case 2: // 1 medio, 2 vacíos
                corazonContainers[0].transform.Find("Lleno").GetComponent<RawImage>().enabled = true;
                corazonContainers[1].transform.Find("Vacio").GetComponent<RawImage>().enabled = true;
                corazonContainers[2].transform.Find("Vacio").GetComponent<RawImage>().enabled = true;
                break;
            case 1: // 1 medio, 2 vacíos
                corazonContainers[0].transform.Find("Medio").GetComponent<RawImage>().enabled = true;
                corazonContainers[1].transform.Find("Vacio").GetComponent<RawImage>().enabled = true;
                corazonContainers[2].transform.Find("Vacio").GetComponent<RawImage>().enabled = true;
                break;
            default: // 3 vacíos
                corazonContainers[0].transform.Find("Vacio").GetComponent<RawImage>().enabled = true;
                corazonContainers[1].transform.Find("Vacio").GetComponent<RawImage>().enabled = true;
                corazonContainers[2].transform.Find("Vacio").GetComponent<RawImage>().enabled = true;
                break;
        }
    }
}