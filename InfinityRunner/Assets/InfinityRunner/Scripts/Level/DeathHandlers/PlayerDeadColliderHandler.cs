using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace InfinityRunner.Scripts.Level.DeathHandlers
{
    /// <summary>
    /// Handler, that register Player death by collision with ground
    /// </summary>
    public class PlayerDeadColliderHandler : IDisposable
    {
        private readonly Bounds colliderBounds;
        private readonly Transform _provider;
        private readonly IDisposable _disposable;
        private readonly Transform trs;
        
        
        public bool IsDead { get; private set; }

        public void ResetDeadTrigger()
        {
            IsDead = false;
        }

        public PlayerDeadColliderHandler(Transform transform, Bounds colliderBounds)
        {
            this.colliderBounds = colliderBounds;
            trs = transform;
            _disposable = transform.gameObject.OnCollisionEnter2DAsObservable().Subscribe(OnCollisionEnter2D);
        }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(Vector2.Dot(collision.contacts[0].normal, Vector2.left) >= 0.9f)
            {
                if (Physics2D.OverlapArea(
                        trs.position + Vector3.up * colliderBounds.size.y * 0.4f +
                        Vector3.right * colliderBounds.size.x * 0.55f,
                        trs.position + Vector3.down * colliderBounds.size.y * 0.4f +
                        Vector3.right * colliderBounds.size.x * 0.55f, 1))
                {
                    var byCenter = Physics2D.Raycast(trs.position, Vector2.right, 1);

                    if (byCenter.collider == null)
                    {
                        return;
                    }
                    IsDead = true;
                }
            }
        }
        
        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}