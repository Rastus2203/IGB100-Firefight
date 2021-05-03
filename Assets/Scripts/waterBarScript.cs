using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterBarScript : MonoBehaviour
{
    playerScript player;
    float maxWater = 100;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("MainCamera").GetComponent<playerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        float currentWater = player.waterLevel;
        transform.localScale = new Vector3((currentWater / maxWater), 1, 1);
    }
}
