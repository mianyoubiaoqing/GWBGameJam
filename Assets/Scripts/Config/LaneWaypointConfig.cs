using UnityEngine;

namespace GWBGameJam
{
    [CreateAssetMenu(fileName = "LaneWaypointConfig", menuName = "GWBGameJam/Configs/LaneWaypointConfig")]
    public class LaneWaypointConfig : ScriptableObject
    {
        public LaneWaypoints[] Lanes = new LaneWaypoints[5];
        public int RecordedStepCount;
    }
}
