using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;

public class Options_Setting : MonoBehaviour
{
    public TMP_Dropdown Grapic1;
    public TMP_Dropdown Grapic2;
    public TMP_Dropdown Grapic3;
    public Slider Grapic4;

    public Slider Audio1; 
    public Slider Audio2;
    public Slider Audio3;

    public TMP_Text Grapic_Txt;
    public TMP_Text Audio_Volume;
    public TMP_Text Audio_Bgm;
    public TMP_Text Audio_SFX;

    public Image bright;

    // Start is called before the first frame update
    void Start()
    {
        Grapic_reset();
        Audio_reset();
    }

    // Update is called once per frame
    void Update()
    {
        Grapic_Txt.text = Grapic4.value.ToString();
        Audio_Volume.text = Audio1.value.ToString();
        Audio_Bgm.text = Audio2.value.ToString();
        Audio_SFX.text = Audio3.value.ToString();

        bright.color = new Color(0, 0, 0, (200 - 2 * Grapic4.value) / 255);
    }
        

    public void Grapic_reset()
    {
        Grapic1.value = 2;
        Grapic2.value = 1;
        Grapic3.value = 1;
        Grapic4.value = 50;
    }

    public void Audio_reset()
    {
        Audio1.value = 50;
        Audio2.value = 50;
        Audio3.value = 50;
    }
}
