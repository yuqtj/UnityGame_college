using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//--------自制ScrollBar，用于垂直方向的ScrollRect

public class SkillScrollBar : MonoBehaviour {
    //要修改的目标
    public RectTransform rectTrans;
    //面板上能够显示的Grid数量
    public int gridNum = 4;
    //单个Grid的长度
    public int gridHeight = 64;

    private Scrollbar scrollBar;

    void Awake()
    {
        scrollBar = GetComponent<Scrollbar>();
    }


    //优化----1.当Scroll.value数值改变时调用
    public void OnScrollValueChangedEvent()
    {
        if (rectTrans != null)
        {
            int maxY = (rectTrans.childCount - gridNum) * gridHeight;
            rectTrans.anchoredPosition = new Vector2(0, scrollBar.value * maxY);
        }
    }

    //当被改变职业技能面板时
    public void SetScrollRect(RectTransform rectTrans)
    {
        this.rectTrans = rectTrans;
        scrollBar.value = 0;
    }
}
