using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class SkillManager{

    private static SkillManager _instance;
    
    public static SkillManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SkillManager();
            }

            return _instance;
        }
    } 

    private SkillManager()
    {
        skillData = XmlIO.LoadSkillInfo();

        skillNameToInfor = new Dictionary<string, SkillData>();

        foreach (List<SkillData> item in skillData.Values)
        {
            for (int i = 0; i < item.Count; i++)
            {
                StringCombine.AppendStr(Consts.SkillIconDir);

                //添加贴图信息
                item[i].iconSprite = Resources.Load<Sprite>(StringCombine.AppendStr(item[i].iconName));

                skillNameToInfor.Add(item[i].skillName, item[i]);

                StringCombine.Clear();
            }
        }
    }
    //职业名称----技能信息，无技能贴图
    public Dictionary<string, List<SkillData>> skillData;
    //技能名字----技能信息，有技能贴图
    public Dictionary<string, SkillData> skillNameToInfor; 

    public void AddSkillLevel(string skillName, int point = 1)
    {
        SkillData data;

        if (skillNameToInfor.TryGetValue(skillName, out data))
        {//如果有这个技能
            data.currentLevel++;
            
            PlayerPropertyManager.playerData.skillPoint--;
        }
    }

    public SkillData GetSkillData(string skillName)
    {
        SkillData data;

        if (skillNameToInfor.TryGetValue(skillName, out data))
        {
            return data;
        }

        return null;
    }
}
