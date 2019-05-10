using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemManager {

    private static ItemManager _instance;

    public static ItemManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new ItemManager();
            }

            return _instance;
        }
    }


    Dictionary<string, ItemData> itemsData;

    private ItemManager()
    {
    }

    /// <summary>
    /// 通过Xml加载物品信息到内存中
    /// </summary>
    public void LoadItemsInfo()
    {
        itemsData = XmlIO.LoadItemsInfo();
    }

    /// <summary>
    /// 使用物品
    /// </summary>
    /// <param name="itemName"></param>
    /// <param name="num"></param>
    public void UseItem(string itemName, int num = 1)
    {
        //1.给玩家添加响应的效果

        //2.减少响应的物品数量
        SubItem(itemName, num);

        Debug.Log("使用了" + num + "个" + itemName);
    }

    /// <summary>
    /// 给背包添加物体
    /// </summary>
    /// <param name="itemName">物品名称</param>
    /// <param name="num">要添加的数量</param>
    public void AddItem(string itemName, int num = 1)
    {
        Dictionary<string, ItemData> data = PlayerPropertyManager.playerData.itemData;


        if (data.ContainsKey(itemName))
        {//如果玩家身上有这个物品,直接叠加
            data[itemName].itemNum += num;
        }
        else
        {//如果玩家身上没有这个物品
            ItemData itemData = null;

            if(itemsData.ContainsKey(itemName))
            {
                itemsData.TryGetValue(itemName, out itemData);

                itemData.itemNum = num;

                data.Add(itemName, itemData);

                Debug.Log("添加了" + itemName);
            }
            else
            {
                Debug.Log("Xml里没有这个物品, 添加物品失败！");
            }

        }
    }

    /// <summary>
    /// 减少物品
    /// </summary>
    /// <param name="itemName"></param>
    /// <param name="num"></param>
    public void SubItem(string itemName, int num = 1)
    {
        Dictionary<string, ItemData> data = PlayerPropertyManager.playerData.itemData;

        if (data.ContainsKey(itemName))
        {//如果玩家身上有这个物品
            data[itemName].itemNum -= num;

            //如果这个物品数量已经为0了，移除
            if (data[itemName].itemNum <= 0)
            {
                data.Remove(itemName);
            }
        }
        else
        {
            Debug.Log("玩家身上没有这个物品，减少失败！");
        }
    }
}
