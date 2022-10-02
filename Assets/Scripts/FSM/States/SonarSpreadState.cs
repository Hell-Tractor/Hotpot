using UnityEngine;
using System.Linq;

namespace AI.FSM {

    public class SonarSpreadState : FSMState {
        private float _elapsedTime = 0f;
        private GameObject[] _targetList;

        protected override void init() {
            this.StateID = FSMStateID.SonarSpread;
        }

        public override void OnStateEnter(FSMBase fsm) {
            base.OnStateEnter(fsm);
            _elapsedTime = 0f;
            Sonar sonar = fsm as Sonar;
            Vector2 sonarDirection = sonar.sonarTarget - (Vector2)sonar.transform.position;

            _targetList = Physics2D.OverlapCircleAll(
                sonar.transform.position,
                sonar.SpreadSpeed * sonar.DurationSeconds,
                LayerMask.GetMask("Food")
            ).Where(collider => {
                Vector2 targetDirection = collider.transform.position - sonar.transform.position;
                return Vector2.Angle(sonarDirection, targetDirection) <= sonar.Degree / 2;
            }).Select(collider => collider.gameObject).ToArray();
        }

        public override void OnStateStay(FSMBase fsm) {
            base.OnStateStay(fsm);
            Sonar sonar = fsm as Sonar;
            _targetList.Where(food => {
                float time = Vector2.Distance(sonar.transform.position, food.transform.position) / sonar.SpreadSpeed;
                return _elapsedTime <= time && time < _elapsedTime + Time.deltaTime;
            }).ToList().ForEach(food => food.GetComponent<FoodBehaviour>().OnSonarDetected());

            _elapsedTime += Time.deltaTime;
            if (_elapsedTime >= sonar.DurationSeconds) {
                fsm.SetTrigger(FSMTriggerID.StateEnd);
            }
        }
    }

}