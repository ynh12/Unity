using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasLook : MonoBehaviour
{
    GameObject player;
    public GameObject canvas;

    private void Start()
    {
        player = GameObject.Find("Main Camera");
    }
    void Update()
    {
        canvas.transform.LookAt(player.transform.position);
    }
}
