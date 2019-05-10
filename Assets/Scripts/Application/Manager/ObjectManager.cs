using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public enum UIPivot
{
    Middle,
    RightDown
}

public enum ToggleType
{
    BackgroundTab,
    ToggleCommand,
    Backpack
}

public class ObjectManager
{

    #region 字段

    public static Transform bgTrans;

    public static GameObject dialogue;

    public static Transform canvas;
    //通用选项卡
    public static GameObject toggleGo;
    //背包使用/取消选项卡
    public static GameObject backpackToggleGo;
    //角色主面板
    public static GameObject personPanel;
    //角色属性面板
    public static GameObject characterPropertyPanel;
    //角色背包面板
    public static GameObject backpackPanel;
    //背包的Grid队列，对象池
    public static Queue<GameObject> gridQueue;
    //Item的父节点
    public static Transform itemParentTrans;
    //技能面板
    public static GameObject skillPanelGo;
    //技能多个职业列表
    public static List<GameObject> skillViewPortList;
    //技能介绍面板
    public static GameObject skillIntroPanel;

    //存储场景中npc角色
    public static Dictionary<string, AIData> npcDic;
    //玩家通过背景卡得到的6项数据
    public static int[] playerInitData;



    #endregion

    #region 属性

    //对话框
    public static GameObject DialogueGo
    {
        get
        {
            if (dialogue == null)
            {
                CreateDialogue();
            }

            return dialogue;
        }
    }
    //背景图片
    public static Transform BgTrans
    {
        get
        {
            if (bgTrans == null)
            {
                bgTrans = GameObject.FindWithTag("bg").transform;
            }

            return bgTrans;
        }
    }
    //画布
    public static Transform CanvasTrans
    {
        get
        {
            if (canvas == null)
            {
                canvas = GameObject.FindWithTag("Canvas").transform;
            }

            return canvas;
        }
    }



    public static GameObject PersonPanelGo
    {
        get
        {
            if (personPanel == null)
            {
                CreatePersonPanel();
            }

            return personPanel;
        }
    }

    public static GameObject CharacterPropertyPanelGo
    {
        get
        {
            if (characterPropertyPanel == null)
            {
                CreateCharacterPropertyPanel();
            }

            return characterPropertyPanel;
        }
    }

    public static GameObject BackpackPanelGo
    {
        get
        {
            if (backpackPanel == null)
            {
                CreateBackpackPanel();
            }

            return backpackPanel;
        }
    }

    public static GameObject BackpackToggleGo
    {
        get
        {
            if (backpackToggleGo == null)
            {
                CreateBackpackToggle();
            }

            return backpackToggleGo;
        }
    }

    public static GameObject SkillPanelGo
    {
        get
        {
            if (skillPanelGo == null)
            {
                CreateSkillPanel();
            }

            return skillPanelGo;
        }
    }

    public static GameObject SkillIntroPanelGo
    {
        get
        {
            if (skillIntroPanel == null)
            {
                CreateSkillIntroPanel();
            }

            return skillIntroPanel;
        }
    }

    #endregion

    #region 方法

    /// <summary>
    /// 初始化
    /// </summary>
    static void Init()
    {
        npcDic = new Dictionary<string, AIData>();

        gridQueue = new Queue<GameObject>();

        skillViewPortList = new List<GameObject>(); 
    }


    #region 角色

    /// <summary>
    /// 创建这个场景的人物
    /// </summary>
    public static void CreateObj()
    {
        Init();

        CreateNpc();

        CreatePlayer();

        ItemManager.Instance.AddItem("凉席", 2);
        ItemManager.Instance.AddItem("吹风机", 99);
    }

    /// <summary>
    /// 创建npc
    /// </summary>
    public static void CreateNpc()
    {
        string[] npcArray = AnalyXml.AnalySceneOfNpc(Game.CurrentSceneName);

        for (int i = 0; i < npcArray.Length; i++)
        {
            if (npcArray[i] != "")
            {
                GameObject go = Resources.Load<GameObject>(Consts.PrefabDir + npcArray[i]);
                GameObject clone = GameObject.Instantiate(go);

                clone.transform.parent = BgTrans;
                //clone.transform.localScale = new Vector3(1, 1, 1);
                clone.name = npcArray[i];
                clone.GetComponent<NpcAnimation>().GetAnimTexture();

                if (npcDic.ContainsKey(npcArray[i]))
                {//如果已经创建，直接填充
                    npcDic[npcArray[i]].Go = clone;
                }
                else
                {//添加到字典中
                    npcDic.Add(npcArray[i], InitNpcData(clone));
                }
            }
        }
    }

