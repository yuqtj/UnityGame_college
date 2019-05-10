using UnityEngine;
using System.Collections;
using System;

public abstract class ApplicationBase<T> : Singleton<T> 
    where T : MonoBehaviour
{
    //注册控制器
    protected void RegisterController(string eventName, Type controllerType)
    {
        MVC.RegisterController(eventName, controllerType);
    }
    //发送事件的封装
    protected void SendEvent(string eventName, object data = null)
    {
        MVC.SendEvent(eventName, data);
    }
}
