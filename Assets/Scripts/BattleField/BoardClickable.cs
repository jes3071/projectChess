using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class BoardClickable : MonoBehaviour , IPointerClickHandler
{
    
    private GameObject boardManager;

	// Use this for initialization
	void Start () {
        boardManager = GameObject.Find("UIBattle").transform.Find("BattleBoard").gameObject;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Board onClick!!");
        if(boardManager.GetComponent<BoardManager>().CurPiece != null)
        {
           
            if (boardManager.GetComponent<BoardManager>().CurPiece.GetComponent<Image>().sprite.name == "White" + RemoveNumber(boardManager.GetComponent<BoardManager>().CurPiece.name))
            {
                //Debug.Log("Check Color");
                gameObject.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/BattleSquareBlueStemp" + RemoveNumber(boardManager.GetComponent<BoardManager>().CurPiece.name));
            }
            else if(boardManager.GetComponent<BoardManager>().CurPiece.GetComponent<Image>().sprite.name == "Black" + RemoveNumber(boardManager.GetComponent<BoardManager>().CurPiece.name))
            {
                gameObject.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/BattleSquareRedStemp" + RemoveNumber(boardManager.GetComponent<BoardManager>().CurPiece.name));
            }

            boardManager.GetComponent<BoardManager>().CurPiece = null;
        }
    }

    public string RemoveNumber(string str)
    {
        return Regex.Replace(str, @"\d", "");
    }
}
