using UnityEngine;
using UnityEngine.Serialization;

namespace GWBGameJam
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "GWBGameJam/Configs/LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField, FormerlySerializedAs("Levels")]
        private LevelData[] _levels = new LevelData[]
        {
            new LevelData { SpawnIntervalSeconds = 9f, TotalMonsters = 10 },
            new LevelData { SpawnIntervalSeconds = 7f, TotalMonsters = 15 },
            new LevelData { SpawnIntervalSeconds = 5f, TotalMonsters = 20 },
        };

        public int LevelCount => _levels?.Length ?? 0;

        public LevelData GetLevel(int index)
        {
            return _levels[index];
        }
    }
}
