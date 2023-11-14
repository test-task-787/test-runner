using System;
using System.Collections.Generic;
using UniRx;

public static class TouchLocker
{
    private static readonly ReactiveProperty<bool> _isLocked = new(false);
    public static IReadOnlyReactiveProperty<bool> isLocked { get; } = new ReadOnlyReactiveProperty<bool>(_isLocked);

    private static readonly LinkedList<IDisposable> _lockers = new();

    public static IDisposable Lock()
    {
        var handler = new LockDisposable(Unlock);
        _lockers.AddLast(handler);
        return handler;
    }

    private static void Unlock(IDisposable disposable)
    {
        _lockers.Remove(disposable);
        _isLocked.Value = _lockers.Count > 0;
    }

    public static void UnlockAll()
    {
        foreach (var locker in _lockers)
        {
            locker.Dispose();
        }
    }
}
