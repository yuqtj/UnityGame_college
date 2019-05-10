using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnalyXml {
    //-------------这个类主要按照一定的规则解析从XML得到的字符串---------

    /// <summary>
    /// 解析这个场景的npc
    /// </summary>
    public static string[] AnalySceneOfNpc(string sceneName)
    {
        string str = XmlIO.LoadNpcFromScene(sceneName);

        string[] npcArray = str.Split(',');

        return npcArray;
    }

    /// <summary>
    /// 解析npc一系列行为
    /// </summary>
    /// <param name="npcName"></param>
    /// <param name="step"></param>
    public static void AnalyNpcBehavior(string npcName, StoryStep step)
    {
        List<string> commandList = XmlIO.LoadNpcStory(npcName, step);
        //异步指令
        bool needAC;

        if (commandList == null)
        {
            Debug.Log("没有读取到Xml信息");
            return;
        }

        //把命令添加到命令队列中
        foreach (string item in commandList)
        {
            string[] strArray = item.Split(';');
            needAC = false;

            if (strArray[1] == "T")
            {
                needAC = true;
            }

            switch (strArray[0])
            {
                case "Dialogue":
                    AnalyDialogueCommand(strArray[2], needAC);
                    break;
                case "Move":
                    AnalyMoveCommand(strArray[2], needAC);
                    break;
                case "Turn":
                    AnalyTurnCommand(strArray[2], needAC);
                    break;
                case "Toggle":
                    AnalyToggleCommand(strArray[2], npcName, needAC);
                    break;
                case "Property":
                    AnalyPropertyCommand(strArray[2], needAC);
                    break;
            }
        }
        //开始执行命令
        CommandManager.Instance.ExcuteCommand();
    }


    /// <summary>
    /// 解析对话指令
    /// </summary>
    /// <param name="commandStr"></param>
    public static void AnalyDialogueCommand(string commandStr, bool needAC)
    {
        CommandManager.Instance.AddCommand(new DialogueCommand(commandStr, needAC));
    }

    /// <summary>
    /// 解析移动指令
    /// </summary>
    /// <param name="commandStr"></param>
    public static void AnalyMoveCommand(string commandStr, bool needAC)
    {
        commandStr = commandStr.Replace("\t", "");
        commandStr = commandStr.Replace("\n", "");
        //以逗号形式分割
        string[] strArray = commandStr.Split(',');
        //前两个为坐标点
        float x = float.Parse(strArray[0]);
        float y = float.Parse(strArray[1]);

        Vector2 targetPoint = new Vector2(x, y);

        //第3个为要移动人物的名字
        string name = strArray[2];

        CommandManager.Instance.AddCommand(new MoveCommand(name, targetPoint, needAC));
    }

    /// <summary>
    /// 解析转身指令
    /// </summary>
    /// <param name="commandStr"></param>
    /// <param name="needAC"></param>
    public static void AnalyTurnCommand(string commandStr, bool needAC)
    {
        //以逗号形式分割
        string[] strArray = commandStr.Split(',');

        PersonDirection dir = PersonDirection.None;

        switch (strArray[1])
        {
            case "Left":
                dir = PersonDirection.Left;
                break;
            case "Right":
                dir = PersonDirection.Right;
                break;
            case "Up":
                dir = PersonDirection.Up;
                break;
            case "Down":
                dir = PersonDirection.Down;
                break;
        }

        CommandManager.Instance.AddCommand(new TurnCommand(strArray[0], dir, needAC));
    }

    /// <summary>
    /// 解析选项卡指令
    /// </summary>
    /// <param name="commandStr"></param>
    /// <param name="npcName">待改变状态的npc</param>
    /// <param name="needAC"></param>
    public static void AnalyToggleCommand(string commandStr, string npcName, bool needAC)
    {
        CommandManager.Instance.AddCommand(new ToggleCommand(commandStr, npcName, needAC));
    }

    public static void AnalyPropertyCommand(string commandStr, bool needAC)
    {
        string[] strArray = commandStr.Split(',');
        int value = int.Parse(strArray[1]);

        switch(strArray[0])
        {
            case "HP":
                CommandManager.Instance.AddCommand(new ChangePropertyCommand(BasePropertyType.HP, value, needAC));
                break;
            case "IQ":
                CommandManager.Instance.AddCommand(new ChangePropertyCommand(BasePropertyType.IQ, value, needAC));
                break;
            case "EQ":
                CommandManager.Instance.AddCommand(new ChangePropertyCommand(BasePropertyType.EQ, value, needAC));
                break;
            case "MONEY":
                CommandManager.Instance.AddCommand(new ChangePropertyCommand(BasePropertyType.MONEY, value, needAC));
                break;
            case "APPEAR":
                CommandManager.Instance.AddCommand(new ChangePropertyCommand(BasePropertyType.APPEAR, value, needAC));
                break;
            case "EXPENSES":
                CommandManager.Instance.AddCommand(new ChangePropertyCommand(BasePropertyType.EXPENSES, value, needAC));
                break;
        }
    }

    /// <summary>
    /// 解析摄像机最大限制位置
    /// </summary>
    /// <param name="sceneName"></param>
    public static void AnalyCameraPos(string sceneName)
    {
        string str = XmlIO.LoadCameraPosFormScene(sceneName);

        string[] strArray = str.Split(',');

        float xMin = float.Parse(strArray[0]);
        float xMax = float.Parse(strArray[1]);
        float yMin = float.Parse(strArray[2]);
        float yMax = float.Parse(strArray[3]);

        CameraFollow._instance.GetXYThresholdPos(xMin, xMax, yMin, yMax);
    }

    /// <summary>
    /// 解析Npc的下一个状态
    /// </summary>
    /// <param name="npcName"></param>
    /// <param name="step">当前状态</param>
    public static StoryStep AnalyNpcNextStep(string npcName, StoryStep step)
    {
        string str = XmlIO.LoadNpcChangeStep(npcName, step);

        str = str.Replace("Step", "");

        int index = int.Parse(str);

        return (StoryStep)index;

    }
}
