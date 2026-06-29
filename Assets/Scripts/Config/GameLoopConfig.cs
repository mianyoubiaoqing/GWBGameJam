using UnityEngine;
using UnityEngine.Serialization;

namespace GWBGameJam
{
    [CreateAssetMenu(fileName = "GameLoopConfig", menuName = "GWBGameJam/Configs/GameLoopConfig")]
    public class GameLoopConfig : ScriptableObject
    {
        [SerializeField, Min(1), FormerlySerializedAs("TotalLevels")]
        private int _totalLevels = 3;
        [SerializeField, Range(1.5f, 10f), FormerlySerializedAs("DevSpeedMultiplier")]
        private float _devSpeedMultiplier = 3f;
        [SerializeField, Min(0f), FormerlySerializedAs("LevelTransitionDuration")]
        private float _levelTransitionDuration = 0f;

        public int TotalLevels => _totalLevels;
        public float DevSpeedMultiplier => _devSpeedMultiplier;
        public float LevelTransitionDuration => _levelTransitionDuration;
    }
}
