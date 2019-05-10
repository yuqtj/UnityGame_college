using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveCommand : BaseCommand
{

    //---------------这个命令结束是在PersonMove中，Xml中该命令的形式为：
    //---------------<Command CommandType = "Move" AC = "F"> X坐标, Y坐标，需要移动的人物名称 </Command>

    Vector2 targetPoint;

    PersonMove personMove;

    string npcName;

    public MoveCommand(string npcName, Vector2 targetPoint, bool needAC = false)
    {//谁移动到哪个位置
        this.npcName = npcName;
        this.targetPoint = targetPoint;
        needAsync = needAC;

        //如果是Player
        if (npcName == "Player")
        {
            personMove = PlayerPropertyManager.PlayerGo.GetComponent<AIAttribute>().GetPersonMove();
        }
        else
        {//不是玩家
            //获取这个角色身上的PersonMove组件
            personMove = ObjectManager.GetGameObject(npcName).GetComponent<AIAttribute>().GetPersonMove();
        }
    }

    public override void Excute()
    {
        MyEventSystem.Instance.TriggerChangeState(AIStateEnum.StoryMove);

        personMove.Move(targetPoint);
    }
}