    /// <summary>
    /// 创建主角
    /// </summary>
    public static void CreatePlayer()
    {//之后这个go会不会自动被销毁？
        GameObject go = Resources.Load<GameObject>(Consts.PrefabDir + "Player");

        GameObject clone = GameObject.Instantiate(go);

        if (ScenePos.Instance.HaveScenePos(Game.CurrentSceneName))
        {//设置位置
            clone.transform.position = ScenePos.Instance.GetPlayerPos(Game.CurrentSceneName);
        }

        clone.transform.SetParent(BgTrans, false);
        clone.transform.localScale = new Vector3(1, 1, 1);
        clone.name = "Player";
        clone.GetComponent<NpcAnimation>().GetAnimTexture();

        //传递给全局数据保存
        PlayerPropertyManager.playerData = InitPlayerData(clone);
    }



    /// <summary>
    /// 初始化玩家信息
    /// </summary>
    /// <param name="go"></param>
    /// <returns></returns>
    public static PlayerData InitPlayerData(GameObject go)
    {
        //初始化信息
        PlayerData playerData = new PlayerData();

        playerData.CurrentPos = go.transform.localPosition;
        playerData.Dir = PersonDirection.Down;
        playerData.Go = go;
        playerData.Name = go.name;
        playerData.Speed = 0.8f;
        playerData.DoingStep = StoryStep.Step0;
        playerData.itemData = new Dictionary<string, ItemData>();
        playerData.skillPoint = 10;

        if (playerInitData != null)
        {//如果背景板有数据显示
            playerData.hp = playerInitData[0];
            playerData.intelligenceQ = playerInitData[1];
            playerData.emotionQ = playerInitData[2];
            playerData.profession = playerInitData[3];
            playerData.expenses = playerInitData[4];
            playerData.money = playerInitData[5];
        }

        return playerData;
    }

    public static NpcData InitNpcData(GameObject go)
    {
        NpcData npcData = new NpcData();

        npcData.CurrentPos = go.transform.localPosition;
        npcData.Dir = PersonDirection.Down;
        npcData.Go = go;
        npcData.Name = go.name;
        npcData.DoingStep = StoryStep.Step0;
        npcData.SceneName = Game.CurrentSceneName;

        return npcData;
    }

    public static AIData GetAIData(string npcName)
    {
        AIData data = null;

        npcDic.TryGetValue(npcName, out data);

        return data;
    }

    public static GameObject GetGameObject(string npcName)
    {
        return GetAIData(npcName).Go;
    }

    #endregion

    #region UI

    /// <summary>
    /// 创建对话框
    /// </summary>
    public static void CreateDialogue()
    {
        GameObject go = Resources.Load<GameObject>(Consts.PrefabUIDir + "Dialogue");

        dialogue = GameObject.Instantiate(go);
        //新方法，可让一个对象添加到父对象而不改变其坐标
        dialogue.transform.SetParent(CanvasTrans, false);
        dialogue.transform.localScale = new Vector3(1, 1, 1);
        dialogue.name = "Dialogue";

        dialogue.SetActive(false);
    }

    public static void ShowDialogue()
    {
        DialogueGo.SetActive(true);

        //开启对话
        GetDialogueCom().StartDialogue();
    }

    /// <summary>
    /// 给予Dialogue组件
    /// </summary>
    /// <returns></returns>
    public static Dialoge GetDialogueCom()
    {
        if (dialogue != null)
        {
            return dialogue.GetComponent<Dialoge>();
        }

        return null;
    }

    public static void CreateBackpackToggle()
    {
        backpackToggleGo = CreateToggleDynamic(new string[] { "使用", "不使用" }, UIPivot.RightDown, ToggleType.Backpack, null);

        backpackToggleGo.SetActive(false);
    }

