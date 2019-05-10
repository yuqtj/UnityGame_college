using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoryManager {
    private static StoryManager _instance;

    public static StoryManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new StoryManager();
            }

            return _instance;
        }
    }



    #region 选择背景阶段

    //每个选项卡对应的数据
    private int[][] cardsData;

    public int curretnIndex;

    public BackgroundTab excuteScript;

    #endregion

    private StoryManager()
    {
        //初始化选项卡数据
        cardsData = new int[5][];

        cardsData[0] = new int[] { 5, 5, 5, 5, 5, 1000 };
        cardsData[1] = new int[] { 7, 20, 5, 6, 10, 2000 };
        cardsData[2] = new int[] { 12, 7, 20, 14, 8, 1500 };
        cardsData[3] = new int[] { 20, 5, 10, 9, 9, 1500 };
        cardsData[4] = new int[] { 7, 5, 5, 7, 25, 5000 };
    }


    /// <summary>
    /// 根据当前npc的剧情改变到下一个剧情状态
    /// </summary>
    /// <param name="npcName"></param>
    /// <param name="step"></param>
    /// <param name="immediateTrigger">需要立即触发这个状态的行为</param>
    public static void ChangeStep(string npcName, StoryStep step, bool immediateTrigger = false)
    {
        NpcData npcData = ObjectManager.GetAIData(npcName) as NpcData;

        npcData.DoingStep = step;

        if(immediateTrigger)
        {
            TalkAction.Instance.TryWithNpcDialogue();
        }

        Debug.Log(npcName + " changes " + step);
    }

    /// <summary>
    /// 用于开场动画对话后的显示选项卡
    /// </summary>
    public static void ShowTab()
    {
        ObjectManager.CanvasTrans.Find("BackgroundTab").gameObject.SetActive(true);
    }

    /// <summary>
    /// 进入场景前检验是否有剧情
    /// </summary>
    public static void CheckStoryBeforeScene()
    {

        StoryStep step = PlayerPropertyManager.playerData.DoingStep;

        if (step == StoryStep.Step0)
        {//触发第一阶段剧情
            AnalyXml.AnalyNpcBehavior("Player", step);
        }
    }

    //响应选项卡的选项，根据不同的gridName改变npc的状态
    public void OnToggleCommandClick(string gridName, string npcName, bool isCommand = false)
    {
        switch(gridName)
        {
            case "取消":
                ObjectManager.toggleGo.SetActive(false);
                excuteScript.enabled = true;
                break;

            case "确认":
                ObjectManager.playerInitData = cardsData[curretnIndex];
                Game.Instance.LoadScene(Consts.firstPlayGameScene);
                break;

            case "凉席":
                ChangeStep(npcName, StoryStep.Step1, true);
                break;
            case "收音机":
                ChangeStep(npcName, StoryStep.Step2, true);
                break;
            case "毛毯":
                ChangeStep(npcName, StoryStep.Step3, true);
                break;
        }

        if(isCommand)
        {
            //选择结束后，Destory这个Toggle
            ObjectManager.DestroyToggle();

            //响应命令结束
            CommandManager.Instance.CommandOver();
        }
        
    }

    //响应选择背景的Toggle
    public void OnBackgroundTabClick(string gridName)
    {
        switch (gridName)
        {
            case "取消":
                ObjectManager.toggleGo.SetActive(false);
                excuteScript.enabled = true;
                break;

            case "确认":
                ObjectManager.playerInitData = cardsData[curretnIndex];
                Game.Instance.LoadScene(Consts.firstPlayGameScene);
                break;
        }
    }
}
