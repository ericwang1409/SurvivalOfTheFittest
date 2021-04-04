using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddAnimals : MonoBehaviour
{
    public GameObject rabbit;
    private int counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //If pressing B and rabbit count < 5
        if (Input.GetKeyDown(KeyCode.B) && counter < 5)
        if (Input.GetKeyDown(KeyCode.B) && counter < 20)
        {
            //Random position in 35 unit sphere. Always spawns from middle
            Vector3 position = Random.insideUnitSphere * 35;
            //New rabbit object is instnatiated at that position
            GameObject newRabbit = Instantiate(rabbit, new Vector3(position.x, 0.2f, position.y), Quaternion.identity) as GameObject;
            //Scaling down the rabbit's size
            newRabbit.transform.localScale = new Vector3(7.5f, 7.5f, 7.5f);
            counter++;
            Debug.Log(counter);
        }
    }
}
