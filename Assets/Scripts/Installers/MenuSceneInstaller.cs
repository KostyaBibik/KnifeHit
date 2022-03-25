using UnityEngine;
using Zenject;

namespace Installers
{
    public class MenuSceneInstaller : MonoInstaller
    {
        [SerializeField] private TransitionHandler transitionHandler;
    
        public override void InstallBindings()
        {
            Container.Bind<TransitionHandler>().FromInstance(transitionHandler).AsSingle().NonLazy();
        }
    }
}