using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapRot : MonoBehaviour
{
    public GameObject player;
    public GameObject pos;
    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
        transform.LookAt(pos.transform);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }
}
