using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ExitSceneController : Controller
{
    /// <summary>
    /// 当一个场景要离开时
    /// </summary>
    /// <param name="data"></param>
    public override void Excute(object data)
    {
        //保存好玩家在这个场景的位置
        if (Game.EnterGameScene)
        {
            SavePlayerPos();
        }

        ExitSceneArgs e = data as ExitSceneArgs;

        Game.CurrentSceneName = e.nextSceneName;

        if(e.needShowLoadScene)
        {
            //加载Loading场景
            SceneManager.LoadScene("LoadScene");
        }
        else
        {
            SceneManager.LoadScene(e.nextSceneName);
        }


    }

    void SavePlayerPos()
    {//保存主角当前坐标
     
        GameObject playerGo = PlayerPropertyManager.PlayerGo;

        ScenePos.Instance.SavePlayerPos(Game.CurrentSceneName, playerGo.transform.localPosition);
    }
}
