using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LionLogic : MonoBehaviour
{
    public float hunger = 100;
    public float thirst = 100;
    public float attraction = 0;

    public string gender;
    public static int sphereRadius = 1;
    public static float wsphereRadius = 1.5f;

    public static int tsphereRadius = 1;

    public Animator lionAnimate;

    public bool dying = false;

    LionMove movement;

    // Start is called before the first frame update
    void Start()
    {
        movement = gameObject.GetComponent<LionMove>();
        //globalVariables = gameObject.GetComponent<GlobalVars>();
        //rabbitAnimate = gameObject.GetComponent<Animator>();
        InvokeRepeating("decreaseHunger", 1.0f, 1.0f);
        int generate = Random.Range(0, 2);
        if (generate == 0)
        {
            gender = "female";
        }
        else if (generate == 1)
        {
            gender = "male";
        }

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(hunger);
        if (hunger < 40)
        {
            movement.lionSpeed = 10;
        }
        else
        {
            movement.lionSpeed = 15;
        }
        if (hunger <= 0)
        {
            dying = true;
            if (dying)
            {
                StartCoroutine(dyingAnimation());
            }
            dying = false;
        }
        if (thirst < 40)
        {
            movement.lionSpeed = 10;
        }
        else
        {
            movement.lionSpeed = 15;
        }
        if (thirst <= 0)
        {
            StartCoroutine(dyingAnimation());
        }
    }

    void decreaseHunger()
    {
        if (hunger > 0)
        {
            hunger -= 2;
        }
        if (thirst > 0)
        {
            thirst -= 2;
        }
        attraction += 2;
    }

    IEnumerator dyingAnimation()
    {
        lionAnimate.SetBool("died", true);
        yield return new WaitForSeconds(0.65f);

        gameObject.SetActive(false);
        if (gameObject.GetComponent<ThirdPersonController>().enabled)
        {
            SceneManager.LoadScene("EndScreen");
        }
        AddAnimals.worldLion--;
    }
}
