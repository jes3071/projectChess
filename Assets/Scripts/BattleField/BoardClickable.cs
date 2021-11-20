using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System;

public class BoardClickable : MonoBehaviour , IPointerClickHandler
{
    
    private GameObject boardManager;

    public static int kingBlueCoord;
    public static int kingRedCoord;

    // Use this for initialization
    void Start () {
        boardManager = GameObject.Find("UIBattle").transform.Find("BattleBoard").gameObject;
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        if (boardManager.GetComponent<BoardManager>().CurPiece != null && !gameObject.transform.GetComponent<Image>().sprite.name.Contains("Empty") && !gameObject.transform.GetComponent<Image>().sprite.name.Contains("Block") && !boardManager.GetComponent<BoardManager>().CR_update)
        {
            Debug.Log("Board onClick!!  -> " + RemoveAlpha(gameObject.name));
            if (boardManager.GetComponent<BoardManager>().CurPiece.GetComponent<Image>().sprite.name.Contains("White"))
            {
                bool pieceFlag = false;
                for(int i = 0; i < boardManager.GetComponent<BoardManager>().PieceBlueCoord.Count; i++)
                {
                    if(RemoveAlpha(gameObject.transform.name) == boardManager.GetComponent<BoardManager>().PieceBlueCoord[i])
                    {
                        pieceFlag = true;
                        break;
                    }
                }
                if (gameObject.transform.GetComponent<Image>().sprite.name.Contains("Blue") && !pieceFlag)
                {
                    gameObject.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/BattleSquareBlueStemp" + RemoveNumber(boardManager.GetComponent<BoardManager>().CurPiece.name));
                    boardManager.GetComponent<BoardManager>().CurPiece.transform.GetChild(0).gameObject.SetActive(false);
                    boardManager.GetComponent<BoardManager>().indexInformation = RemoveAlpha(gameObject.name);
                    
                    kingBlueCoord = RemoveAlpha(gameObject.name);
                }

            }
            else if(boardManager.GetComponent<BoardManager>().CurPiece.GetComponent<Image>().sprite.name.Contains("Black"))
            {
                bool pieceFlag = false;
                for (int i = 0; i < boardManager.GetComponent<BoardManager>().PieceRedCoord.Count; i++)
                {
                    if (RemoveAlpha(gameObject.transform.name) == boardManager.GetComponent<BoardManager>().PieceRedCoord[i])
                    {
                        pieceFlag = true;
                        break;
                    }
                }
                if (gameObject.transform.GetComponent<Image>().sprite.name.Contains("Red") && !pieceFlag)
                {
                    gameObject.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/BattleSquareRedStemp" + RemoveNumber(boardManager.GetComponent<BoardManager>().CurPiece.name));
                    boardManager.GetComponent<BoardManager>().CurPiece.transform.GetChild(0).gameObject.SetActive(false);
                    boardManager.GetComponent<BoardManager>().indexInformation = RemoveAlpha(gameObject.name);

                    kingRedCoord = RemoveAlpha(gameObject.name);
                }

            }

            //boardManager.GetComponent<BoardManager>().CurPiece = null;
        }
    }

    public string RemoveNumber(string str)
    {
        return Regex.Replace(str, @"\d", "");
    }

    public int RemoveAlpha(string str)
    {
        return int.Parse( Regex.Replace(str, @"\D", ""));
        //Regex.Replace(_body, @"[^0-9]", "");

    }
}
