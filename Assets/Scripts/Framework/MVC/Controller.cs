using UnityEngine;
using System.Collections;
using System;

public abstract class Controller {
    public abstract void Excute(object data);

    protected void RegisterController(string eventName, Type controllerType)
    {
        MVC.RegisterController(eventName, controllerType);
    }

    protected void RegisterView(View view)
    {
        MVC.RegisterView(view);
    }
}
