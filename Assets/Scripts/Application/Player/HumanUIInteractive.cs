using UnityEngine;
using System.Collections;

public class HumanUIInteractive : InteractiveState
{

    public override void EnterState()
    {
        Debug.Log("Player is UIInteractive");
    }

    public override IEnumerator ExcuteState()
    {
        //要转换的状态
        AIStateEnum nextState = AIStateEnum.None;

        while (nextState == AIStateEnum.None)
        {//只要待转换的状态还是空
            nextState = CheckNextState();

            yield return 0;
        }

        yield return 0;
    }

    public override AIStateEnum CheckNextState()
    {
        //if (InputManager.PressedK())
        //{
        //    ObjectManager.HidePersonPanel();

        //    MyEventSystem.Instance.TriggerChangeState(AIStateEnum.Idle);
        //}

        return AIStateEnum.None;
    }
}
