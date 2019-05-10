using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TalkAction  {

    //----------玩家聊天行为，存储着NPC对话-----------

    //发出射线长度
    public float distance = 0.2f;
    //玩家身上的数据访问
    public AIAttribute aiAttribute;
    //临时方向向量
    Vector3 dirVector;

    int npcMask;
    //与玩家聊天的npc
    private GameObject npcGo;

    PersonDirection dir;

    private static TalkAction _instance;

    public static TalkAction Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new TalkAction();
            }

            return _instance;
        }
    }

    private TalkAction()
    {
        npcMask = LayerMask.GetMask("NPC");
        aiAttribute = GameObject.FindGameObjectWithTag("Player").GetComponent<AIAttribute>();

        if (aiAttribute == null)
        {
            Debug.Log("玩家没有Player这个标签");
        }
    }

    /// <summary>
    /// 检测玩家是否能与npc聊天，返回true表示可以聊天
    /// </summary>
    public bool TryWithNpcDialogue()
    {
        if (GetNpcGoByRay())
        {//如果射线接触到NPC，开始获取这个npc的Xml指令
            //指定npc朝向
            npcGo.GetComponent<AIAttribute>().PlayerTouchYou(dir);

            StoryStep step = npcGo.GetComponent<AIAttribute>().GetAIData().DoingStep;
            AnalyXml.AnalyNpcBehavior(npcGo.name, step);
            
            return true;
        }

        return false;
    }

    /// <summary>
    /// 封装根据玩家射线方向获取到的npc，如果存在npc就返回True
    /// </summary>
    public bool GetNpcGoByRay()
    {
        dir = aiAttribute.GetPlayerData().Dir;

        switch (dir)
        {//根据玩家不同的朝向发射不同的射线
            case PersonDirection.Down:
                dirVector = -aiAttribute.transform.up;
                break;
            case PersonDirection.Up:
                dirVector = aiAttribute.transform.up;
                break;
            case PersonDirection.Left:
                dirVector = -aiAttribute.transform.right;
                break;
            case PersonDirection.Right:
                dirVector = aiAttribute.transform.right;
                break;
        }

        RaycastHit2D hit = Physics2D.Raycast(aiAttribute.transform.position, dirVector, distance, npcMask);

        if (hit.collider != null)
        {
            npcGo = hit.collider.gameObject;

            return true;
        }

        return false;
    }

    /// <summary>
    /// 封装底层显示对话框方法
    /// </summary>
    public void ShowDialogue()
    {
        if (npcGo != null)
        {
            Debug.Log("你在跟" + npcGo.name + "聊天");

            ObjectManager.ShowDialogue();
        }
        else
        {//如果NpcGo为空，再寻找一下npc
            GetNpcGoByRay();

            ObjectManager.ShowDialogue();
        }

    }

    /// <summary>
    /// 显示对话框与玩家的按键J交互
    /// </summary>
    public void TalkingDeal()
    {
        ObjectManager.GetDialogueCom().DialogueDeal();
    }
}
