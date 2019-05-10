using UnityEngine;
using System.Collections;

//-----Xml中命令格式为：
//-----<Command CommandType = "Turn" AC = "F">人物名称，转的方向(例Right、Left)</Command>

public class TurnCommand : BaseCommand
{
    string npcName;
    PersonDirection dir;

    public TurnCommand(string npcName, PersonDirection turnDir, bool needAC = false)
    {
        this.npcName = npcName;
        needAsync = needAC;
        dir = turnDir;
    }

    public override void Excute()
    {
        if (npcName == "Player")
        {
            PlayerPropertyManager.PlayerGo.GetComponent<NpcAnimation>().SetIdle(dir);
        }
        else
        {
            ObjectManager.GetGameObject(npcName).GetComponent<NpcAnimation>().SetIdle(dir);
        }

        CommandManager.Instance.CommandOver();
    }
}
