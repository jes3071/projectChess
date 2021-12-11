using System.Collections;
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
	void Update () {
        
        //if(boardManager.CurPiece == null)
        //{
        //    // 0 -> blue , 1 -> red
        //    if (boardManager.curTurn == 0)
        //    {
        //        for (int i = 0; i < 6; i++)
        //        {
        //            if (AutoClick(GameObject.Find("PlayerStateBoard/PieceList").transform.Find("Pawn" + i.ToString()).gameObject))
        //            {
        //                return;
        //            }
        //        }
        //        for (int i = 0; i < 2; i++)
        //        {
        //            if (AutoClick(GameObject.Find("PlayerStateBoard/PieceList").transform.Find("Rook" + i.ToString()).gameObject))
        //            {
        //                return;
        //            }
        //        }
        //        for (int i = 0; i < 2; i++)
        //        {
        //            if (AutoClick(GameObject.Find("PlayerStateBoard/PieceList").transform.Find("Knight" + i.ToString()).gameObject))
        //            {
        //                return;
        //            }
        //        }
        //        for (int i = 0; i < 2; i++)
        //        {
        //            if (AutoClick(GameObject.Find("PlayerStateBoard/PieceList").transform.Find("Bishop" + i.ToString()).gameObject))
        //            {
        //                return;
        //            }
        //        }
        //        if (AutoClick(GameObject.Find("PlayerStateBoard/PieceList").transform.Find("Queen0").gameObject))
        //        {
        //            return;
        //        }
        //        if (AutoClick(GameObject.Find("PlayerStateBoard/PieceList").transform.Find("Prince0").gameObject))
        //        {
        //            return;
        //        }
        //    }
        //    else
        //    {
        //        for (int i = 0; i < 6; i++)
        //        {
        //            if (AutoClick(GameObject.Find("EnemyStateBoard/PieceList").transform.Find("Pawn" + i.ToString()).gameObject))
        //            {
        //                return;
        //            }
        //        }
        //        for (int i = 0; i < 2; i++)
        //        {
        //            if (AutoClick(GameObject.Find("EnemyStateBoard/PieceList").transform.Find("Rook" + i.ToString()).gameObject))
        //            {
        //                return;
        //            }
        //        }
        //        for (int i = 0; i < 2; i++)
        //        {
        //            if (AutoClick(GameObject.Find("EnemyStateBoard/PieceList").transform.Find("Knight" + i.ToString()).gameObject))
        //            {
        //                return;
        //            }
        //        }
        //        for (int i = 0; i < 2; i++)
        //        {
        //            if (AutoClick(GameObject.Find("EnemyStateBoard/PieceList").transform.Find("Bishop" + i.ToString()).gameObject))
        //            {
        //                return;
        //            }
        //        }
        //        if (AutoClick(GameObject.Find("EnemyStateBoard/PieceList").transform.Find("Queen0").gameObject))
        //        {
        //            return;
        //        }
        //        if (AutoClick(GameObject.Find("EnemyStateBoard/PieceList").transform.Find("Prince0").gameObject))
        //        {
        //            return;
        //        }
        //    }
        //    EventSystem.current.SetSelectedGameObject(null);
        //}
    }

    //public bool AutoClick(GameObject obj)
    //{
    //    if (!obj.GetComponent<Image>().sprite.name.Contains("Used"))
    //    {
    //        obj.GetComponent<Animator>().SetBool("Highlighted", true);
    //        boardManager.CurPiece = obj;
    //        return true;
    //    }
    //    return false;
    //}

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Piece onClick!!");
        boardManager.CurPiece = gameObject;
    }

    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    boardManager.CurPiece.GetComponent<Animator>().SetBool("Normal", true);
    //}
}
