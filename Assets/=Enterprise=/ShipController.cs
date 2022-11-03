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
		private BehaviorTree behaviorTree;
		private bool needFire;
		public override void Initialize(SpaceShipView spaceship, GameData data)
		{
			behaviorTree = GetComponent<BehaviorTree>();
		}

		public override InputData UpdateInput(SpaceShipView spaceship, GameData data)
		{
            Vector2 v_diff = (targetPoint - (Vector2)transform.position);
            float atan2 = Mathf.Atan2(v_diff.y, v_diff.x);
			UpdateBlackboard(spaceship, data);
			SpaceShipView otherSpaceship = data.GetSpaceShipForOwner(1 - spaceship.Owner);
			float thrust = 1.0f;
			float targetOrient = Quaternion.Euler(0f, 0f, atan2 * Mathf.Rad2Deg).z;
			bool needShoot = AimingHelpers.CanHit(spaceship, otherSpaceship.Position, otherSpaceship.Velocity, 0.15f);
			return new InputData(thrust, targetOrient, needFire, false, false);
		}

		public void UpdateBlackboard(SpaceShipView spaceship, GameData data)
		{
			SpaceShipView otherSpaceship = data.GetSpaceShipForOwner(1 - spaceship.Owner);
			//SET VARIABLE SHOOT
			behaviorTree.SetVariableValue("CanHit", AimingHelpers.CanHit(spaceship, otherSpaceship.Position, 180.0f));	
        }

		public void Fire()
        {
			needFire = true;
		}
	}

}
