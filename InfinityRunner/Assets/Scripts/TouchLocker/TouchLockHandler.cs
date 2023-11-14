using UniRx;
using UnityEngine;

internal class TouchLockHandler : MonoBehaviour
{
    private void Awake()
    {
        TouchLocker.isLocked.Subscribe(x => gameObject.SetActive(!x)).AddTo(this);
    }
}