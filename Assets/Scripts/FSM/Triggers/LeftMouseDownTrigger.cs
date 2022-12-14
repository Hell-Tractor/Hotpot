using UnityEngine;

namespace AI.FSM {
    public class LeftMouseDownTrigger : FSMTrigger {
        protected override void init() {
            this.TriggerID = FSMTriggerID.LeftMouseDown;
        }

        public override bool HandleTrigger(FSMBase fsm) {
            return
                Input.GetMouseButtonDown(1) &&
                fsm.gameObject
                    .GetComponentInParent<BothChopstickBehaviour>()
                    .CurrentState == BothChopstickBehaviour.State.Idle;
        }
    }
}