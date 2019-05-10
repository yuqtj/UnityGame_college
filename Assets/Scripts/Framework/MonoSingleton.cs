using UnityEngine;
using System.Collections;

/*---------------------------------------------------------------------------------*
 *  脚本说明：通用的Unity单例模板。
 *  需求: 1.能接受MonoBehaviour生命周期
 *        2.约束脚本实例对象个数
 * *      3.如果调用脚本的时候场景没有这个物体，就自动创建一个不会被销毁的物体
 * *        
 *---------------------------------------------------------------------------------*/


public abstract class MonoSingleton<T> : MonoBehaviour
    where T : MonoSingleton<T>
{
    protected static T _instance = null;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();

                if (FindObjectsOfType<T>().Length > 1)
                {//如果在当前场景里找到了这个单例
                    return _instance;
                }

                if (_instance == null)
                {//如果在场景里找不到单例，自己创建一个
                    string instanceName = typeof(T).Name;

                    GameObject instanceGo = GameObject.Find(instanceName);

                    if (instanceGo == null)
                    {
                        instanceGo = new GameObject(instanceName);
                    }

                    _instance = instanceGo.AddComponent<T>();

                    DontDestroyOnLoad(instanceGo);

                }
            }

            return _instance;
        }
    }

    protected virtual void OnDestroy()
    {
        _instance = null;
    }
}
