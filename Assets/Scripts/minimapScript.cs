using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minimapScript : MonoBehaviour
{
    GameObject[] trees;
    GameObject[] treeIcons;


    float realToMapScalar = 1f; //200f / 60f;
    float halfMapSize = 1f; //00f;

    GameObject canvas;

    public GameObject treeIcon;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.FindWithTag("canvas").transform.GetChild(5).gameObject;
        trees = GameObject.FindGameObjectsWithTag("tree");
        treeIcons = new GameObject[trees.Length];

        for (int i = 0; i < trees.Length; i++)
        {
            treeIcons[i] = Instantiate(treeIcon, canvas.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i=0; i < trees.Length; i++)
        {
            Vector3 currentTransform = trees[i].transform.position;
            Debug.Log(currentTransform);
            //treeIcons[i].transform.localPosition = new Vector3(currentTransform.x * realToMapScalar - halfMapSize, currentTransform.z * realToMapScalar - halfMapSize + 20f, 0);

        }
    }
}
