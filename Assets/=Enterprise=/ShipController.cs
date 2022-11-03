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
		public SpaceShipView OtherSpaceship { get; private set; } 
		public GameData Data { get; private set; }

		public BehaviorTree behaviorTree { get; private set; }
		private bool needFire;
		private bool needShockwave;
		public override void Initialize(SpaceShipView spaceship, GameData data)
        {
			view = spaceship;
            Data = data;
			OtherSpaceship = data.GetSpaceShipForOwner(1 - view.Owner); ;
			behaviorTree = GetComponent<BehaviorTree>();
		}

		public override InputData UpdateInput(SpaceShipView spaceship, GameData data)
		{
			UpdateBlackboard();
			float thrust = 1.0f;
            float targetOrient = rotation;
			return new InputData(thrust, targetOrient, needFire, false, needShockwave);
		}

		public void UpdateBlackboard()
		{
			//SET VARIABLE SHOOT
			if (AimingHelpers.CanHit(view, OtherSpaceship.Position, 15.0f))
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
		public void Shockwave(bool CanShockwave)
		{
			needShockwave = CanShockwave;
		}
	}

}
