/*
using UnityEngine;

public class Vida : MonoBehaviour
{
    public int valorVida;
    private AudioSource sonidoMoneda;
    void Start (){
        // Inicializa el componente AudioSource.
        sonidoMoneda = GetComponent<AudioSource>();
    }
    void Update()
    {
   
    }

    void OnTriggerEnter(Collider other)
    {
        sonidoMoneda.Play();
        transform.localScale = new Vector3(0f, 0f, 0f); 
        Destroy(gameObject, 0.8f);
        GameManager.currentLifePoints += valorVida;
    }
}
*/
using UnityEngine;
public class Vida : Recogible
{
    protected override void Recoger()
    {
        GameManager.Instancia.Curar(valor);
    }
}