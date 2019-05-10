using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class SkillPanel : View {

    public override string Name
    {
        get
        {
            return Consts.V_SkillPanel;
        }
    }
    //Model
    public Dictionary<string, List<SkillData>> skillData;
    //多个职业面板，View
    public List<GameObject> skillViewPortList;
    //不同职业的选项Image
    Image[] professBtnImage;
    //当前职业面板索引
    int index, tempIndex = -1;
    //01是黄色，02是灰色
    Sprite panelBtn_01, panelBtn_02;
    //职业名称
    Text professNameText;
    //所有职业名称
    List<string> professNameList;
    //职业技能滚动条
    SkillScrollBar skillScrollBar;
    //技能点
    Text skillPointText;

    void Awake()
    {
        professNameList = new List<string>();

        professBtnImage = transform.Find("ProfessBtn").GetComponentsInChildren<Image>();
        professNameText = transform.Find("ProfessName").GetComponent<Text>();
        panelBtn_01 = Resources.Load<Sprite>(Consts.SkillIconDir + "PanelBtn_01");
        panelBtn_02 = Resources.Load<Sprite>(Consts.SkillIconDir + "PanelBtn_02");

        skillScrollBar = transform.Find("Scrollbar").GetComponent<SkillScrollBar>();
        skillPointText = transform.Find("Point").GetComponent<Text>();
    }

    void Start()
    {
        foreach(string professName in skillData.Keys)
        {
            professNameList.Add(professName);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {//往左
            if (index == 0)
            {
                return;
            }

            index--;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {//往右
            if (index == skillViewPortList.Count - 1)
            {
                return;
            }

            index++;
        }

        //优化
        if (index != tempIndex)
        {
            RealUpdateViewPort();
            tempIndex = index;
        }

    }

    /// <summary>
    /// 当被打开时调用
    /// </summary>
    public void OnOpened()
    {
        //1.显示第一个ViewPort
        //skillViewPortList[0].SetActive(true);

    }

    public override void HandleEvent(string eventName, object data)
    {
        
    }

    //当更换职业技能面板时
    void RealUpdateViewPort()
    {
        RectTransform rectTrans;

        Debug.Log("更新技能面板" + (index + 1));

        for (int i = 0; i < skillViewPortList.Count; i++)
        {
            skillViewPortList[i].SetActive(false);
            professBtnImage[i].sprite = panelBtn_02;
        }

        
        skillViewPortList[index].SetActive(true);
        professBtnImage[index].sprite = panelBtn_01;
        professNameText.text = professNameList[index];

        //调整位置
        rectTrans = skillViewPortList[index].transform.Find("GridGroup").GetComponent<RectTransform>();
        rectTrans.anchoredPosition = Vector2.zero;
        //传递消息给SkillScrollBar
        skillScrollBar.SetScrollRect(rectTrans);

        UpdateLevelUpButton();
    }

    /// <summary>
    /// 更新显示技能升级按钮和技能点显示
    /// </summary>
    public void UpdateLevelUpButton()
    {
        //更新技能点显示
        skillPointText.text = PlayerPropertyManager.playerData.skillPoint.ToString();

        //检测每个技能的按钮能否被点击
        Button[] levelUpButtons = skillViewPortList[index].transform.GetComponentsInChildren<Button>();

        //是否可按下这个按钮，取决于1.技能点是否足够，2.是否符合加点的条件(比如需要点够哪个技能才能点这个技能)
        if (PlayerPropertyManager.playerData.skillPoint > 0)
        {//如果玩家有技能点，查看每一个技能是否符合要求点击
            for (int j = 0; j < levelUpButtons.Length; j++)
            {
                if (true)
                {//TODO：如果符合要求就可以加点
                    levelUpButtons[j].interactable = true;
                }
            }
        }
        else
        {//如果玩家没有技能点
            for (int j = 0; j < levelUpButtons.Length; j++)
            {//所有技能设为不可点击
                levelUpButtons[j].interactable = false;
            }
        }
    }
}
