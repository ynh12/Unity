using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class RemoveFallinSword : MonoBehaviour
{
    public GameObject sword;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(sword, 1.6f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
