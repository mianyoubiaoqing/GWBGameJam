using UnityEngine;
using UnityEngine.Serialization;

namespace GWBGameJam
{
    [CreateAssetMenu(fileName = "BakingConfig", menuName = "GWBGameJam/Configs/BakingConfig")]
    public class BakingConfig : ScriptableObject
    {
        [SerializeField, Min(0.1f), FormerlySerializedAs("UndercookedDuration")]
        private float _undercookedDuration = 0.5f;
        [SerializeField, Min(0.1f), FormerlySerializedAs("CookedDuration")]
        private float _cookedDuration = 1.5f;
        [SerializeField, Min(0.1f), FormerlySerializedAs("BurntForcedThrowDuration")]
        private float _burntForcedThrowDuration = 2.5f;

        public float UndercookedDuration => _undercookedDuration;
        public float CookedDuration => _cookedDuration;
        public float BurntForcedThrowDuration => _burntForcedThrowDuration;

        private void OnValidate()
        {
            if (_cookedDuration <= _undercookedDuration)
            {
                Debug.LogError("[BakingConfig] CookedDuration 必须大于 UndercookedDuration，已自动修正");
                _cookedDuration = _undercookedDuration + 0.1f;
            }
            if (_burntForcedThrowDuration <= _cookedDuration)
            {
                Debug.LogError("[BakingConfig] BurntForcedThrowDuration 必须大于 CookedDuration，已自动修正");
                _burntForcedThrowDuration = _cookedDuration + 0.1f;
            }
        }

        public void Validate()
        {
            if (_cookedDuration <= _undercookedDuration)
            {
                Debug.LogError("[BakingConfig] CookedDuration 必须大于 UndercookedDuration，已自动修正");
                _cookedDuration = _undercookedDuration + 0.1f;
            }
            if (_burntForcedThrowDuration <= _cookedDuration)
            {
                Debug.LogError("[BakingConfig] BurntForcedThrowDuration 必须大于 CookedDuration，已自动修正");
                _burntForcedThrowDuration = _cookedDuration + 0.1f;
            }
        }
    }
}
