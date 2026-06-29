using UnityEngine;
using UnityEngine.Serialization;

namespace GWBGameJam
{
    [CreateAssetMenu(fileName = "MonsterData", menuName = "GWBGameJam/MonsterData")]
    public class MonsterData : ScriptableObject
    {
        [SerializeField, FormerlySerializedAs("MonsterName")]
        private string _monsterName;
        [SerializeField, FormerlySerializedAs("IdleSprite")]
        private Sprite _idleSprite;
        [SerializeField, FormerlySerializedAs("HitSprite")]
        private Sprite _hitSprite;
        [SerializeField, DoughTargetState, FormerlySerializedAs("TargetDoughState")]
        private DoughState _targetDoughState = DoughState.Medium;

        [Tooltip("额外整体缩放倍率，乘在透视 ScaleCurve 之上。sprite 太小时调大此值")]
        [SerializeField, Min(0.01f), FormerlySerializedAs("DisplayScale")]
        private float _displayScale = 1f;

        public string MonsterName => _monsterName;
        public Sprite IdleSprite => _idleSprite;
        public Sprite HitSprite => _hitSprite;
        public DoughState TargetDoughState => _targetDoughState;
        public float DisplayScale => _displayScale;

        private void OnValidate()
        {
            ValidateTargetDoughState();
        }

        public void ValidateTargetDoughState()
        {
            if (IsValidTargetState(_targetDoughState)) return;

            Debug.LogError($"[MonsterData] {name} 的 TargetDoughState 只能是 Softest / Medium / Hardest，已自动修正为 Medium");
            _targetDoughState = DoughState.Medium;
        }

        public static bool IsValidTargetState(DoughState state)
        {
            return state == DoughState.Softest
                || state == DoughState.Medium
                || state == DoughState.Hardest;
        }
    }
}
