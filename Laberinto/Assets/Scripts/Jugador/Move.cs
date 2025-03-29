using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move : MonoBehaviour
{
    public float forceValue;
    public float jumpValue;
    private Rigidbody fisicaEsfera;
    private AudioSource sounds;
    public GameObject explosion;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fisicaEsfera = GetComponent<Rigidbody>();
        sounds = GetComponent<AudioSource> ();
    }

    // Update is called once per frame
    void Update()
    {
        //Implementa el salto 
        // GetButton daria true cuando el usuario pulsa el boton y mientras permanezca pulsado
        // GetButtonDown daria true solo el primer instante en el que usuario haya pulsado el botón (no mientras permanezca pulsado)
        //desde cruceta
        if(Input.GetButtonDown ("Jump") && Mathf.Abs(fisicaEsfera.linearVelocity.y) < 0.01f){
            fisicaEsfera.AddForce(Vector3.up * jumpValue, ForceMode.Impulse);
            sounds.Play();
        }
        //desde toques en pantallas táctiles
        //touches es un array y en la primera componente tienes el primer toque (0)
        //fases del touch tocamos(began) en los siugientes update si sostenemos o desplazamos(stationary o move) o soltamos (endid)
        if (Input.touchCount == 1)
        if(Input.touches[0].phase == TouchPhase.Began && Mathf.Abs(fisicaEsfera.linearVelocity.y) < 0.01f){
            fisicaEsfera.AddForce(Vector3.up * jumpValue, ForceMode.Impulse);
            sounds.Play();
        }
    }

    void FixedUpdate()
    {
        //Implementa movimiento con fisicas (la bola está rodando)
        //desde cruceta
        fisicaEsfera.AddForce(new Vector3(Input.GetAxis ("Horizontal"),
                                        0,
                                        Input.GetAxis ("Vertical")) * forceValue);
        //desde acelerometro en dispositivos moviles
        fisicaEsfera.AddForce(new Vector3(Input.acceleration.x,
                                        0,
                                        Input.acceleration.y) * forceValue);
    }


    void LateUpdate()
    {
        if(gameObject.transform.position.y < 0f){
                //GameManager.MensajeFinal = "Te Caiste! \n Fin del juego";
                GenerarExplosion();
                StartCoroutine(LoadSceneAfterDelay(0.8f));
                
        }

        if(GameManager.Instancia.vidaActual <= 0){
                //GameManager.MensajeFinal = "Te quedaste sin vida! \n Fin del juego";
                //GameManager.currentNumberCollectedPoints = 0;
                //GameManager.currentNumberCollectedCoins = 0;
				GenerarExplosion();
                StartCoroutine(LoadSceneAfterDelay(0.8f));
        }
    }
    IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Final");
    }

    void GenerarExplosion(){
        fisicaEsfera.useGravity = false;
        // Detiene la velocidad del Rigidbody
        fisicaEsfera.linearVelocity = Vector3.zero;
        fisicaEsfera.angularVelocity = Vector3.zero;
        Instantiate(explosion, transform.position, Quaternion.identity);
        transform.localScale = Vector3.zero;// Escala el objeto a cero
    }

}
