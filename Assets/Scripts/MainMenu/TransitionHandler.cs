using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionHandler : MonoBehaviour
{
    private static readonly int FadeIn = Animator.StringToHash("FadeIn");
    
    [SerializeField] private string nextScene = "";
    [SerializeField] private bool disableFadeInAnimation = false;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void StartFade()
    {
        _animator.SetTrigger(FadeIn);
    }
    
    private void Start()
    {
        if (disableFadeInAnimation)
        {
            _animator.Play("FadeOut", 0 , 1);
        }
    }

    public void FadeinFinished()
    {
        SceneManager.LoadScene(nextScene);
    }
}