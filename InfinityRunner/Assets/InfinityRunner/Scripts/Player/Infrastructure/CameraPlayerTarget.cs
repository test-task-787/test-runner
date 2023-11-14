using UnityEngine;

namespace InfinityRunner.Scripts.Player.Infrastructure
{
    /// <summary>
    /// Thin MonoBehaviour, that give Camera position for target
    /// </summary>
    public class CameraPlayerTarget : MonoBehaviour
    {
        private Transform _target;
        
        public void SetTarget(Transform target)
        {
            _target = target;
        }

        private void Update()
        {
            if (_target != null)
            {
                transform.position = _target.position;
            }
        }
    }
}