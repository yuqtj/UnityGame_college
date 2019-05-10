using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerData : AIData
{
    //生命值，关系到玩家的身体素质---Sport
    public int hp;
    //智商，决定学习能力和挂科率
    public int intelligenceQ;
    //情商，决定与人沟通好感度的增加速度
    public int emotionQ;
    //财富水平，取决于每个月的生活费
    public int expenses;
    //专业水平
    public int profession;
    //玩家的金钱
    public int money;
    //玩家技能点
    public int skillPoint;
    //背包物品
    public Dictionary<string, ItemData> itemData;

}
