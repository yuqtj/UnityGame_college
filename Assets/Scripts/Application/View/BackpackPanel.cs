using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class BackpackPanel : View {
    //背包里的物品链表 View
    private List<GameObject> items;
    //背包里的物品数据链表 Model
    private List<ItemData> itemsData;
    //当前被选择的Item
    private GameObject currentSelectedItem;
    //item名字
    private Text itemName;
    //item信息
    private Text itemInfo;
    //当前是否显示背包的Toggle
    private string currentItemName;
    //当前界面显示着背包界面
    private bool showBackpack;

    public override string Name
    {
        get
        {
            return Consts.V_BackpackPanel;
        }
    }


    // Use this for initialization
    void Awake () {
        items = new List<GameObject>();
        itemsData = new List<ItemData>();
        itemName = transform.Find("Bg/ItemName").GetComponent<Text>();
        itemInfo = transform.Find("Bg/ItemInfo").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        //如果当前选择的Item改变了，更新背包面板上的物品名字和信息

        if (EventSystem.current.currentSelectedGameObject != currentSelectedItem && showBackpack)
        {
            currentSelectedItem = EventSystem.current.currentSelectedGameObject;
            RealTimeShowItemInfo(currentSelectedItem);
        }
        
    }

    /// <summary>
    /// 根据玩家的数据更新背包的物品
    /// </summary>
    public void UpdateItemGrid()
    {
        //清空链表
        items.Clear();
        itemsData.Clear();

        Dictionary<string, ItemData> data = PlayerPropertyManager.playerData.itemData;
        //更新面板脚本的数据
        foreach(ItemData item in data.Values)
        {
            //创建Grid，由ObjectManager创建
            items.Add(ObjectManager.CreateItem(item));

            itemsData.Add(item);
        }

        if(items.Count != 0)
        {//如果背包里有物品
         //使背包里的物品恢复焦点
            for (int i = 0; i < items.Count; i++)
            {
                items[i].GetComponent<Button>().interactable = true;
            }

            EventSystem.current.SetSelectedGameObject(RealTimeShowItemInfo(currentSelectedItem));

            Debug.Log("当前被选择的Item是: " + currentSelectedItem.GetComponentInChildren<Text>().text);
        }

        for(int i = 0; i < items.Count; i++)
        {//建立事件关联
            items[i].GetComponent<Button>().onClick.AddListener(delegate()
            {
                OnUseItemEvent();
            });
        }

        showBackpack = true;

        Debug.Log("更新背包");

    }

    /// <summary>
    /// 实时更新物品信息
    /// </summary>
    GameObject RealTimeShowItemInfo(GameObject selectedGo)
    {
        if (currentSelectedItem == null)
        {
            currentSelectedItem = items[0];

            return items[0];
        }

        for (int i = 0; i < items.Count; i++)
        {
            if(selectedGo == items[i])
            {//如果找到了这个物体
                itemName.text = itemsData[i].itemName;
                itemInfo.text = itemsData[i].itemInfo;

                currentItemName = itemsData[i].itemName;

                return items[i];
            }
        }

        itemName.text = itemsData[0].itemName;
        itemInfo.text = itemsData[0].itemInfo;
        currentItemName = itemsData[0].itemName;
        currentSelectedItem = items[0];

        //如果找不到，就返回第一个
        return items[0];
    }

    /// <summary>
    /// 使用物品事件，选择物品后调用
    /// </summary>
    void OnUseItemEvent()
    {
        //使背包里的物品不能被聚焦
        for (int i = 0; i < items.Count; i++)
        {
            items[i].GetComponent<Button>().interactable = false;
        }

        //显示Toggle
        ObjectManager.BackpackToggleGo.SetActive(true);

        //消隐Button，意味这个被选中
        currentSelectedItem.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f);

        //给主面板发送指令，让它把Toggle压入栈中
        SendEvent(Consts.E_OpenBackpackToggle);

        UseItemArgs e = new UseItemArgs();
        e.itemName = currentItemName;
        e.num = 1;
        SendEvent(Consts.E_UserItem, e);
    }

    public void OnClosed()
    {
        //当被关闭的时候

        //1.把面板上的Grid存回队列中
        for (int i = 0; i < items.Count; i++)
        {
            ObjectManager.ItemEnQueue(items[i]);
            items[i].SetActive(false);
        }
    }

    void OnToggleClose()
    {
        //选过后，恢复背包里的按钮聚焦
        for (int i = 0; i < items.Count; i++)
        {
            items[i].GetComponent<Button>().interactable = true;
            //取消被选中
            items[i].GetComponent<Image>().color = new Color(1f, 1f, 1f);

        }

        EventSystem.current.SetSelectedGameObject(currentSelectedItem);
    }


    public override void HandleEvent(string eventName, object data)
    {
        switch(eventName)
        {
            case Consts.E_CloseBackpackToggle:
                OnToggleClose();

                showBackpack = true;
                break;

            case Consts.E_BackpackUpdate:
                OnClosed();

                UpdateItemGrid();
                break;

            case Consts.E_OpenBackpackToggle:
                showBackpack = false;
                break;
        }
    }

    public override void RegisterEvents()
    {
        AttentionEvents.Add(Consts.E_CloseBackpackToggle);
        AttentionEvents.Add(Consts.E_BackpackUpdate);
        AttentionEvents.Add(Consts.E_OpenBackpackToggle);
    }
}
