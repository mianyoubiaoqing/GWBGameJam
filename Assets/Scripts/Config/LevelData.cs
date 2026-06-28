using System;
using UnityEngine;

namespace GWBGameJam
{
    [Serializable]
    public class LevelData
    {
        [Min(0.5f)] public float SpawnIntervalSeconds = 9f;
        [Min(1)] public int TotalMonsters = 10;
    }
}
