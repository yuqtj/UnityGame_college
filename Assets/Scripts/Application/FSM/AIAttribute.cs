using UnityEngine;
using System.Collections;

public class AIAttribute : MonoBehaviour {

    //---------访问人物内部数据的组件，这个脚本放在每个角色上

    NpcData data;

    #region 得到或修改当前人物的信息

    /// <summary>
    /// 获得游戏物体
    /// </summary>
    /// <returns></returns>
    public GameObject GetGo()
    {
        if (data == null)
        {
            data = ObjectManager.GetAIData(name) as NpcData;
        }

        return data.Go;
    }

    /// <summary>
    /// 给予这个人物的Data
    /// </summary>
    /// <returns></returns>
    public NpcData GetAIData()
    {
        if (data == null)
        {
            data = ObjectManager.GetAIData(name) as NpcData;
        }

        return data;
    }

    /// <summary>
    /// 给予或动态添加PersonMove组件
    /// </summary>
    /// <returns></returns>
    public PersonMove GetPersonMove()
    {
        PersonMove personMove = GetComponent<PersonMove>();

        if (personMove == null)
        {
            personMove = gameObject.AddComponent<PersonMove>();
        }

        return personMove;
    }

    #endregion

    #region 得到或修改玩家的信息

    /// <summary>
    /// 得到玩家数据
    /// </summary>
    /// <returns></returns>
    public PlayerData GetPlayerData()
    {
        return PlayerPropertyManager.playerData;
    }

    public void SetPlayerDir(PersonDirection dir)
    {
        GetPlayerData().Dir = dir;
    }
    
    public PersonDirection GetPlayerDir()
    {
        PersonDirection dir = InputManager.GetCurrentDir();

        if (dir == PersonDirection.None)
        {//如果方向没有改变，返回原值
            return GetPlayerData().Dir;
        }

        SetPlayerDir(dir);

        return dir;
    }

    /// <summary>
    /// 玩家找上npc，根据玩家的朝向，npc转往玩家方向
    /// </summary>
    /// <param name="dir"></param>
    public void PlayerTouchYou(PersonDirection dir)
    {
        switch (dir)
        {
            case PersonDirection.Left:
                GetComponent<NpcAnimation>().SetIdle(PersonDirection.Right);
                break;
            case PersonDirection.Right:
                GetComponent<NpcAnimation>().SetIdle(PersonDirection.Left);
                break;
            case PersonDirection.Up:
                GetComponent<NpcAnimation>().SetIdle(PersonDirection.Down);
                break;
            case PersonDirection.Down:
                GetComponent<NpcAnimation>().SetIdle(PersonDirection.Up);
                break;
        }
    }

    #endregion
}
