namespace InfinityRunner.Scripts.Input
{
    public abstract class InputModule
    {
        public abstract bool IsJumpClick { get; }
        public abstract bool IsJumpPressed { get; }
        public abstract bool Down { get; }
        public abstract bool Up { get; }
    }
}