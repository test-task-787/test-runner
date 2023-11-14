namespace InfinityRunner.Scripts.Input
{
    public class InputFactory
    {
        public InputModule Create()
        {
            return new MobileInput();
        }
    }
}