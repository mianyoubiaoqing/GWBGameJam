using UnityEngine;

namespace GWBGameJam
{
    [CreateAssetMenu(fileName = "GameLoopConfig", menuName = "GWBGameJam/Configs/GameLoopConfig")]
    public class GameLoopConfig : ScriptableObject
    {
        [Min(1)] public int TotalLevels = 3;
        [Range(1.5f, 10f)] public float DevSpeedMultiplier = 3f;
        [Min(0f)] public float LevelTransitionDuration = 0f;
    }
}
