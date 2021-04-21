using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class ScreenStatistics : MonoBehaviour
{
    public GameObject display;
    public GameObject hunger;
    public GameObject thirst;
    public GameObject rabbitPopulation;
    public GameObject lionPopulation;
    public GameObject timeObject;

    private GameObject[] rabbits;

    public static float time;

    Text populationRabbit;
    Text populationLion;
    Slider hungerSlide;
    Slider thirstSlide;
    Text timeText;

    RabbitLogic rabbitHungerThirstValues;
    LionLogic lionHungerThirstValues;

    public GameObject thirdPersonCamera;
    CinemachineFreeLook freeLookCamera;


    // Start is called before the first frame update
    void Start()
    {
        populationRabbit = rabbitPopulation.GetComponent<Text>();
        populationLion = lionPopulation.GetComponent<Text>();
        hungerSlide = hunger.GetComponent<Slider>();
        thirstSlide = thirst.GetComponent<Slider>();
        timeText = timeObject.GetComponent<Text>();

        //gets the third person camera and uses the hunger and thirst values based on which gameobject the camera is looking at
        freeLookCamera = thirdPersonCamera.GetComponent<CinemachineFreeLook>();

        rabbitHungerThirstValues = freeLookCamera.Follow.gameObject.GetComponent<RabbitLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        if (freeLookCamera.Follow.gameObject.CompareTag("lion")) 
        {
            lionHungerThirstValues = freeLookCamera.Follow.gameObject.GetComponent<LionLogic>();
            hungerSlide.value = lionHungerThirstValues.hunger / 100;
            thirstSlide.value = lionHungerThirstValues.thirst / 100;
        }
        else if (freeLookCamera.Follow.gameObject.CompareTag("rabbit"))
        {
            rabbitHungerThirstValues = freeLookCamera.Follow.gameObject.GetComponent<RabbitLogic>();
            hungerSlide.value = rabbitHungerThirstValues.hunger / 100;
            thirstSlide.value = rabbitHungerThirstValues.thirst / 100;
        }

        time += Time.deltaTime;

        populationRabbit.text = "Rabbits: " + AddAnimals.worldRabbit;
        populationLion.text = "Lions: " + AddAnimals.worldLion;
        timeText.text = "" + Mathf.Round(time * 100)/100;

        
    }
}
