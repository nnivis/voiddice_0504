using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Infrastructure.SceneLoad
{
    public class WipeController : MonoBehaviour
    {
        public float circleSize = 0;
        public event Action TransitionCompleted;
        [SerializeField] private Animator _animator;
        [SerializeField] private Image _image;
        private readonly int _circleSizeId = Shader.PropertyToID("_Circle_Size");
        private bool _transitionCompletedFired = false;

        public void AnimateIn()
        {
            _transitionCompletedFired = true; // блокируем повторный fire пока играет AnimateIn
            _animator.SetTrigger("In");
        }

        public void AnimateOut()
        {
            _transitionCompletedFired = false;
            _animator.SetTrigger("Out");
        }

        private void Update()
        {
            _image.materialForRendering.SetFloat(_circleSizeId, circleSize);

            if (!_transitionCompletedFired && circleSize == 0)
            {
                TransitionCompleted?.Invoke();
                _transitionCompletedFired = true;
            }
        }
    }
}
