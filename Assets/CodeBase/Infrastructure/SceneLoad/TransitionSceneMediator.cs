using System;
using UnityEngine;

namespace CodeBase.Infrastructure.SceneLoad
{
    public class TransitionSceneMediator
    {
        public Action<SceneType> OnTransitionScene;

        public void NotifyTransition(SceneType sceneType)
        {
            int subscribers = OnTransitionScene?.GetInvocationList().Length ?? 0;
            Debug.Log($"[TransitionSceneMediator] NotifyTransition({sceneType}). Subscribers: {subscribers}");
            OnTransitionScene?.Invoke(sceneType);
        }
    }
}
