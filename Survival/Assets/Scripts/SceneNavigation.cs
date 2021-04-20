using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class SceneNavigation : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadGame()
    {
        SceneManager.LoadScene("Game");
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
