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

            StartCoroutine(CountBlue(bdManager.mapMaker.BlueTile.Count, int.Parse(BlueTarget.GetComponent<Text>().text), BlueTarget));
            StartCoroutine(CountRed(bdManager.mapMaker.RedTile.Count, int.Parse(RedTarget.GetComponent<Text>().text), RedTarget));
            if (bdManager.FirstTurn.isOn)
            {
                if (!alphaBlueFlag && bdManager.turnCount == 29)
                {
                    StartCoroutine(FadeTextToFullAlpha(BlueTarget.GetComponent<Text>()));
                    alphaBlueFlag = false;
                }
                if(!alphaRedFlag && bdManager.turnCount == 30)
                {
                    StartCoroutine(FadeTextToFullAlpha(RedTarget.GetComponent<Text>()));
                    alphaRedFlag = false;
                }
            } else
            {
                if (!alphaBlueFlag && bdManager.turnCount == 30)
                {
                    StartCoroutine(FadeTextToFullAlpha(BlueTarget.GetComponent<Text>()));
                    alphaBlueFlag = false;
                }
                if(!alphaRedFlag && bdManager.turnCount == 29)
                {
                    StartCoroutine(FadeTextToFullAlpha(RedTarget.GetComponent<Text>()));
                    alphaRedFlag = false;
                }
            }
            bdManager.turnChange = false;
        }
    }

    IEnumerator CountRed(float target, float current, GameObject HpNumber)
    {
        float duration = 0.5f; // 카운팅에 걸리는 시간 설정. 

        float offset = Mathf.Abs(target - current) / duration;

        if (current > target)
        {
            while (current > target)
            {
                float time = Time.deltaTime;
                current -= offset * time;
                HpNumber.GetComponent<Text>().text = ((int)current).ToString();
                if((int)current == (int)target)
                {
                    break;
                }
                yield return null;
            }
        }
        else if(current < target)
        {
            while (current < target)
            {
                float time = Time.deltaTime;
                current += offset * time;
                HpNumber.GetComponent<Text>().text = ((int)current).ToString();
                if ((int)current == (int)target)
                {
                    break;
                }
                yield return null;
            }
        }
        current = target;
        HpNumber.GetComponent<Text>().text = ((int)current).ToString();
    }

    IEnumerator CountBlue(float target, float current, GameObject HpNumber)
    {
        float duration = 0.5f; // 카운팅에 걸리는 시간 설정. 

        float offset = Mathf.Abs(target - current) / duration;

        if (current > target)
        {
            while (current > target)
            {
                float time = Time.deltaTime;
                current -= offset * time;
                HpNumber.GetComponent<Text>().text = ((int)current).ToString();
                if ((int)current == (int)target)
                {
                    break;
                }
                yield return null;
            }
        }
        else if (current < target)
        {
            while (current < target)
            {
                float time = Time.deltaTime;
                current += offset * time;
                HpNumber.GetComponent<Text>().text = ((int)current).ToString();
                if ((int)current == (int)target)
                {
                    break;
                }
                yield return null;
            }
        }
        current = target;
        HpNumber.GetComponent<Text>().text = ((int)current).ToString();
    }

    public IEnumerator FadeTextToFullAlpha(Text text) // 알파값 0에서 1로 전환
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1f);
        //while (text.color.a < 1.0f)
        //{
        //    text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime / 2.0f));
            yield return null;
        //}
        //StartCoroutine(FadeTextToZeroAlpha());
    }

    //public IEnumerator FadeTextToZero(Text text)  // 알파값 1에서 0으로 전환
    //{
    //    text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
    //    while (text.color.a > 0.0f)
    //    {
    //        text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime / 2.0f));
    //        yield return null;
    //    }
    //    //StartCoroutine(FadeTextToFullAlpha());
    //}
}
