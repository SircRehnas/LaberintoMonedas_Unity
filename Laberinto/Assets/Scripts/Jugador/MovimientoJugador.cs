/*New*/
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    public float velocidadMovimiento = 5f;
    public float fuerzaSalto = 5f;
    private CharacterController controlador;
    private Vector3 direccionMovimiento;

    void Start()
    {
        controlador = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        direccionMovimiento = new Vector3(horizontal, 0, vertical);
        direccionMovimiento = transform.TransformDirection(direccionMovimiento);
        direccionMovimiento *= velocidadMovimiento;

        if (controlador.isGrounded && Input.GetButtonDown("Jump"))
        {
            direccionMovimiento.y = fuerzaSalto;
        }

        direccionMovimiento.y -= 9.81f * Time.deltaTime;
        controlador.Move(direccionMovimiento * Time.deltaTime);
    }
}