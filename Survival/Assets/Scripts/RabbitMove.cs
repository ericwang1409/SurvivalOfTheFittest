using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitMove : MonoBehaviour
{
    private float speed = 1;
    private float directionChangeInterval = 1;
    private float maxHeadingChange = 90;

    CharacterController controller;
    float heading;
    Vector3 targetRotation;

    private float radius = 35;

    private Vector3 playerVelocity;

    // Start is called before the first frame update
    void Awake()
    {
        controller = GetComponent<CharacterController>();

        //Set random initial rotation
        heading = Random.Range(0, 360);
        transform.eulerAngles = new Vector3(0, heading, 0);

        StartCoroutine(NewHeading());
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, new Vector3(0, 0, 0));
        
        
        transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, targetRotation, Time.deltaTime * directionChangeInterval);
        var forward = transform.TransformDirection(Vector3.forward);
        Vector3 up = transform.TransformDirection(Vector3.up);
        
        if (controller.isGrounded)
        {
            playerVelocity.y += Mathf.Sqrt(.8f * -3f * -9.81f);
        }

        playerVelocity.y += -9.81f * Time.deltaTime;
        controller.Move((forward + playerVelocity) * speed * Time.deltaTime);
        //Debug.Log("here1");


        if (distance > radius)
        {
            Vector3 fromOriginToObject = transform.position - Vector3.zero;
            fromOriginToObject *= radius / distance;

            transform.position = Vector3.zero + fromOriginToObject;
            
        }

    }

    IEnumerator NewHeading()
    {
        while (true)
        {
            NewHeadingRoutine();
            yield return new WaitForSeconds(directionChangeInterval);
        }
    }

    void NewHeadingRoutine()
    {
        var floor = Mathf.Clamp(heading - maxHeadingChange, 0, 360);
        var ceil = Mathf.Clamp(heading + maxHeadingChange, 0, 360);
        heading = Random.Range(floor, ceil);
        targetRotation = new Vector3(0, heading, 0);
    }
}
