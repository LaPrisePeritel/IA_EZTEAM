using BehaviorDesigner.Runtime.Tasks;
using DoNotModify;
using System.Collections.Generic;
using UnityEngine;
using IIM;

namespace EnterpriseTeam
{
    [TaskCategory("Enterprise")]
    public class GoToNearNeutralWaypoint : Action
    {
        private ShipController self;
        private GameData data;

        private WayPointView nearestWaypoint = null;

        public override void OnAwake()
        {
            self = gameObject.GetComponent<ShipController>();
            data = self.Data;
        }

        public override void OnStart()
        {
            base.OnStart();

            List<WayPointView> wayPoints = data.WayPoints;
            List<WayPointView> wayPointsToGet = new List<WayPointView>();
            for (int i = 0; i < wayPoints.Count; i++)
            {
                if (wayPoints[i].Owner != self.view.Owner)
                {
                    wayPointsToGet.Add(wayPoints[i]);
                }
            }

            float nearestDistance = 9999f;

            for (int i = 0; i < wayPointsToGet.Count; i++)
            {
                float currentDistance = Vector2.Distance(wayPointsToGet[i].Position, self.view.Position);
                if (currentDistance < nearestDistance)
                {
                    nearestWaypoint = wayPointsToGet[i];
                    nearestDistance = currentDistance;
                }
            }
        }

        public override TaskStatus OnUpdate()
        {
            if (Vector2.Distance(nearestWaypoint.Position, self.view.Position) > nearestWaypoint.Radius)
            {
                /*float dx = nearestWaypoint.Position.x - self.view.Position.x;
                float dy = nearestWaypoint.Position.y - self.view.Position.y;
                float angle = Mathf.Atan2(dy, dx) * 180 / Mathf.PI;*/
                float angle = AimingHelpers.ComputeSteeringOrient(self.view, nearestWaypoint.Position);

                self.GoToNearNeutralWaypoint(angle);
                return TaskStatus.Running;
            }
            else
            {
                Debug.LogError("Success");
                return TaskStatus.Success;
            }
        }
    }
}