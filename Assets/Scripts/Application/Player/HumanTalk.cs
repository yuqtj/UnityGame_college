using UnityEngine;
using System.Collections;

public class HumanTalk : TalkState {
    public override void EnterState()
    {
        TalkAction.Instance.ShowDialogue();

        Debug.Log("Player is Talking");
    }

    public override IEnumerator ExcuteState()
    {
        //yield return new WaitForSeconds(0.1f);

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
        if (InputManager.PressedJ())
        {
            TalkAction.Instance.TalkingDeal();
        }

        return AIStateEnum.None;
    }
}
