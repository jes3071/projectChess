using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBtn : MonoBehaviour {

    public GameObject UIPreset;
    public GameObject UIBattle;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BattleStartButton()
    {
        UIPreset.SetActive(false);
        UIBattle.SetActive(true);
    }
}
