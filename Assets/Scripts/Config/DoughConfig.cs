using UnityEngine;
using UnityEngine.Serialization;

namespace GWBGameJam
{
    [CreateAssetMenu(fileName = "DoughConfig", menuName = "GWBGameJam/Configs/DoughConfig")]
    public class DoughConfig : ScriptableObject
    {
        [SerializeField, Min(0.1f), FormerlySerializedAs("FlourClickAmount")]
        private float _flourClickAmount = 0.75f;
        [SerializeField, Min(0.1f), FormerlySerializedAs("WaterFillRate")]
        private float _waterFillRate = 0.5f;
        [SerializeField, Min(0f), FormerlySerializedAs("InitialRatio")]
        private float _initialRatio = 1f;
        [SerializeField, Min(0.1f), FormerlySerializedAs("MaxRatio")]
        private float _maxRatio = 3f;

        public float FlourClickAmount => _flourClickAmount;
        public float WaterFillRate => _waterFillRate;
        public float InitialRatio => _initialRatio;
        public float MaxRatio => _maxRatio;
    }
}
