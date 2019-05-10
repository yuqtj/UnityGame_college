using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class PersonPanel : View {

    public Stack<GameObject> UIStack;
    //角色属性面板
    private GameObject characterPropertyPanel;
    //背包面板
    private GameObject backpackPanel;
    //技能面板
    private GameObject skillPanel;
    //角色面板下的第一个按钮
    private Button[] buttons;

    public override string Name
    {
        get
        {
            return Consts.V_PersonPanel;
        }
    }

    void Awake()
    {
        UIStack = new Stack<GameObject>();
        //本自身压入栈中
        UIStack.Push(gameObject);


    }	

    void Start()
    {
        //设置Button按钮回调
        buttons = gameObject.GetComponentsInChildren<Button>();
        
        buttons[0].onClick.AddListener(delegate ()
        {
            OnPersonPanelClick(buttons[0].name);
        });

        buttons[1].onClick.AddListener(delegate ()
        {
            OnPersonPanelClick(buttons[1].name);
        });

        buttons[2].onClick.AddListener(delegate ()
        {
            OnPersonPanelClick(buttons[2].name);
        });

        buttons[3].onClick.AddListener(delegate ()
        {
            OnPersonPanelClick(buttons[3].name);
        });

        characterPropertyPanel = ObjectManager.CharacterPropertyPanelGo;
        backpackPanel = ObjectManager.BackpackPanelGo;
        skillPanel = ObjectManager.SkillPanelGo;
    }

    // Update is called once per frame
    void Update () {
        //当玩家按K键时，返回上一级UI
        if (InputManager.PressedK())
        {
            if(UIStack.Count == 1)
            {//如果UI栈上只有个人面板本身，就隐藏UI
                MyEventSystem.Instance.TriggerChangeState(AIStateEnum.Idle);

                gameObject.SetActive(false);

                Debug.Log("关闭人物面板");

                return;
            }

            CloseUI();

            if (UIStack.Count == 1)
            {//如果返回到还有个人面板，恢复按钮聚焦，设置第一个按钮聚焦
                for (int i = 0; i < buttons.Length; i++)
                {
                    buttons[i].interactable = true;
                }

                EventSystem.current.SetSelectedGameObject(buttons[0].gameObject);
            }
        }
    }

    /// <summary>
    /// 关闭最上层的UI
    /// </summary>
    void CloseUI()
    {
        //如果不止有一个UI显示了，隐藏最上面的UI
        GameObject go = UIStack.Pop();

        if(go.name == "BackPack")
        {//如果要关闭的UI是背包面板
            go.GetComponent<BackpackPanel>().OnClosed();

            Debug.Log("关闭背包面板");
        }
        else if (go.name == "Toggle")
        {
            SendEvent(Consts.E_CloseBackpackToggle);
        }
        else if (go.name == "SkillPanel")
        {
            
        }

        go.SetActive(false);
    }

    /// <summary>
    /// 入栈
    /// </summary>
    public void PushUI(GameObject go)
    {
        UIStack.Push(go);
    }


    #region 按钮事件回调

    public void OnPersonPanelClick(string buttonName)
    {
        //使PersonPanel上的Button不能被聚焦
        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }

        switch (buttonName)
        {//显示UI并压入栈中
            case "Character":
                Debug.Log("打开人物属性面板");

                characterPropertyPanel.SetActive(true);
                UIStack.Push(characterPropertyPanel);
                //更新玩家数据
                characterPropertyPanel.GetComponent<CharacterPropertyPanel>().UpdatePanelData();

                EventSystem.current.SetSelectedGameObject(characterPropertyPanel);
                
                break;
            case "BackPack":
                Debug.Log("打开背包面板");

                backpackPanel.SetActive(true);
                UIStack.Push(backpackPanel);
                //更新背包的物品显示并聚焦到背包的第一个Grid
                backpackPanel.GetComponent<BackpackPanel>().UpdateItemGrid();

                break;
            case "Skill":
                Debug.Log("打开技能面板");

                skillPanel.SetActive(true);
                UIStack.Push(skillPanel);

                skillPanel.GetComponent<SkillPanel>().OnOpened();

                break;
            case "Exit":
                gameObject.SetActive(false);

                MyEventSystem.Instance.TriggerChangeState(AIStateEnum.Idle);
                break;

        }
    }

    public override void RegisterEvents()
    {
        AttentionEvents.Add(Consts.E_OpenBackpackToggle);
        AttentionEvents.Add(Consts.E_CloseBackpackToggle);
    }

    public override void HandleEvent(string eventName, object data)
    {
        switch(eventName)
        {
            case Consts.E_OpenBackpackToggle:
                //当接收到打开背包Toggle事件时
                UIStack.Push(ObjectManager.BackpackToggleGo);

                Debug.Log("打开背包Toggle");
                break;

            case Consts.E_CloseBackpackToggle:
                //当关闭时背包Toggle时
                if (UIStack.Peek().name == "Toggle")
                {//如果Toggle还没出栈
                    UIStack.Pop().SetActive(false);
                }
                break;
        }
    }

    #endregion

}
