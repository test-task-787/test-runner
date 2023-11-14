using InfinityRunner.Scripts.Player.View.Interfaces;
using UniRx;
using UnityEngine;

namespace InfinityRunner.Scripts.Player.View
{
    public class PlayerSkinView : PlayerView
    {
        [SerializeField] private Animator animator;

        private CompositeDisposable _disposable = new();
        private static readonly int IsDead = Animator.StringToHash("IsDead");
        private static readonly int MovementSpeed = Animator.StringToHash("MovementSpeed");
        private static readonly int State = Animator.StringToHash("State");

        public override void Link(AnimationController animationController)
        {
            _disposable.Clear();
            animationController.IsDead.Subscribe(x => animator.SetBool(IsDead, x)).AddTo(_disposable);
            animationController.MovementSpeed.Subscribe(x => animator.SetFloat(MovementSpeed, x)).AddTo(_disposable);
            animationController.StateId.Subscribe(x => animator.SetInteger(State, x)).AddTo(_disposable);
        }

        public void Unlink()
        {
            _disposable.Clear();
        }
    }
}