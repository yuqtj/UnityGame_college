using UnityEngine;
using System.Collections;

public class HumanMove : MoveState {

    public float speed;

    private Rigidbody2D rigid;

    private Vector3 movement;

    private NpcAnimation npcAnim;

    PersonDirection dir;

    public override void EnterState()
    {
        Debug.Log("Player is Moving");

        rigid = aiAttribute.GetComponent<Rigidbody2D>();

        speed = aiAttribute.GetPlayerData().Speed;

        npcAnim = aiAttribute.GetComponent<NpcAnimation>();
    }

    public override IEnumerator ExcuteState()
    {
        //要转换的状态
        AIStateEnum nextState = AIStateEnum.None;

        dir = aiAttribute.GetPlayerDir();

        npcAnim.SetRunDir(dir);

        npcAnim.StartRunAnim();

        while (nextState == AIStateEnum.None)
        {//只要还没退出

            nextState = CheckNextState();

            dir = aiAttribute.GetPlayerDir();

            //播放动画
            npcAnim.SetRunDir(dir);
            //移动留到FixedUpdate后执行
            yield return new WaitForFixedUpdate();
            //主角移动
            Move();

            yield return 0;
        }

        npcAnim.StopRunAnim();

        aiAttribute.GetComponent<PlayerController>().stateMachine.ChangeState(nextState);

        yield return null;
    }

    void Move()
    {
        Vector2 vector2 = InputManager.GetHV();

        movement.Set(vector2.x, vector2.y, 0f);
        movement = movement.normalized * speed * Time.deltaTime;

        rigid.MovePosition(movement + rigid.transform.position);
    }

    public override AIStateEnum CheckNextState()
    {
        if (!InputManager.PressedDirKeyDown())
        {//如果在Idle状态下按下了移动键，则改变其状态
            return AIStateEnum.Idle;
        }

        if (InputManager.PressedK())
        {//如果在Idle状态下按下K键，进入控制面板
            return AIStateEnum.UIInteractive;
        }

        return AIStateEnum.None;
    }
}
