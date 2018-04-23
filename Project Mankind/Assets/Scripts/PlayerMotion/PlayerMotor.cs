using UnityEngine;

public class PlayerMotor : MonoBehaviour
{

    //conteiner de inputs
    bool sprinting = false;
    bool walking = false;
    bool jumping = false;
    bool interacting = false;

    //contantes de velocidade
    public float walkSpeed = 2f;
    public float runSpeed = 4f;
    public float sprintSpeed = 6f;
    float currentSpeed;//constante de velocidade atual

    //constantes da força gravitacional
    public float gravity = -12; //forca da gravidade
    float velocityY; //velocidade no eixo Y
    public float jumpHeight = 1; //altura do pulo

    Vector2 inputDir = Vector2.zero; //input do movimento no plano XZ

    CharacterController controller; //controlador do personagem
    Animator animator;
    InteractableDetector detector;
    public Transform cameraTransform; //posicao da camera


    //constantes de suavizacao de rotacao
    public float turnSmoothTime = 0.2f; //tempo para chegar na rotacao alvo
    float turnSmoothVelocity; //velocidade de rotacao

    //constantes de suavizacao de locomocao
    public float speedSmoothTime = 0.1f;
    float speedSmoothVelocity;

    [Range(0, 1)]
    public float airControlPercent; //quanto de controle o personagem tem no ar


    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        detector = GetComponent<InteractableDetector>();
    }

    //Setando os inputs de movimento
    public void Move(Vector2 inputDir, bool sprinting, bool walking, bool jumping, bool interacting)
    {
        this.inputDir = inputDir;
        this.sprinting = sprinting;
        this.walking = walking;
        this.jumping = jumping;
        this.interacting = interacting;
    }

    //animando o movimento do char baseado em seu estado de movimento e sua velocidade atual
    void AnimateRun()
    {
        float animationSpeedPercent;
        if (walking)
        {
            animationSpeedPercent = currentSpeed / walkSpeed * .5f;
        }
        else if (sprinting)
        {
            animationSpeedPercent = currentSpeed / sprintSpeed;
        }
        else
        {
            animationSpeedPercent = currentSpeed / runSpeed * .75f;
        }
        animator.SetFloat("speedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);
    }

    //update que faz se mexer
    void Update()
    {
        DoRotation();
        DoMovement();
        Jump();
        AnimateRun();
        detector.Detect(interacting);
    }

    private void DoMovement()
    {
        currentSpeed = Mathf.SmoothDamp(currentSpeed, GetTargetSpeed(), ref speedSmoothVelocity, GetModifiedSmoothTime(speedSmoothTime));

        //gravidade
        velocityY += Time.deltaTime * gravity;

        //movendo o char
        //ESSA LINHA PRECISA DE AJUSTE PARA TER INERCIA DURANTE O PULO
        Vector3 velocity = ((transform.right * this.inputDir.x) + (transform.forward * this.inputDir.y)) * currentSpeed + Vector3.up * velocityY;
        controller.Move(velocity * Time.deltaTime);
        currentSpeed = new Vector2(controller.velocity.x, controller.velocity.z).magnitude; //att a vel evitando moonwalk

        //quando cai no chao sua velocidadeY eh nula
        if (controller.isGrounded)
        {
            velocityY = 0;
        }
    }

    private void DoRotation()
    {
        float targetRotation = cameraTransform.eulerAngles.y;
        transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, GetModifiedSmoothTime(turnSmoothTime));

    }

    public void Jump()
    {
        if (jumping)
        {
            if (controller.isGrounded)
            {
                float jumpVelocity = Mathf.Sqrt(-2 * gravity * jumpHeight);
                velocityY = jumpVelocity;
            }
        }
    }


    float GetModifiedSmoothTime(float smoothTime)
    {
        if (controller.isGrounded)
        {
            return smoothTime;
        }

        if (airControlPercent == 0)
        {
            return float.MaxValue;
        }
        return smoothTime / airControlPercent;
    }

    float GetTargetSpeed()
    {
        float targetSpeed;
        if (walking)
        {
            targetSpeed = walkSpeed * this.inputDir.magnitude;
        }
        else if (sprinting)
        {
            targetSpeed = sprintSpeed * this.inputDir.magnitude;
        }
        else
        {
            targetSpeed = runSpeed * this.inputDir.magnitude;
        }

        return targetSpeed;
    }

}
