using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum AIStateEnum
{
    None,
    Idle,
    Move,
    Talk,
    StoryMove, //剧情模式
    UIInteractive, //UI交互状态
}

public class AIState {

    public AIStateEnum type = AIStateEnum.None;
    //通过这个可让状态类能访问该游戏物体数据
    public AIAttribute aiAttribute;
    //存储着事件类型
    public List<EveType> regEvent;

    public virtual void EnterState()
    {
    }

    public virtual void ExitState()
    {
    }

    public virtual IEnumerator ExcuteState()
    {
        yield return null;
    }

    /// <summary>
    /// 执行状态时调用这个函数，检测是否符合条件执行这个状态，返回True表示要转换为其他状态
    /// </summary>
    /// <returns></returns>
    protected void CheckEvent()
    {
    }

    /// <summary>
    /// 更换状态时用到这个函数，检测是否符合切换状态的要求
    /// </summary>
    /// <param name="next"></param>
    /// <returns></returns>
    public virtual AIStateEnum CheckNextState()
    {
        return AIStateEnum.None;
    }

    /// <summary>
    /// 注册事件
    /// </summary>
    public void RegEvent()
    {
        if (regEvent != null)
        {
            foreach (EveType item in regEvent)
            {
                MyEventSystem.Instance.RegisterEvent(item, OnEvent);
            }
        }
    }

    public virtual void OnEvent(EventArgs args)
    {

    }
}

public class IdleState : AIState
{
    protected IdleState()
    {
        type = AIStateEnum.Idle;
    }
}

public class MoveState : AIState
{
    protected MoveState()
    {
        type = AIStateEnum.Move;
    }
}

public class TalkState : AIState
{
    protected TalkState()
    {
        type = AIStateEnum.Talk;
    }
}

public class StoryMoveState : AIState
{
    protected StoryMoveState()
    {
        type = AIStateEnum.StoryMove;
    }
}

public class InteractiveState : AIState
{
    protected InteractiveState()
    {
        type = AIStateEnum.UIInteractive;
    }
}
