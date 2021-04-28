using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeScript : MonoBehaviour
{
    public float health = 100f;
    public bool onFire = true;

    GameObject fireParticles;
    GameObject gameManager;
    GameObject lavaParticles;
    


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindWithTag("gameManager");
        fireParticles = transform.Find("fireParticle").gameObject;
        lavaParticles = transform.Find("lavaParticle").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        fireParticles.SetActive(onFire);
        float healthPercent = health / 100f;
        float fireScale = healthPercent + 0.5f;
        fireParticles.transform.localScale = new Vector3(fireScale, fireScale, fireScale);
    }

    public void damage()
    {
        if (health > 0)
        {
            health -= 50f * Time.deltaTime;
        } else if (health <= 0 && onFire)
        {
            extinguish();
        } 
        
    }

    void extinguish()
    {
        gameManager.GetComponent<gameManager>().incrementTreeScore();
        onFire = false;
        lavaParticles.gameObject.SetActive(false);

        transform.GetComponent<SphereCollider>().enabled = false;
    }
}
