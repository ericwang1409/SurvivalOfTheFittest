using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoacherMove : MonoBehaviour
{
    //public static float rabbitSpeed = 1;
    //Changing direction
    private float directionChangeInterval = 1;
    //Angle change
    private float maxHeadingChange = 90;

    private int timer = 0;

    //Us to control character
    CharacterController controller;
    float heading;
    Vector3 targetRotation;

    private float radius = 35;

    private Vector3 playerVelocity;

    private bool jumped;

    public static int poacherSpeed = 20;
    private float closestLion = int.MaxValue;
    Vector3 lionPosition;
    GameObject lion;


    // Start is called before the first frame update. Each rabbit has the script so it runs for each one
    void Awake()
    {
        //animals = GetComponent<AddAnimals>();
        controller = GetComponent<CharacterController>();
        //Set random initial rotation. Which way rabbit is facing
        heading = Random.Range(0, 360);
        //Changes the angle 
        transform.eulerAngles = new Vector3(0, heading, 0);
        //Delays 1 sec
        //StartCoroutine(NewHeading());
        InvokeRepeating("decreaseHunger", 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (AddAnimals.worldLion > 0)
        {
            FindLion();
        }

        else
        {
            idleMotion();
        }
        if (timer >= 60)
        {
            //Debug.Log("Here");
            Destroy(gameObject);
        }

    }
    void FindLion()
    {
        //if (Physics.CheckSphere(transform.position, sphereRadius))
        Collider[] canSee = Physics.OverlapSphere(transform.position, 100);
        foreach (var detected in canSee)
        {
            if (detected.gameObject.tag == "lion")
            {
                if (Vector3.Distance(detected.transform.position, transform.position) < closestLion)
                {
                    closestLion = Vector3.Distance(lionPosition, transform.position);
                    lion = detected.gameObject;
                    lionPosition = detected.transform.position;
                }
            }
        }

        transform.LookAt(lion.transform);
        Vector3 goToLion = lionPosition - transform.position;
        goToLion = goToLion.normalized;
        //goToGrass = transform.TransformDirection(goToGrass);
        controller.Move(goToLion * Time.deltaTime);
        //Debug.Log(Time.deltaTime);
        /*Debug.Log("Rabbit location: " + transform.position);
        Debug.Log("Grass location: " + grassPosition);
        Debug.Log("Moving vector: " + goToGrass);*/
        //If contacted with the floor
        Debug.Log(jumped);
        if (controller.isGrounded && !jumped)
        {
            playerVelocity.y += Mathf.Sqrt(.8f * -3f * -9.81f);
            StartCoroutine(DelayJump());
            jumped = true;
            //Debug.Log(playerVelocity.y);
        }
        else if (!controller.isGrounded)
        {
            //playerVelocity.y += -9.81f * Time.deltaTime;
            controller.Move(Vector3.down * 3 * Time.deltaTime);
        }

        //Does not update very frame
        playerVelocity.y += -9.81f * Time.deltaTime;

        controller.Move(playerVelocity * Time.deltaTime);

        Collider[] objectsCollided = Physics.OverlapSphere(transform.position, 1);
        foreach (var objectC in objectsCollided)
        {
            if (objectC.gameObject.tag == "lion")
            {
                Destroy(objectC.gameObject);
                AddAnimals.worldLion--;
                closestLion = int.MaxValue;
            }
        }
    }
    void idleMotion()
    {
        //Position from the center
        float distance = Vector3.Distance(transform.position, new Vector3(0, 0, 0));

        //Eurlerangles is the angle. Make the movements gradual 
        transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, targetRotation, Time.deltaTime * directionChangeInterval);
        //vecto forward value is 0, 0, 1
        var forward = transform.TransformDirection(Vector3.forward);
        //Vector value 0, 1, 0
        Vector3 up = transform.TransformDirection(Vector3.up);

        //If contacted with the floor
        if (controller.isGrounded && !jumped)
        {
            playerVelocity.y += Mathf.Sqrt(.8f * -3f * -9.81f);
            StartCoroutine(DelayJump());
            jumped = true;
        }
        else if (!controller.isGrounded)
        {
            //playerVelocity.y += -9.81f * Time.deltaTime;
            controller.Move(Vector3.down * 3 * Time.deltaTime);
        }

        //Does not update very frame
        playerVelocity.y += -9.81f * Time.deltaTime;
        //moves in x and y direction
        controller.Move((forward + playerVelocity) * poacherSpeed / 10 * Time.deltaTime);
        //Debug.Log("here1");

        //If the rabbbit is farther than 35 from the cetner
        if (distance > radius)
        {
            //Vector from object to center
            Vector3 fromOriginToObject = transform.position - Vector3.zero;
            //Multiply by radius/distance
            fromOriginToObject *= radius / distance;

            transform.position = Vector3.zero + fromOriginToObject;

        }
    }
    //Wait one second and then runs new heading routine
    IEnumerator NewHeading()
    {
        while (true)
        {
            NewHeadingRoutine();
            //Delays 1 sec
            yield return new WaitForSeconds(directionChangeInterval);
        }
    }
    //
    void NewHeadingRoutine()
    {
        //Caps values?
        var floor = Mathf.Clamp(heading - maxHeadingChange, 0, 360);
        var ceil = Mathf.Clamp(heading + maxHeadingChange, 0, 360);
        //Chooses random value to change direction
        heading = Random.Range(floor, ceil);
        //New rotation for rabbit
        targetRotation = new Vector3(0, heading, 0);
    }

    IEnumerator DelayJump()
    {

        yield return new WaitForSeconds(.3f);
        jumped = false;
        //Debug.Log("here");

    }

    void decreaseHunger()
    {
        timer += 1;
    }

}
