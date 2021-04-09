using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    private CharacterController controller;
    public Camera cam;

    private float speed = 3f;

    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    private Vector3 playerVelocity;

    private bool jumped = false;

    // Start is called before the first frame update
    void Start()
    {
        //get character controller
        controller = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //get inputs for rabbit control; horizontal is A and D and vertical is W and S, creates a Vector with those inputs
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;


        //if the rabbit is on the ground and the player is pressing wasd, and the rabbit did not jump yet makes it jump

        if (controller.isGrounded && (horizontal != 0 || vertical != 0) && !jumped)
        {
            playerVelocity.y += Mathf.Sqrt(.8f * -3f * -9.81f);
            StartCoroutine(DelayJump());
            jumped = true;
        }
        else if (!controller.isGrounded && horizontal == 0 && vertical == 0)
        {
            //playerVelocity.y += -9.81f * Time.deltaTime;
            controller.Move(Vector3.down * speed * Time.deltaTime);
        }

        //if player velocity is greater than -1 makes it decrease by gravity; this is so that the y velocity of the rabbit doesn't go exponentially lower
        if (playerVelocity.y >= -1)
        {
            playerVelocity.y += -9.81f * Time.deltaTime;
        }

        //moves the rabbit on the y axis (jumping)
        controller.Move(playerVelocity * Time.deltaTime);

        //if the rabbit is getting input using wasd
        if (direction.magnitude >= 0.1f)
        {
            //calculates the direction that the rabbit is moving in
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            
            //calculates the smoothing of the angle, transforms
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            //gets move direction based on target angle
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            //moves rabbit in the direction of what the camera is facing
            controller.Move((moveDir.normalized + Vector3.down) * speed * Time.deltaTime);
        }

        

    }

    //puts in a delay so that there is no double jump, triple jump
    IEnumerator DelayJump()
    {
        
        yield return new WaitForSeconds(.3f);
        jumped = false;
        //Debug.Log("here");
        
    }
}
