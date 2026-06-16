using UnityEngine;
using DG.Tweening;

namespace CodeBase.UI.Animation
{
    public class SmoothScaleAnimation
    {
        private RectTransform _targetTransform;
        private const float maxScale = 15.5f;
        private const float animationDuration = 1.5f;
        private Tween animationTween;

        public SmoothScaleAnimation(RectTransform targetTransform)
        {
            _targetTransform = targetTransform;
        }

        public void StartAnimation()
        {
            animationTween = _targetTransform.DOScale(maxScale, animationDuration).SetLoops(-1, LoopType.Yoyo);
        }

        public void StopAnimation()
        {
            if (animationTween != null)
            {
                animationTween.Kill();
                animationTween = null;
            }
        }
    }
}
