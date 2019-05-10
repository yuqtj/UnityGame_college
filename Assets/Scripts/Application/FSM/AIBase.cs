using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIBase : MonoBehaviour {

    //---------------物体控制基类，管理事件的交互--------------

    //存储着事件类型
    public List<EveType> regEvent;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// 注册事件
    /// </summary>
    public void RegEvent()
    {
        if (regEvent != null)
        {
            foreach (EveType item in regEvent)
            {
                MyEventSystem.Instance.RegisterEvent(item, OnEvent);
            }
        }
    }

    public virtual void OnEvent(EventArgs args)
    {

    }

    /// <summary>
    /// 消除事件
    /// </summary>
    public void DropEvent()
    {
        if (regEvent != null)
        {
            foreach (EveType item in regEvent)
            {
                MyEventSystem.Instance.DropEvent(item, OnEvent);
            }
        }
    }

    protected virtual void OnDestroy()
    {
        DropEvent();
    }
}
