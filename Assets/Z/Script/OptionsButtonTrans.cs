using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OptionsButtonTrans : MonoBehaviour
{
    public GameObject display;
    public TMP_Text txt;
    public TMP_Text sign;
    public GameObject Button_effect;
    bool change;
    // Start is called before the first frame update
    void Start()
    {
        change = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (display.activeSelf)
            change = true;

        if (change)
        {
            txt.color = new Color(255, 255, 255, 150 / 255f);
            txt.fontSize = 70;
            Button_effect.SetActive(true);
            if (sign != null)
            {
                sign.text = "¡ß";
                sign.color = new Color(255, 255, 255, 150 / 255f);
            }
        }

        else
        {
            txt.color = new Color(0, 0, 0, 200 / 255f);
            txt.fontSize = 60;
            Button_effect.SetActive(false);
            if (sign != null)
            {
                sign.text = "¡Þ";
                sign.color = new Color(0, 0, 0, 200 / 255f);
            }
        }
    }

    public void ChangeTrue()
    {
        change = true;
    }

    public void ChangeFalse()
    {
        change = false;
    }
}
