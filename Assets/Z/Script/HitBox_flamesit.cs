using Invector.vCharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox_flamesit : MonoBehaviour
{
    private void OnParticleTrigger()
    {
        if (!vThirdPersonController.usingHyper)
            Player.hp -= 1;
    }
}
