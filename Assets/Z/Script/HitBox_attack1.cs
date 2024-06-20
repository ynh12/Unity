using Invector.vCharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox_attack1 : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.tag == "Player" && !vThirdPersonController.usingHyper)
            Player.hp -= 10f;
    }
}
