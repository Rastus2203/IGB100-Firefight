using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{

    public int score = 0;
    int treeScore = 0;
    int animalScore = 0;

    int deltaTreeScore = 10;
    int deltaAnimalScore = 50;

    public float totalTime;
    public float startTime;

    sceneManager sceneMan;
    playerScript player;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        totalTime = 12; //60 * 1;

        
        player = GameObject.FindWithTag("MainCamera").GetComponent<playerScript>();
        sceneMan = GameObject.FindWithTag("sceneManager").GetComponent<sceneManager>();
    }

    // Update is called once per frame
    void Update()
    {
        score = treeScore + animalScore;
        checkTime();
        checkHealth();
    }

    void checkTime()
    {
        if (totalTime < (Time.time - startTime))
        {
            Cursor.lockState = CursorLockMode.None;
            sceneMan.score = score;
            sceneMan.scoreScene();
        }
    }

    void checkHealth()
    {
        if (player.health <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            sceneMan.diedScene();
        }
    }


    public void incrementTreeScore()
    {
        treeScore += deltaTreeScore;
        sceneMan.trees = treeScore / deltaTreeScore;
    }

    public void incrementAnimalScore()
    {
        animalScore += deltaAnimalScore;
        sceneMan.animals = animalScore / deltaAnimalScore;
    }

}
