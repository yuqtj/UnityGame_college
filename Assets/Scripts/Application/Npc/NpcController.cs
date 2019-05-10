using UnityEngine;
using System.Collections;


[RequireComponent(typeof(AIAttribute))]
[RequireComponent(typeof(PersonMove))]
[RequireComponent(typeof(NpcAnimation))]
public class NpcController : MonoBehaviour {

    public AIAttribute aiAttribute;

    void Awake()
    { //自动添加脚本
        aiAttribute = GetComponent<AIAttribute>();
    }

}
