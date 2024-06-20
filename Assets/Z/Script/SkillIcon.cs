using Invector.vCharacterController;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillIcon : MonoBehaviour
{
    public Slider skill1;
    public Slider skill2;
    public Slider skill3;
    public Slider skill4;
    public Slider potion;
    public TMP_Text skill1Text;
    public TMP_Text skill2Text;
    public TMP_Text skill3Text;
    public TMP_Text skill4Text;
    public TMP_Text potionText;
    public TMP_Text potionCnt;

    void Update()
    {
        skill1.value = Player.attack2_cooldown;
        skill2.value = Player.skill1_cooldown;
        skill3.value = Player.skill2_cooldown;
        skill4.value = Player.hyper_cooldown;
        potion.value = Player.potion_cooldown;

        skill1Text.text = Player.attack2_cooldown.ToString("F1");
        skill2Text.text = Player.skill1_cooldown.ToString("F1");
        skill3Text.text = Player.skill2_cooldown.ToString("F1");
        skill4Text.text = Player.hyper_cooldown.ToString("F1");
        potionText.text = Player.potion_cooldown.ToString("F1");
        potionCnt.text = vThirdPersonController.potion.ToString();


        if (Player.attack2_cooldown <= 0)
            skill1Text.text = "";

        if (Player.skill1_cooldown <= 0)
            skill2Text.text = "";

        if (Player.skill2_cooldown <= 0)
            skill3Text.text = "";

        if (Player.hyper_cooldown <= 0)
            skill4Text.text = "";

        if (Player.potion_cooldown <= 0)
            potionText.text = "";

        if (vThirdPersonController.potion == 0)
            Player.potion_cooldown = 10f;
    }
}
