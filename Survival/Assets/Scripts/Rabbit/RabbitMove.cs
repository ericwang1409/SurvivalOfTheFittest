using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitMove : MonoBehaviour
{
    public GameObject rabbit;
    public GameObject lion;
    public GameObject poacher;
    public int rabbitSpeed = 20;

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

    private float closestGrass = int.MaxValue;
    private float closestPond = int.MaxValue;
    private float closestRabbit = int.MaxValue;

    private bool lookingForGrass = false;
    private bool lookingForWater = false;
    private bool lookingForMate = false;

    Vector3 grassPosition;
    Vector3 pondPosition;
    Vector3 rabbitPosition;
    GameObject grass;
    GameObject pond;
    GameObject rabbitDetect;
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
        else if (theLogic.thirst < 50 && theLogic.thirst <= theLogic.hunger)
        {
            FindWater();
        }
        else if (theLogic.attraction > 50 && theLogic.hunger > 50 && theLogic.thirst > 50)
        {
            FindMate();
        }
        else
        {
            IdleMotion();
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
                if (Vector3.Distance(detected.transform.position, transform.position) < closestGrass)
                {
                    closestGrass = Vector3.Distance(grassPosition, transform.position);
                    grass = detected.gameObject;
                    grassPosition = detected.transform.position;
                }
            }
        }

        if (!lookingForGrass)
        {
            IdleMotion();
        }

        if (lookingForGrass && grass != null)
        {
            transform.LookAt(grass.transform);
            Vector3 goToGrass = grassPosition - transform.position;
            goToGrass = goToGrass.normalized;
            //goToGrass = transform.TransformDirection(goToGrass);
            controller.Move(goToGrass * Time.deltaTime);
            //Debug.Log(Time.deltaTime);
            /*Debug.Log("Rabbit location: " + transform.position);
            Debug.Log("Grass location: " + grassPosition);
            Debug.Log("Moving vector: " + goToGrass);*/
            //If contacted with the floor
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
        }
        else if (grass == null)
        {
            lookingForGrass = false;
            closestGrass = int.MaxValue;
        }

        Collider[] objectsCollided = Physics.OverlapSphere(transform.position, RabbitLogic.sphereRadius);
        foreach (var objectC in objectsCollided)
        {
            if (objectC.gameObject.tag == "grass" && theLogic.hunger <= 50)
            {
                Destroy(objectC.gameObject);
                GenerateMap.numGrass--;
                theLogic.hunger += 50;
                lookingForGrass = false;
                closestGrass = int.MaxValue;


            }
        }


    }

    void FindWater()
    {
        //if (Physics.CheckSphere(transform.position, sphereRadius))
        Collider[] canSee = Physics.OverlapSphere(transform.position, 15);
        foreach (var detected in canSee)
        {
            if (detected.gameObject.tag == "water" && !lookingForWater)
            {
                lookingForWater = true;
                if (Vector3.Distance(detected.transform.position, transform.position) < closestPond)
                {
                    closestPond = Vector3.Distance(pondPosition, transform.position);
                    pond = detected.gameObject;
                    pondPosition = detected.transform.position;
                }
            }
        }

        if (!lookingForWater)
        {
            IdleMotion();
        }

        if (lookingForWater)
        {
            transform.LookAt(pond.transform);
            Vector3 goToPond = pondPosition - transform.position;
            goToPond = goToPond.normalized;
            //goToGrass = transform.TransformDirection(goToGrass);
            controller.Move(goToPond * Time.deltaTime);
            //Debug.Log(Time.deltaTime);
            // Debug.Log("Rabbit location: " + transform.position);
            // Debug.Log("Grass location: " + grassPosition);
            // Debug.Log("Moving vector: " + goToPond);

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

            controller.Move(playerVelocity * Time.deltaTime);
        }

        Collider[] objectsCollided = Physics.OverlapSphere(transform.position, RabbitLogic.wsphereRadius);
        foreach (var objectC in objectsCollided)
        {
            if (objectC.gameObject.tag == "water" && theLogic.hunger <= 50)
            {
                theLogic.thirst += 50;

                lookingForWater = false;
                closestPond = int.MaxValue;
            }
        }

    }

    void FindMate()
    {

        //Debug.Log("Here");
        Collider[] rabbitCanSee = Physics.OverlapSphere(transform.position, 10);
        foreach (var detected in rabbitCanSee)
        {
            var mate = detected.gameObject.GetComponent<RabbitLogic>();
            var theLogic1 = detected.gameObject.GetComponent<RabbitMove>();
            if (detected.gameObject.tag == "rabbit" && mate.attraction > 50 && mate.gender != theLogic.gender && !lookingForMate)
            {
                lookingForMate = true;
                if (Vector3.Distance(detected.transform.position, transform.position) < closestRabbit)
                {
                    closestRabbit = Vector3.Distance(rabbitPosition, transform.position);
                    rabbitDetect = detected.gameObject;
                    rabbitPosition = detected.transform.position;
                }
            }
        }

        if (!lookingForMate)
        {
            IdleMotion();
        }

        if (lookingForMate && rabbitDetect.GetComponent<RabbitLogic>().attraction > 50)
        {
            transform.LookAt(rabbitDetect.transform);
            Vector3 goToRabbit = rabbitPosition - transform.position;
            goToRabbit = goToRabbit.normalized;

            controller.Move(new Vector3 (goToRabbit.x, -1, goToRabbit.z) * Time.deltaTime);

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

            controller.Move(playerVelocity * Time.deltaTime);
        }
        else if (lookingForMate && rabbitDetect.GetComponent<RabbitLogic>().attraction <= 50)
        {
            lookingForMate = false;
            closestRabbit = int.MaxValue;
        }

        Collider[] objectsCollided = Physics.OverlapSphere(transform.position, RabbitLogic.sphereRadius);
        foreach (var objectC in objectsCollided)
        {
            var mate = objectC.gameObject.GetComponent<RabbitLogic>();
            var theLogic1 = objectC.gameObject.GetComponent<RabbitMove>();

            if (objectC.gameObject.tag == "rabbit" && mate.attraction > 50 && mate.gender != theLogic.gender)
            {
                //transform.position = Vector3.MoveTowards(transform.position, objectC.gameObject.position, Time.deltaTime * GlobalVars.rabbitSpeed);
                //WaitForSeconds(1);
                Vector3 yeet = transform.position;
                GameObject newRabbit = Instantiate(rabbit, new Vector3(yeet.x, 0.2f, yeet.z), Quaternion.identity) as GameObject;
                AddAnimals.worldRabbit--;
                var newRab = newRabbit.GetComponent<RabbitLogic>();
                newRab.attraction = 0;
                newRab.hunger = 100;
                newRab.thirst = 100;

                theLogic.attraction = 0;
                mate.attraction = 0;
                lookingForMate = false;
                theLogic1.lookingForMate = false;
                closestRabbit = int.MaxValue;

            }
        }
    }

    void IdleMotion()
    {
        //Position from the center
        float distance = Vector3.Distance(transform.position, new Vector3(0, 0, 0));

        //Eurlerangles is the angle. Make the movements gradual 
        transform.eulerAngles = Vector3.Slerp(new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z), targetRotation, Time.deltaTime * directionChangeInterval);
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

    }
}
