using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{

    public GameObject timeDisplayObject;
    public GameObject rabbitDisplayObject;
    public GameObject lionDisplayObject;

    Text timeDisplay;
    Text rabbitDisplay;
    Text lionDisplay;

    // Start is called before the first frame update
    void Start()
    {
        timeDisplay = timeDisplayObject.GetComponent<Text>();
        rabbitDisplay = rabbitDisplayObject.GetComponent<Text>();
        lionDisplay = lionDisplayObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        timeDisplay.text = "" + Mathf.Round(ScreenStatistics.time * 100)/100;
        rabbitDisplay.text = "" + AddAnimals.worldRabbit;
        lionDisplay.text = "" + AddAnimals.worldLion;
    }
}
