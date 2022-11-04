using BehaviorDesigner.Runtime.Tasks;

namespace EnterpriseTeam
{
    [TaskCategory("Enterprise")]
    public class DropMine : Action
    {
        private ShipController self;

        public override void OnAwake()
        {
            self = gameObject.GetComponent<ShipController>();
        }

        public override void OnStart()
        {
            base.OnStart();

            if (self.view.Energy > self.view.MineEnergyCost)
                self.POSERMINE();
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.Success;
        }
    }
}