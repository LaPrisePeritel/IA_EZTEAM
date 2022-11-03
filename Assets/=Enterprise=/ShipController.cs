using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoNotModify;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using static UnityEngine.GraphicsBuffer;

namespace EnterpriseTeam {

	public class ShipController : BaseSpaceShipController
    {
        private Vector2 targetPoint;

        public float rotation { get; set; }
        public uint thrust { get; set; }


        public SpaceShipView view { get; private set; }
        public GameData Data { get; private set; }

		private BehaviorTree behaviorTree;
		private bool needFire;
		public override void Initialize(SpaceShipView spaceship, GameData data)
        {
            view = spaceship;
            Data = data;
			behaviorTree = GetComponent<BehaviorTree>();
		}

		public override InputData UpdateInput(SpaceShipView spaceship, GameData data)
		{
            SpaceShipView otherSpaceship = data.GetSpaceShipForOwner(1 - spaceship.Owner);
			UpdateBlackboard(spaceship, data);
			float thrust = 1.0f;
            float targetOrient = rotation;
			return new InputData(thrust, targetOrient, needFire, false, false);
		}

		public void UpdateBlackboard(SpaceShipView spaceship, GameData data)
		{
			SpaceShipView otherSpaceship = data.GetSpaceShipForOwner(1 - spaceship.Owner);
			//SET VARIABLE SHOOT
			if(AimingHelpers.CanHit(spaceship, otherSpaceship.Position, 15.0f))
				behaviorTree.SetVariableValue("CanHit", true);
            else
			{
				behaviorTree.SetVariableValue("CanHit", false);
				needFire = false;
			}
		}

		public void Fire()
        {
			needFire = true;
		}

		public void GoToNearNeutralWaypoint(float angle)
        {
			rotation = angle;
        }
	}

}
