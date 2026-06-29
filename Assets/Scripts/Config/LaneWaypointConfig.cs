using UnityEngine;
using UnityEngine.Serialization;

namespace GWBGameJam
{
    [CreateAssetMenu(fileName = "LaneWaypointConfig", menuName = "GWBGameJam/Configs/LaneWaypointConfig")]
    public class LaneWaypointConfig : ScriptableObject
    {
        [SerializeField, FormerlySerializedAs("Lanes")]
        private LaneWaypoints[] _lanes = new LaneWaypoints[5];
        [SerializeField, FormerlySerializedAs("RecordedStepCount")]
        private int _recordedStepCount;

        public LaneWaypoints[] Lanes => _lanes;
        public int RecordedStepCount => _recordedStepCount;

        public void SetWaypoints(LaneWaypoints[] lanes, int recordedStepCount)
        {
            _lanes = lanes;
            _recordedStepCount = recordedStepCount;
        }
    }
}
