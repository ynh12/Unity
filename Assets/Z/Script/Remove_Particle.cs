using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remove_Particle : MonoBehaviour
{
    public GameObject particle;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(particle, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
