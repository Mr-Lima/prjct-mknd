using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float walkSpeed = 2f;
    public float runSpeed = 4f;
    public float sprintSpeed = 6f;
    public float gravity = -12;
    public float jumpHeight = 1;
    [Range(0,1)]
    public float airControlPercent;

    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;

    public float speedSmoothTime = 0.1f;
    float speedSmoothVelocity;
    float currentSpeed;
    float velocityY;

    Animator animator;
    Transform cameraT;
    CharacterController controller;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        cameraT = Camera.main.transform;
        controller = GetComponent<CharacterController>();
		
	}
	
	// Update is called once per frame
	void Update () {
        //Pegando inputs externos
        Vector2 input = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized;
        bool sprinting = Input.GetKey(KeyCode.LeftShift);
        bool walking = Input.GetKey(KeyCode.LeftControl);
        Move(inputDir, sprinting, walking);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        //animando o char YAY
        float animationSpeedPercent;
        if (walking)
        {
            animationSpeedPercent = currentSpeed / walkSpeed * .5f;
        }else if (sprinting)
        {
            animationSpeedPercent = currentSpeed / sprintSpeed;
        }
        else
        {
            animationSpeedPercent = currentSpeed / runSpeed * .75f;
        }
        animator.SetFloat("speedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);


    }

    void Move(Vector2 inputDir, bool sprinting, bool walking)
    {
        /*
        * se o input for diferente de zero
        * o char vira para o input
        */
        if (inputDir != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, GetModifiedSmoothTime(turnSmoothTime));
        }

        //se esta correndo
        float targetSpeed;
        if (walking)
        {
            targetSpeed = walkSpeed * inputDir.magnitude;
        }else if (sprinting)
        {
            targetSpeed = sprintSpeed * inputDir.magnitude;
        }else 
        {
            targetSpeed = runSpeed * inputDir.magnitude;
        }
        
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, GetModifiedSmoothTime(speedSmoothTime));

        //gravidade
        velocityY += Time.deltaTime * gravity;

        //movendo o char
        Vector3 velocity = transform.forward * currentSpeed + Vector3.up * velocityY;
        controller.Move(velocity * Time.deltaTime);
        currentSpeed = new Vector2(controller.velocity.x, controller.velocity.z).magnitude; //att a vel evitando moonwalk

        //quando cai no chao sua velocidadeY eh nula
        if (controller.isGrounded)
        {
            velocityY = 0;
        }

        
    }

    void Jump()
    {
        if (controller.isGrounded)
        {
            float jumpVelocity = Mathf.Sqrt(-2 * gravity * jumpHeight);
            velocityY = jumpVelocity;
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
}
