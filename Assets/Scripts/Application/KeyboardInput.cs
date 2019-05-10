using UnityEngine;
using System.Collections;

public class KeyboardInput : MonoBehaviour {

    //-----------------------用于接收键盘的输入--------------------------


	// Update is called once per frame
	void Update () {
        InputManager.h = Input.GetAxisRaw("Horizontal");
        InputManager.v = Input.GetAxisRaw("Vertical");
        //如果玩家按下了WASD，可能会移动主角，可能会控制菜单面板
        
        InputManager.pressedJ = Input.GetKeyDown(KeyCode.J);

        InputManager.pressedK = Input.GetKeyDown(KeyCode.K);

        InputManager.pressedL = Input.GetKeyDown(KeyCode.L);
	}
}
