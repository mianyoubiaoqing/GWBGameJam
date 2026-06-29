using UnityEngine;
using UnityEngine.Serialization;

namespace GWBGameJam
{
    [CreateAssetMenu(fileName = "ThrowConfig", menuName = "GWBGameJam/Configs/ThrowConfig")]
    public class ThrowConfig : ScriptableObject
    {
        [SerializeField, Range(0.1f, 2f), FormerlySerializedAs("ThrowDuration")]
        private float _throwDuration = 0.4f;
        [SerializeField, Range(0.5f, 10f), FormerlySerializedAs("PeakHeight")]
        private float _peakHeight = 3f;

        public float ThrowDuration => _throwDuration;
        public float PeakHeight => _peakHeight;
    }
}
