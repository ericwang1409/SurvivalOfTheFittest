using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitLogic : MonoBehaviour
{
    private int hunger = 100;
    private int thirst = 100;
    private int sphereRadius = 3;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("decreaseHunger", 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (hunger < 40)
        {
            GlobalVars.rabbitSpeed = 6;
        }
        else
        {
            GlobalVars.rabbitSpeed = 10;
        }
        if (hunger <= 0)
        {
            Destroy(gameObject);
        }
        if (thirst < 40)
        {
            GlobalVars.rabbitSpeed = 6;
        }
        else
        {
            GlobalVars.rabbitSpeed = 10;
        }
        if (thirst <= 0)
        {
            Destroy(gameObject);
        }

        //if (Physics.CheckSphere(transform.position, sphereRadius))
        Collider[] objectsCollided = Physics.OverlapSphere(transform.position, sphereRadius);
        foreach (var objectC in objectsCollided)
        {
            if (objectC.gameObject.name != "CharacterController" && objectC.gameObject.name != "ThirdPersonPlayer" && objectC.gameObject.name != "island_G4C")
            {
                Debug.Log(objectC);
                //Destroy(objectC.gameObject);
            }
            //Debug.Log(hunger);

            if (objectC.gameObject.tag == "grass" && hunger <= 50)
            {
                //transform.position = Vector3.MoveTowards(transform.position, objectC.gameObject.position, Time.deltaTime * GlobalVars.rabbitSpeed);
                //WaitForSeconds(1);
                Destroy(objectC.gameObject);
                GenerateMap.numGrass--;
                hunger += 50;
            }

            //Debug.Log(thirst);
            if (objectC.gameObject.tag == "water" && thirst <= 50)
            {
                Debug.Log("HI");
                thirst += 50;
            }
        }
        //Debug.Log(hunger);

    }

    void decreaseHunger()
    {
        if (hunger > 0)
        {
            hunger -= 1;
        }
        if (thirst > 0)
        {
            thirst -= 1;
        }
    }
}
