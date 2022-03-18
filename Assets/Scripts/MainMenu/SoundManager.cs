using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private AudioClip btnClip;
    [SerializeField] private AudioClip timberClip;
    [SerializeField] private AudioClip appleClip;
    [SerializeField] private AudioClip knifeHitClip;
    [SerializeField] private AudioClip knifeThrowClip;
    
    private AudioSource efxSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            efxSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void PlaySingle(AudioClip clip, float vol = 1f)
    {
        if (clip != null)
        {
            efxSource.PlayOneShot(clip, vol);
        }
    }

    public void PlayBtnSfx()
    {
        PlaySingle(btnClip);
    }

    public void PlayTimberHit()
    {
        PlaySingle(timberClip, 0.8f);
    }
    
    public void PlayAppleHit()
    {
        PlaySingle(appleClip);
    }
    
    public void PlayKnifeHit()
    {
        PlaySingle(knifeHitClip);
    }
    
    public void PlayKnifeThrow()
    {
        PlaySingle(knifeThrowClip);
    }
}