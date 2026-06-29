using UnityEngine;
using UnityEngine.Serialization;

namespace GWBGameJam
{
    [CreateAssetMenu(fileName = "DoughStateBoundaryConfig", menuName = "GWBGameJam/Configs/DoughStateBoundaryConfig")]
    public class DoughStateBoundaryConfig : ScriptableObject
    {
        [SerializeField, FormerlySerializedAs("ThresholdSoftToSoftest")]
        private float _thresholdSoftToSoftest = 1.75f;
        [SerializeField, FormerlySerializedAs("ThresholdSoftestToMedium")]
        private float _thresholdSoftestToMedium = 1.25f;
        [SerializeField, FormerlySerializedAs("ThresholdMediumToHardest")]
        private float _thresholdMediumToHardest = 0.75f;
        [SerializeField, FormerlySerializedAs("ThresholdHardestToHard")]
        private float _thresholdHardestToHard = 0.25f;
        [SerializeField, Min(0.01f), FormerlySerializedAs("ToleranceHalfWidth")]
        private float _toleranceHalfWidth = 0.25f;

        public float ThresholdSoftToSoftest => _thresholdSoftToSoftest;
        public float ThresholdSoftestToMedium => _thresholdSoftestToMedium;
        public float ThresholdMediumToHardest => _thresholdMediumToHardest;
        public float ThresholdHardestToHard => _thresholdHardestToHard;
        public float ToleranceHalfWidth => _toleranceHalfWidth;

        private void OnValidate() => Validate();

        public void Validate()
        {
            if (_thresholdHardestToHard <= 0f)
            {
                Debug.LogError("[DoughStateBoundaryConfig] ThresholdHardestToHard 必须大于 0，已修正为默认值");
                _thresholdHardestToHard = 0.25f;
            }
            if (_thresholdMediumToHardest <= _thresholdHardestToHard)
            {
                Debug.LogError("[DoughStateBoundaryConfig] ThresholdMediumToHardest 必须大于 ThresholdHardestToHard，已自动修正");
                _thresholdMediumToHardest = _thresholdHardestToHard + 0.5f;
            }
            if (_thresholdSoftestToMedium <= _thresholdMediumToHardest)
            {
                Debug.LogError("[DoughStateBoundaryConfig] ThresholdSoftestToMedium 必须大于 ThresholdMediumToHardest，已自动修正");
                _thresholdSoftestToMedium = _thresholdMediumToHardest + 0.5f;
            }
            if (_thresholdSoftToSoftest <= _thresholdSoftestToMedium)
            {
                Debug.LogError("[DoughStateBoundaryConfig] ThresholdSoftToSoftest 必须大于 ThresholdSoftestToMedium，已自动修正");
                _thresholdSoftToSoftest = _thresholdSoftestToMedium + 0.5f;
            }
        }

        public float GetCenterRatio(DoughState state)
        {
            return state switch
            {
                DoughState.Softest => (_thresholdSoftToSoftest + _thresholdSoftestToMedium) / 2f,
                DoughState.Medium  => (_thresholdSoftestToMedium + _thresholdMediumToHardest) / 2f,
                DoughState.Hardest => (_thresholdMediumToHardest + _thresholdHardestToHard) / 2f,
                _ => -1f
            };
        }

        public DoughState GetDoughState(float ratio)
        {
            if (ratio >= _thresholdSoftToSoftest)   return DoughState.TooSoft;
            if (ratio >= _thresholdSoftestToMedium) return DoughState.Softest;
            if (ratio >= _thresholdMediumToHardest) return DoughState.Medium;
            if (ratio >= _thresholdHardestToHard)   return DoughState.Hardest;
            return DoughState.TooHard;
        }
    }
}
