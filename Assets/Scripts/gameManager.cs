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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        score = treeScore + animalScore;
    }

    public void incrementTreeScore()
    {
        treeScore += deltaTreeScore;
    }

    public void incrementAnimalScore()
    {
        animalScore += deltaAnimalScore;
    }

}
