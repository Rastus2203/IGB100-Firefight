using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreText : MonoBehaviour
{
    sceneManager sceneMan;


    // Start is called before the first frame update
    void Start()
    {
        sceneMan = GameObject.FindWithTag("sceneManager").GetComponent<sceneManager>();

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(gameObject);
        int animals = sceneMan.animals;
        int trees = sceneMan.trees;
        int score = sceneMan.score; 

        gameObject.GetComponent<Text>().text = string.Format("Trees: {0} \nAnimals: {1} \nScore: {2}", trees, animals, score );
    }
}
