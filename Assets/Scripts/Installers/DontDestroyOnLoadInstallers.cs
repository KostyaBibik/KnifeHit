using UnityEngine;
using Zenject;

namespace Installers
{
    public class DontDestroyOnLoadInstallers : MonoInstaller
    {
        [SerializeField] private DifficultyDeterminer difficultyDeterminer;
        [SerializeField] private SoundManager soundManager;

        public override void InstallBindings()
        {
            var difficultInstance = Container.InstantiatePrefabForComponent<DifficultyDeterminer>(difficultyDeterminer);
            Container.Bind<DifficultyDeterminer>().FromInstance(difficultInstance).AsSingle().NonLazy();
            
            var soundInstance = Container.InstantiatePrefabForComponent<SoundManager>(soundManager);
            Container.Bind<SoundManager>().FromInstance(soundInstance).AsSingle().NonLazy();
            
        }
    }
}