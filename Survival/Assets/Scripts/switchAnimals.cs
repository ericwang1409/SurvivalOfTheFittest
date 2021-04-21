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

    GameObject thirdPersonCamera;

    bool rabbitSwitching = false;
    bool lionSwitching = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        thirdPersonCamera = GameObject.FindGameObjectWithTag("thirdCamera");

        if (Input.GetKeyDown(KeyCode.P) && gameObject.GetComponent<ThirdPersonController>().enabled)
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
        //Debug.Log("method");

        Collider[] rabbitCanSee = Physics.OverlapSphere(transform.position, 10);

        foreach (var detected in rabbitCanSee)
        {
            //Debug.Log("for");
            //Debug.Log(detected.gameObject.Equals(gameObject));
            if (detected.CompareTag("rabbit") && !detected.gameObject.Equals(gameObject))
            {
                Debug.Log("here");
                float dist = Vector3.Distance(transform.position, detected.transform.position);
                if (dist < closestRabbitDist)
                {
                    closestRabbitDist = dist;
                    closestRabbit = detected.gameObject;
                    //Debug.Log(closestRabbit);
                    rabbitSwitching = true;
                }
            }
        }
        if (rabbitSwitching)
        {
            gameObject.GetComponent<ThirdPersonController>().enabled = false;
            if (gameObject.CompareTag("lion"))
            {
                gameObject.GetComponent<LionMove>().enabled = true;

            }
            if (gameObject.CompareTag("rabbit"))
            {
                gameObject.GetComponent<RabbitMove>().enabled = true;

            }
            Debug.Log(gameObject);
            Debug.Log(closestRabbit);
            closestRabbit.GetComponent<ThirdPersonController>().enabled = true;
            closestRabbit.GetComponent<RabbitMove>().enabled = false;
            closestRabbit.GetComponent<switchAnimals>().enabled = true;
            thirdPersonCamera.GetComponent<CinemachineFreeLook>().Follow = closestRabbit.transform;
            thirdPersonCamera.GetComponent<CinemachineFreeLook>().LookAt = closestRabbit.transform;
            gameObject.GetComponent<switchAnimals>().enabled = false;
            rabbitSwitching = false;
        }

    }

    void switchLion()
    {
        //Debug.Log("method");

        Collider[] rabbitCanSee = Physics.OverlapSphere(transform.position, 10);

        foreach (var detected in rabbitCanSee)
        {
            //Debug.Log("for");
            //Debug.Log(detected.gameObject.Equals(gameObject));
            if (detected.CompareTag("lion") && !detected.gameObject.Equals(gameObject))
            {
                //Debug.Log("here");
                float dist = Vector3.Distance(transform.position, detected.transform.position);
                if (dist < closestLionDist)
                {
                    closestLionDist = dist;
                    closestLion = detected.gameObject;
                    //Debug.Log(closestLion);
                    lionSwitching = true;
                }
            }
        }
        if (lionSwitching)
        {
            gameObject.GetComponent<ThirdPersonController>().enabled = false;
            if (gameObject.CompareTag("lion"))
            {
                gameObject.GetComponent<LionMove>().enabled = true;

            }
            if (gameObject.CompareTag("rabbit"))
            {
                gameObject.GetComponent<RabbitMove>().enabled = true;

            }
            Debug.Log(gameObject);
            Debug.Log(closestLion);
            closestLion.GetComponent<ThirdPersonController>().enabled = true;
            closestLion.GetComponent<LionMove>().enabled = false;
            closestLion.GetComponent<switchAnimals>().enabled = true;
            thirdPersonCamera.GetComponent<CinemachineFreeLook>().Follow = closestLion.transform;
            thirdPersonCamera.GetComponent<CinemachineFreeLook>().LookAt = closestLion.transform;
            gameObject.GetComponent<switchAnimals>().enabled = false;
            lionSwitching = false;
        }
    }
}
