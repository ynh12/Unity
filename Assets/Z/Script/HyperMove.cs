using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyperMove : MonoBehaviour
{
    public GameObject hyper;
    GameObject character;
    float x, y, z;
    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.Find("Character");
        hyper.transform.rotation = Quaternion.Euler(Random.Range(-45f, 45f), Random.Range(0f, 360f), 0);
        transform.position += hyper.transform.TransformDirection(0, 0, 50);
        x = transform.position.x;
        y = transform.position.y + Random.Range(-1f, 1f);
        z = transform.position.z;
        transform.position = new Vector3(x, y, z);
        transform.LookAt(new Vector3(character.transform.position.x, character.transform.position.y + 3, character.transform.position.z));
        transform.Rotate(0, Random.Range(-15f, 15f), 0);
        Destroy(gameObject, 0.4f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * 10);   
    }
}
