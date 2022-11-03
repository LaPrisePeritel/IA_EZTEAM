using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DoNotModify;
using static UnityEngine.GraphicsBuffer;

namespace EnterpriseTeam {

    public struct EnemyMoveData
    {
        public float rotation;
        public float thrust;

        public EnemyMoveData(float rot, float thr)
        {
            rotation = rot;
            thrust = thr;
        }
    }


	public class ShipController : BaseSpaceShipController
    {
        public float rotation { get; set; }
        public float thrust { get; set; }


        public SpaceShipView view { get; private set; }
        public GameData Data { get; private set; }

        public Dictionary<int, EnemyMoveData> enemyMoveData { get; set; }
        public int lastIndex { get; set; }
        public int firstIndex { get; set; }

        public override void Initialize(SpaceShipView spaceship, GameData data)
        {
            view = spaceship;
            Data = data;
            enemyMoveData = new Dictionary<int, EnemyMoveData>();
            lastIndex = 0;
            firstIndex = 0;
        }

		public override InputData UpdateInput(SpaceShipView spaceship, GameData data)
		{
            //Secure to forbid memory leak and memory overflow
            if (enemyMoveData.Count > 10000)
            {
                enemyMoveData.Remove(enemyMoveData.ElementAt(0).Key);
                firstIndex++;
            }

            SpaceShipView otherSpaceship = data.GetSpaceShipForOwner(1 - spaceship.Owner);
            float targetOrient = rotation;
			bool needShoot = AimingHelpers.CanHit(spaceship, otherSpaceship.Position, otherSpaceship.Velocity, 0.15f);
			return new InputData(thrust, targetOrient, needShoot, false, false);
		}
	}

}
