using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DoNotModify;

namespace EnterpriseTeam
{
    [TaskCategory("Enterprise")]
    public class TestGetRandomWayPoint : Action
    {

        public TaskStatus SetRandomWaypoint(GameData data, ref SharedVector2 result)
        {
            result = (SharedVector2)data.WayPoints[UnityEngine.Random.Range(0, data.WayPoints.Count - 1)].Position;
            return TaskStatus.Success;
        }
    }

}


