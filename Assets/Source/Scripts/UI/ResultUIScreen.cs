using System;
using DG.Tweening;
using Kuhpik;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.UI
{
    public class ResultUIScreen : UIScreen
    {
        [SerializeField] private GameObject window;
        [SerializeField] [BoxGroup("Animation")] float animDuration = 0.2f;

        public event Action OnScreenOpened;
        
        public override void Subscribe()
        {
            Hide(false);
        }

        public void Show()
        {
            OnScreenOpened?.Invoke();
            Open();
            window.transform.localScale = Vector3.zero;
            DOTween.Kill(window);
            window.gameObject.SetActive(true);
            window.transform.DOScale(Vector3.one, animDuration).SetUpdate(true);
        }

        public void Hide(bool needAnim = true, Action callback = null)
        {
            DOTween.Kill(window);
            window.transform.DOScale(Vector3.zero,needAnim ? animDuration : 0).OnComplete(() =>
                {
                    Close();
                    callback?.Invoke();
                });
        }
    }
}