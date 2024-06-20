using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class Enemy1 : MonoBehaviour
{
    bool dead;
    int cnt;
    float hp;
    int speed;
    float distance;
    const float maxhp = 300;
    GameObject character;
    Animator anim;
    Collider collide;
    Rigidbody rb;
    public static int damage;
    public Slider slider;
    public GameObject floattext;
    public GameObject canvas;
    public GameObject hitparticle;
    public GameObject fireball;
    public Floating text;

    // Start is called before the first frame update
    void Start()
    {
        dead = false;
        cnt = 0;
        hp = 300;
        speed = 3;
        character = GameObject.Find("Character");
        anim = GetComponent<Animator>();
        collide = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0 && !dead)
        {
            dead = true;
            collide.enabled = false;
            rb.isKinematic = true;
            anim.CrossFadeInFixedTime("Die", 0.2f);
            Destroy(gameObject, 3f);
        }

        slider.value = hp / maxhp;

        if (hp <= 0)
            return;

        if (Player.hp <= 0) return;


        distance = Vector3.Distance(transform.position, character.transform.position);

        if (distance <= 25f)
        {
            Vector3 dir = character.transform.position - this.transform.position;
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 3);
        }

        if (distance >= 15f && distance <= 25f)
        {
            transform.position = Vector3.MoveTowards(transform.position, character.transform.position, speed * Time.deltaTime);
            anim.SetBool("Run", true);
        }

        else
            anim.SetBool("Run", false);

        if (distance <= 17f)
            anim.SetTrigger("Attack");
    }

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
            hp -= damage;
            if (cnt == 0)
                Instantiate(floattext, canvas.transform);

            if (cnt >= 3)
                cnt = 0;
            cnt++;
        }

        if (other.transform.tag == "Hyper2")
        {
            damage = Random.Range(160, 241);
            hp -= damage;
            Instantiate(floattext, canvas.transform);
        }

        if (other.transform.tag == "Hyper3")
        {
            damage = 2500;
            hp -= damage;
            Instantiate(floattext, canvas.transform);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Skill2")
        {
            damage = Random.Range(6, 11);
            hp -= damage;
            if (cnt == 0)
            {
                Instantiate(hitparticle, transform);
                Instantiate(floattext, canvas.transform);
            }
        }

        if (other.transform.tag == "Hyper")
        {
            damage = Random.Range(2, 5);
            hp -= damage;
            if (cnt == 0)
            {
                Instantiate(floattext, canvas.transform);
            }
        }
        cnt++;

        if (cnt >= 3)
            cnt = 0;
    }

    void Attack()
    {
        Instantiate(fireball, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.rotation);
    }

    IEnumerator Damage(int dmg)
    {
        yield return 0.1f;

        Instantiate(hitparticle, transform);
        hp -= dmg;
        damage = dmg;
        Instantiate(floattext, canvas.transform);
    }
}
