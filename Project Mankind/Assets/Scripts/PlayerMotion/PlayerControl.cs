using UnityEngine;

//SCRIPT DOS INPUTS
[RequireComponent(typeof(PlayerMotor))]
public class PlayerControl : MonoBehaviour
{
    PlayerMotor motor;

    // Use this for initialization
    void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        //Pegando inputs externos
        float inputXMov = Input.GetAxisRaw("Horizontal");
        float inputZMov = Input.GetAxisRaw("Vertical");
        Vector2 input = new Vector3(inputXMov, inputZMov); //formando vetor com inputs
        Vector2 inputDir = input.normalized;//soma dos inputs igual a um
        bool sprinting = Input.GetKey(KeyCode.LeftShift); //input de corrida
        bool walking = Input.GetKey(KeyCode.LeftControl); //input de andar
        bool jumping = Input.GetKeyDown(KeyCode.Space); //input de pular
        bool interacting = Input.GetKeyDown(KeyCode.F); //input de interagir

        motor.Move(inputDir, sprinting, walking, jumping, interacting); //passando para motor os inputs
    }

}