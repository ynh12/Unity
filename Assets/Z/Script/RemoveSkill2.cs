using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveSkill2 : MonoBehaviour
{
    public GameObject slash;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(slash, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
