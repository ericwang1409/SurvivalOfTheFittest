using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitLogic : MonoBehaviour
{
    private float hunger;
    private float thirst;

    public float diminishingTime;

    // Start is called before the first frame update
    void Start()
    {
        hunger = 100;
        thirst = 100;
    }

    // Update is called once per frame
    void Update()
    {
        hunger -= diminishingTime * Time.deltaTime;
        thirst -= diminishingTime * Time.deltaTime;

        
    }

    void FindFood()
    {
        
    }
}
