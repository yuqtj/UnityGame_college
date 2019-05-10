using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScenePos {
    //-----------管理着每个人物每个场景中的位置

    private static ScenePos _instance;

    public static ScenePos Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ScenePos();
            }

            return _instance;
        }
    }

    Dictionary<string, Vector2> playerScenePos;

    private ScenePos()
    {
        playerScenePos = new Dictionary<string, Vector2>();

        playerScenePos.Add("School", new Vector2(4.091f, 0.96f));
        playerScenePos.Add("Room1", new Vector2(-0.472f, 0.272f));
    }

    /// <summary>
    /// 保存当前坐标
    /// </summary>
    /// <param name="currentSceneName"></param>
    /// <param name="npcPos"></param>
    public void SavePlayerPos(string currentSceneName, Vector2 npcPos)
    {
        if (playerScenePos.ContainsKey(currentSceneName))
        {
            playerScenePos[currentSceneName] = npcPos;
        }
        else
        {
            playerScenePos.Add(currentSceneName, npcPos);
        }
    }

    /// <summary>
    /// 得到坐标
    /// </summary>
    /// <param name="currentSceneName"></param>
    /// <returns></returns>
    public Vector2 GetPlayerPos(string currentSceneName)
    {
        return playerScenePos[currentSceneName];
    }

    public bool HaveScenePos(string currentSceneName)
    {
        if (playerScenePos.ContainsKey(currentSceneName))
        {
            return true;
        }

        return false;
    }
}
