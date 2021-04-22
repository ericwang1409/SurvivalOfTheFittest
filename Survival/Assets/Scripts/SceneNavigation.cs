using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using Cinemachine;

public class SceneNavigation : MonoBehaviour
{

    public CinemachineFreeLook banana;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // if (AddAnimals.worldRabbit == 0)
        // {
        //     endGame();
        // }

    }

    public void loadGame()
    {
        SceneManager.LoadScene("Game");
        ScreenStatistics.time = 0;
        AddAnimals.worldLion = 0;
        AddAnimals.worldRabbit = 0;
    }

    /*
    public void howToPlay()
    {
        SceneManager.LoadScene("HowToPlay");
    }
    */

    public void exitGame()
    {
        Application.Quit();
    }

    public void endGame()
    {
        SceneManager.LoadScene("EndScreen");
    }

    public void playAgain()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
