using UnityEngine;

namespace GWBGameJam
{
    [CreateAssetMenu(fileName = "MonsterData", menuName = "GWBGameJam/MonsterData")]
    public class MonsterData : ScriptableObject
    {
        public string MonsterName;
        public Sprite IdleSprite;
        public Sprite HitSprite;
        public DoughState TargetDoughState = DoughState.Medium;
    }
}
