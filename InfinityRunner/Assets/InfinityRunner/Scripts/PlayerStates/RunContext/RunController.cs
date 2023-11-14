using System.Linq;
using InfinityRunner.Scripts.Input;
using InfinityRunner.Scripts.Player.Infrastructure;
using InfinityRunner.Scripts.PlayerStates.RunContext.Configs;
using UnityEngine;

namespace InfinityRunner.Scripts.PlayerStates.RunContext
{
    /// <summary>
    /// RigidBody velocity calculation logic - from Dreamteck.Forever.MagicValleyPlayer
    /// Then this logic refactored for needed infrastructure
    /// </summary>
    public class RunController
    {
        private bool inAir = false;
        private float _notPlatformTime = -1f;
        private int _jumpCount = 0;
        
        private readonly RunConfig _runConfig;
        private readonly PlayerStats _playerStats;
        private readonly Rigidbody2D _rigidBody;
        private readonly Transform _transform;
        private readonly Bounds _colliderBounds;
        private readonly InputModule _inputModule;
        
        public RunController(
            RunConfig runConfig,
            PlayerStats playerStats,
            Rigidbody2D rigidBody,
            Transform transform,
            Bounds colliderBounds,
            InputModule inputModule)
        {
            _runConfig = runConfig;
            _playerStats = playerStats;
            _rigidBody = rigidBody;
            _transform = transform;
            _colliderBounds = colliderBounds;
            _inputModule = inputModule;
            _playerStats.CalculatedSpeed = new Vector2(runConfig.initialSpeed, _playerStats.CalculatedSpeed.y);
        }

        public void OnPhysicsTick(float dt)
        {
            //Source Dreamteck.Forever.MagicValleyPlayer
            var speed = Mathf.MoveTowards(_playerStats.CalculatedSpeed.x, _runConfig.endSpeed,
                Time.deltaTime * _runConfig.acceleration);
            inAir = !Physics2D.OverlapArea(
                _transform.position + Vector3.down * _colliderBounds.size.y * 0.5f +
                Vector3.left * _colliderBounds.size.x * 0.5f,
                _transform.position + Vector3.down * _colliderBounds.size.y * 0.5f +
                Vector3.right * _colliderBounds.size.x * 0.5f, 1);
            var velocity = _playerStats.CalculatedSpeed;
            velocity.x = speed;
            _playerStats.CalculatedSpeed = new Vector2(speed, _playerStats.CalculatedSpeed.y);
            var velocityXMultiplier = _playerStats.SpeedMultipiers.Aggregate(1f, (current, m) => current * m);
            _rigidBody.velocity = new Vector2(velocity.x * velocityXMultiplier, _rigidBody.velocity.y);
            _rigidBody.AddForce(Vector2.down * _runConfig.gravityForce, ForceMode2D.Force);
            //Source Dreamteck.Forever.MagicValleyPlayer

            
            FixHugeSpeed(_rigidBody);

            if (!inAir)
                _jumpCount = 0;

            // For continious force
            if (inAir && _jumpCount <= _runConfig.jumpsCount && velocity.y > 0f)
            {
                //Source Dreamteck.Forever.MagicValleyPlayer
                if (_inputModule.IsJumpPressed)
                    _rigidBody.AddForce(Vector2.up * _runConfig.continuousJumpForce, ForceMode2D.Force);
                
                //Source Dreamteck.Forever.MagicValleyPlayer
            }
        }

        public void OnTick(float dt)
        {
            _notPlatformTime = (inAir) ? _notPlatformTime + dt : -1f;
            _playerStats.ActualSpeed = _rigidBody.velocity;

            
            //Source Dreamteck.Forever.MagicValleyPlayer
            if (_inputModule.IsJumpClick)
            {
                // And Coyot-time logic
                if ((_notPlatformTime < _runConfig.coyotTime && _jumpCount == 0) ||
                    (_jumpCount < _runConfig.jumpsCount && _inputModule.IsJumpClick))
                {
                    var v = _rigidBody.velocity;
                    v = new Vector2(v.x, 0);
                    _rigidBody.velocity = v;
                    _rigidBody.AddForce(Vector2.up * _runConfig.initialJumpForce, ForceMode2D.Impulse);
                    _jumpCount++;
                }
            }
            //Source Dreamteck.Forever.MagicValleyPlayer
        }

        private void FixHugeSpeed(Rigidbody2D rb)
        {
            if (!inAir && rb.velocity.y < -2)
            {
                rb.velocity = new Vector2(rb.velocity.x, -2f);
            }
        }
    }
}