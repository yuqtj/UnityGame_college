using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class EnterSceneController : Controller
{
    public override void Excute(object data)
    {
        EnterSceneArgs e = data as EnterSceneArgs;

        Debug.Log("进入了" + e.CurrentSceneName + "场景");

        switch (e.CurrentSceneName)
        {
            case "School":
                //设置标识
                Game.EnterGameScene = true;

                //进入第一个场景
                ObjectManager.CreateObj();

                //是否有要触发的剧情
                //StoryManager.CheckStoryBeforeScene();

                //注册视图：主面板、个人属性面板、背包面板、背包Toggle
                RegisterView(ObjectManager.PersonPanelGo.GetComponent<PersonPanel>());
                RegisterView(ObjectManager.CharacterPropertyPanelGo.GetComponent<CharacterPropertyPanel>());
                RegisterView(ObjectManager.BackpackPanelGo.GetComponent<BackpackPanel>());
                RegisterView(ObjectManager.BackpackToggleGo.GetComponent<BackpackToggle>());


                break;

            case "Room1":
                ObjectManager.CreateObj();
                break;
        }
    }
}
