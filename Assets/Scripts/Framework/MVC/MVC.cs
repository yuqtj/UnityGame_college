using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public static class MVC{

    //名字---模型
    public static Dictionary<string, Model> Models = new Dictionary<string, Model>();
    //名字---视图
    public static Dictionary<string, View> Views = new Dictionary<string, View>();
    //事件名---控制器，这里的Value不设置为Controller是因为要动态生成Controller实例，所以要用到反射机制
    public static Dictionary<string, Type> CommandMap = new Dictionary<string, Type>();

    public static void RegisterModel(Model model)
    {
        Models[model.Name] = model;
    }

    public static void RegisterView(View view)
    {
        //防止重复注册
        if (Views.ContainsKey(view.Name))
        {
            Views.Remove(view.Name);
        }

        //再注册事件
        view.RegisterEvents();

        Views[view.Name] = view;
    }

    public static void RegisterController(string eventName, Type controlType)
    {
        CommandMap[eventName] = controlType;
    }

    /// <summary>
    /// 发送事件
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="data"></param>
    public static void SendEvent(string eventName, object data = null)
    {
        if (CommandMap.ContainsKey(eventName))
        {
            Type t = CommandMap[eventName];
            Controller c = Activator.CreateInstance(t) as Controller;

            c.Excute(data);
        }

        foreach (View v in Views.Values)
        {
            if (v.AttentionEvents.Contains(eventName))
            {
                //视图响应事件
                v.HandleEvent(eventName, data);
            }            
        }
    }
}
