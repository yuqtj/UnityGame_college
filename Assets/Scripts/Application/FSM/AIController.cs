using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIController {

    //----------状态机基类-----------
    public AIState state;

    Dictionary<AIStateEnum, AIState> stateMap;

    //获取
    AIAttribute aiAttribute;

    public AIController(AIAttribute aiAttribute)
    {
        this.aiAttribute = aiAttribute;
        stateMap = new Dictionary<AIStateEnum, AIState>();
	}

    /// <summary>
    /// 为状态机添加状态
    /// </summary>
    /// <param name="state"></param>
    public void AddState(AIState state)
    {
        if (!stateMap.ContainsKey(state.type))
        {
            //把状态机的AIAttribute分发给状态
            state.aiAttribute = aiAttribute;
            stateMap.Add(state.type, state);
        }
    }

    /// <summary>
    /// 状态机改变状态
    /// </summary>
    /// <param name="stateEnum"></param>
    /// <returns></returns>
    public bool ChangeState(AIStateEnum stateEnum)
    {
        if (!stateMap.ContainsKey(stateEnum))
        {
            Debug.Log("没有这个状态");
            return false;
        }

        if (state != null && state.type == stateEnum)
        {
            return false;
        }
        //如果当前正在执行状态，停止当前状态并退出
        if (state != null)
        {
            state.ExitState();
            aiAttribute.StopAllCoroutines();
        }

        InputManager.Clear();

        state = stateMap[stateEnum];
        state.EnterState();
        aiAttribute.StartCoroutine(state.ExcuteState());

        return true;
    }
}
