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

    public GameObject UITutorial;
    private int MinTutorialNum = 0;
    private int MaxTutorialNum = 4;

    private int TutorialState = 0;

    public Animator RotateTutorial;

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

    public void TutorialButton()
    {
        // 로비에서 튜토리얼 버튼 클릭시
        AudioManager.instance.SelectSound();
        UITutorial.SetActive(true);
        RotateTutorial.SetInteger("TutorialState", MinTutorialNum);
    }

    public void TutorialToLobby()
    {
        // 튜토리얼 화면에서 나가기 버튼 클릭시
        AudioManager.instance.BackSound();
        UITutorial.SetActive(false);
    }

    public void TutorialNextButton()
    {
        AudioManager.instance.SelectSound();
        TutorialState++;
        if (TutorialState > MaxTutorialNum)
            TutorialState = MinTutorialNum;
        RotateTutorial.SetInteger("TutorialState", TutorialState);
    }

    public void TutorialPrevButton()
    {
        AudioManager.instance.SelectSound();
        TutorialState--;
        if (TutorialState < MinTutorialNum)
            TutorialState = MaxTutorialNum;
        RotateTutorial.SetInteger("TutorialState", TutorialState);

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
        UIResult.SetActive(false);
        UICredit.SetActive(false);
        UIPreset.SetActive(false);
        UILobby.SetActive(true);
    }

}
