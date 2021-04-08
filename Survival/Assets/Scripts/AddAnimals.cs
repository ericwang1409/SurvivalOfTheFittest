using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddAnimals : MonoBehaviour
{
    public GameObject rabbit;
    public GameObject lion;
    private int rabbitCounter = 0;
    private int lionCounter = 0;

    public int totalRabbit;
    public int totalLion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //If pressing r and rabbit count < 20
        
        if (Input.GetKeyDown(KeyCode.R) && rabbitCounter < totalRabbit)
        {
            //Random position in 35 unit sphere. Always spawns from middle
            Vector3 position = Random.insideUnitSphere * 35;
            //New rabbit object is instnatiated at that position
            GameObject newRabbit = Instantiate(rabbit, new Vector3(position.x, 0.2f, position.y), Quaternion.identity) as GameObject;
            //Scaling down the rabbit's size
            newRabbit.transform.localScale = new Vector3(7.5f, 7.5f, 7.5f);
            rabbitCounter++;
            Debug.Log(rabbitCounter);
        }

        //add lion
        if (Input.GetKeyDown(KeyCode.T) && lionCounter < totalLion)
        {
            //Random position in 35 unit sphere. Always spawns from middle
            Vector3 position = Random.insideUnitSphere * 35;
            //New rabbit object is instnatiated at that position
            GameObject newLion = Instantiate(lion, new Vector3(position.x, 0.674f, position.y), Quaternion.identity) as GameObject;
            //Scaling down the rabbit's size
            newLion.transform.localScale = new Vector3(19.14f, 19.14f, 19.14f);
            lionCounter++;
            Debug.Log(lionCounter);
        }
    }
}
