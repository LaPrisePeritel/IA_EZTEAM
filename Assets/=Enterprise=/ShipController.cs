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
			SpaceShipView otherSpaceship = data.GetSpaceShipForOwner(1 - spaceship.Owner);
			float thrust = 1.0f;
            float targetOrient = rotation;
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
