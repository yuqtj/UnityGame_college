using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//命令结束的委托
public delegate void CommandOverCallBack();

public class CommandManager {

    private static CommandManager _instance;

    public static CommandManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new CommandManager();
            }
            return _instance;
        }
    }

    //当前执行命令数量
    int currentCommmandNum;

    //执行命令队列
    Queue<BaseCommand> commandQueue;

    //当前命令
    BaseCommand currentCommand;

    //命令结束的回调
    public CommandOverCallBack callBack;

    private CommandManager()
    {
        commandQueue = new Queue<BaseCommand>();
        currentCommmandNum = 0;
    }

    /// <summary>
    /// 执行命令
    /// </summary>
    public void ExcuteCommand()
    {
        //如果队列中有命令
        if (CheckNextCommand())
        {//执行命令
            currentCommmandNum++;

            currentCommand.Excute();

            if (currentCommand != null && currentCommand.needAsync)
            {//只要当前是异步的，继续执行命令
                ExcuteCommand();
            }
        }
        else
        {//如果队列中没有命令，先查看当前是否有命令执行
            if (currentCommmandNum == 0)
            {//如果当前没有命令执行， 通知玩家回到Idle状态
                currentCommand = null;

                MyEventSystem.Instance.TriggerChangeState(AIStateEnum.Idle);

                if (callBack != null)
                {
                    callBack();
                    //执行后清空
                    callBack = null;
                }

                Debug.Log("命令执行完毕");
            }
        }
    }

    /// <summary>
    /// 添加命令
    /// </summary>
    /// <param name="item"></param>
    public void AddCommand(BaseCommand item)
    {
        commandQueue.Enqueue(item);
    }

    bool CheckNextCommand()
    {
        if (commandQueue.Count != 0)
        {//如果命令队列还有命令，就继续执行下一个命令
            currentCommand = commandQueue.Dequeue();

            return true;
        }
        return false;
    }

    /// <summary>
    /// 命令结束
    /// </summary>
    public void CommandOver()
    {
        if (currentCommmandNum == 1)
        {//如果当前正在执行的命令为1
            currentCommmandNum = 0;
            ExcuteCommand();
        }
        else
        {
            currentCommmandNum--;
        }
    }

    /// <summary>
    /// 设置命令结束后的回调
    /// </summary>
    public void SetCommandOverCallback(CommandOverCallBack callBack)
    {
        this.callBack = callBack;
    }

}
