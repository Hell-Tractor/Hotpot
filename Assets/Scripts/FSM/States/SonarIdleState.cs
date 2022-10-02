using UnityEngine;

namespace AI.FSM {

    public class SonarIdleState : FSMState {
        protected override void init() {
            this.StateID = FSMStateID.SonarIdle;
        }

        public override void OnStateStay(FSMBase fsm) {
            base.OnStateStay(fsm);
            SonarFSM sonar = fsm as SonarFSM;
            // rotate sonar to face mouse position
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            sonar.transform.up = mousePos - (Vector2)sonar.transform.position;
        }

        public override void OnStateExit(FSMBase fsm) {
            base.OnStateExit(fsm);
            SonarFSM sonar = fsm as SonarFSM;
            sonar.sonarTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

}