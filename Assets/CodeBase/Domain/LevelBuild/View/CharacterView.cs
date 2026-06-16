using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace CodeBase.Domain.LevelBuild.View
{
    public class CharacterView : MonoBehaviour
    {
        public Action OnAnimationComplete;
        private const float MoveDistancePercentage = 0.33f;
        private const float AppearDuration = 1.0f;
        private const float MoveDuration = 1.0f;
        private const float yOffset = 210.5f;

        [SerializeField] private Image _visualImage;
        private RectTransform _visualTransform;
        private List<Vector3> _blockPositions = new List<Vector3>();

        public void Initialization(List<Vector3> blockPositions)
        {
            _blockPositions = blockPositions;
            _visualTransform = GetComponent<RectTransform>();
        }

        public void StartAnimation()
        {
            Sequence sequence = DOTween.Sequence();

            Vector3 initialPosition = _blockPositions.Count > 0 ? _blockPositions[0] : Vector3.zero;
            initialPosition += new Vector3(0.0f, yOffset, 0.0f);
            _visualTransform.anchoredPosition = initialPosition;
            _visualImage.color = new Color(_visualImage.color.r, _visualImage.color.g, _visualImage.color.b, 0f);

            sequence.Append(_visualImage.DOFade(1f, AppearDuration));

            MoveToPosition(1);
        }

        public void MoveToPosition(int positionIndex)
        {
            if (_blockPositions.Count > positionIndex)
            {
                Vector3 targetPosition = new Vector3(_blockPositions[positionIndex].x, _blockPositions[positionIndex].y, 0f);
                targetPosition.y = _visualTransform.anchoredPosition.y;
                float moveDistance = Mathf.Abs(targetPosition.x - _visualTransform.anchoredPosition.x) * MoveDistancePercentage;
                Vector3 intermediatePosition = new Vector3(
                    targetPosition.x > _visualTransform.anchoredPosition.x
                        ? _visualTransform.anchoredPosition.x + moveDistance
                        : _visualTransform.anchoredPosition.x - moveDistance,
                    yOffset, 0f);

                Sequence sequence = DOTween.Sequence();
                sequence.Append(_visualTransform.DOAnchorPos(intermediatePosition, MoveDuration / 2).SetEase(Ease.OutCubic));
                sequence.Join(_visualTransform.DOAnchorPos(targetPosition, MoveDuration).SetEase(Ease.InCubic));
                sequence.OnComplete(() => OnAnimationComplete?.Invoke());
            }
        }
    }
}
