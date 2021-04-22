using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseSettings : MonoBehaviour
{
    public GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !pauseMenu.activeSelf)
        {
            //Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (Cursor.visible && Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu();
        }

        if (pauseMenu.activeSelf)
        {
            Time.timeScale = 0;
        }
        else if (!pauseMenu.activeSelf)
        {
            Time.timeScale = 1;
        }
    }

    void PauseMenu()
    {
        pauseMenu.SetActive(true);
    }

    
}
