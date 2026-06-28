using UnityEngine;

namespace GWBGameJam
{
    [CreateAssetMenu(fileName = "DoughStateBoundaryConfig", menuName = "GWBGameJam/Configs/DoughStateBoundaryConfig")]
    public class DoughStateBoundaryConfig : ScriptableObject
    {
        public float ThresholdSoftToSoftest = 1.75f;
        public float ThresholdSoftestToMedium = 1.25f;
        public float ThresholdMediumToHardest = 0.75f;
        public float ThresholdHardestToHard = 0.25f;
        [Min(0.01f)] public float ToleranceHalfWidth = 0.25f;

        private void OnValidate() => Validate();

        public void Validate()
        {
            bool invalid = ThresholdSoftToSoftest <= ThresholdSoftestToMedium
                        || ThresholdSoftestToMedium <= ThresholdMediumToHardest
                        || ThresholdMediumToHardest <= ThresholdHardestToHard
                        || ThresholdHardestToHard <= 0f;
            if (invalid)
            {
                Debug.LogError("[DoughStateBoundaryConfig] 边界阈值必须严格递减且大于 0，已重置为默认值");
                ThresholdSoftToSoftest = 1.75f;
                ThresholdSoftestToMedium = 1.25f;
                ThresholdMediumToHardest = 0.75f;
                ThresholdHardestToHard = 0.25f;
            }
        }

        public float GetCenterRatio(DoughState state)
        {
            return state switch
            {
                DoughState.Softest => (ThresholdSoftToSoftest + ThresholdSoftestToMedium) / 2f,
                DoughState.Medium  => (ThresholdSoftestToMedium + ThresholdMediumToHardest) / 2f,
                DoughState.Hardest => (ThresholdMediumToHardest + ThresholdHardestToHard) / 2f,
                _ => -1f
            };
        }

        public DoughState GetDoughState(float ratio)
        {
            if (ratio >= ThresholdSoftToSoftest)   return DoughState.TooSoft;
            if (ratio >= ThresholdSoftestToMedium) return DoughState.Softest;
            if (ratio >= ThresholdMediumToHardest) return DoughState.Medium;
            if (ratio >= ThresholdHardestToHard)   return DoughState.Hardest;
            return DoughState.TooHard;
        }
    }
}
