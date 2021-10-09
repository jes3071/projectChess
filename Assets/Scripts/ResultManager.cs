﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour {

    public GameObject BlueText;
    public GameObject RedText;

    public GameObject BlueNum;
    public GameObject RedNum;

    public GameObject BluePiece;
    public GameObject RedPiece;

    public BoardManager bdManager;

    private void OnEnable()
    {
        if(bdManager.mapMaker.BlueTile.Count > bdManager.mapMaker.RedTile.Count)
        {
            BlueText.GetComponent<Text>().text = "승리";
            RedText.GetComponent<Text>().text = "패배";

            BlueText.GetComponent<Text>().fontSize = 130;
            RedText.GetComponent<Text>().fontSize = 130;

            BlueText.GetComponent<Text>().color = new Color(0/255f, 119/255f, 215/255f);
            RedText.GetComponent<Text>().color = new Color(158 / 255f, 0 / 255f, 0 / 255f);
        }
        else if (bdManager.mapMaker.BlueTile.Count < bdManager.mapMaker.RedTile.Count)
        {
            BlueText.GetComponent<Text>().text = "패배";
            RedText.GetComponent<Text>().text = "승리";

            BlueText.GetComponent<Text>().fontSize = 130;
            RedText.GetComponent<Text>().fontSize = 130;

            BlueText.GetComponent<Text>().color = new Color(158 / 255f, 0 / 255f, 0 / 255f);
            RedText.GetComponent<Text>().color = new Color(0 / 255f, 119 / 255f, 215 / 255f);
        }
        else
        {
            BlueText.GetComponent<Text>().text = "무승부";
            RedText.GetComponent<Text>().text = "무승부";

            BlueText.GetComponent<Text>().fontSize = 95;
            RedText.GetComponent<Text>().fontSize = 95;


            BlueText.GetComponent<Text>().color = Color.white;
            RedText.GetComponent<Text>().color = Color.white;
        }


        BlueNum.GetComponent<Text>().text = "차지한 영토 : " + bdManager.mapMaker.BlueTile.Count.ToString();
        RedNum.GetComponent<Text>().text = "차지한 영토 : " + bdManager.mapMaker.RedTile.Count.ToString();

        BluePiece.GetComponent<Text>().text = "살아남은 기물 : " + bdManager.PieceBlueCoord.Count.ToString();
        RedPiece.GetComponent<Text>().text = "살아남은 기물 : " + bdManager.PieceRedCoord.Count.ToString();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}