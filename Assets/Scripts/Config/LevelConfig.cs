using UnityEngine;

namespace GWBGameJam
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "GWBGameJam/Configs/LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        public LevelData[] Levels = new LevelData[]
        {
            new LevelData { SpawnIntervalSeconds = 9f, TotalMonsters = 10 },
            new LevelData { SpawnIntervalSeconds = 7f, TotalMonsters = 15 },
            new LevelData { SpawnIntervalSeconds = 5f, TotalMonsters = 20 },
        };
    }
}
