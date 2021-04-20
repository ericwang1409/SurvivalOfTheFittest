using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class troubleshooting : MonoBehaviour
{
    public GameObject grass;
    public GameObject rabbit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Vector3 position = Random.insideUnitSphere * 35;
            Instantiate(grass, new Vector3(position.x, 0.199f, position.z), Quaternion.Euler(-90, 0, 0));
            
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            //Random position in 35 unit sphere. Always spawns from middle
            Vector3 position = Random.insideUnitSphere * 35;
            //New rabbit object is instnatiated at that position
            Instantiate(rabbit, new Vector3(position.x, 0.2f, position.y), Quaternion.identity);
            //Scaling down the rabbit's size
            //newRabbit.transform.localScale = Vector3.one;
            
            //Debug.Log(rabbitCounter);
        }
    }
}