    /// <summary>
    /// 动态创建Toggle，包括动态绑定按钮方法
    /// </summary>
    /// <param name="togglesText"></param>
    /// <param name="gridList"></param>
    public static GameObject CreateToggleDynamic(string[] togglesText, UIPivot pivot, ToggleType type, string npcName)
    {
        int length = togglesText.Length;

        if (length < 2)
        {
            Debug.Log("不需要创建Toggle");
            return null;
        }
        //根据不同的选项个数创建不同长度的toggle
        float toggleHeight = length * 90 - 20;

        GameObject go = Resources.Load<GameObject>(Consts.PrefabUIDir + "Toggle");

        toggleGo = GameObject.Instantiate(go);
        toggleGo.transform.SetParent(CanvasTrans, false);
        toggleGo.transform.localScale = new Vector3(1, 1, 1);
        toggleGo.name = "Toggle";
        RectTransform rect = toggleGo.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(200, toggleHeight);

        Vector3 initPos = Vector3.zero;

        //选择创建的位置
        switch (pivot)
        {
            case UIPivot.Middle:
                initPos = new Vector3(0, 0, 0);
                rect.anchorMin = new Vector2(0.5f, 0.5f);
                rect.anchorMax = new Vector2(0.5f, 0.5f);
                break;

            case UIPivot.RightDown:
                initPos = new Vector3(-190, 130, 0);
                rect.anchorMin = new Vector2(1, 0);
                rect.anchorMax = new Vector2(1, 0);
                break;
        }

        toggleGo.GetComponent<RectTransform>().localPosition = initPos;

        for (int i = 0; i < togglesText.Length; i++)
        {
            go = Resources.Load<GameObject>(Consts.PrefabUIDir + "ToggleGrid");

            GameObject gridGo = GameObject.Instantiate(go);

            gridGo.transform.SetParent(toggleGo.transform, false);
            gridGo.transform.name = i.ToString();
            gridGo.GetComponentInChildren<Text>().text = togglesText[i];

            if (i == 0)
            {
                //选项默认第一个高亮
                EventSystem.current.SetSelectedGameObject(gridGo);
            }

            //添加事件响应
            string gridName = gridGo.GetComponentInChildren<Text>().text;

            switch (type)
            {
                case ToggleType.ToggleCommand:
                    //角色对话时的响应事件
                    gridGo.GetComponent<Button>().onClick.AddListener(delegate ()
                    {
                        StoryManager.Instance.OnToggleCommandClick(gridName, npcName, true);
                    });
                    break;

                case ToggleType.BackgroundTab:
                    //起始选择角色背景的Toggle
                    gridGo.GetComponent<Button>().onClick.AddListener(delegate ()
                    {
                        StoryManager.Instance.OnBackgroundTabClick(gridName);
                    });
                    break;

                case ToggleType.Backpack:
                    //背包里的Toggle，使用或取消
                    BackpackToggle bt = toggleGo.AddComponent<BackpackToggle>();

                    gridGo.GetComponent<Button>().onClick.AddListener(delegate ()
                    {
                        bt.UseItemDeal(gridName);
                    });
                    break;
            }
           
        }

        toggleGo.SetActive(true);

        return toggleGo;
    }

    public static void DestroyToggle()
    {
        GameObject.Destroy(toggleGo);
    }

    /// <summary>
    /// 创建个人面板
    /// </summary>
    public static void CreatePersonPanel()
    {
        GameObject go = Resources.Load<GameObject>(Consts.PrefabUIDir + "PersonPanel");

        personPanel = GameObject.Instantiate(go);
        personPanel.transform.SetParent(CanvasTrans, false);
        personPanel.transform.localScale = new Vector3(1, 1, 1);
        personPanel.name = "PersonPanel";

        personPanel.SetActive(false);
    }

    /// <summary>
    /// 显示个人面板
    /// </summary>
    public static void ShowPersonPanel()
    {
        PersonPanelGo.SetActive(true);

        GameObject firstGo = GameObject.Find("Character");

        Button[] buttons = PersonPanelGo.GetComponentsInChildren<Button>();

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = true;
        }

