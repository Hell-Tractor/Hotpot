namespace AI.FSM {

    public class StateEndTrigger : FSMTrigger {
        protected override void init() {
            this.TriggerID = FSMTriggerID.StateEnd;
        }

        public override bool HandleTrigger(FSMBase fsm) {
            return false;
        }
    }

}