using UnityEngine;

namespace GWBGameJam
{
    [CreateAssetMenu(fileName = "TableConfig", menuName = "GWBGameJam/Configs/TableConfig")]
    public class TableConfig : ScriptableObject
    {
        [Min(1)] public int MaxHits = 5;

        public void Validate()
        {
            if (MaxHits < 1)
            {
                Debug.LogError("[TableConfig] MaxHits 不能小于 1，已强制设为 1");
                MaxHits = 1;
            }
        }
    }
}
