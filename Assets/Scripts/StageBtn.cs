using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBtn : MonoBehaviour {

    public GameObject UILobby;
    public GameObject UIPreset;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StageSettingButton()
    {
        UILobby.SetActive(false);
        UIPreset.SetActive(true);
    }
    
}
