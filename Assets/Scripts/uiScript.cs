using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiScript : MonoBehaviour
{
    GameObject player;
    playerScript playerScriptVar;

    GameObject uiCanvas;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("MainCamera");
        playerScriptVar = player.GetComponent<playerScript>();
        uiCanvas = GameObject.FindWithTag("canvas");

    }

    // Update is called once per frame
    void Update()
    {
        float currentWater = playerScriptVar.waterLevel;
        uiCanvas.transform.GetChild(0).GetComponent<Text>().text = string.Format("Water: {0:0}", currentWater);

        uiCanvas.transform.GetChild(1).GetComponent<Image>().enabled = (playerScriptVar.nearHydrant);

        uiCanvas.transform.GetChild(2).GetComponent<Image>().enabled = (playerScriptVar.nearAnimal);

        float currentHealth = playerScriptVar.health;
        uiCanvas.transform.GetChild(3).GetComponent<Text>().text = string.Format("Health: {0:0}", currentHealth);

    }


}
