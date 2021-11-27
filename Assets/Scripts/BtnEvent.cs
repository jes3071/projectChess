using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnEvent : MonoBehaviour {

    public GameObject UILobby;
    public GameObject UIPreset;
    public GameObject UIBattle;
    public GameObject UIResult;
    public GameObject UICredit;
    public GameObject UIPause;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StageSettingButton()
    {
        AudioManager.instance.SelectSound();
        UILobby.SetActive(false);
        UIPreset.SetActive(true);
    }

    public void PrevToLobby()
    {
        AudioManager.instance.BackSound();
        UILobby.SetActive(true);
        UIPreset.SetActive(false);
    }

    public void BattleStartButton()
    {
        AudioManager.instance.SelectSound();
        UIPreset.SetActive(false);
        UIBattle.SetActive(true);
    }

    public void BattleResult()
    {
        AudioManager.instance.ResultSound();
        UIResult.SetActive(true);
    }

    public void BackToLobby()
    {
        AudioManager.instance.BackSound();
        UILobby.SetActive(true);
        UIBattle.SetActive(false);
        UIResult.SetActive(false);
    }

    public void CreditButton()
    {
        AudioManager.instance.SelectSound();
        UICredit.SetActive(true);
        UILobby.SetActive(false);
    }

    public void CreditToLobby()
    {
        AudioManager.instance.BackSound();
        UILobby.SetActive(true);
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

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            UIPause.SetActive(true);
            Time.timeScale = 0;
        }
        
    }

    public void ReturnToGameButton()
    {
        Time.timeScale = 1;
        UIPause.SetActive(false);
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1;
        UIPause.SetActive(false);
        UIBattle.SetActive(false);
        UILobby.SetActive(true);
    }

}
