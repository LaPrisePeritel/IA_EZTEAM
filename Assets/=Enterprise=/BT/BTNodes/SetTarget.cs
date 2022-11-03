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
            float dx = _target.Position.x - ship.Position.y;
            float dy = _target.Position.y - ship.Position.y;
            float angle = Mathf.Atan2(dy, dx) * 180 / Mathf.PI;
            controller.rotation = angle;
            Debug.Log(angle);
            //tree.SetVariable("Target", _target);
            return TaskStatus.Success;
        }
    }

}

