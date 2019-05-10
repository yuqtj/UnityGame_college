using UnityEngine;
using System.Collections;

public abstract class Model
{
    public abstract string Name { get; }

    protected void SendEvent(string eventName, object data = null)
    {

    }
}
