using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source.Scripts.Extensions
{
    public static class AnimationExtension
    {
        private const float JumpPower = 0.5f;
        private const float PunchDuration = 0.1f;
        private const float DefaultDuration = 0.45f;

        public static void MoveAnim(Transform item, Transform parent, Vector3 localPosition, float scaleValue, Vector3 rotation, float duration = DefaultDuration, Action onComplete = null)
        {
            var anim = DOTween.Sequence();

            DOTween.Kill(item);
            item.SetParent(parent);
            anim.Append(item.DOLocalMove(localPosition, duration));
            anim.Join(item.DOScale(scaleValue, duration));
            anim.Join(item.DOLocalRotate(rotation, duration));
            anim.AppendCallback(() => onComplete?.Invoke());
            anim.Append(item.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), PunchDuration, 1));
        }
        
        public static void MoveAnim(Transform item, Transform parent, Vector3 localPosition, Vector3 rotation, float duration = DefaultDuration, Action onComplete = null)
        {
            var anim = DOTween.Sequence();

            DOTween.Kill(item);
            item.SetParent(parent);
            anim.Append(item.DOLocalMove(localPosition, duration));
            anim.Join(item.DOLocalRotate(rotation, duration));
            anim.AppendCallback(() => onComplete?.Invoke());
        }
        
        public static void MoveAnim(Transform item, Vector3 position, Vector3 localRotation, float duration = DefaultDuration, Action onComplete = null)
        {
            var anim = DOTween.Sequence();

            DOTween.Kill(item);
            anim.Append(item.DOMove(position, duration));
            anim.Join(item.DOLocalRotate(localRotation, duration));
            anim.AppendCallback(() => onComplete?.Invoke());
        }

        public static void JumpAnim(Transform item, Transform parent, Vector3 localPos, float scaleValue, Vector3 rotation, float duration = DefaultDuration, Action onComplete = null)
        {
            var anim = DOTween.Sequence();

            DOTween.Kill(item);
            item.SetParent(parent);
            
            anim.Append(item.DOLocalJump(localPos, JumpPower, 1, duration));
            anim.Join(item.DOScale(scaleValue, duration));
            anim.Join(item.DOLocalRotate(rotation, duration));
            anim.OnComplete(() =>
            {
                onComplete?.Invoke();
            });
        }
        
        public static void JumpAnim(Transform item, Vector3 pos, float scaleValue, Vector3 rotation, float duration = DefaultDuration, Action onComplete = null)
        {
            var anim = DOTween.Sequence();

            DOTween.Kill(item);
            
            anim.Append(item.DOJump(pos, JumpPower, 1, duration));
            anim.Join(item.DOScale(scaleValue, duration));
            anim.Join(item.DOLocalRotate(rotation, duration));
            anim.OnComplete(() =>
            {
                onComplete?.Invoke();
            });
        }
        
        public static void JumpSpendAnim(Transform item, Transform parent, Vector3 pos, float scaleValue, Vector3 rotation, float duration = DefaultDuration, Action onComplete = null)
        {
            var anim = DOTween.Sequence();

            DOTween.Kill(item);
            item.SetParent(parent);
            
            var randVector = new Vector3(Random.Range(-1f, 1), 0, Random.Range(-1f, 1));
            anim.Append(item.DOLocalMove(item.transform.position + randVector, duration));
            anim.Append(item.DOLocalJump(pos, JumpPower, 1, duration));
            anim.Join(item.DOScale(scaleValue, duration));
            anim.Join(item.DOLocalRotate(rotation, duration));
            anim.OnComplete(() =>
            {
                onComplete?.Invoke();
            });
        }
        
        public static void ScaleToAnim(Transform item, float startScale, float endScale, float duration = DefaultDuration, Action onComplete = null)
        {
            var anim = DOTween.Sequence();

            DOTween.Kill(item);

            anim.Append(item.DOScale(startScale, 0));
            anim.Append(item.DOScale(endScale, duration));
            anim.OnComplete(() =>
            {
                onComplete?.Invoke();
            });
        }
    }
}