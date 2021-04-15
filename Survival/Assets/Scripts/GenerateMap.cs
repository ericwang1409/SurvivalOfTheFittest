using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
    public GameObject tree;
    public GameObject waterHole;
    public GameObject rock;
    public GameObject rock1;
    public GameObject rock2;
    public GameObject rock3;
    public GameObject grass;
   

    public static int numGrass = 0;

    // Start is called before the first frame update
    void Start()
    {
        //instantiate watering hole. 4
        for (int i = 0; i < 4; i++)
        {
            // Random position for water hole and create ibject
            Vector3 position = Random.insideUnitSphere * 35;
            Instantiate(waterHole, new Vector3(position.x, .24f, position.y), Quaternion.identity);
        }
        //instantiate trees
        for (int i = 0; i < 50; i++)
        {
            Vector3 position = Random.insideUnitSphere * 35;
            //Quanternion Eurler is to rate the object 
            Instantiate(tree, new Vector3(position.x, 0.199f, position.y), Quaternion.Euler(-90, 0, 0));
        }
        //instantiate grass
        for (int i = 0; i < 50; i++)
        {
            Vector3 position = Random.insideUnitSphere * 35;
            Instantiate(grass, new Vector3(position.x, 0.199f, position.z), Quaternion.Euler(-90, 0, 0));
            numGrass = 50;
        }
        //rock
        for (int i = 0; i < 15; i++)
        {
            Vector3 position = Random.insideUnitSphere * 35;
            Instantiate(rock, new Vector3(position.x, .665f, position.y), Quaternion.Euler(-90, 0, 0));
        }
        //rock1
        for (int i = 0; i < 15; i++)
        {
            Vector3 position = Random.insideUnitSphere * 35;
            Instantiate(rock1, new Vector3(position.x, .335f, position.y), Quaternion.Euler(-90, 0, 0));
        }
        //rock2
        for (int i = 0; i < 15; i++)
        {
            Vector3 position = Random.insideUnitSphere * 35;
            Instantiate(rock2, new Vector3(position.x, .326f, position.y), Quaternion.Euler(-90, 0, 0));
        }
        //rock3
        for (int i = 0; i < 15; i++)
        {
            Vector3 position = Random.insideUnitSphere * 35;
            Instantiate(rock3, new Vector3(position.x, .339f, position.y), Quaternion.Euler(-90, 0, 0));
        }

        StartCoroutine(AddGrass());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //keeps adding grass until a limit of 100
    IEnumerator AddGrass()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            if (numGrass < 100)
            {
                Vector3 position = Random.insideUnitSphere * 35;
                Instantiate(grass, new Vector3(position.x, 0.199f, position.y), Quaternion.Euler(-90, 0, 0));
                numGrass++;
                //Debug.Log(numGrass);
            }
        }
        
    }
}
