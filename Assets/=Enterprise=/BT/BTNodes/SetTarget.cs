using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using DoNotModify;
using UnityEngine;
using System.Linq;
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

        private float distBehind;

        public override void OnAwake()
        {
            tree = GetComponent<BehaviorTree>();
            controller = GetComponent<ShipController>();
            ship = controller.view;
            data = controller.Data;
            _target = data.GetSpaceShipForOwner(1 - ship.Owner);
            distBehind = (float)tree.GetVariable("DistanceBehind").GetValue();
        }

        public override TaskStatus OnUpdate()
        {
            //Rotation towards enemy
            Vector3 pos = _target.Position - _target.LookAt * distBehind;
            float dx = pos.x - ship.Position.x;
            float dy = pos.y - ship.Position.y;
            float angle = Mathf.Atan2(dy, dx) * 180 / Mathf.PI;
            controller.rotation = angle;

            
            //Detection of enemy position: If behind us, stop thrusters to rotate
            float back = Vector2.Distance(ship.Position - ship.LookAt, _target.Position);
            float front = Vector2.Distance(ship.Position + ship.LookAt, _target.Position);
            if (back < front) controller.thrust = 0;
            else if (back > front) controller.thrust = 1; 

            controller.enemyMoveData.Add(controller.lastIndex, new EnemyMoveData(_target.Orientation, _target.Thrust));
            controller.lastIndex++;


            return TaskStatus.Success;
        }
    }

}

