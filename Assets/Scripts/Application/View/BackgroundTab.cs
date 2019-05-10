using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class BackgroundTab : View {
    //当前索引
    int curretnIndex, leftIndex, rightIndex, tempIndex;

    public Transform[] cards;


    //Toggle
    private GameObject toggleGo;

    public override string Name
    {
        get { return Consts.V_BackgroundTab; }
    }

    void Awake()
    {
        tempIndex = 0;
        curretnIndex = -1;

        toggleGo = ObjectManager.CreateToggleDynamic(new string[] { "确认", "取消" }, UIPivot.RightDown, ToggleType.BackgroundTab, null);
    }

    void Update()
    {
        CheckKey();
        AdjustPosition();
    }

    /// <summary>
    /// 按键选项卡交互
    /// </summary>
    void CheckKey()
    {
        //如果这个Toggle当前没显示，可以响应选项卡按键
        if (!toggleGo.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (tempIndex == 0)
                {//如果已经是最左
                    return;
                }
                tempIndex--;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                if (tempIndex == cards.Length - 1)
                {//如果已经在最右
                    return;
                }
                tempIndex++;
            }

            if (Input.GetKeyDown(KeyCode.J))
            {//表示确定当前的选项卡
                toggleGo.SetActive(true);

                StoryManager.Instance.curretnIndex = tempIndex;

                EventSystem.current.firstSelectedGameObject = toggleGo.transform.Find("0").gameObject;

                EventSystem.current.SetSelectedGameObject(toggleGo.transform.Find("0").gameObject);

                StoryManager.Instance.excuteScript = this;

                enabled = false;
            }

        }
    }

    /// <summary>
    /// 调整每个选项卡的位置
    /// </summary>
    void AdjustPosition()
    {
        if (curretnIndex == tempIndex)
        {//如果用户没有操作就没有必要修改选项卡位置，为了提高效率
            return;
        }

        Debug.Log(tempIndex);

        curretnIndex = tempIndex;

        HideCards();

        leftIndex = curretnIndex - 1;
        rightIndex = curretnIndex + 1;
        //把当前选项卡放在中间并显示
        cards[curretnIndex].localPosition = new Vector3(0, 222, 0);
        cards[curretnIndex].gameObject.SetActive(true);

        if (leftIndex < 0)
        {//如果当前选项是第一个选项
            cards[rightIndex].localPosition = new Vector3(500, 222, 0);
            cards[rightIndex].gameObject.SetActive(true);
        }
        else if (rightIndex >= cards.Length)
        {//如果当前选项是最后一个选项
            cards[leftIndex].localPosition = new Vector3(-500, 222, 0);
            cards[leftIndex].gameObject.SetActive(true);
        }
        else
        {//同时显示左右的选项卡
            cards[leftIndex].localPosition = new Vector3(-500, 222, 0);
            cards[rightIndex].localPosition = new Vector3(500, 222, 0);
            cards[leftIndex].gameObject.SetActive(true);
            cards[rightIndex].gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// 在进行移动选项前需要对所有卡进行隐藏
    /// </summary>
    void HideCards()
    {
        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].gameObject.SetActive(false);
        }
    }

    public override void HandleEvent(string eventName, object data)
    {
    }
}
