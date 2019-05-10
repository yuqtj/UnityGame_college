using UnityEngine;
using System.Collections;

public class PlayerPropertyManager{
    //---------管理着玩家的属性


    private static PlayerPropertyManager _instance;

    public static PlayerPropertyManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new PlayerPropertyManager();
            }

            return _instance;
        }
    }


    private PlayerPropertyManager()
    {
        
    }

    //存储玩家数据
    public static PlayerData playerData;

    public static GameObject PlayerGo
    {
        get
        {
            return playerData.Go;
        }
    }

    public static void AddPlayerAppear(int value)
    {
        playerData.profession += value;

        ChangePropertyEndDeal();
    }

    public static void AddPlayerEQ(int value)
    {
        playerData.emotionQ += value;

        ChangePropertyEndDeal();
    }

    public static void AddPlayerIQ(int value)
    {
        playerData.intelligenceQ += value;

        ChangePropertyEndDeal();
    }

    public static void AddPlayerMoney(int value)
    {
        playerData.money += value;

        ChangePropertyEndDeal();
    }

    public static void AddPlayerHp(int value)
    {
        playerData.hp += value;

        ChangePropertyEndDeal();
    }

    public static void AddPlayerExpenses(int value)
    {
        playerData.expenses += value;

        ChangePropertyEndDeal();
    }

    private static void ChangePropertyEndDeal()
    {
        CommandManager.Instance.CommandOver();
    }
}
