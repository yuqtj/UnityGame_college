using UnityEngine;
using System.Collections;

public class FirstStoryInit : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //如果进入了开场动画界面，先添加一个对话命令
        SendPromptImformation.OpenningAnimDialogue1();
        //设置命令后的回调，用于对话结束后出现的选项
        CommandManager.Instance.SetCommandOverCallback(StoryManager.ShowTab);
        //显示对话框
        ObjectManager.ShowDialogue();
        //开启与对话框的交互，因为这里没有主角，所以需要自己手动开启交互
        ObjectManager.GetDialogueCom().SetDialogueInteractive();
    }
	
}
