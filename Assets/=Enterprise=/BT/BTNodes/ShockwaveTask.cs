using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace EnterpriseTeam
{
    [TaskCategory("Enterprise")]
    public class ShockwaveTask : Action
    {
        ShipController self;
        public override void OnAwake()
        {
            self = gameObject.GetComponent<ShipController>();
        }

        public override TaskStatus OnUpdate()
        {
            self.Shockwave();
            return TaskStatus.Success;
        }
    }
}