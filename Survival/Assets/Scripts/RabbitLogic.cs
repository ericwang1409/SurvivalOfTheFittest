using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitLogic : MonoBehaviour
{
    private int hunger = 3;
    private int thirst = 1000;
    private int sphereRadius = 3;

    public Animator rabbitAnimate;

    public bool dying = false;

    // Start is called before the first frame update
    void Start()
    {
        //rabbitAnimate = gameObject.GetComponent<Animator>();
        InvokeRepeating("decreaseHunger", 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(hunger);
        if(hunger < 40){
            GlobalVars.rabbitSpeed = 6;
        }
        else{
            GlobalVars.rabbitSpeed = 10;
        }
        if(hunger <= 0){
            dying = true;
            if (dying)
            {
                Debug.Log(dying);
                StartCoroutine(dyingAnimation());
            }
            dying = false;
        }
        if(thirst < 400){
            GlobalVars.rabbitSpeed = 6;
        }
        else{
            GlobalVars.rabbitSpeed = 10;
        }
        if(thirst <= 10){
            Destroy(gameObject);
        }

        //if (Physics.CheckSphere(transform.position, sphereRadius))
        Collider[] objectsCollided = Physics.OverlapSphere(transform.position, sphereRadius);
        foreach (var objectC in objectsCollided)
        {
            //if(objectC.gameObject.name != "CharacterController" && objectC.gameObject.name != "ThirdPersonPlayer" && objectC.gameObject.name != "island_G4C"){
            //Debug.Log(objectC);
                //Destroy(objectC.gameObject);
            //}
            //Debug.Log(hunger);
    
            if (objectC.gameObject.name == "grass(Clone)" && hunger <= 50){
                //transform.position = Vector3.MoveTowards(transform.position, objectC.gameObject.position, Time.deltaTime * GlobalVars.rabbitSpeed);
                //WaitForSeconds(1);
                Destroy(objectC.gameObject);
                GenerateMap.numGrass--;
                hunger += 50;
            }
            
            //Debug.Log(thirst);
            if (objectC.gameObject.name == "watering hole(Clone)" && thirst <= 500){
                Debug.Log("HI");
                thirst += 500;
            }
        }
        //Debug.Log(hunger);
         
    }

    void decreaseHunger(){
        if (hunger > 0){
            hunger -= 1;
        }
        if (thirst > 0){
            thirst -= 15;
        }
    }

    IEnumerator dyingAnimation()
    {
        Debug.Log("here");
        rabbitAnimate.SetBool("died", true);
        yield return new WaitForSeconds(0.65f);
        
        Destroy(gameObject);
        Debug.Log("here2");

    }
}
