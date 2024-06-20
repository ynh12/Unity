using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Invector.vCharacterController;
using static UnityEngine.Rendering.DebugUI;

public class Boss : MonoBehaviour
{
    float albedo;
    public static bool fight;
    public static float hp;
    public static bool attack;
    public static bool scream;
    const float maxhp = 50000;
    int pattern;
    float distance;
    float cooldown;
    bool usable;
    bool usinghyper;
    public static Animator anim;
    public GameObject character;
    public GameObject slid;
    public GameObject fill;
    public GameObject attack1;
    public GameObject attack2;
    public Slider slider;
    public Transform player;

    public GameObject flame_sit;
    public GameObject flame_fly;
    public GameObject flame_hyper;
    public GameObject hyper_effect;

    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject summoneffect;

    public GameObject panel;
    public GameObject ClearScreen;
    public GameObject btn;

    Rigidbody rb;
    Collider collide;
    // Start is called before the first frame update
    void Start()
    {
        albedo = 0;
        scream = false;
        fight = false;
        hp = 50000;
        pattern = 0;
        attack = false;
        cooldown = 10f;
        usable = false;
        usinghyper = false;
        anim = GetComponent<Animator>();
        anim.CrossFadeInFixedTime("Sleep", 0.2f);
        rb = GetComponent<Rigidbody>();
        collide = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!fight)
        {
            if (!scream) return;

            Vector3 dir = character.transform.position - this.transform.position;
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 3);
            return;
        }

        slider.value = hp / maxhp;

        if (slider.value <= 0)
            fill.SetActive(false);

        if (hp <= 0)
        {
            StartCoroutine(End());
            anim.CrossFadeInFixedTime("Die", 3f);
            fight = false;
            rb.isKinematic = true;
            collide.isTrigger = true;
        }

        if (Player.hp <= 0)
            fight = false;

        if (vThirdPersonController.usingHyper) return;

        distance = Vector3.Distance(transform.position, player.position);

        if (!attack)
            Move();

        cooldown -= Time.deltaTime;

        if (cooldown <= 0)
            usable = true;

        if (usable && distance < 10f && !attack)
        {
            Skill();
            usable = false;
        }

        if (usinghyper)
            Hyper();

    }

    public void Startfight()
    {
        slid.SetActive(true);
    }

    void Move()
    {
        Vector3 dir = character.transform.position - this.transform.position;
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 3);
        
        if (distance >= 8f)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, 5 * Time.deltaTime);
            anim.SetBool("Fly", true);
        }

        else
            anim.SetBool("Fly", false);
    }


    void Skill()
    {
        if (pattern >= 15)
        {
            if (hp <= 15000)
            {
                usinghyper = true;
                attack = true;
                hyper_effect.SetActive(true);
                Invoke("Hyper_Start", 5f);
                Invoke("Hyper_End", 15f);
                pattern = 0;
            }
        }

        if(!usinghyper)
        {
            switch (pattern % 8)
            {
            case 0: Attack(); break;
            case 1: Attack(); break;
            case 2: Attack(); break;
            case 3: Pattern1(); break;
            case 4: Attack(); break;
            case 5: Attack(); break;
            case 6: Attack(); break;
            case 7: Pattern2(); break;
            }
            pattern++;
        }

    }

    void Attack()
    {
        int rand = Random.Range(0, 11);
        if (rand <= 5)
        {
            anim.CrossFadeInFixedTime("Basic Attack", 0.2f);
            Invoke("Attack1", 0.5f);
            Invoke("UnAttack", 1f);
            cooldown = 3f;
        }

        else if (rand <= 8)
        {
            anim.CrossFadeInFixedTime("Claw Attack", 0.2f);
            Invoke("Attack2", 1.3f);
            Invoke("UnAttack", 3f);
            cooldown = 5f;
        }

        else
        {
            anim.CrossFadeInFixedTime("Flame Attack", 0.2f);
            Invoke("Flame", 0.5f);
            Invoke("UnAttack", 2f);
            cooldown = 10f;
        }

        attack = true;
    }

    void Attack1()
    {
        attack1.SetActive(true);
        Invoke("DeAttack1", 0.1f);
    }

    void DeAttack1()
    {
        attack1.SetActive(false);
    }

    void Attack2()
    {
        attack2.SetActive(true);
        Invoke("DeAttack2", 0.3f);
    }
    void DeAttack2()
    {
        attack2.SetActive(false);
    }

    void Pattern1()
    {
        Instantiate(enemy1, new Vector3(transform.position.x - 5, transform.position.y, transform.position.z), transform.rotation);
        Instantiate(enemy2, new Vector3(transform.position.x + 5, transform.position.y, transform.position.z), transform.rotation);
        Instantiate(summoneffect, new Vector3(transform.position.x - 5, transform.position.y, transform.position.z), transform.rotation);
        Instantiate(summoneffect, new Vector3(transform.position.x + 5, transform.position.y, transform.position.z), transform.rotation);
        cooldown = 5f;
    }

    void Pattern2()
    {
        anim.CrossFadeInFixedTime("Take Off", 0.2f);
        attack = true;
        Invoke("Pattern2_2", 4f);
    }

    void Pattern2_2()
    {
        anim.CrossFadeInFixedTime("Fly Flame Attack", 0.2f);
        flame_fly.SetActive(true);
        Invoke("Pattern2_3", 4f);
    }

    void Pattern2_3()
    {
        flame_fly.SetActive(false);
        attack = false;
        cooldown = 7f;
    }


    void Hyper()
    {
        transform.LookAt(character.transform);
    }

    void Hyper_Start()
    {
        flame_hyper.SetActive(true);
        hyper_effect.SetActive(false);
    }

    void Hyper_End()
    {
        flame_hyper.SetActive(false);
        cooldown = 5f;
        usinghyper = false;
        attack = false;
    }

    void Flame()
    {
        flame_sit.SetActive(true);
    }

    void UnAttack()
    {
        attack = false;
        flame_sit.SetActive(false);
    }
    IEnumerator End()
    {
        yield return new WaitForSeconds(10);

        while (albedo <= 1)
        {
            albedo += 1f / 255f;
            panel.GetComponent<Image>().color = new Color(0, 0, 0, albedo);
            yield return null;
        }
        ClearScreen.SetActive(true);
        btn.SetActive(true);
        UIDisplay.UI_display = true;
    }
}
