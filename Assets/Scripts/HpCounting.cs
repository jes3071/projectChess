using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpCounting : MonoBehaviour {
    
    public GameObject RedTarget;
    public GameObject BlueTarget;

    public BoardManager bdManager;
    public bool alphaBlueFlag = false;
    public bool alphaRedFlag = false;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (bdManager.turnChange)
        {
            if (bdManager.FirstTurn.isOn)
            {
                if (!alphaBlueFlag && bdManager.turnCount == 29)
                {
                    BlueTarget.GetComponent<Text>().text = bdManager.BlueCoord.Count.ToString();
                    FadeInText(BlueTarget.GetComponent<Text>());
                    alphaBlueFlag = true;
                }
                if(!alphaRedFlag && bdManager.turnCount == 30)
                {
                    RedTarget.GetComponent<Text>().text = bdManager.RedCoord.Count.ToString();
                    FadeInText(RedTarget.GetComponent<Text>());
                    alphaRedFlag = true;
                }
            } else
            {
                if (!alphaBlueFlag && bdManager.turnCount == 30)
                {
                    BlueTarget.GetComponent<Text>().text = bdManager.BlueCoord.Count.ToString();
                    FadeInText(BlueTarget.GetComponent<Text>());
                    alphaBlueFlag = true;
                }
                if(!alphaRedFlag && bdManager.turnCount == 29)
                {
                    RedTarget.GetComponent<Text>().text = bdManager.RedCoord.Count.ToString();
                    FadeInText(RedTarget.GetComponent<Text>());
                    alphaRedFlag = true;
                }
            }
            bdManager.turnChange = false;
        }
    }

    void OnEnable() {
        alphaBlueFlag = false;
        alphaRedFlag = false;
        FadeOutText(BlueTarget.GetComponent<Text>());
        FadeOutText(RedTarget.GetComponent<Text>());
    }
    
    public void FadeInText(Text text)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1f);
    }

    public void FadeOutText(Text text)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0f);
    }
}
