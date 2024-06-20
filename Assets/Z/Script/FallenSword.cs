using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FallenSword : MonoBehaviour
{
    public GameObject sword;
    float x, z;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnSword", 0, 0.01f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnSword()
    {
        x = this.transform.position.x;
        z = this.transform.position.z;
        x += Random.Range(-5f, 5f);
        z += Random.Range(-5f, 5f);

        Vector3 pos = new Vector3(x, 5, z);
        Quaternion rot = Quaternion.Euler(180, 0, 0);

        Instantiate(sword, pos, rot);
    }
}
