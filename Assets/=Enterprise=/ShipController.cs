using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoNotModify;
using static UnityEngine.GraphicsBuffer;

namespace EnterpriseTeam {

	public class ShipController : BaseSpaceShipController
    {
        public float rotation { get; set; }
        public int thrust { get; set; }


        public SpaceShipView view { get; private set; }
        public GameData Data { get; private set; }

		public override void Initialize(SpaceShipView spaceship, GameData data)
        {
            view = spaceship;
            Data = data;
        }

		public override InputData UpdateInput(SpaceShipView spaceship, GameData data)
		{
            SpaceShipView otherSpaceship = data.GetSpaceShipForOwner(1 - spaceship.Owner);
			//float thrust = 1.0f;
            float targetOrient = rotation;
			bool needShoot = AimingHelpers.CanHit(spaceship, otherSpaceship.Position, otherSpaceship.Velocity, 0.15f);
			return new InputData(thrust, targetOrient, needShoot, false, false);
		}
	}

}
