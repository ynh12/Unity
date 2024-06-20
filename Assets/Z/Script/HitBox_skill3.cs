using Invector.vCharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox_skill3 : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnParticleTrigger()
    {
        if (!vThirdPersonController.usingHyper)
            Player.hp -= 10;
    }
}
