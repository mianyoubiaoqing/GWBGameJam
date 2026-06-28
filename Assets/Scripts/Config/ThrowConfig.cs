using UnityEngine;

namespace GWBGameJam
{
    [CreateAssetMenu(fileName = "ThrowConfig", menuName = "GWBGameJam/Configs/ThrowConfig")]
    public class ThrowConfig : ScriptableObject
    {
        [Range(0.1f, 2f)] public float ThrowDuration = 0.4f;
        [Range(0.5f, 10f)] public float PeakHeight = 3f;
    }
}
