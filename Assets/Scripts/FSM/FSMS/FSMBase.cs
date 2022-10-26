using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM {

/// <summary>
/// <para>有限状态机</para>
/// <para>子类需要调用父类Start, Update方法，调用并重写setUpFSM方法</para>
/// </summary>
public class FSMBase : MonoBehaviour {
    /// <summary>
    /// 初始化状态机，子类需要调用base.setUpFSM()
    /// </summary>
    protected virtual void setUpFSM() {
        _states = new List<FSMState>();
    }
    /// <summary>
    /// 初始化其他组件
    /// </summary>
    protected virtual void init() {
    }
    protected void loadDefaultState() {
        // 加载默认状态
        _currentState = _defaultState = _states.Find(s => s.StateID == defaultStateID);
        _currentState.OnStateEnter(this);
        currentStateID = defaultStateID.ToString();
    }
    private void Start() {
        init();
        setUpFSM();
        loadDefaultState();
    }
    private void Update() {
        // 更新状态
        _currentState.Reason(this);
        _currentState.OnStateStay(this);
    }
    private void FixedUpdate() {
        _currentState.OnStateFixedStay(this);
    }
    /// <summary>
    /// 切换当前状态至目标状态
    /// </summary>
    /// <param name="targetStateID">目标状态ID</param>
    public void changeActiveState(FSMStateID targetStateID) {
        _currentState.OnStateExit(this);
        _currentState = targetStateID == FSMStateID.Default ? _defaultState : _states.Find(s => s.StateID == targetStateID);
        currentStateID = _currentState.StateID.ToString();
        _currentState.OnStateEnter(this);
    }
    public void SetTrigger(FSMTriggerID triggerID) {
        _settledTriggers.Add(triggerID);
    }
    public void ResetTrigger(FSMTriggerID triggerID) {
        _settledTriggers.Remove(triggerID);
    }
    public bool HasTrigger(FSMTriggerID triggerID) {
        return _settledTriggers.Contains(triggerID);
    }

    [Tooltip("默认状态"), Header("状态设置")]
    public FSMStateID defaultStateID;
    protected FSMState _defaultState;
    protected List<FSMState> _states;
    protected FSMState _currentState;
    protected SortedSet<FSMTriggerID> _settledTriggers = new SortedSet<FSMTriggerID>();
    [ReadOnly]
    public string currentStateID;
    public FSMStateID CurrentState {
        get {
            return _currentState.StateID;
        }
    }
    public Animator animator;
}

}