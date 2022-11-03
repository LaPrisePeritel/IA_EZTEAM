using System.Linq;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DoNotModify;
using static UnityEngine.GraphicsBuffer;
using UnityEngine;

namespace EnterpriseTeam
{

    [TaskCategory("Enterprise")]
    public class Follow : Action
    {
        private SpaceShipView ship;
        private ShipController controller;

        public override void OnAwake()
        {
            controller = GetComponent<ShipController>();
            ship = controller.view;
        }

        public override TaskStatus OnUpdate()
        {
            EnemyMoveData data = controller.enemyMoveData[controller.enemyMoveData.ElementAt(0).Key];
            controller.rotation = data.rotation;
            
            controller.thrust = data.thrust;

            controller.enemyMoveData.Remove(controller.enemyMoveData.ElementAt(0).Key);
            return TaskStatus.Success;
        }
    }
    
}
