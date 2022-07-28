using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    private Animator animator;

    [SerializeField]
    private float movementSpeed, jumpSpeed, gravity;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 movementDirection = Vector3.zero;
    private bool playerGrounded;
    private bool pistol = false;
    private bool shooting;
    private bool running;
    private float timeToFire;

    public float timeToFireInterval;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        playerGrounded = characterController.isGrounded;
        running = false;

        //movement
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        characterController.Move(move * Time.deltaTime * movementSpeed);
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        //player rotation
        var direction = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.down);

        //pistol
        if(Input.GetKeyDown("1") && pistol == false)
        {
            pistol = true;
        }
        else if(Input.GetKeyDown("1") && pistol == true)
        {
            pistol = false;
        }

        //jumping
        if(Input.GetButton("Jump") && playerGrounded)
        {
            movementDirection.y = jumpSpeed;
        }
        movementDirection.y -= gravity;

        characterController.Move(movementDirection * Time.deltaTime);

        //shooting
        timeToFire -= Time.deltaTime;

        if (Input.GetMouseButton(0) && pistol && timeToFire <= 0)
        {
            shooting = true;
            timeToFireInterval = 0.2f;
            timeToFire = timeToFireInterval;
        }
        else
        {
            shooting = false;
        }


        //animations
        if(horizontalInput > 0 || horizontalInput < 0 || verticalInput > 0 || verticalInput < 0)
        {
            running = true;
        }
        
        animator.SetBool("Pistol", pistol);
        animator.SetBool("Running", running);
        animator.SetBool("Shooting", shooting);
    }
}
