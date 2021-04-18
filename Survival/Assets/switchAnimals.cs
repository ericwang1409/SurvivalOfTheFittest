using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class switchAnimals : MonoBehaviour
{
    GameObject closestRabbit;
    float closestRabbitDist = int.MaxValue;

    GameObject closestLion;
    float closestLionDist = int.MaxValue;

    public GameObject thirdPersonCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            switchRabbit();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            switchLion();
        }
    }

    void switchRabbit()
    {
        
        gameObject.GetComponent<ThirdPersonController>().enabled = false;
        gameObject.GetComponent<RabbitMove>().enabled = true;
        Collider[] rabbitCanSee = Physics.OverlapSphere(transform.position, 10);
        foreach (var detected in rabbitCanSee)
        {
            if (detected.CompareTag("rabbit"))
            {
                float dist = Vector3.Distance(transform.position, detected.transform.position);
                if (dist < closestRabbitDist)
                {
                    closestRabbitDist = dist;
                    closestRabbit = detected.gameObject;
                }
            }
        }
        closestRabbit.GetComponent<ThirdPersonController>().enabled = true;
        closestRabbit.GetComponent<RabbitMove>().enabled = false;
        thirdPersonCamera.GetComponent<CinemachineFreeLook>().Follow = closestRabbit.transform;
        thirdPersonCamera.GetComponent<CinemachineFreeLook>().LookAt = closestRabbit.transform; 
    }

    void switchLion()
    {

    }
}
