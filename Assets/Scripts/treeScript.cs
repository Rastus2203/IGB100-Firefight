using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeScript : MonoBehaviour
{
    public float health = 100f;
    public bool onFire = true;

    GameObject fireParticles;



    // Start is called before the first frame update
    void Start()
    {
        fireParticles = transform.Find("fireParticle").gameObject;
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
        } else
        {
            onFire = false;
        }
        
    }
}
