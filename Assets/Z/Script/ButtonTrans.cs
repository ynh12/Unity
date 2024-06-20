using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Unity.VisualScripting;

public class ButtonTrans : MonoBehaviour
{

    public TMP_Text txt;
    public TMP_Text sign;
    public GameObject Button_effect;
    float size;
    // Start is called before the first frame update
    private void Start()
    {
        GameObject myEventSystem = GameObject.Find("EventSystem");
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);

        size = txt.fontSize;
    }
    public void OnMouse()
    {
        txt.color = new Color(255, 255, 255, 150 / 255f);
        txt.fontSize = size + 10;
        if(Button_effect != null)
            Button_effect.SetActive(true);
        if (sign != null)
        {
            sign.text = "¡ß";
            sign.color = new Color(255, 255, 255, 150 / 255f);
        }
    }

    public void ExitMouse()
    {
        txt.color = new Color(0, 0, 0, 200 / 255f);
        txt.fontSize = size;
        if (Button_effect != null)
            Button_effect.SetActive(false);
        if (sign != null)
        {
            sign.text = "¡Þ";
            sign.color = new Color(0, 0, 0, 200 / 255f);
        }
    }
}