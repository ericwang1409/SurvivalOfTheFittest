using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    float timeGame;

    [SerializeField] Text Timer;
    // Start is called before the first frame update
    void Start()
    {
        timeGame = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void startTimer()
    {
        timeGame += Time.deltaTime;
        Timer.text = timeGame + " s";
    }
}
