using UnityEngine;

namespace InfinityRunner.Scripts.Level.DeathHandlers
{
    /// <summary>
    /// Handler, that register Player death by y-position. 
    /// </summary>
    public class PlayerDeadZoneHandler
    {
        private readonly Transform _provider;
        private const float DeathLevel = -10f;

        public PlayerDeadZoneHandler(Transform provider)
        {
            _provider = provider;
        }


        public bool IsInDeadZone => (_provider.position.y < DeathLevel);
    }
}