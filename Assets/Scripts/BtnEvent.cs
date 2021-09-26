using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnEvent : MonoBehaviour {

    public GameObject UILobby;
    public GameObject UIPreset;
    public GameObject UIBattle;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StageSettingButton()
    {
        UILobby.SetActive(false);
        UIPreset.SetActive(true);
    }

    public void PrevToLobby()
    {
        UILobby.SetActive(true);
        UIPreset.SetActive(false);
    }

    public void BattleStartButton()
    {
        UIPreset.SetActive(false);
        UIBattle.SetActive(true);
    }

    
}
