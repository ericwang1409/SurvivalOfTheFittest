using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddAnimals : MonoBehaviour
{
    public GameObject rabbit;
    public GameObject lion;
    public GameObject poacher;
    private int rabbitCounter = 0;
    private int lionCounter = 0;
    private int poacherCounter = 0;

    public int totalRabbit;
    public int totalLion;
    public int totalPoacher;

    public static int worldRabbit;
    public static int worldLion;
    public static int worldPoacher;

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
            Instantiate(rabbit, new Vector3(position.x, 0.2f, position.y), Quaternion.identity);
            //Scaling down the rabbit's size
            //newRabbit.transform.localScale = Vector3.one;
            rabbitCounter++;
            worldRabbit++;
            //Debug.Log(rabbitCounter);
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
            worldLion++;
            //Debug.Log(lionCounter);
        }

        //add poacher
        if (Input.GetKeyDown(KeyCode.Y) && poacherCounter < totalPoacher)
        {
            //Random position in 35 unit sphere. Always spawns from middle
            Vector3 position = Random.insideUnitSphere * 35;
            //New rabbit object is instnatiated at that position
            GameObject newPoacher = Instantiate(poacher, new Vector3(position.x, 0.432f, position.y), Quaternion.identity) as GameObject;
            //Scaling down the rabbit's size
            newPoacher.transform.localScale = new Vector3(0.1117118f, 0.1117118f, 0.1117118f);
            poacherCounter++;
            worldPoacher++;
            //Debug.Log(poacherCounter);
        }
    }
}
