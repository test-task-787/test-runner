using InfinityRunner.CollectableAppliers;
using InfinityRunner.Scripts.Level.Interfaces;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace InfinityRunner.Scripts.Player.Infrastructure
{
    //MonoBehaviour wrapper, that check enter in any `ILevelTrigger` and then trigger CollectableApplyService logic
    public class PlayerTrigger : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        [Inject] private CollectableApplyService _collectableApplyService;
        
        private readonly CompositeDisposable _disposable = new();
        
        private void OnEnable()
        {
            _disposable.Clear();
            gameObject.OnTriggerEnter2DAsObservable().Subscribe(HandleTrigger).AddTo(_disposable);
        }

        private void OnDisable()
        {
            _disposable.Clear();
        }
        
        private void HandleTrigger(Collider2D colliderData)
        {
            var trigger = colliderData.GetComponent<ILevelTrigger>();
            if (trigger != null && _collectableApplyService != null)
            {
                _collectableApplyService.ApplyTrigger(trigger, playerController);
            }
        }
    }
}
