using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoNotModify;
using static UnityEngine.GraphicsBuffer;

namespace EnterpriseTeam {

	public class ShipController : BaseSpaceShipController
    {
        private Vector2 targetPoint;

		public override void Initialize(SpaceShipView spaceship, GameData data)
		{
		}

		public override InputData UpdateInput(SpaceShipView spaceship, GameData data)
		{
            Vector2 v_diff = (targetPoint - (Vector2)transform.position);
            float atan2 = Mathf.Atan2(v_diff.y, v_diff.x);

            SpaceShipView otherSpaceship = data.GetSpaceShipForOwner(1 - spaceship.Owner);
			float thrust = 1.0f;
			float targetOrient = Quaternion.Euler(0f, 0f, atan2 * Mathf.Rad2Deg).z;
			bool needShoot = AimingHelpers.CanHit(spaceship, otherSpaceship.Position, otherSpaceship.Velocity, 0.15f);
			return new InputData(thrust, targetOrient, needShoot, false, false);
		}
	}

}
