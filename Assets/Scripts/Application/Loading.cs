using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Loading : MonoBehaviour {

    public Slider slider;

    public Text progressText;

    AsyncOperation async;
   

    public string LoadNextSceneName
    { 
        get
        {
            return Game.CurrentSceneName;
        }
    }
  
	// Use this for initialization
	void Start () {
        Debug.Log("加载中....即将进入" + LoadNextSceneName + "场景");

        StartCoroutine(LoadScene());

	}
	
    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(1f);

        async = Application.LoadLevelAsync(LoadNextSceneName);

        yield return async;
    }

    void Update()
    {
        if(async != null)
        {
            slider.value = async.progress / 0.9f;

            progressText.text = (slider.value * 100).ToString();
        }
    }
}
