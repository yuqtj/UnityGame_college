using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class View : MonoBehaviour
{
    //标识
    public abstract string Name { get; }

    //关心的事件列表
    [HideInInspector]
    public List<string> AttentionEvents = new List<string>();

    //注册
    public virtual void RegisterEvents()
    {

    }

    //事件处理
    public abstract void HandleEvent(string eventName, object data);

    //发送事件
    protected void SendEvent(string eventName, object data = null)
    {
        MVC.SendEvent(eventName, data);
    }
}
