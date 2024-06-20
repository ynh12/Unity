using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateCamera : MonoBehaviour
{
    public GameObject character;
    public static GameObject main_camera_pos;
    public static GameObject main_camera;
    public static GameObject hyper_camera;
    // Start is called before the first frame update
    void Start()
    {
        main_camera_pos = GameObject.Find("Main Camera");
        main_camera = GameObject.Find("Virtual Camera_Main");
        hyper_camera = GameObject.Find("Virtual Camera_Hyper");
        hyper_camera.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        main_camera.transform.position = main_camera_pos.transform.position;
        main_camera.transform.rotation = main_camera_pos.transform.rotation;
    }
}
