using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoNotModify;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using static UnityEngine.GraphicsBuffer;
using System.Linq;

namespace EnterpriseTeam
{
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
        public SpaceShipView OtherSpaceship { get; private set; }
        public GameData Data { get; private set; }

        public BehaviorTree behaviorTree { get; private set; }
        private bool needFire;
        private bool needShockwave;

        public Dictionary<int, EnemyMoveData> enemyMoveData { get; set; }
        public int lastIndex { get; set; }
        public int firstIndex { get; set; }

        public override void Initialize(SpaceShipView spaceship, GameData data)
        {
            view = spaceship;
            Data = data;
            OtherSpaceship = data.GetSpaceShipForOwner(1 - view.Owner); ;
            behaviorTree = GetComponent<BehaviorTree>();
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

            UpdateBlackboard();
            float targetOrient = rotation;
            return new InputData(1, targetOrient, needFire, false, needShockwave);
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

        public void GoToNearNeutralWaypoint(float angle)
        {
            rotation = angle;
        }
    }
}