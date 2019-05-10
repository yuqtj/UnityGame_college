using System;
using System.Collections;
using UnityEngine;

class HumanStoryMove : StoryMoveState
{
    //-----进入剧情移动模式，这个模式应该不让玩家有任何交互

    public override void EnterState()
    {
        Debug.Log("Player is Moveing of story");

    }

    public override IEnumerator ExcuteState()
    {
        yield return null;
    }
}
