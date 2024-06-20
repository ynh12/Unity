using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyperSpawn : MonoBehaviour
{
    public GameObject hyper;
    float x, y, z;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 0, 0.1f);
        Destroy(gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn()
    {
        Instantiate(hyper, transform);
    }
}
