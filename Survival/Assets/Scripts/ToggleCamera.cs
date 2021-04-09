using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ToggleCamera : MonoBehaviour
{

    public Camera thirdPersonCamera;
    public Camera freeLookCamera;

    private bool switched;

    public GameObject rabbit;
    public GameObject freeLook;

    // Start is called before the first frame update
    void Start()
    {
        switched = false;
        freeLook.GetComponent<UnityTemplateProjects.SimpleCameraController>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B) && !switched)
        {
            freeLookCamera.enabled = true;
            thirdPersonCamera.enabled = false;
            rabbit.GetComponent<ThirdPersonController>().enabled = false;
            freeLook.GetComponent<UnityTemplateProjects.SimpleCameraController>().enabled = true;
            switched = true;
        }
        else if (Input.GetKeyDown(KeyCode.B) && switched)
        {
            freeLookCamera.enabled = false;
            thirdPersonCamera.enabled = true;
            rabbit.GetComponent<ThirdPersonController>().enabled = true;
            freeLook.GetComponent<UnityTemplateProjects.SimpleCameraController>().enabled = false;
            switched = false;
        }
    }
}
