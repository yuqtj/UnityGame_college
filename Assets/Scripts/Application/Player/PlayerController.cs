using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AIAttribute))]
public class PlayerController : AIBase {

    //状态机
    public AIController stateMachine;
    //访问数据
    AIAttribute aiAttribute;

    void Awake()
    { 
        aiAttribute = GetComponent<AIAttribute>();
        //给摄像机动态添加脚本
        Camera.main.gameObject.AddComponent<CameraFollow>();
    }

	// Use this for initialization
	void Start () {
        stateMachine = new AIController(aiAttribute);

        stateMachine.AddState(new HumanIdle());
        stateMachine.AddState(new HumanMove());
        stateMachine.AddState(new HumanTalk());
        stateMachine.AddState(new HumanStoryMove());
        stateMachine.AddState(new HumanUIInteractive());

        stateMachine.ChangeState(AIStateEnum.Idle);

        //注册事件
        regEvent = new List<EveType>()
        {
            EveType.ChangeState
        };

        RegEvent();
	}

    public override void OnEvent(EventArgs args)
    {
        if (args.type == EveType.ChangeState)
        {
            AIStateEnum state = args.GetMessage<AIStateEnum>();

            stateMachine.ChangeState(state);
        }
    }


    void OnTriggerStay2D(Collider2D coll)
    {
        switch (coll.tag)
        {
            case "Room1":
                if (Input.GetKey(KeyCode.W))
                {
                    Game.Instance.LoadScene("Room1");
                }
                break;
            case "BackSchool":
                if (Input.GetKey(KeyCode.W))
                {
                    Game.Instance.LoadScene("School");
                }
                break;
        }
    }
}
