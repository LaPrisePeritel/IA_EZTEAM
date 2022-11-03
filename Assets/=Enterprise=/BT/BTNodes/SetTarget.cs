using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using DoNotModify;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace EnterpriseTeam
{
    [TaskCategory("Enterprise")]
    public class SetTarget : Action
    {
        private BehaviorTree tree;

        public SpaceShipView _target;

        private SpaceShipView ship;

        private GameData data;

        private ShipController controller;

        public override void OnAwake()
        {
            tree = GetComponent<BehaviorTree>();
            controller = GetComponent<ShipController>();
            ship = controller.view;
            data = controller.Data;
            _target = data.GetSpaceShipForOwner(1 - ship.Owner);
        }

        public override TaskStatus OnUpdate()
        {
            Vector3 pos = _target.Position - _target.LookAt * (float)tree.GetVariable("DistanceBehind").GetValue();
            /*
            if (ship.Owner == 0)
            {
                Debug.DrawLine(ship.Position, ship.Position * -2, Color.red);
                Debug.DrawLine(_target.Position, _target.Position - _target.LookAt * 1.25f, Color.green);
            }
            */
            float dx = pos.x - ship.Position.y;
            float dy = pos.y - ship.Position.y;
            float angle = Mathf.Atan2(dy, dx) * 180 / Mathf.PI;
            controller.rotation = angle;

            
            Vector2 shipVec = (ship.Position - ship.LookAt);
            Vector2 targetVec = (_target.Position - _target.LookAt);

            float back = Vector2.Distance(ship.Position - ship.LookAt, _target.Position);
            float front = Vector2.Distance(ship.Position + ship.LookAt, _target.Position);
            if (back < front) controller.thrust = 0;
            else if (back > front) controller.thrust = 1; 

            return TaskStatus.Success;
        }
    }

}

