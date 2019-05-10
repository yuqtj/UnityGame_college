using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {

    AIAttribute aiAttribute;

    Animator anim;

	// Use this for initialization
	void Awake () {
        aiAttribute = GetComponent<AIAttribute>();
        anim = GetComponent<Animator>();
	}
	
	public void SetIdle()
    {
        anim.SetInteger("Move", 0);
    }

    public void SetRun()
    {
        Vector2 vector2 = InputManager.GetHV();
        float h = vector2.x;
        float v = vector2.y;
        

        if (h != 0)
        {//x轴方向移动
            if (h < 0)
            {//向左
                aiAttribute.SetPlayerDir(PersonDirection.Left);
                anim.SetInteger("Move", 3);
            }
            else
            {//向右
                aiAttribute.SetPlayerDir(PersonDirection.Right);
                anim.SetInteger("Move", 4);
            }
        }
        else if (v != 0)
        {//y轴方向移动
            if (v < 0)
            {//向上
                aiAttribute.SetPlayerDir(PersonDirection.Down);
                anim.SetInteger("Move", 1);
            }
            else
            {//向下
                aiAttribute.SetPlayerDir(PersonDirection.Up);
                anim.SetInteger("Move", 2);
            }
        }
    }
}
