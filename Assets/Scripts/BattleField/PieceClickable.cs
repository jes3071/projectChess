﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PieceClickable : MonoBehaviour , IPointerClickHandler
{
    public BoardManager boardManager;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update()
    {

    }
    

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.instance.BaseSound();
        boardManager.CurPiece = gameObject;
    }
    
}
