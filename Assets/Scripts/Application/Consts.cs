using UnityEngine;
using System.Collections;

public static class Consts {
    //预设路径
    public const string PrefabDir = "Prefabs/";
    //预设UI路径
    public const string PrefabUIDir = "Prefabs/UI/";
    //贴图路径
    public const string TextureDir = "Texture/";
    //Xml场景数据路径
    public const string XmlOfScene = "Assets/Resources/XmlConfig/Scene/";
    //Xml人物数据路径
    public const string XmlOfNpc = "Assets/Resources/XmlConfig/Npc/";
    //Xml物品路径
    public const string XmlOfItem = "Assets/Resources/XmlConfig/Item/";
    //Xml技能信息路径
    public const string XmlOfSkillInfo = "Assets/Resources/XmlConfig/Skill/";
    //技能图标路径
    public const string SkillIconDir = "Texture/Skill/";


    //视图

    //开始界面的UI按钮
    public const string V_StartOption = "V_StartOption";
    //开场动画的选项卡
    public const string V_BackgroundTab = "V_BackgroundTab";
    //开场动画的选项卡
    public const string V_Togglegroup = "V_Togglegroup";
    //背包面板
    public const string V_BackpackPanel = "V_BackpackPanel";
    //主面板
    public const string V_PersonPanel = "V_PersonPanel";
    //个人属性面板
    public const string V_CharacterPropertyPanel = "V_CharacterPropertyPanel";
    //背包里的Toggle
    public const string V_BackpackToggle = "V_BackpackToggle";
    //技能面板
    public const string V_SkillPanel = "V_SkillPanel";
    //技能Grid
    public const string V_SkillGrid = "V_SkillGrid";
    //技能介绍面板
    public const string V_SkillIntroPanel = "V_SkillIntroPanel";

    //事件

    //启动游戏事件
    public const string E_StartUp = "E_StartUp";
    //退出游戏事件
    public const string E_ExitScene = "E_ExitScene";
    //进入一个新场景事件
    public const string E_EnterScene = "E_EnterScene";
    //打开背包Toggle事件
    public const string E_OpenBackpackToggle = "E_OpenBackpackToggle";
    //关闭背包Toggle事件
    public const string E_CloseBackpackToggle = "E_CloseBackpackToggle";
    //使用物品事件
    public const string E_UserItem = "E_UserItem";
    //背包更新事件
    public const string E_BackpackUpdate = "E_BackpackUpdate";

    //场景

    //第一个要加载的场景
    public const string firstLoadScene = "1_Start";

    //进入游戏的第一个场景
    public const string firstPlayGameScene = "School";
}
