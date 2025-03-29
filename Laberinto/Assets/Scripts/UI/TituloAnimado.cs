using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TituloAnimado : MonoBehaviour
{
    public float velocidadParpadeo = 1.0f;
    public Color color1 = Color.white;
    public Color color2 = Color.yellow;
    public float alturaSalto = 10.0f;
    public float velocidadSalto = 0.5f;

    private Text textoTitulo;
    private Vector3 posicionInicial;

    void Start()
    {
        textoTitulo = GetComponent<Text>();
        posicionInicial = transform.position;
        StartCoroutine(AnimacionCompleta());
    }

    IEnumerator AnimacionCompleta()
    {
        while (true)
        {
            // Parpadeo y cambio de color
            textoTitulo.color = color1;
            yield return new WaitForSeconds(velocidadParpadeo);

            textoTitulo.color = color2;
            yield return new WaitForSeconds(velocidadParpadeo);

            // Salto
            Vector3 posicionSalto = posicionInicial + Vector3.up * alturaSalto;
            float tiempoTranscurrido = 0.0f;

            while (tiempoTranscurrido < velocidadSalto)
            {
                tiempoTranscurrido += Time.deltaTime;
                transform.position = Vector3.Lerp(posicionInicial, posicionSalto, tiempoTranscurrido / velocidadSalto);
                yield return null;
            }

            tiempoTranscurrido = 0.0f;
            while (tiempoTranscurrido < velocidadSalto)
            {
                tiempoTranscurrido += Time.deltaTime;
                transform.position = Vector3.Lerp(posicionSalto, posicionInicial, tiempoTranscurrido / velocidadSalto);
                yield return null;
            }
        }
    }
}