//using UnityEngine;
//[RequireComponent(typeof(PlayerMotor))]
//public class PlayerController : MonoBehaviour
//{

//    public float walkSpeed = 2f;
//    public float runSpeed = 4f;
//    public float sprintSpeed = 6f;
//    public float gravity = -12;
//    public float jumpHeight = 1;

//    public float mouseSensitivity = 10;

//    [Range(0, 1)]
//    public float airControlPercent;

//    public float turnSmoothTime = 0.2f;
//    float turnSmoothVelocity;

//    public float speedSmoothTime = 0.1f;
//    float speedSmoothVelocity;
//    float currentSpeed;
//    float velocityY;

//    Animator animator;
//    public Transform cameraT;
//    PlayerMotor motor;
//    CharacterController controller;

//    // Use this for initialization
//    void Start()
//    {
//        motor = GetComponent<PlayerMotor>();
//        animator = GetComponent<Animator>();
//        controller = GetComponent<CharacterController>();

//    }

//    // Update is called once per frame
//    void Update()
//    {
//        bool sprinting = Input.GetKey(KeyCode.LeftShift);
//        bool walking = Input.GetKey(KeyCode.LeftControl);

//        //Pegando inputs externos
//        float _inputXMov = Input.GetAxisRaw("Horizontal");
//        float _inputZMov = Input.GetAxisRaw("Vertical");

//        Vector3 _movX = transform.right * _inputXMov;
//        Vector3 _movZ = transform.forward * _inputZMov;
//        Vector3 _velocity = (_movX + _movZ).normalized * runSpeed;

//        //motor.setVelocity(_velocity);


//        float _inputYRot = Input.GetAxisRaw("Mouse X");

//        Vector3 _rotation = new Vector3(0f, _inputYRot, 0f) * mouseSensitivity;

//    }
//}