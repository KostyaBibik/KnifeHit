using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingSword : MonoBehaviour
{
    public void PlaySoundSword()
    {
        SoundManager.instance.PlayKnifeThrow();
    }
}
