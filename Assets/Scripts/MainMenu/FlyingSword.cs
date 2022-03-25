using UnityEngine;
using Zenject;

namespace MainMenu
{
    public class FlyingSword : MonoBehaviour
    {
        [Inject] private SoundManager _soundManager;
    
        public void PlaySoundSword()
        {
            _soundManager.PlayKnifeThrow();
        }
    }
}
