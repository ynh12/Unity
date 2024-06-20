using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHp : MonoBehaviour
{
    int cnt = 0;
    public static int damage;
    public GameObject boss;
    public GameObject floattext;
    public GameObject canvas;
    public GameObject hitparticle;
    public FloatingText text;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Attack1")
        {
            damage = Random.Range(80, 121);
            StartCoroutine(Damage(damage));
        }

        if (other.transform.tag == "Attack2")
        {
            damage = Random.Range(125, 176);
            StartCoroutine(Damage(damage));
        }

        if (other.transform.tag == "Skill1")
        {
            damage = Random.Range(4, 8);
            Boss.hp -= damage;
            if (cnt == 0)
                Instantiate(floattext, canvas.transform);

            if (cnt >= 3)
                cnt = 0;
            cnt++;
        }

        if (other.transform.tag == "Hyper2")
        {
            damage = Random.Range(160, 241);
            Boss.hp -= damage;
            Instantiate(floattext, canvas.transform);
        }

        if (other.transform.tag == "Hyper3")
        {
            damage = 2500;
            Boss.hp -= damage;
            Instantiate(floattext, canvas.transform);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Skill2")
        {
            damage = Random.Range(6, 11);
            Boss.hp -= damage;
            if(cnt == 0)
            {
                Instantiate(hitparticle, boss.transform);
                Instantiate(floattext, canvas.transform);
            }
        }

        if (other.transform.tag == "Hyper")
        {
            damage = Random.Range(2, 5);
            Boss.hp -= damage;
            if(cnt == 0)
            {
                Instantiate(floattext, canvas.transform);
            }
        }
        cnt++;

        if (cnt >= 3)
            cnt = 0;
    }

    IEnumerator Damage(int dmg)
    {
        yield return 0.1f;

        Instantiate(hitparticle, boss.transform);
        Boss.hp -= dmg;
        damage = dmg;
        Instantiate(floattext, canvas.transform);
    }
}
