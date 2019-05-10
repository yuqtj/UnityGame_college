using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class SkillGrid : View, IPointerEnterHandler{

    public string skillName;

    private Text skillLevelText;

    public override string Name
    {
        get
        {
            return Consts.V_SkillGrid;
        }
    }

    void Awake()
    {
        skillLevelText = transform.Find("SkillLevel").GetComponent<Text>();
    }
    
    public override void HandleEvent(string eventName, object data)
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ObjectManager.ShowSkillIntroPanel(skillName);
    }

    /// <summary>
    /// 响应技能提升按钮事件
    /// </summary>
    public void OnLevelUpClick()
    {
        //可以加点，通知SkillManager要加点
        SkillManager.Instance.AddSkillLevel(skillName);
        //显示到Grid上
        skillLevelText.text = SkillManager.Instance.GetSkillData(skillName).currentLevel.ToString();
        //更新显示LevelUpButton
        ObjectManager.SkillPanelGo.GetComponent<SkillPanel>().UpdateLevelUpButton();
    }
}
