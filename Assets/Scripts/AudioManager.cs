using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

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
    public AudioClip Gimmick;

    public AudioSource audioSource;

    public static AudioManager instance;

    private void Awake()
    {
        if (AudioManager.instance == null)
            AudioManager.instance = this;
    }

    public void BaseSound()
    {
        audioSource.PlayOneShot(ChessPiece_Base);
    }

    public void PawnSound()
    {
        audioSource.PlayOneShot(ChessPiece_Pawn);
    }

    public void KnightSound()
    {
        audioSource.PlayOneShot(ChessPiece_Horse);
    }

    public void BishopSound()
    {
        audioSource.PlayOneShot(ChessPiece_Bishop);
    }

    public void RookSound()
    {
        audioSource.PlayOneShot(ChessPiece_Rook);
    }

    public void QueenSound()
    {
        //audioSource.PlayOneShot();
    }

    public void KingSound()
    {
        //audioSource.PlayOneShot(ChessPiece_Horse);
    }

    public void ResultSound()
    {
        audioSource.PlayOneShot(GameResult);
    }

    public void BackSound()
    {
        audioSource.PlayOneShot(Menu_Back);
    }

    public void SelectSound()
    {
        audioSource.PlayOneShot(Menu_Select);
    }

    public void StartSound()
    {
        audioSource.PlayOneShot(Menu_Start);
    }

    public void GimmickSound()
    {
        audioSource.PlayOneShot(Gimmick);
    }
}
