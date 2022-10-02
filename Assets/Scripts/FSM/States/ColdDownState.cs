using UnityEngine;

namespace AI.FSM {

    public class ColdDownState : FSMState {
        private float _coldDownSeconds;
        protected override void init() {
            this.StateID = FSMStateID.ColdDown;
        }
        public override void OnStateEnter(FSMBase fsm) {
            base.OnStateEnter(fsm);
            _coldDownSeconds = (fsm as SonarFSM).ColdDownSeconds;
        }
        public override void OnStateStay(FSMBase fsm) {
            base.OnStateStay(fsm);
            _coldDownSeconds -= Time.deltaTime;
            if (_coldDownSeconds <= 0) {
                fsm.SetTrigger(FSMTriggerID.StateEnd);
            }
        }
    }

}
