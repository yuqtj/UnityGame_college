using UnityEngine;
using System.Collections;

public class StartUpController : Controller {

    //---------进入游戏时的初始化------------
    public override void Excute(object data)
    {   
        //注册事件

        //EnterScene是进入场景后才调用
        RegisterController(Consts.E_EnterScene, typeof(EnterSceneController));
        //ExitScene是离开上一个场景后调用
        RegisterController(Consts.E_ExitScene, typeof(ExitSceneController));

        //从Xml加载物品信息
        ItemManager.Instance.LoadItemsInfo();


        //进入第一个游戏场景，开发状态下直接进入第一个游戏场景
        //Game.Instance.LoadScene(Consts.firstLoadScene, false);
       
        Game.Instance.LoadScene(Consts.firstLoadScene);
    }
}
