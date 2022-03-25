using GameScripts;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private TransitionHandler transitionHandler;
        [SerializeField] private KnifeCounterUI knifeCounterUI;
        [SerializeField] private ProgressPlayer progressPlayer;
        [SerializeField] private UiManager uiManager;
        [SerializeField] private KnivesSpawner knifeSpawner;
        [SerializeField] private GameController gameController;
        [SerializeField] private StageCounterUI stageCounterUI;
        
        public override void InstallBindings()
        {
            Container.Bind<TransitionHandler>().FromInstance(transitionHandler).AsSingle().NonLazy();
            Container.Bind<KnifeCounterUI>().FromInstance(knifeCounterUI).AsSingle().NonLazy();
            Container.Bind<ProgressPlayer>().FromInstance(progressPlayer).AsSingle().NonLazy();
            Container.Bind<UiManager>().FromInstance(uiManager).AsSingle().NonLazy();
            Container.Bind<KnivesSpawner>().FromInstance(knifeSpawner).AsSingle().NonLazy();
            Container.Bind<GameController>().FromInstance(gameController).AsSingle().NonLazy();
            Container.Bind<StageCounterUI>().FromInstance(stageCounterUI).AsSingle().NonLazy();
        }
    }
}