using System;

internal class LockDisposable : IDisposable
{
    private Action<IDisposable> _a;

    public LockDisposable(Action<IDisposable> a)
    {
        _a = a;
    }
    
    public void Dispose()
    {
        _a.Invoke(this);
        _a = null;
    }
}