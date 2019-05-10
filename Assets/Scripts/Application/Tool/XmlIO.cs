using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;
using System.Collections.Generic;

public class XmlIO {
    
    /// <summary>
    /// 根据npc现在的状态加载npc的剧情
    /// </summary>
    public static List<string> LoadNpcStory(string npcName, StoryStep step)
    {
        int index = (int)step;
        string strIndex = "Step" + index;
        List<string> commandList = new List<string>();

        XmlDocument xml = new XmlDocument();

        Debug.Log("打开了" + Consts.XmlOfNpc + npcName + ".xml文件");

        xml.Load(XmlReader.Create(Consts.XmlOfNpc + npcName + ".xml"));
        //寻找根节点
        XmlNodeList nodeList = xml.SelectSingleNode(npcName).ChildNodes;

        foreach (XmlElement x1 in nodeList)
        {
            if (x1.Name == "Story")
            {
                foreach (XmlElement x2 in x1)
                {
                    if (strIndex == x2.Name)
                    {
                        foreach (XmlElement x3 in x2)
                        {//命令类型与具体命令用 ; 分割
                            commandList.Add(x3.GetAttribute("CommandType") + ";" + x3.GetAttribute("AC") + ";" + x3.InnerText);
                        }
                    }
                }
            }
        }

        return commandList;
    }

    /// <summary>
    /// 根据Npc现在的剧情状态转换下一个剧情状态
    /// </summary>
    /// <param name="npcName"></param>
    /// <param name="step"></param>
    /// <returns></returns>
    public static string LoadNpcChangeStep(string npcName, StoryStep step)
    {
        int index = (int)step;
        string strIndex = "Step" + index;

        XmlDocument xml = new XmlDocument();
        xml.Load(XmlReader.Create(Consts.XmlOfNpc + npcName + ".xml"));

        XmlNodeList nodeList = xml.SelectSingleNode(npcName).ChildNodes;

        foreach (XmlElement x1 in nodeList)
        {
            if (x1.Name == "ChangeStep")
            {
                foreach (XmlElement x2 in x1)
                {
                    if (x2.Name == strIndex)
                    {
                        return x2.InnerText;
                    }
                }
            }
        }

        return null;
    }

    /// <summary>
    /// 根据所处的场景加载该场景里的npc
    /// </summary>
    public static string LoadNpcFromScene(string currentScene)
    {
        XmlDocument xml = new XmlDocument();
        xml.Load(XmlReader.Create(Consts.XmlOfScene + currentScene + ".xml"));

        //寻找根节点
        XmlNodeList nodeList = xml.SelectSingleNode(currentScene).ChildNodes;

        foreach (XmlElement item in nodeList)
        {
            if (item.Name == "Npc")
            {
                return item.InnerText;
            }
        }

        return null;
    }

    /// <summary>
    /// 根据所处场景记载该场景的摄像机阈值坐标
    /// </summary>
    /// <param name="currentScene"></param>
    /// <returns></returns>
    public static string LoadCameraPosFormScene(string currentScene)
    {
        string str = "";

        XmlDocument xml = new XmlDocument();
        xml.Load(XmlReader.Create(Consts.XmlOfScene + currentScene + ".xml"));

        //寻找根节点
        XmlNodeList nodeList = xml.SelectSingleNode(currentScene).ChildNodes;

        foreach (XmlElement x1 in nodeList)
        {
            if (x1.Name == "CameraPos")
            {
                foreach (XmlElement x2 in x1)
                {
                    if (x2.Name == "X")
                    {
                        str = x2.InnerText + ",";
                    }

                    if (x2.Name == "Y")
                    {
                        str += x2.InnerText;
                    }
                }
            }
        }

        return str;
    }

    /// <summary>
    /// 加载道具的详细信息
    /// </summary>
    /// <returns></returns>
    public static Dictionary<string, ItemData> LoadItemsInfo()
    {
        Dictionary<string, ItemData> itemDataDic = new Dictionary<string, ItemData>();

        XmlDocument xml = new XmlDocument();
        xml.Load(XmlReader.Create(Consts.XmlOfItem + "ItemsInfo.xml"));

        //寻找根节点
        XmlNodeList nodeList = xml.SelectSingleNode("Items").ChildNodes;

        foreach (XmlElement x1 in nodeList)
        {//遍历每一个物品信息
            ItemData data = new ItemData();
            data.itemName = x1.GetAttribute("name");

            foreach (XmlElement x2 in x1)
            {//遍历物品的每一个属性
                switch(x2.Name)
                {
                    case "Info":
                        data.itemInfo = x2.InnerText;
                        break;
                    case "iq":
                        data.iq = int.Parse(x2.InnerText);
                        break;
                    case "eq":
                        data.eq = int.Parse(x2.InnerText);
                        break;
                    case "hp":
                        data.hp = int.Parse(x2.InnerText);
                        break;
                    case "money":
                        data.money = int.Parse(x2.InnerText);
                        break;
                    case "appear":
                        data.appear = int.Parse(x2.InnerText);
                        break;

                }
                

            }

            itemDataDic.Add(data.itemName, data);
        }

        return itemDataDic;
    }

    /// <summary>
    /// 加载技能信息
    /// </summary>
    /// <returns></returns>
    public static Dictionary<string, List<SkillData>> LoadSkillInfo()
    {
        List<SkillData> skillList;
        Dictionary<string, List<SkillData>> skillData = new Dictionary<string, List<SkillData>>();

        XmlDocument xml = new XmlDocument();
        xml.Load(XmlReader.Create(Consts.XmlOfSkillInfo + "SkillInfo.xml"));

        //寻找根节点
        XmlNodeList nodeList = xml.SelectSingleNode("SkillInfo").ChildNodes;

        foreach (XmlElement x1 in nodeList)
        {//每个职业
            skillList = new List<SkillData>();

            foreach (XmlElement x2 in x1)
            {//每个技能
                SkillData data = new SkillData();

                data.skillName = x2.Name;

                foreach (XmlElement x3 in x2)
                {//每列说明
                    switch (x3.Name)
                    {
                        case "最高等级":
                            data.maxLevel = int.Parse(x3.InnerText);
                            break;
                        case "技能图标名":
                            data.iconName = x3.InnerText;
                            break;
                        case "详细说明1":
                            data.des1 = x3.InnerText;
                            break;
                    }
                }

                skillList.Add(data);

            }

            skillData.Add(x1.Name, skillList);
        }

        return skillData;
    }
}
