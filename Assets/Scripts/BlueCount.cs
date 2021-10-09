using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlueCount : MonoBehaviour {

    public BoardManager bdManager;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (bdManager.testTurn)
        {
            StartCoroutine(Count(bdManager.mapMaker.BlueTile.Count, int.Parse(gameObject.GetComponent<Text>().text), gameObject));
            
        }
    }

    IEnumerator Count(float target, float current, GameObject HpNumber)
    {

        float duration = 0.5f; // 카운팅에 걸리는 시간 설정. 

        float offset = (target - current) / duration;



        while (current < target)

        {

            current += offset * Time.deltaTime;

            HpNumber.GetComponent<Text>().text = ((int)current).ToString();
            Debug.Log("blue");

            yield return Time.deltaTime;

        }



        current = target;

        HpNumber.GetComponent<Text>().text = ((int)current).ToString();

    }
}
