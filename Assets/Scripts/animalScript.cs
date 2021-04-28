using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animalScript : MonoBehaviour
{

    GameObject gameManager;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindWithTag("gameManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void onRescue()
    {
        gameManager.GetComponent<gameManager>().incrementAnimalScore();
        Destroy(transform.gameObject);
    }
}
