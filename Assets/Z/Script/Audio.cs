using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioClip[] clip;
    AudioClip sound;
    int cnt = 0;
    // Start is called before the first frame update
    public void AudioStart(int sel)
    {
        sound = clip[sel];

        GetComponent<AudioSource>().PlayOneShot(sound, 0.3f);
    }

    public void AudioWalk()
    {
        sound = clip[cnt];
        
        GetComponent<AudioSource>().PlayOneShot(sound, 0.05f);

        cnt++;

        if (cnt == 2)
            cnt = 0;
    }
}