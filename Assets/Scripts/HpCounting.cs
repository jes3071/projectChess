using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpCounting : MonoBehaviour {

    public string redCount;
    public string blueCount;

    public GameObject RedTarget;
    public GameObject BlueTarget;

    public BoardManager bdManager;
	
    // Use this for initialization
	void Start () {
        redCount = RedTarget.GetComponent<Text>().text;
        blueCount = BlueTarget.GetComponent<Text>().text;
    }
	
	// Update is called once per frame
	void Update () {
        if (bdManager.testTurn)
        {

            StartCoroutine(Count(bdManager.mapMaker.RedTile.Count, int.Parse(redCount), RedTarget,
                bdManager.mapMaker.BlueTile.Count, int.Parse(blueCount), BlueTarget));

            bdManager.testTurn = false;

            //redCount = RedTarget.GetComponent<Text>().text;
            //blueCount = BlueTarget.GetComponent<Text>().text;
        }

        
    }
    


    IEnumerator Count(float target, float current, GameObject HpNumber, float target2, float current2, GameObject HpNumber2)

    {

        float duration = 1f; // 카운팅에 걸리는 시간 설정. 

        float offset = (target - current) / duration;

        float offset2 = (target2 - current2) / duration;



        while (current < target)

        {

            current += offset * Time.deltaTime;
            current2 += offset2 * Time.deltaTime;

            HpNumber.GetComponent<Text>().text = ((int)current).ToString();

            

            HpNumber2.GetComponent<Text>().text = ((int)current2).ToString();

            yield return null;

        }



        current = target;

        current2 = target2;

        HpNumber.GetComponent<Text>().text = ((int)current).ToString();

        HpNumber2.GetComponent<Text>().text = ((int)current2).ToString();

    }
}
