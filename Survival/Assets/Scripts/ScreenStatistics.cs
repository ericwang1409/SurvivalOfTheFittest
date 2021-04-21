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

    RabbitLogic hungerThirstValues;
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

        hungerThirstValues = freeLookCamera.Follow.gameObject.GetComponent<RabbitLogic>();
    }

    // Update is called once per frame
    void Update()
    {

        time += Time.deltaTime;

        populationRabbit.text = "Rabbits: " + AddAnimals.worldRabbit;
        populationLion.text = "Lions: " + AddAnimals.worldLion;
        hungerSlide.value = hungerThirstValues.hunger/100;
        thirstSlide.value = hungerThirstValues.thirst/100;
        timeText.text = "" + Mathf.Round(time * 100)/100;

        
    }
}
