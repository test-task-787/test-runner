using System.Collections.Generic;
using UnityEngine;

namespace InfinityRunner.Scripts.Player.Infrastructure
{
    public class PlayerStats
    {
        public Vector2 ActualSpeed { get; set; }
        public Vector2 CalculatedSpeed { get; set; }
        public List<float> SpeedMultipiers { get; } = new();
        
        public bool IsDead { get; set; } = false;
    }
}