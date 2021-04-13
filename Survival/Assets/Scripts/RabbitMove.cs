using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class GlobalVars : MonoBehaviour
{
    public int rabbitSpeed = 10;
}*/

public class RabbitMove : MonoBehaviour
{
    public GameObject rabbit;
    public GameObject lion;
    public GameObject poacher;
    public int rabbitSpeed = 10;
    //public static float rabbitSpeed = 1;
    //Changing direction
    private float directionChangeInterval = 1;
    //Angle change
    private float maxHeadingChange = 120;

    //Us to control character
    CharacterController controller;
    float heading;
    Vector3 targetRotation;

    private float radius = 35;

    private Vector3 playerVelocity;

    private bool jumped;

    RabbitLogic theLogic;

    //private float closestGrass = int.MaxValue;

    private bool lookingForGrass = false;
    private bool lookingForMate = false;

    Vector3 grassPosition;

    GameObject grass;

    // Start is called before the first frame update. Each rabbit has the script so it runs for each one
    void Awake()
    {
        theLogic = gameObject.GetComponent<RabbitLogic>();


        controller = GetComponent<CharacterController>();

        //Set random initial rotation. Which way rabbit is facing
        heading = Random.Range(0, 360);
        //Changes the angle 
        transform.eulerAngles = new Vector3(0, heading, 0);
        //Delays 1 sec
        StartCoroutine(NewHeading());
    }

    // Update is called once per frame
    void Update()
    {
        if (theLogic.hunger < 50 && theLogic.hunger < theLogic.thirst)
        {
            FindGrass();
        }
        else if (theLogic.thirst < 50 && theLogic.thirst < theLogic.hunger)
        {
            FindWater();
        }
        else
        {
            IdleMotion();
        }
        if (theLogic.attraction > 50)
        {
            FindMate();
        }
    }

    void FindGrass()
    {
        //if (Physics.CheckSphere(transform.position, sphereRadius))
        Collider[] canSee = Physics.OverlapSphere(transform.position, 15);
        foreach (var detected in canSee)
        {
            if (detected.gameObject.tag == "grass" && !lookingForGrass)
            {
                lookingForGrass = true;
                grassPosition = detected.transform.position;
                grass = detected.gameObject;
                Debug.Log("detected");
            }
            else if (lookingForGrass)
            {
                transform.LookAt(grass.transform);
                Vector3 goToGrass = grassPosition - transform.position;
                goToGrass = goToGrass.normalized;
                controller.Move(goToGrass * 0.1f * Time.deltaTime);
                Debug.Log("Rabbit location: " + transform.position);
                Debug.Log("Grass location: " + grassPosition);
                Debug.Log("Moving vector: " + goToGrass);
            }
            else
            {
                IdleMotion();
            }
        }

        Collider[] objectsCollided = Physics.OverlapSphere(transform.position, RabbitLogic.sphereRadius);
        foreach (var objectC in objectsCollided)
        {
            if (objectC.gameObject.tag == "grass" && theLogic.hunger <= 50)
            {
                //transform.position = Vector3.MoveTowards(transform.position, objectC.gameObject.position, Time.deltaTime * GlobalVars.rabbitSpeed);
                //WaitForSeconds(1);
                Destroy(objectC.gameObject);
                GenerateMap.numGrass--;
                theLogic.hunger += 50;
                lookingForGrass = false;
                Debug.Log("eat");
            }
        }
        //Debug.Log("Hunger:" + hunger);
    }

    void FindWater()
    {
        Collider[] objectsCollided = Physics.OverlapSphere(transform.position, RabbitLogic.wsphereRadius);
        foreach (var objectC in objectsCollided)
        {
            if (objectC.gameObject.tag == "water" && theLogic.thirst <= 50)
            {
                Debug.Log("HI");
                theLogic.thirst += 50;
            }
        }
    }

    void FindMate()
    {
        //if (Physics.CheckSphere(transform.position, sphereRadius))
        Collider[] canSee = Physics.OverlapSphere(transform.position, 10);
        foreach (var detected in canSee)
        {
            var mate = detected.gameObject.GetComponent<RabbitLogic>();
            if (detected.gameObject.tag == "rabbit" && mate.attraction > 50 && mate.gender != theLogic.gender)
            {
                Vector3 goToGrass = Vector3.MoveTowards(transform.position, detected.transform.position, rabbitSpeed * Time.deltaTime);
                controller.Move(goToGrass * Time.deltaTime);
            }
            else
            {
                IdleMotion();
            }
        }

        Collider[] objectsCollided = Physics.OverlapSphere(transform.position, RabbitLogic.sphereRadius);
        foreach (var objectC in objectsCollided)
        {
            var mate = objectC.gameObject.GetComponent<RabbitLogic>();
            if (objectC.gameObject.tag == "rabbit" && mate.attraction > 50 && mate.gender != theLogic.gender)
            {
                //transform.position = Vector3.MoveTowards(transform.position, objectC.gameObject.position, Time.deltaTime * GlobalVars.rabbitSpeed);
                //WaitForSeconds(1);
                Vector3 position = objectC.gameObject.transform.position;
                GameObject newRabbit = Instantiate(rabbit, new Vector3(position.x, 0.432f, position.y), Quaternion.identity) as GameObject;
                theLogic.attraction = 0;
                mate.attraction = 0;
            }
        }
        //Debug.Log("Hunger:" + hunger);
    }

    void IdleMotion()
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
        controller.Move(((forward * rabbitSpeed / 10) + playerVelocity) * Time.deltaTime);
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
}
