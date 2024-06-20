using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recognition : MonoBehaviour
{
    public Boss boss;
    // Start is called before the first frame update

    void Scream()
    {
        Boss.anim.CrossFadeInFixedTime("Scream", 1f);
        Invoke("Fight", 3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player" && Player.hp > 0)
        {
            Boss.scream = true;
            boss.Startfight();
            Invoke("Scream", 1f);
        }
    }

    void Fight()
    {
        Boss.fight = true;
        Boss.scream = false;
    }
}
