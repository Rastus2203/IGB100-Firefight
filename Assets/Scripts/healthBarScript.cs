using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthBarScript : MonoBehaviour
{
    playerScript player;
    float maxHealth = 100;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("MainCamera").GetComponent<playerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        float currentHealth = player.health;
        transform.localScale = new Vector3((currentHealth / maxHealth), 1, 1);
    }
}
