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
            if (Vector2.Distance(self.view.Position, self.OtherSpaceship.Position) < 5.0f)
            {
                self.Shockwave(true);
                return TaskStatus.Success;
            }
            else
            {
                self.Shockwave(false);
                return TaskStatus.Failure;
            }
        }
    }
}