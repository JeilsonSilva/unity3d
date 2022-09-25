using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.ProBuilder;

public class movimentPlayer : MonoBehaviour
{
    CharacterController characterController;

    Vector3 Lado, Pulo, Frente;

    float MouseX, MouseY;
    public float Speed, MaxJump, TimeJump;
    private float JumpForce, Gravity;

    public Transform HeadPlayer, Cam;
    private Transform BodyPlayer;


    // Start is called before the first frame update
    void Start()
    {

        characterController = GetComponent<CharacterController>();
        BodyPlayer = GetComponent<Transform>();

        Gravity = (-2 * MaxJump) / (TimeJump * TimeJump);
        JumpForce = (2 * MaxJump) / TimeJump;

    }

    // Update is called once per frame
    void Update()
    {

        Pulo += Gravity * Time.deltaTime * Vector3.up;

        Frente = Input.GetAxisRaw("Vertical") * Speed * transform.forward;
        Lado = Input.GetAxisRaw("Horizontal") * Speed * transform.right;

        Vector3 Velocity = Lado + Pulo + Frente;

        characterController.Move(Velocity * Time.deltaTime);

        MouseX += Input.GetAxis("Mouse X") * 1.5f;
        MouseY -= Input.GetAxis("Mouse Y") * 1.5f;

        Cam.transform.localEulerAngles = new Vector3(MouseY, MouseX, 0);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (characterController.isGrounded)
        {
            Pulo = Vector3.down;
        }

        if (Pulo.y > 0 && (characterController.collisionFlags & CollisionFlags.Above) != 0)
        {
            Pulo = Vector3.down;
        }

        if (Input.GetButtonDown("Jump") && characterController.isGrounded)
        {

            Pulo = JumpForce * Vector3.up;
            Debug.Log("Pulo");

        }

    }

    private void LateUpdate()
    {
        Cam.transform.position = HeadPlayer.transform.position;

        BodyPlayer.transform.eulerAngles = new Vector3(0, Cam.transform.eulerAngles.y, 0);
    }

}
