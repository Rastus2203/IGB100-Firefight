using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    public int animals = 0;
    public int trees = 0;
    public int score = 0;


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void gameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void menuScene()
    {
        SceneManager.LoadScene("mainMenu");
    }

    public void helpScene()
    {
        SceneManager.LoadScene("helpMenu");
    }

    public void diedScene()
    {
        SceneManager.LoadScene("diedMenu");
    }

    public void scoreScene()
    {
        SceneManager.LoadScene("scoreScene");
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
