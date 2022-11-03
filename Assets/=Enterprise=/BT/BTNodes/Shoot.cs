using BehaviorDesigner.Runtime.Tasks;

namespace EnterpriseTeam
{
    [TaskCategory("Enterprise")]
    public class Shoot : Action
    {
        ShipController self;
        public override void OnAwake()
        {
            self = gameObject.GetComponent<ShipController>();
        }

        public override TaskStatus OnUpdate()
        {
            self.Fire();
            return TaskStatus.Success;
        }
    }
}