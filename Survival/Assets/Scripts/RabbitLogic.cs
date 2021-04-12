using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitLogic : MonoBehaviour
{
    public int hunger = 50;
    public int thirst = 100;
    public static int sphereRadius = 1;
    public static int wsphereRadius = 1;

    public static int tsphereRadius = 1;

    public Animator rabbitAnimate;

    public bool dying = false;

    RabbitMove movement;

    // Start is called before the first frame update
    void Start()
    {
        movement = gameObject.GetComponent<RabbitMove>();
        //globalVariables = gameObject.GetComponent<GlobalVars>();
        //rabbitAnimate = gameObject.GetComponent<Animator>();
        InvokeRepeating("decreaseHunger", 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(hunger);
        if (hunger < 40)
        {
            movement.rabbitSpeed = 6;
        }
        else
        {
            movement.rabbitSpeed = 10;
        }
        if (hunger <= 0)
        {
            dying = true;
            if (dying)
            {
                StartCoroutine(dyingAnimation());
            }
            dying = false;
        }
        if (thirst < 40)
        {
            movement.rabbitSpeed = 6;
        }
        else
        {
            movement.rabbitSpeed = 10;
        }
        if (thirst <= 0)
        {
            Destroy(gameObject);
            AddAnimals.worldRabbit--;
        }

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

    IEnumerator dyingAnimation()
    {
        rabbitAnimate.SetBool("died", true);
        yield return new WaitForSeconds(0.65f);

        Destroy(gameObject);
        AddAnimals.worldRabbit--;
    }
}
