using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill2Slash : MonoBehaviour
{
    public GameObject slash;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Slash", 0, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Slash()
    {
        Vector3 pos = transform.position;
        pos.x += Random.Range(-2, 3);
        pos.y += Random.Range(0, 3);
        pos.z += Random.Range(-2, 3);
        Quaternion rot = Quaternion.Euler(Random.Range(-30, 31), Random.Range(0, 361), Random.Range(-30, 31));
        Instantiate(slash, pos, rot);
    }

    
}
