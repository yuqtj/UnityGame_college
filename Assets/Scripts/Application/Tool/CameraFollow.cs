using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public static CameraFollow _instance;
    //自己调试得出
    private Vector3 offsetPos = new Vector3(0.1f, 0, -4.6f);

    private GameObject playerGo;
    //摄像机能到达的最大坐标
    private float XMax, XMin, YMax, YMin;

	// Use this for initialization
	void Start () {
        _instance = this;

        playerGo = GameObject.FindGameObjectWithTag("Player");

        AnalyXml.AnalyCameraPos(Game.CurrentSceneName);
	}
	
	// Update is called once per frame
	void Update () {
        CheckCameraPos();
	}

    void CheckCameraPos()
    {
        if (XMax == 0 && XMin == 0 && YMax == 0 && YMin == 0)
        {
            return;
        }

        transform.position = playerGo.transform.position + offsetPos;

        if (transform.position.x > XMax)
        {
            transform.position = new Vector3(XMax, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < XMin)
        {
            transform.position = new Vector3(XMin, transform.position.y, transform.position.z);
        }

        if (transform.position.y > YMax)
        {
            transform.position = new Vector3(transform.position.x, YMax, transform.position.z);
        }
        else if (transform.position.y < YMin)
        {
            transform.position = new Vector3(transform.position.x, YMin, transform.position.z);
        }
    }

    /// <summary>
    /// 得到XY的阈值
    /// </summary>
    public void GetXYThresholdPos(float xMin, float xMax, float yMin, float yMax)
    {
        XMin = xMin;
        XMax = xMax;
        YMin = yMin;
        YMax = yMax;
    }
}
