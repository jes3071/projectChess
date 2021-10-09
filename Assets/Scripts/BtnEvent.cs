using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnEvent : MonoBehaviour {

    public GameObject UILobby;
    public GameObject UIPreset;
    public GameObject UIBattle;
    public GameObject UIResult;
    public GameObject UICredit;

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

    public void BattleResult()
    {
        UIResult.SetActive(true);
    }

    public void BackToLobby()
    {
        UILobby.SetActive(true);
        UIBattle.SetActive(false);
        UIResult.SetActive(false);
    }

    public void CreditButton()
    {
        UICredit.SetActive(true);
    }

    public void CreditToLobby()
    {
        UICredit.SetActive(false);
    }

    public void QuitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
