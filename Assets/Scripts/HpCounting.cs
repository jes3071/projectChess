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

            StartCoroutine(Count(bdManager.mapMaker.RedTile.Count, int.Parse(RedTarget.GetComponent<Text>().text), RedTarget,
                bdManager.mapMaker.BlueTile.Count, int.Parse(BlueTarget.GetComponent<Text>().text), BlueTarget));

            bdManager.turnChange = false;
        }

        
    }
    
    IEnumerator Count(float target, float current, GameObject HpNumber, float target2, float current2, GameObject HpNumber2)

    {

        float duration = 0.5f; // 카운팅에 걸리는 시간 설정. 

        float offset = Mathf.Abs(target - current) / duration;

        float offset2 = Mathf.Abs(target2 - current2) / duration;



        if(current > target)
        {
            //offset *= -1;
            while (current > target)

            {
                float time = Time.deltaTime;
                current -= offset * time;
                current2 += offset2 * time;



                HpNumber.GetComponent<Text>().text = ((int)current).ToString();

                HpNumber2.GetComponent<Text>().text = ((int)current2).ToString();

                yield return null;

            }
        }
        else
        {
            //offset2 *= -1;
            while (current < target)

            {
                float time = Time.deltaTime;
                current += offset * time;
                current2 -= offset2 * time;



                HpNumber.GetComponent<Text>().text = ((int)current).ToString();

                HpNumber2.GetComponent<Text>().text = ((int)current2).ToString();

                yield return null;

            }
        }


        current = target;

        current2 = target2;

        HpNumber.GetComponent<Text>().text = ((int)current).ToString();

        HpNumber2.GetComponent<Text>().text = ((int)target2).ToString();

    }
}
