/*
using UnityEngine;

    public class PlayerController : MonoBehaviour
    {
        public float moveSpeed = 5f;
        public float jumpForce = 5f;
        private CharacterController controller;
        private Vector3 moveDirection;
        private Animator animator;

        void Start()
        {
            controller = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Debug.Log(Input.GetAxis("Vertical"));
            moveDirection = new Vector3(horizontal, 0, vertical);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= moveSpeed;

            // Control de animaciones usando parámetros
            animator.SetFloat("Horizontal", horizontal);
            animator.SetFloat("Vertical", vertical);

            if (controller.isGrounded)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    animator.SetTrigger("Jump");
                    moveDirection.y = jumpForce;
                }
            }
            animator.SetBool("IsGrounded", controller.isGrounded);
            moveDirection.y -= 9.81f * Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);

            if (Input.GetButtonDown("Fire1"))
            {
                animator.SetTrigger("Attack");
            }
        }
    }
    */
    /*
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocidadMovimiento = 5f;
    public float fuerzaSalto = 5f;
    private CharacterController controlador;
    private Animator animador;
    private float velocidadVertical;
    public Joystick joystick; // Asigna el joystick virtual desde el editor
    public GameObject botonSalto; // Asigna el botón de salto desde el editor

    void Start()
    {
        controlador = GetComponent<CharacterController>();
        animador = GetComponent<Animator>();

        // Activar/desactivar joystick y botón de salto según la plataforma
        #if UNITY_ANDROID
            if (joystick != null) joystick.gameObject.SetActive(true);
            if (botonSalto != null) botonSalto.gameObject.SetActive(true);
        #else
            if (joystick != null) joystick.gameObject.SetActive(false);
            if (botonSalto != null) botonSalto.gameObject.SetActive(false);
        #endif
    }

    void Update()
    {
        // Salto condicional (Windows)
        #if UNITY_ANDROID
            // El salto se maneja con el botón virtual en Android
        #else
            if (controlador.isGrounded && Input.GetButtonDown("Jump"))
            {
                animador.SetTrigger("Jump");
                velocidadVertical = fuerzaSalto;
            }
        #endif

        // Movimiento horizontal condicional (Android o Windows)
        float horizontal = 0f;
        float vertical = 0f;

        #if UNITY_ANDROID
            if (joystick != null)
            {
                horizontal = joystick.Horizontal;
                vertical = joystick.Vertical;
            }
        #else
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
        #endif

        // Suavizado de la entrada del joystick
        horizontal = Mathf.Clamp(horizontal, -1f, 1f);
        vertical = Mathf.Clamp(vertical, -1f, 1f);

        Vector3 direccionMovimientoHorizontal = new Vector3(horizontal, 0, vertical);

        // Normalización de la dirección del movimiento
        if (direccionMovimientoHorizontal.magnitude > 1f)
        {
            direccionMovimientoHorizontal.Normalize();
        }

        direccionMovimientoHorizontal = transform.TransformDirection(direccionMovimientoHorizontal);
        direccionMovimientoHorizontal *= velocidadMovimiento;

        velocidadVertical -= 9.81f * Time.deltaTime;

        Vector3 movimientoFinal = new Vector3(direccionMovimientoHorizontal.x, velocidadVertical, direccionMovimientoHorizontal.z);

        animador.SetFloat("Horizontal", horizontal);
        animador.SetFloat("Vertical", vertical);
        animador.SetBool("IsGrounded", controlador.isGrounded);

        controlador.Move(movimientoFinal * Time.deltaTime);

        if (controlador.isGrounded)
        {
            velocidadVertical = 0f;
        }
    }

    // Función para el botón de salto en Android
    public void Saltar()
    {
        #if UNITY_ANDROID
            if (controlador.isGrounded)
            {
                animador.SetTrigger("Jump");
                velocidadVertical = fuerzaSalto;
            }
        #endif
    }
}
        /*
        if (Input.GetButtonDown("Fire1"))
        {
            animador.SetTrigger("Attack");
        }*/
    
    /*
using UnityEngine;

    public class PlayerController : MonoBehaviour
    {
        public float moveSpeed = 5f;
        public float jumpForce = 5f;
        private CharacterController controller;
        private Vector3 moveDirection;
        private Animator animator;

        void Start()
        {
            controller = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Debug.Log(Input.GetAxis("Vertical"));
            moveDirection = new Vector3(horizontal, 0, vertical);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= moveSpeed;

            // Control de animaciones usando parámetros
            animator.SetFloat("Horizontal", horizontal);
            animator.SetFloat("Vertical", vertical);

            if (controller.isGrounded)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    animator.SetTrigger("Jump");
                    moveDirection.y = jumpForce;
                }
            }
            animator.SetBool("IsGrounded", controller.isGrounded);
            moveDirection.y -= 9.81f * Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);

            if (Input.GetButtonDown("Fire1"))
            {
                animator.SetTrigger("Attack");
            }
        }
    }
    */
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float velocidadMovimiento = 5f;
    public float fuerzaSalto = 5f;
    private CharacterController controlador;
    private Animator animador;
    private float velocidadVertical;
    public Joystick joystick; // Asigna el joystick virtual desde el editor
    public GameObject botonSalto; // Asigna el botón de salto desde el editor

    private InputSystem_Actions playerInputActions; // Añadido

    private void Awake()
    {
        playerInputActions = new InputSystem_Actions(); // Añadido
        playerInputActions.Player.Enable(); // Añadido
    }

    void Start()
    {
        controlador = GetComponent<CharacterController>();
        animador = GetComponent<Animator>();

        // Activar/desactivar joystick y botón de salto según la plataforma
        #if UNITY_ANDROID
            if (joystick != null) joystick.gameObject.SetActive(true);
            if (botonSalto != null) botonSalto.gameObject.SetActive(true);
        #else
            if (joystick != null) joystick.gameObject.SetActive(false);
            if (botonSalto != null) botonSalto.gameObject.SetActive(false);
        #endif
    }

    void Update()
    {
        // Obtener entrada del movimiento
        Vector2 moveInput = playerInputActions.Player.Move.ReadValue<Vector2>(); // Modificado
        float horizontal = moveInput.x;
        float vertical = moveInput.y;
        #if UNITY_ANDROID
            if (joystick != null)
            {
                horizontal = joystick.Horizontal;
                vertical = joystick.Vertical;
            }
        #endif
        // Obtener entrada del salto
        if (playerInputActions.Player.Jump.triggered) // Modificado
        {
            if (controlador.isGrounded)
            {
                animador.SetTrigger("Jump");
                velocidadVertical = fuerzaSalto;
            }
        }

        // Suavizado de la entrada del joystick
        horizontal = Mathf.Clamp(horizontal, -1f, 1f);
        vertical = Mathf.Clamp(vertical, -1f, 1f);

        Vector3 direccionMovimientoHorizontal = new Vector3(horizontal, 0, vertical);

        // Normalización de la dirección del movimiento
        if (direccionMovimientoHorizontal.magnitude > 1f)
        {
            direccionMovimientoHorizontal.Normalize();
        }

        direccionMovimientoHorizontal = transform.TransformDirection(direccionMovimientoHorizontal);
        direccionMovimientoHorizontal *= velocidadMovimiento;

        velocidadVertical -= 9.81f * Time.deltaTime;

        Vector3 movimientoFinal = new Vector3(direccionMovimientoHorizontal.x, velocidadVertical, direccionMovimientoHorizontal.z);

        animador.SetFloat("Horizontal", horizontal);
        animador.SetFloat("Vertical", vertical);
        animador.SetBool("IsGrounded", controlador.isGrounded);

        controlador.Move(movimientoFinal * Time.deltaTime);

        if (controlador.isGrounded)
        {
            velocidadVertical = 0f;
        }
    }

    // Función para el botón de salto en Android
    public void Saltar()
    {
        #if UNITY_ANDROID
            if (controlador.isGrounded)
            {
                animador.SetTrigger("Jump");
                velocidadVertical = fuerzaSalto;
            }
        #endif
    }

    private void OnDisable()
    {
        playerInputActions.Player.Disable(); // Añadido
    }
}
        /*
        if (Input.GetButtonDown("Fire1"))
        {
            animador.SetTrigger("Attack");
        }*/
    