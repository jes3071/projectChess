using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuidioManager : MonoBehaviour {

    public AudioClip Chess_GameMusic;
    public AudioClip ChessPiece_Base;
    public AudioClip ChessPiece_Bishop;
    public AudioClip ChessPiece_Horse;
    public AudioClip ChessPiece_Pawn;
    public AudioClip ChessPiece_Rook;
    public AudioClip Flash;
    public AudioClip GameResult;
    public AudioClip Menu_Back;
    public AudioClip Menu_Select;
    public AudioClip Menu_Start;

    AudioSource audioSource;

    private GameObject boardManager;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        //boardManager = GameObject.Find("UIBattle").transform.Find("BattleBoard").gameObject;
    }

    public void baseSound()
    {
        audioSource.clip = ChessPiece_Base;
        audioSource.Play();
    }
}
