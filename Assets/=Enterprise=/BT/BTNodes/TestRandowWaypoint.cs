using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DoNotModify;
using UnityEngine;
using UnityEngine.AI;

namespace EnterpriseTeam
{
    [TaskCategory("Enterprise")]
    public class TestRandowWaypoint : Action
    {
        private SharedVector2 targetPoint;

        public override void OnAwake()
        {

        }

        public override TaskStatus OnUpdate()
        {
            if (transform.position == (Vector3)targetPoint.Value)
                return TaskStatus.Success;
            else
            {
                return TaskStatus.Running;
            }
        }
    }
    
}
