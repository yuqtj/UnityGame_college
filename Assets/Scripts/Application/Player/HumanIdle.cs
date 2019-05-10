using UnityEngine;
using System.Collections;

public class HumanIdle : IdleState {

    PersonDirection dir;

    public override void EnterState()
    {
        Debug.Log("Player is Idle");
    }

    public override IEnumerator ExcuteState()
    {
        //要转换的状态
        AIStateEnum nextState = AIStateEnum.None;

        while (nextState == AIStateEnum.None)
        {//只要待转换的状态还是空
            nextState = CheckNextState();
            //不加这个unity会崩溃
            yield return 0;
        }

        yield return 0;
    }

    public override AIStateEnum CheckNextState()
    {
        if (InputManager.PressedDirKeyDown())
        {//如果在Idle状态下按下了移动键，则改变其状态
            MyEventSystem.Instance.TriggerChangeState(AIStateEnum.Move);
        }

        if (InputManager.PressedJ())
        {//如果在Idle状态下按下J键，发起剧情，
            //因为剧情不一定先是对话，所以不在这里发送事件改变玩家状态
            //如果有npc，发出射线
            TalkAction.Instance.TryWithNpcDialogue();
        }

        if (InputManager.PressedK())
        {//如果在Idle状态下按下K键，进入控制面板
         //打开面板
            ObjectManager.ShowPersonPanel();

            MyEventSystem.Instance.TriggerChangeState(AIStateEnum.UIInteractive);
        }

        return AIStateEnum.None;
    }
}
