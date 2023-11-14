using UnityEngine;

namespace InfinityRunner.Scripts.Input
{
    /// <summary>
    /// Implementation of input for PC control
    /// </summary>
    public class PCInputModule : InputModule
    {
        public override bool IsJumpClick => UnityEngine.Input.GetKeyDown(KeyCode.Space);
        public override bool IsJumpPressed => UnityEngine.Input.GetKey(KeyCode.Space);


        public override bool Down => UnityEngine.Input.GetKey(KeyCode.DownArrow);
        public override bool Up => UnityEngine.Input.GetKey(KeyCode.UpArrow);
    }
}