using UnityEngine;
using System.Collections;

public class SendPromptImformation{

    /// <summary>
    /// 开场动画的对话1
    /// </summary>
    public static void OpenningAnimDialogue1()
    {
        string commandStr = "欢迎来到口袋妖怪的世界\n你好，我是大木博士，也就是人们说的口袋怪兽博士。\n虽然我们的游戏跟口袋怪兽没有任何联系......\n你将会作为一名计算机专业的大学生在学校度过4年的时间。而且没有女朋友~\n开玩笑的，请先说一下你的背景吧。";
        //加载对话命令
        CommandManager.Instance.AddCommand(new DialogueCommand(commandStr));
        //执行命令
        CommandManager.Instance.ExcuteCommand();
    }

	public static void BeFriendWithNpc(string npcName)
    {
        CommandManager.Instance.AddCommand(new DialogueCommand("你与" + npcName + "成为了好友！"));

        CommandManager.Instance.ExcuteCommand();
    }
}
