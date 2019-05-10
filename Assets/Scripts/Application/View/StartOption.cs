using UnityEngine;
using System.Collections;

public class StartOption : View {
    //视图标识
    public override string Name
    {
        get { 
            return Consts.V_StartOption; 
        }
    }

    public override void HandleEvent(string eventName, object data)
    {
    }

    //响应开始游戏按钮事件
    public void OnStartGameButton()
    {
        //加载第一个故事场景
        Game.Instance.LoadScene("2_FirstStory", false);
    }
}
