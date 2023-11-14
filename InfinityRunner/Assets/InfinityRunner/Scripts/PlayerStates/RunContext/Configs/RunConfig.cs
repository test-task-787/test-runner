using System;

namespace InfinityRunner.Scripts.PlayerStates.RunContext.Configs
{
    /// <summary>
    /// Example state config
    /// </summary>
    [Serializable]
    public class RunConfig
    {
        public float initialSpeed = 2f;
        public float endSpeed = 5f;
        public float acceleration = 0.5f;
        public float initialJumpForce = 7f;
        public float continuousJumpForce = 15f;
        public float gravityForce = 0f; 
        public float coyotTime = 0.5f;
        public int jumpsCount = 2;
    }
}