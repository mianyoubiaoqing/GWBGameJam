using UnityEngine;
using UnityEngine.Serialization;

namespace GWBGameJam
{
    [CreateAssetMenu(fileName = "TableConfig", menuName = "GWBGameJam/Configs/TableConfig")]
    public class TableConfig : ScriptableObject
    {
        [SerializeField, Min(1), FormerlySerializedAs("MaxHits")]
        private int _maxHits = 5;

        public int MaxHits => _maxHits;

        public void Validate()
        {
            if (_maxHits < 1)
            {
                Debug.LogError("[TableConfig] MaxHits 不能小于 1，已强制设为 1");
                _maxHits = 1;
            }
        }
    }
}
