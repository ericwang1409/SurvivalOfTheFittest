using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenStatistics : MonoBehaviour
{
    public GameObject display;

    private GameObject[] rabbits;

    Text population;

    // Start is called before the first frame update
    void Start()
    {
        population = display.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
        population.text = "Rabbit population: " + AddAnimals.worldRabbit;
    }
}