using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    private CharacterController controller;
    Camera cam;

    private bool hello = true;

    public GameObject rabbit;
    public GameObject lion;
    //private int hunger = 100;
    //private int thirst = 100;

    private float speed = 3f;

    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    private Vector3 playerVelocity;
    private int radius = 34;

    private bool jumped = false;

    RabbitLogic thirdPersonLogic;
    LionLogic thirdPersonLogicLion;

    AudioSource eating;
    AudioSource drinking;
    // Start is called before the first frame update
    void Start()
    {
        eating = GameObject.Find("Eat Sound Effect").GetComponent<AudioSource>();
        drinking = GameObject.Find("Drink Sound Effect").GetComponent<AudioSource>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        thirdPersonLogic = gameObject.GetComponent<RabbitLogic>();
        thirdPersonLogicLion = gameObject.GetComponent<LionLogic>();
        //get character controller
        controller = GetComponent<CharacterController>();
        AddAnimals.worldRabbit++;
        //InvokeRepeating("decreaseHunger", 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        ThirdPersonMovement();
        
        if (gameObject.CompareTag("lion"))
        {
            FindLionMate();
            FindWater();
        }
        else if (gameObject.CompareTag("rabbit"))
        {
            FindMate();
            FindGrass();
        }
        float distance = Vector3.Distance(transform.position, new Vector3(0, 0, 0));
        if (distance > radius)
        {
            //Vector from object to center
            Vector3 fromOriginToObject = transform.position - Vector3.zero;
            //Multiply by radius/distance
            fromOriginToObject *= radius / distance;

            transform.position = Vector3.zero + fromOriginToObject;

        }
    }

    void FindRabbit()
    {
        //if (Physics.CheckSphere(transform.position, sphereRadius))
        Collider[] objectsCollided = Physics.OverlapSphere(transform.position, RabbitLogic.tsphereRadius);
        foreach (var objectC in objectsCollided)
        {
            if (objectC.gameObject.tag == "rabbit" && thirdPersonLogicLion.hunger <= 50)
            {
                //transform.position = Vector3.MoveTowards(transform.position, objectC.gameObject.position, Time.deltaTime * GlobalVars.rabbitSpeed);
                //WaitForSeconds(1);
                Destroy(objectC.gameObject);
                AddAnimals.worldRabbit--;
                thirdPersonLogicLion.hunger += 50;
                eating.Play();
                //Debug.Log(RabbitLogic.hunger);

            }
        }
        //Debug.Log("Hunger:" + hunger);
    }

    void FindGrass()
    {
        //if (Physics.CheckSphere(transform.position, sphereRadius))
        Collider[] objectsCollided = Physics.OverlapSphere(transform.position, RabbitLogic.tsphereRadius);
        foreach (var objectC in objectsCollided)
        {
            if (objectC.gameObject.tag == "grass" && thirdPersonLogic.hunger <= 50)
            {
                //transform.position = Vector3.MoveTowards(transform.position, objectC.gameObject.position, Time.deltaTime * GlobalVars.rabbitSpeed);
                //WaitForSeconds(1);
                Destroy(objectC.gameObject);
                GenerateMap.numGrass--;
                thirdPersonLogic.hunger += 50;
                eating.Play();
                //Debug.Log(RabbitLogic.hunger);

            }
        }
        //Debug.Log("Hunger:" + hunger);
    }

    void FindWater()
    {
        Collider[] objectsCollided = Physics.OverlapSphere(transform.position, RabbitLogic.wsphereRadius);
        foreach (var objectC in objectsCollided)
        {
            if (objectC.gameObject.tag == "water" && thirdPersonLogic.thirst <= 50)
            {
                drinking.Play();
                thirdPersonLogic.thirst += 50;
                //Debug.Log(RabbitLogic.thirst);
            }
        }
    }

    void FindMate()
    {
        //Debug.Log(thirdPersonLogic.attraction);
        Collider[] objectsCollided = Physics.OverlapSphere(transform.position, RabbitLogic.sphereRadius);
        foreach (var objectC in objectsCollided)
        {
            //if (objectC.gameObject.tag == "rabbit")
            //{
            var mate = objectC.gameObject.GetComponent<RabbitLogic>();
            //    Debug.Log(mate.gender);
            //    Debug.Log(mate.attraction);
            //    Debug.Log(thirdPersonLogic.gender);
            // }
            if (objectC.gameObject.tag == gameObject.tag && mate.attraction > 50 && thirdPersonLogic.attraction > 50 && mate.gender != thirdPersonLogic.gender)
            {
                //Debug.Log("Here");
                Vector3 yeet = transform.position;
                GameObject newRabbit = Instantiate(rabbit, new Vector3(yeet.x, 0.2f, yeet.z), Quaternion.identity) as GameObject;
                thirdPersonLogic.attraction = 0;
                mate.attraction = 0;
                hello = false;
            }
        }
    }

    void FindLionMate()
    {
        //Debug.Log(thirdPersonLogic.attraction);
        Collider[] objectsCollided = Physics.OverlapSphere(transform.position, LionLogic.sphereRadius);
        foreach (var objectC in objectsCollided)
        {
            //if (objectC.gameObject.tag == "rabbit")
            //{
            var mate = objectC.gameObject.GetComponent<LionLogic>();
            //    Debug.Log(mate.gender);
            //    Debug.Log(mate.attraction);
            //    Debug.Log(thirdPersonLogic.gender);
            // }
            if (objectC.gameObject.tag == gameObject.tag && mate.attraction > 50 && thirdPersonLogicLion.attraction > 50 && mate.gender != thirdPersonLogicLion.gender)
            {
                //Debug.Log("Here");
                Vector3 yeet = transform.position;
                GameObject newRabbit = Instantiate(lion, new Vector3(yeet.x, 0.2f, yeet.z), Quaternion.identity) as GameObject;
                thirdPersonLogic.attraction = 0;
                mate.attraction = 0;
                hello = false;
            }
        }
    }

    void ThirdPersonMovement()
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
    /*void decreaseHunger()
    {
        if (hunger > 0)
        {
            hunger -= 1;
        }
        if (thirst > 0)
        {
            thirst -= 1;
        }
    }*/

}
