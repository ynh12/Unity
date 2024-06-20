using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDisplay : MonoBehaviour
{
    public GameObject ESC_UI;
    public GameObject Options_UI;
    public GameObject Skill_UI;
    public GameObject Exit_UI;
    public GameObject Grapic_UI;
    public GameObject Audio_UI;
    public static bool UI_display;
    public static bool Options_display;
    public static bool ExitUI_display;
    public static bool GrapicUI_display;
    public static bool AudioUI_display;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        UI_display = false;
        Options_display = false;
        ExitUI_display = false;
        GrapicUI_display = false;
        AudioUI_display = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (ExitUI_display)
                ExitUI_display = false;
            else
                UI_display = !UI_display;
        }

        if (!UI_display)
        {
            Options_display = false;
            ExitUI_display = false;
            GrapicUI_display = false;
            AudioUI_display = false;
            Time.timeScale = 1;
        }

        if (UI_display)
            Time.timeScale = 0;

        ESC_UI.SetActive(UI_display);
        Options_UI.SetActive(Options_display);
        Exit_UI.SetActive(ExitUI_display);
        Grapic_UI.SetActive(GrapicUI_display);
        Audio_UI.SetActive(AudioUI_display);

        Cursor.visible = UI_display;
    }
}
