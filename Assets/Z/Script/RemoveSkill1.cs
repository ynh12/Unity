using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveSkill1 : MonoBehaviour
{
    public GameObject skill1;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(skill1, 7f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
