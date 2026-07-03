using UnityEngine;
using VContainer;
using VContainer.Unity;
using CodeBase.Domain.Character;
using CodeBase.Domain.Enemy;
using CodeBase.Domain.LevelBuild;
using CodeBase.Domain.Location;
using CodeBase.Domain.Player;
using CodeBase.Domain.Player.Input;
using CodeBase.Infrastructure.SceneLoad;
using CodeBase.Services.GamePlay;
using CodeBase.Services.StateMachine;

namespace CodeBase.Infrastructure.VContainer
{
    public class GameSceneLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            Debug.Log("[GameSceneLifetimeScope] Configure() started");
            RegisterPureServices(builder);
            RegisterSceneComponents(builder);
            Debug.Log("[GameSceneLifetimeScope] Configure() completed");
        }

        private void RegisterPureServices(IContainerBuilder builder)
        {
            builder.Register<GamePlayMediator>(Lifetime.Singleton);
            builder.Register<AbilityMediator>(Lifetime.Singleton);
            builder.Register<TransitionSceneMediator>(Lifetime.Singleton);
            builder.Register<InputHandler>(Lifetime.Singleton);
            builder.RegisterEntryPoint<DesktopInput>(Lifetime.Singleton).AsSelf();
        }

        private void RegisterSceneComponents(IContainerBuilder builder)
        {
            RegisterInHierarchyIfFound<LocationHandler>(builder, asInterfaces: true);
            RegisterInHierarchyIfFound<LevelProgressHandler>(builder);
            RegisterInHierarchyIfFound<GameFightHandler>(builder);
            RegisterInHierarchyIfFound<CharacterSpawner>(builder);
            RegisterInHierarchyIfFound<EnemySpawner>(builder);
            RegisterInHierarchyIfFound<Player>(builder);
            RegisterInHierarchyIfFound<TransitionScene>(builder);
            RegisterInHierarchyIfFound<Domain.Dice.DiceSpawner>(builder);
            RegisterInHierarchyIfFound<Services.Timer.Timer>(builder);
            RegisterInHierarchyIfFound<MainSceneMode>(builder);
        }

        private void RegisterInHierarchyIfFound<T>(IContainerBuilder builder, bool asInterfaces = false)
            where T : Component
        {
            // Проверяем существование чтобы избежать VContainerException
            if (FindObjectOfType<T>(true) == null)
            {
                Debug.LogWarning($"[GameSceneLifetimeScope] {typeof(T).Name} not found in scene, skipping.");
                return;
            }

            // RegisterComponentInHierarchy И регистрирует компонент И инжектирует в него зависимости
            if (asInterfaces)
                builder.RegisterComponentInHierarchy<T>().AsImplementedInterfaces().AsSelf();
            else
                builder.RegisterComponentInHierarchy<T>();
        }
    }
}
