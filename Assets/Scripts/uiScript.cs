using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiScript : MonoBehaviour
{
    GameObject player;
    playerScript playerScriptVar;

    gameManager manager;

    GameObject uiCanvas;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("MainCamera");
        playerScriptVar = player.GetComponent<playerScript>();
        uiCanvas = GameObject.FindWithTag("canvas");


        manager = GameObject.FindWithTag("gameManager").GetComponent<gameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        uiCanvas.transform.GetChild(1).GetComponent<Image>().enabled = (playerScriptVar.nearHydrant);

        uiCanvas.transform.GetChild(2).GetComponent<Image>().enabled = (playerScriptVar.nearAnimal);

        int score = transform.gameObject.GetComponent<gameManager>().score;
        uiCanvas.transform.GetChild(3).GetComponent<Text>().text = string.Format("Score: {0:0}", score);


        float timeRemaining = manager.totalTime - Time.timeSinceLevelLoad;
        int minutes = ((int)timeRemaining) / 60;
        int seconds = ((int)timeRemaining) % 60;


        uiCanvas.transform.GetChild(4).GetComponent<Text>().text = string.Format("Timer: {0}:{1:00}", minutes, seconds);

    }


}
