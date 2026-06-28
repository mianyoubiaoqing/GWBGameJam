using UnityEngine;

namespace GWBGameJam
{
    [CreateAssetMenu(fileName = "BakingConfig", menuName = "GWBGameJam/Configs/BakingConfig")]
    public class BakingConfig : ScriptableObject
    {
        [Min(0.1f)] public float UndercookedDuration = 0.5f;
        [Min(0.1f)] public float CookedDuration = 1.5f;
        [Min(0.1f)] public float BurntForcedThrowDuration = 2.5f;

        private void OnValidate()
        {
            if (CookedDuration <= UndercookedDuration)
            {
                Debug.LogError("[BakingConfig] CookedDuration 必须大于 UndercookedDuration，已自动修正");
                CookedDuration = UndercookedDuration + 0.1f;
            }
            if (BurntForcedThrowDuration <= CookedDuration)
            {
                Debug.LogError("[BakingConfig] BurntForcedThrowDuration 必须大于 CookedDuration，已自动修正");
                BurntForcedThrowDuration = CookedDuration + 0.1f;
            }
        }

        public void Validate()
        {
            if (CookedDuration <= UndercookedDuration)
            {
                Debug.LogError("[BakingConfig] CookedDuration 必须大于 UndercookedDuration，已自动修正");
                CookedDuration = UndercookedDuration + 0.1f;
            }
            if (BurntForcedThrowDuration <= CookedDuration)
            {
                Debug.LogError("[BakingConfig] BurntForcedThrowDuration 必须大于 CookedDuration，已自动修正");
                BurntForcedThrowDuration = CookedDuration + 0.1f;
            }
        }
    }
}
