using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogueCommand : BaseCommand {

    //---------------这个命令结束是在Dialogue中，Xml中命令形式是：
    //---------------<Command CommandType = "Dialogue" AC = "F">内容 + 回车 + 内容 + .....</Command>

    //存储对话信息
    public static Queue<string> contentQue;
    //临时存储
    public string contentStr;

    public DialogueCommand(string contentStr, bool needAC = false)
    {
        needAsync = needAC;

        if (contentQue == null)
        {
            contentQue = new Queue<string>();
        }

        this.contentStr = contentStr.Replace("\t", "");
    }

    public override void Excute()
    {
        string[] strArray = contentStr.Split('\n');
        //把每句对话都存储到对话队列中。
        for (int i = 0; i < strArray.Length; i++)
        {
            contentQue.Enqueue(strArray[i]);
        }

        //通知Npc和玩家都进入了聊天状态
        MyEventSystem.Instance.TriggerChangeState(AIStateEnum.Talk);


    }

    /// <summary>
    /// 得到下一句对话
    /// </summary>
    /// <returns></returns>
    public static string GetNextContent()
    {
        if (contentQue.Count != 0)
        {
            return contentQue.Dequeue();
        }

        return null;
    }

    /// <summary>
    /// 是否有下一句对话
    /// </summary>
    /// <returns></returns>
    public static bool HaveNextContent()
    {
        if (contentQue.Count == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
