using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SkillIntroPanel : MonoBehaviour {

    Text skillNameLab, maxLevel, skillIntro, curSkillIntro, actualEffect;
    Image skillIcon;

    void Awake()
    {
        skillNameLab = transform.Find("SkillName").GetComponent<Text>();
        maxLevel = transform.Find("MaxLevel").GetComponent<Text>();
        skillIntro = transform.Find("SkillIntro").GetComponent<Text>();
        curSkillIntro = transform.Find("CurSkillIntro").GetComponent<Text>();
        actualEffect = transform.Find("ActualEffect").GetComponent<Text>();
        skillIcon = transform.Find("SkillIcon").GetComponent<Image>();
    }


	public void UpdateInformation(string skillName)
    {
        Dictionary<string, SkillData> skillNameToInfor = SkillManager.Instance.skillNameToInfor;
        SkillData data;

        if (skillNameToInfor.ContainsKey(skillName))
        {//如果有这个技能，更新技能介绍面板
            skillNameToInfor.TryGetValue(skillName, out data);

            skillNameLab.text = skillName;

            //拼接最高等级字符串
            StringCombine.AppendStr("[最高等级 : ");
            StringCombine.AppendStr(data.maxLevel.ToString());
            StringCombine.AppendStr("]");
            maxLevel.text = StringCombine.GetCurrentStr();
            StringCombine.Clear();
            //介绍面板上的详细说明
            skillIntro.text = data.des1;

            StringCombine.AppendStr("[当前等级 : ");
            StringCombine.AppendStr(data.currentLevel.ToString());
            StringCombine.AppendStr("]");
            curSkillIntro.text = StringCombine.GetCurrentStr();
            StringCombine.Clear();

            skillIcon.sprite = data.iconSprite;
        }
    }
}