        EventSystem.current.SetSelectedGameObject(firstGo);
    }

    public static void HidePersonPanel()
    {
        PersonPanelGo.SetActive(false);
    }
    /// <summary>
    /// 创建角色属性面板
    /// </summary>
    public static void CreateCharacterPropertyPanel()
    {
        GameObject go = Resources.Load<GameObject>(Consts.PrefabUIDir + "CharacterPanel");

        characterPropertyPanel = GameObject.Instantiate(go);
        characterPropertyPanel.transform.SetParent(personPanel.transform, false);
        characterPropertyPanel.transform.localScale = new Vector3(1, 1, 1);
        characterPropertyPanel.name = "CharacterPanel";

        characterPropertyPanel.SetActive(false);
    }

    /// <summary>
    /// 创建背包面板
    /// </summary>
    public static void CreateBackpackPanel()
    {
        GameObject go = Resources.Load<GameObject>(Consts.PrefabUIDir + "BackPack");

        backpackPanel = GameObject.Instantiate(go);
        backpackPanel.transform.SetParent(personPanel.transform, false);
        backpackPanel.transform.localScale = new Vector3(1, 1, 1);
        backpackPanel.name = "BackPack";

        backpackPanel.SetActive(false);
    }

    /// <summary>
    /// 创建背包的Item
    /// </summary>
    public static GameObject CreateItem(ItemData data)
    {
        GameObject item;
        
        //先检查对象池里有没有Grid
        if(gridQueue.Count > 0)
        {//如果有，取出来
            item = ItemDeQueue();
        }
        else
        {//如果没有，就创建一个Grid
            GameObject go = Resources.Load<GameObject>(Consts.PrefabUIDir + "BackPackGrid");

            item = GameObject.Instantiate(go);

            if (itemParentTrans == null)
            {
                itemParentTrans = backpackPanel.transform.Find("Bg/Scroll/ViewPort");
            }

            item.transform.SetParent(itemParentTrans, false);
            item.transform.localScale = new Vector3(1, 1, 1);
        }
        //先清空所有Item所有的点击事件
        item.GetComponent<Button>().onClick.RemoveAllListeners();

        //设置Item上的文字
        item.transform.Find("ItemName").GetComponent<Text>().text = data.itemName;
        item.transform.Find("ItemNum").GetComponent<Text>().text = data.itemNum.ToString();

        item.SetActive(true);

        return item;
    }

    /// <summary>
    /// 入队
    /// </summary>
    /// <param name="itemGo"></param>
    public static void ItemEnQueue(GameObject itemGo)
    {
        gridQueue.Enqueue(itemGo);
    }

    /// <summary>
    /// 出队
    /// </summary>
    /// <returns></returns>
    public static GameObject ItemDeQueue()
    {
        return gridQueue.Dequeue();
    }

    public static void CreateSkillPanel()
    {
        GameObject go = Resources.Load<GameObject>(Consts.PrefabUIDir + "SkillPanel");

        skillPanelGo = GameObject.Instantiate(go);
        skillPanelGo.transform.SetParent(personPanel.transform, false);
        skillPanelGo.transform.localScale = new Vector3(1, 1, 1);
        skillPanelGo.name = "SkillPanel";

        skillPanelGo.SetActive(false);

        Dictionary<string, List<SkillData>> skillData = SkillManager.Instance.skillData;

        //创建每个职业的技能面板
        foreach(List<SkillData> skillDataList in skillData.Values)
        {
            CreateSkillViewPort(skillDataList);
        }

        //动态添加SkillPanel脚本
        SkillPanel skillPanel = skillPanelGo.AddComponent<SkillPanel>();

        skillPanel.skillData = skillData;
        skillPanel.skillViewPortList = skillViewPortList;

        CreateSkillIntroPanel();
    }

    public static void CreateSkillViewPort(List<SkillData> skillDataList)
    {
        GameObject go = Resources.Load<GameObject>(Consts.PrefabUIDir + "SkillList");
        
        GameObject viewPort = GameObject.Instantiate(go);
        viewPort.transform.SetParent(SkillPanelGo.transform, false);
        viewPort.transform.localScale = new Vector3(1, 1, 1);
        //根据不同的技能产生不同长度的视口
        viewPort.transform.Find("GridGroup").GetComponent<RectTransform>().sizeDelta = new Vector2(244, 64 * skillDataList.Count);
        viewPort.name = "ViewPort";

        //在技能面板上创建每个职业技能
        foreach(SkillData data in skillDataList)
        {
            CreateSkillGrid(viewPort.transform.Find("GridGroup"), data);
        }

        viewPort.SetActive(false);

        skillViewPortList.Add(viewPort);
    }

    public static void CreateSkillGrid(Transform parent, SkillData data)
    {
        GameObject go = Resources.Load<GameObject>(Consts.PrefabUIDir + "SkillGrid");

        Sprite sprite = Resources.Load<Sprite>(Consts.SkillIconDir + data.iconName);

        GameObject skillGrid = GameObject.Instantiate(go);
        skillGrid.transform.SetParent(parent, false);
        skillGrid.transform.localScale = new Vector3(1, 1, 1);
        skillGrid.name = "skillGrid";

        skillGrid.transform.Find("SkillIcon").GetComponent<Image>().sprite = sprite;
        skillGrid.transform.Find("SkillName").GetComponent<Text>().text = data.skillName;
        skillGrid.transform.Find("SkillLevel").GetComponent<Text>().text = "0";
        skillGrid.GetComponent<SkillGrid>().skillName = data.skillName;
    }


    public static void CreateSkillIntroPanel()
    {
        GameObject go = Resources.Load<GameObject>(Consts.PrefabUIDir + "SkillIntroPanel");

        skillIntroPanel = GameObject.Instantiate(go);
        skillIntroPanel.transform.SetParent(SkillPanelGo.transform, false);
        skillIntroPanel.transform.localScale = new Vector3(1, 1, 1);
        skillIntroPanel.name = "SkillIntroPanel";

        skillIntroPanel.SetActive(false);
    }

    /// <summary>
    /// 显示技能介绍面板
    /// </summary>
    public static void ShowSkillIntroPanel(string skillName)
    {
        SkillIntroPanelGo.SetActive(true);
        SkillIntroPanelGo.GetComponent<SkillIntroPanel>().UpdateInformation(skillName);
    }

    public static void HideSkillIntrolPanel()
    {
        SkillIntroPanelGo.SetActive(false);
    }

    #endregion

    #endregion

    #region 按钮事件回调



    #endregion
}
