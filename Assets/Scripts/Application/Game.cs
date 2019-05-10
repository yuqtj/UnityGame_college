using UnityEngine;
using System.Collections;

public class Game : ApplicationBase<Game> {

    #region 字段

    private static string currentSceneName;

    //正式进入游戏场景，会创建人物的场景
    public static bool EnterGameScene = false;


    #endregion

    #region 属性
    //当前场景名称
    public static string CurrentSceneName
    {
        set
        {
            currentSceneName = value;
        }
        get
        {
            if(currentSceneName == null)
            {
                return null;
            }
            else
            {
                return currentSceneName;
            } 
        }
    }

    

    #endregion


    //游戏入口，初始化数据
    void Start()
    {
        //确保Game对象一直存在
        Object.DontDestroyOnLoad(this.gameObject);
        //注册开始游戏事件
        RegisterController(Consts.E_StartUp, typeof(StartUpController));
        //发送开始游戏的消息
        SendEvent(Consts.E_StartUp);
    }

    /// <summary>
    /// 封装加载场景的方法
    /// </summary>
    /// <param name="levelName">要加载的场景名</param>
    /// <param name="needShowLoadScene">是否需要过度一个加载场景，默认为true</param>
    public void LoadScene(string levelName, bool needShowLoadScene = true)
    {
        ExitSceneArgs e = new ExitSceneArgs();

        e.nextSceneName = levelName;
        e.needShowLoadScene = needShowLoadScene;
        //发送退出当前场景的消息
        SendEvent(Consts.E_ExitScene, e);
    }


    //当场景被加载时的回调函数
    void OnLevelWasLoaded(int level)
    {
        EnterSceneArgs e = new EnterSceneArgs();

        e.LevelID = level;
        //发送进入场景的事件响应
        SendEvent(Consts.E_EnterScene, e);
    }

}
