using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Dialoge : MonoBehaviour {

    /*---------------贴在对话框上------------------*/
    /*---------------开始对话因为要加载对话，所以用StartDialogue，继续对话不用加载，用ContinueDialogue------------------*/



    [SerializeField]
    private Text dialogueText;
    //速度最好设置50
    public float speed, timer;
    //用于记录对话框当前的文字长度
    private int currentLength;
    //当前对话
    private string currentContent;

    private bool isPrintText;
    //判断是否正在一个一个字的播放
    public bool IsPrintText
    {
        get
        {
            return isPrintText;
        }
    }

    #region Unity回调

    void Awake()
    {
        dialogueText = GetComponentInChildren<Text>();

    }

    #endregion

    #region 方法

    /// <summary>
    /// 开始对话
    /// </summary>
    /// <param name="person">从某人那里获取对话</param>
    public void StartDialogue()
    {
        ContinueDialogue();
    }

    /// <summary>
    /// 继续对话
    /// </summary>
    public void ContinueDialogue()
    {
        currentContent = DialogueCommand.GetNextContent();

        if (currentContent == null)
        {
            Debug.Log("聊天结束");
        }

        StartCoroutine(PrintImformation(currentContent));
    }

    /// <summary>
    /// 隐藏对话框
    /// </summary>
    public void HideDialogue()
    {
        dialogueText.text = "";
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 是否有下一句对话，有就返回true，没有显示false
    /// </summary>
    public bool HaveNextStatement()
    {
        return DialogueCommand.HaveNextContent();
    }

    /// <summary>
    /// 处理对话框
    /// </summary>
    public void DialogueDeal()
    {
        //正在对话时按这个键，也有两种情况，1.显示文字 2.对话结束
        if (IsPrintText)
        {//如果当前正在播放文字，再按一下这个键就是直接显示所有文字
            AllPrint();
        }
        else
        {//如果当前不播放文字
            //如果后面还有对话，播放后面的对话
            if (HaveNextStatement())
            {
                ContinueDialogue();
            }
            else
            {//如果后面没有对话，隐藏对话框
                CommandManager.Instance.CommandOver();

                HideDialogue();
            }
        }
    }

    /// <summary>
    /// 一次性打印字体
    /// </summary>
    /// <param name="imformation"></param>
    public void AllPrint()
    {
        StopCoroutine("PrintImformation");
        dialogueText.text = currentContent;
    }

    /// <summary>
    /// 设置对话框交互，也就是按J时跳到下一句话，这个主要用于没有主角开场动画场景，一般情况用不到
    /// </summary>
    public void SetDialogueInteractive()
    {
        StartCoroutine("StartInteractive");
    }

    #endregion


    #region 协程
    /// <summary>
    /// 逐字打印
    /// </summary>
    /// <returns></returns>
    IEnumerator PrintImformation(string imformation)
    {
        timer = 0;
        currentLength = 0;
        dialogueText.text = "";

        while (imformation.Length != dialogueText.text.Length)
        {//正在播放文字
            isPrintText = true;

            timer += Time.deltaTime;
            currentLength = (int)(timer * speed);

            if (imformation.Length < currentLength)
            {//越界判断
                dialogueText.text = imformation;
            }
            else
            {
                dialogueText.text = imformation.Substring(0, currentLength);
            }

            yield return new WaitForSeconds(0.1f);
        }
        //播放结束
        isPrintText = false;
    }

    /// <summary>
    /// 开始交互
    /// </summary>
    /// <returns></returns>
    IEnumerator StartInteractive()
    {//这里设置为无限循环，因为当对话都结束时对话框会自动隐藏，协程也会自动关闭
        while (true)
        {
            if (InputManager.PressedJ())
            {
                DialogueDeal();
            }

            yield return null;
        }
    }

    #endregion



}
