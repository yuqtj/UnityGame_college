using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ToggleCommand : BaseCommand
{
    //---------这个命令结束是在StoryManager---------
    //---------用于显示Toggle的命令-----------

    //各个选项的Text
    string[] grids;

    string npcName;

    public ToggleCommand(string contentStr, string npcName, bool needAC = false)
    {
        needAsync = needAC;

        this.npcName = npcName;

        grids = contentStr.Split(',');
    }

    public override void Excute()
    {
        if(grids != null)
        {
            MyEventSystem.Instance.TriggerChangeState(AIStateEnum.StoryMove);

            ObjectManager.CreateToggleDynamic(grids, UIPivot.Middle, ToggleType.ToggleCommand, npcName).SetActive(true);
        }
    }
    
}
