using UnityEngine;
using System.Collections;

public enum PersonDirection
{
    None,
    Left,
    Right,
    Up,
    Down
}

public class AIData{

    //-------存储基本角色的属性

    #region 字段

    //人物ID
    protected int id;
    //npc场景里的名字
    protected string name;
    //npc当前位置
    protected Vector2 currentPos;
    //游戏物体
    protected GameObject go;
    //人物朝向
    protected PersonDirection dir;    
    //位于的场景
    protected string sceneName;
    //移动速度
    protected float speed;

    //剧情做到第几步
    StoryStep storyStep;

    #endregion

    #region 属性

    //正在执行的剧情
    public StoryStep DoingStep
    {
        get
        {
            return storyStep;
        }
        set
        {
            storyStep = value;
        }
    }

    public int ID
    {
        set
        {
            id = value;
        }
        get
        {
            return id;
        }
    }

    public string Name
    {
        get
        {
            return name;
        }
        set
        {
            name = value;
        }
    }

    public Vector2 CurrentPos
    {
        get
        {
            return currentPos;
        }
        set
        {
            currentPos = value;
        }
    }

    public GameObject Go
    {
        get
        {
            return go;
        }
        set
        {
            go = value;
        }
    }

    public PersonDirection Dir
    {
        get
        {
            return dir;
        }
        set
        {
            dir = value;
        }
    }

    public string SceneName
    {
        get
        {
            return sceneName;
        }
        set
        {
            sceneName = value;
        }
    }

    public float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            speed = value;
        }
    }

    #endregion

}
