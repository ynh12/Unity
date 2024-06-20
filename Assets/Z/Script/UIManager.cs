using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public void Resume()
    {
        UIDisplay.UI_display = false;
    }

    public void Options()
    {
        UIDisplay.Options_display = true;
    }

    public void Skill()
    {
        UIDisplay.Options_display = false;
    }

    public void Exit()
    {
        UIDisplay.ExitUI_display = true;
    }

    public void ExitUI_Yes()
    {
        Application.Quit();
    }

    public void ExitUI_No()
    {
        UIDisplay.ExitUI_display = false;
    }

    public void Disable_Options()
    {
        UIDisplay.Options_display = false;
        UIDisplay.GrapicUI_display = false;
        UIDisplay.AudioUI_display = false;
    }

    public void GrapicUI()
    {
        UIDisplay.GrapicUI_display = true;
        UIDisplay.AudioUI_display = false;
    }

    public void AudioUI()
    {
        UIDisplay.GrapicUI_display = false;
        UIDisplay.AudioUI_display = true;
    }

    public void KeybindUI()
    {
        UIDisplay.GrapicUI_display = false;
        UIDisplay.AudioUI_display = false;
    }
}
