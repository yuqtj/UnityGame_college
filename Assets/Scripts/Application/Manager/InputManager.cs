using UnityEngine;
using System.Collections;

public static class InputManager {

    //-----------------------接收所有玩家的输入，然后传递给各个组件--------------------------

    public static float h, v;
    public static bool pressedJ;
    //按下K键表示面板的返回功能
    public static bool pressedK;
    //按下L键表示打开菜单栏功能
    public static bool pressedL;

    /// <summary>
    /// 是否按下了WASD
    /// </summary>
    /// <returns></returns>
    public static bool PressedDirKeyDown()
    {
        if (Mathf.Abs(v) > 0.1f || Mathf.Abs(h) > 0.1f)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// 根据现在的输入判断玩家的方向
    /// </summary>
    /// <returns></returns>
    public static PersonDirection GetCurrentDir()
    {
        if (h != 0)
        {//x轴方向移动
            if (h < 0)
            {//向左
                return PersonDirection.Left;
            }
            else
            {//向右
                return PersonDirection.Right;
            }
        }
        else if (v != 0)
        {//y轴方向移动
            if (v < 0)
            {//向上
                return PersonDirection.Down;
            }
            else
            {//向下
                return PersonDirection.Up;
            }
        }

        return PersonDirection.None;
    }

    /// <summary>
    /// 得到h和v其中一个的值，因为这个游戏中人物不可能斜着移动
    /// </summary>
    /// <returns></returns>
    public static Vector2 GetHV()
    {
        Vector2 vetor2;

        if (h == 0)
        {
            vetor2 = new Vector2(0, v);
        }
        else
        {
            vetor2 = new Vector2(h, 0);
        }

        return vetor2;
    }

    /// <summary>
    /// 判断是否按下K
    /// </summary>
    /// <returns></returns>
    public static bool PressedK()
    {
        return pressedK;
    }

    public static bool PressedJ()
    {
        return pressedJ;
    }

    /// <summary>
    /// 防止当玩家转变状态时还保留着按键输入状态
    /// </summary>
    public static void Clear()
    {
        pressedJ = false;
        pressedK = false;
    }
}
