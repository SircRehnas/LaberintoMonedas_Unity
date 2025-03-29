/*
using UnityEngine;

public class Moneda : MonoBehaviour
{
    public int valorMoneda;
    private AudioSource sonidoMoneda;
    void Start (){
        // Inicializa el componente AudioSource
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
        GameManager.currentNumberCollectedPoints += valorMoneda;
        GameManager.currentNumberCollectedCoins ++;
    }
}
*/
using UnityEngine;
public class Moneda : Recogible
{
    protected override void Recoger()
    {
        Debug.Log("Moneda.Recoger() llamado");
        GameManager.Instancia.RecogerMoneda(valor);
    }
}