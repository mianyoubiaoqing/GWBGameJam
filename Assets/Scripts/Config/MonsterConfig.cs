using UnityEngine;

namespace GWBGameJam
{
    [CreateAssetMenu(fileName = "MonsterConfig", menuName = "GWBGameJam/Configs/MonsterConfig")]
    public class MonsterConfig : ScriptableObject
    {
        [Min(0.2f)] public float MoveIntervalSeconds = 1f;
        [Min(4)] public int MoveStepCount = 8;
        [Min(0.05f)] public float PendingMoveThreshold = 0.5f;
        public AnimationCurve ScaleCurve = AnimationCurve.Linear(0, 0.01f, 7, 1.5f);
        [Min(0.05f)] public float MoveDuration = 0.3f;
        [Min(1)] public int WrongHitFlashCount = 2;
        [Min(0.05f)] public float WrongHitFlashDuration = 0.1f;

        private void OnValidate() => Validate();

        public void Validate()
        {
            if (MoveDuration >= MoveIntervalSeconds)
            {
                Debug.LogError("[MonsterConfig] MoveDuration 必须小于 MoveIntervalSeconds，已自动修正");
                MoveDuration = MoveIntervalSeconds * 0.3f;
            }
            if (PendingMoveThreshold >= MoveIntervalSeconds)
            {
                Debug.LogError("[MonsterConfig] PendingMoveThreshold 必须小于 MoveIntervalSeconds，已自动修正");
                PendingMoveThreshold = MoveIntervalSeconds * 0.5f;
            }
        }
    }
}
