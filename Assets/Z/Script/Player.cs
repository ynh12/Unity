using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    const float maxhp = 100;
    public static float hp;
    public static bool stuned;
    public static float attack_cooldown = 0;
    public static float attack2_cooldown = 0;
    public static float skill1_cooldown = 0;
    public static float skill2_cooldown = 0;
    public static float hyper_cooldown = 0;
    public static float potion_cooldown = 0;
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        hp = 100;
        stuned = false;
    }

    // Update is called once per frame
    void Update()
    {
        Cooldown();
        HP();
    }

    void Cooldown()
    {
        if (attack_cooldown > 0)
            attack_cooldown -= Time.deltaTime;
        if (attack2_cooldown > 0)
            attack2_cooldown -= Time.deltaTime;
        if (skill1_cooldown > 0)
            skill1_cooldown -= Time.deltaTime;
        if (skill2_cooldown > 0)
            skill2_cooldown -= Time.deltaTime;
        if (hyper_cooldown > 0)
            hyper_cooldown -= Time.deltaTime;
        if (potion_cooldown > 0)
            potion_cooldown -= Time.deltaTime;
    }
    void HP()
    {
        slider.value = hp / maxhp;
    }
}
