using UnityEngine;

namespace GWBGameJam
{
    [CreateAssetMenu(fileName = "DoughConfig", menuName = "GWBGameJam/Configs/DoughConfig")]
    public class DoughConfig : ScriptableObject
    {
        [Min(0.1f)] public float FlourClickAmount = 0.75f;
        [Min(0.1f)] public float WaterFillRate = 0.5f;
        [Min(0f)] public float InitialRatio = 1f;
        [Min(0.1f)] public float MaxRatio = 3f;
    }
}
