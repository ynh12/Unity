using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PresentTime : MonoBehaviour
{
    public TMP_Text txt;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        txt.text = DateTime.Now.ToString(("HH:mm"));
    }
}
