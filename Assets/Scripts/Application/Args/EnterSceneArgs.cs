using UnityEngine;
using System.Collections;

public class EnterSceneArgs {
    //----进入场景事件的参数

    //场景ID
    public int LevelID;
    //场景名
    public string CurrentSceneName
    {
        get
        {
            return Application.loadedLevelName;
        }
    }
}
