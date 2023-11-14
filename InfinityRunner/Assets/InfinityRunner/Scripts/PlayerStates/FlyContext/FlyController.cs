using System.Linq;
using InfinityRunner.Scripts.Input;
using InfinityRunner.Scripts.Player;
using InfinityRunner.Scripts.Player.Infrastructure;
using UnityEngine;

namespace InfinityRunner.Scripts.PlayerStates.FlyContext
{
    // Fly business logic.
    public class FlyController
    {
        private float _flySpeed = 5;
        private float targetYPosition = 5f;
        private float minYPosition = 3;
        private float maxYPosition = 10;
        private float ySpeed = 3f;
        
        private readonly PlayerStats _playerStats;
        private readonly Rigidbody2D rb;
        private readonly Transform trs;
        private readonly InputModule _inputModule;

        public FlyController(
            PlayerStats playerStats,
            Rigidbody2D rb, 
            Transform trs, 
            InputModule inputModule)
        {
            _playerStats = playerStats;
            this.rb = rb;
            this.trs = trs;
            _inputModule = inputModule;
            _playerStats.CalculatedSpeed = new Vector2(_flySpeed, _playerStats.CalculatedSpeed.y);
        }

        public void OnPhysicsTick(float dt)
        {
            var currentYPosition = trs.position.y;
            var speed = _flySpeed;
            var velocity = _playerStats.CalculatedSpeed;
            velocity.x = speed;
            velocity.y = 0f;
            _playerStats.CalculatedSpeed = new Vector2(speed, 0f);

            var deltaTarget = targetYPosition - currentYPosition;
            var calculatedYSpeed = Mathf.Sign(deltaTarget) * Mathf.Min(ySpeed, Mathf.Abs(deltaTarget / dt));
            var velocityXMultiplier = _playerStats.SpeedMultipiers.Aggregate(1f, (current, m) => current * m);
            rb.velocity = new Vector2(velocity.x * velocityXMultiplier, calculatedYSpeed);
        }
        
        public void OnTick(float dt)
        {
            _playerStats.ActualSpeed = rb.velocity;

            if (_inputModule.Down || _inputModule.Up)
            {
                if (_inputModule.Down)
                {
                    targetYPosition -= 2f * dt;
                }

                if (_inputModule.Up)
                {
                    targetYPosition += 2f * dt;
                }
                
                targetYPosition = Mathf.Clamp(targetYPosition, minYPosition, maxYPosition);
            }

        }
    }
}