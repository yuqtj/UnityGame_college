using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class CharacterPropertyPanel : View {

    private Text playerName, hp, appear, iq, eq, money;

    public override string Name
    {
        get
        {
            return Consts.V_CharacterPropertyPanel;
        }
    }


    // Use this for initialization
    void Awake () {
        playerName = transform.Find("Name").GetComponent<Text>();
        hp = transform.Find("Hp").GetComponent<Text>();
        appear = transform.Find("Appear").GetComponent<Text>();
        iq = transform.Find("IQ").GetComponent<Text>();
        eq = transform.Find("EQ").GetComponent<Text>();
        money = transform.Find("Money").GetComponent<Text>();

    }
	
	public void UpdatePanelData()
    {
        PlayerData playerData = PlayerPropertyManager.playerData;

        playerName.text = playerData.Name;
        hp.text = playerData.hp.ToString();
        appear.text = playerData.profession.ToString();
        iq.text = playerData.intelligenceQ.ToString();
        eq.text = playerData.emotionQ.ToString();
        money.text = playerData.money.ToString();
    }

    public override void HandleEvent(string eventName, object data)
    {

    }
}
