using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Loaders
{
    public class LoadingController : MonoBehaviour
    {
        private const float Duration = 0.3f;
        [SerializeField] private CanvasGroup controller;

        private CancellationTokenSource _cts = new();

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            Show(true).Forget();
        }

        public async UniTask Show(bool isImmediately)
        {
            _cts.Cancel();
            _cts = new CancellationTokenSource();
            var ct = _cts.Token;
            controller.blocksRaycasts = true;
            
            if (isImmediately)
            {
                await controller.DOFade(1f, Duration).ToUniTask(TweenCancelBehaviour.Kill, ct);
            }
            else
            {
                controller.alpha = 1f;
            }
        }

        public async UniTask Hide(bool isImmediately)
        {
            _cts.Cancel();
            _cts = new CancellationTokenSource();
            var ct = _cts.Token;
            controller.blocksRaycasts = true;
            
            if (isImmediately)
            {
                await controller.DOFade(0f, Duration).ToUniTask(TweenCancelBehaviour.Kill, ct);
                controller.blocksRaycasts = false;
            }
            else
            {
                controller.alpha = 0f;
                controller.blocksRaycasts = false;
            }
        }
    }
}
