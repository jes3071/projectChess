using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpCounting : MonoBehaviour {
    
    public GameObject RedTarget;
    public GameObject BlueTarget;

    public BoardManager bdManager;
	
    // Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (bdManager.turnChange)
        {
            StartCoroutine(CountBlue(bdManager.mapMaker.BlueTile.Count, int.Parse(BlueTarget.GetComponent<Text>().text), BlueTarget));
            StartCoroutine(CountRed(bdManager.mapMaker.RedTile.Count, int.Parse(RedTarget.GetComponent<Text>().text), RedTarget));
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
}
