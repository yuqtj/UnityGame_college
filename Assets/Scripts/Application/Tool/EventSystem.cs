using System;
using System.Collections.Generic;
using UnityEngine;

public delegate void EventDel(EventArgs args);

public enum EveType
{
    PickItem,
    PlayerDead,

    ChangeState,  //改变状态
}

//用于传递事件委托的参数
public class EventArgs
{
    public object message;

    public EveType type;

    public T GetMessage<T>()
    {
        return (T)message;
    }

    public EventArgs(EveType t)
    {
        type = t;
    }
}

public class MyEventSystem
{
    private static MyEventSystem _instance;

    public static MyEventSystem Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new MyEventSystem();
            }

            return _instance;
        }
    }

    Dictionary<EveType, List<EventDel>> m_listeners;

    MyEventSystem()
    {
        m_listeners = new Dictionary<EveType, List<EventDel>>();
    }

    /// <summary>
    /// 注册事件
    /// </summary>
    /// <param name="evtName">事件类型</param>
    /// <param name="callback">事件回调</param>
    public void RegisterEvent(EveType evtName, EventDel callback)
    {
        List<EventDel> evtList = null;
        //如果有这个事件类型，直接在这个类型链表上添加事件回调
        if (m_listeners.TryGetValue(evtName, out evtList))
        {
            m_listeners[evtName].Add(callback);
        }
        else
        {//如果没有这个事件类型，新建链表再添加事件回调
            evtList = new List<EventDel>();
            evtList.Add(callback);
            m_listeners.Add(evtName, evtList);
        }
    }

    /// <summary>
    /// 消除某事件的回调
    /// </summary>
    /// <param name="evtName"></param>
    /// <param name="callback"></param>
    public void DropEvent(EveType evtName, EventDel callback)
    {
        List<EventDel> evtList = null;
        if (m_listeners.TryGetValue(evtName, out evtList))
        {//如果找到了这个事件的链表
            evtList.Remove(callback);
        }
    }

    /// <summary>
    /// 触发某事情，执行所有回调
    /// </summary>
    /// <param name="evtArgs">事件参数</param>
    public void TriggerEvent(EventArgs evtArgs)
    {
        List<EventDel> evtList = null;
        if (m_listeners.TryGetValue(evtArgs.type, out evtList))
        {//如果找到了这个事件的链表
            foreach (EventDel item in evtList)
            {
                item(evtArgs);
            }
        }
    }

    /// <summary>
    /// 重载，参数为接收事件类型
    /// </summary>
    /// <param name="evtType"></param>
    public void TriggerEvent(EveType evtType)
    {
        TriggerEvent(new EventArgs(evtType));
    }

    /// <summary>
    /// 封装触发改变状态的事件
    /// </summary>
    public void TriggerChangeState(AIStateEnum state)
    {
        EventArgs args = new EventArgs(EveType.ChangeState);
        args.message = state;

        TriggerEvent(args);
    }
}

