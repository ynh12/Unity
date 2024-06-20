using Invector.vCharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox_attack2 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.tag == "Player" && !vThirdPersonController.usingHyper)
            Player.hp -= 25f;
    }
}
