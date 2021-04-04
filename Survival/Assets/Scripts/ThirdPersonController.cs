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

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (controller.isGrounded && (horizontal != 0 || vertical != 0))
        {
            playerVelocity.y += Mathf.Sqrt(.8f * -3f * -9.81f);
        }

        playerVelocity.y += -9.81f * Time.deltaTime;

        controller.Move(playerVelocity * Time.deltaTime);
        
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            
            //Debug.Log(cam.eulerAngles);

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            controller.Move((moveDir.normalized + Vector3.down) * speed * Time.deltaTime);
        }

        

    }
}
