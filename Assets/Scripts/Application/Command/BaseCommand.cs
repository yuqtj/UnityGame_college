using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseCommand  {
    //需要异步执行
    public bool needAsync;

    protected List<EveType> regList;

    public virtual void Excute()
    {
    }

    public virtual void Exit()
    {

    }

    public virtual void OnEvent(EventArgs args)
    {

    }

    public void RegEvent()
    {
        if (regList != null)
        {
            foreach (EveType item in regList)
            {
                MyEventSystem.Instance.RegisterEvent(item, OnEvent);
            }
        }
    }
}
