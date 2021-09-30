using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpCounting : MonoBehaviour {

	
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //StartCoroutine(Count(GameMgr._instance.playerMoney+100, GameMgr._instance.playerMoney));





    //IEnumerator Count(float target, float current)

    //{

    //    float duration = 0.5f; // 카운팅에 걸리는 시간 설정. 

    //    float offset = (target - current) / duration;



    //    while (current < target)

    //    {

    //        current += offset * Time.deltaTime;

    //        moneyLabel.text = ((int)current).ToString();

    //        yield return null;

    //    }



    //    current = target;

    //    moneyLabel.text = ((int)current).ToString();

    //}
}
