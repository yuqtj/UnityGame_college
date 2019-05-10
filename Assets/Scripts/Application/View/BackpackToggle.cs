using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class BackpackToggle : View {

    public override string Name
    {
        get
        {
            return Consts.V_BackpackToggle;
        }
    }
    //当前背包要使用的物品
    string currentNeedUseItem;
    //要使用的数量
    int num;
    
    public override void HandleEvent(string eventName, object data)
    {
        switch (eventName)
        {
            case Consts.E_OpenBackpackToggle:
                //当接收到打开背包Toggle事件时
                GameObject go = transform.Find("0").gameObject;

                EventSystem.current.SetSelectedGameObject(go);
                break;

            case Consts.E_CloseBackpackToggle:

                OnToggleClosed();

                Debug.Log("关闭背包Toggle");

                break;

            case Consts.E_UserItem:
                UseItemArgs e = data as UseItemArgs;

                currentNeedUseItem = e.itemName;
                num = e.num;

                break;
        }
    }

    public override void RegisterEvents()
    {
        AttentionEvents.Add(Consts.E_OpenBackpackToggle);
        AttentionEvents.Add(Consts.E_CloseBackpackToggle);
        AttentionEvents.Add(Consts.E_UserItem);
    }

    /// <summary>
    /// 使用物品的回调，使用物品后调用
    /// </summary>
    public void UseItemDeal(string gridName)
    {
        switch (gridName)
        {
            case "使用":

                //产生对应的效果
                ItemManager.Instance.UseItem(currentNeedUseItem, num);
                //更新背包栏 
                SendEvent(Consts.E_BackpackUpdate);
                break;

            case "不使用":
                Debug.Log("没有使用物品");
                break;
        }


        SendEvent(Consts.E_CloseBackpackToggle);

    }

    /// <summary>
    /// 当背包里的Toggle被关闭时
    /// </summary>
    public void OnToggleClosed()
    {
        //隐藏Toggle
        //gameObject.SetActive(false);
    }
}
