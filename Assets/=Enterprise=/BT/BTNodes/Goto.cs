using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace EnterpriseTeam
{


    [TaskCategory("Enterprise")]
    public class Goto : Action
    {
        enum _travel
        {
            NAVMESH,
            TRANSLATE,
            INPUTDATA
        }

        private _travel travel;

        public SharedInt travelValue;

        public SharedVector2 targetPoint;

        public SharedFloat speed = 1;

        public override void OnAwake()
        {
            travel = (_travel)travelValue.Value;
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.Success;
        }
    }
}

