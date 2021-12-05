using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class PiecePointCounting : MonoBehaviour {

    public GameObject GetPiecePointBlue;
    public GameObject GetPiecePointRed;

    public BoardManager bdManager;

    public int BluePoint;
    public int RedPoint;

	// Use this for initialization
	void Start () {

	}

    private void OnEnable()
    {
        GetPiecePointBlue.GetComponent<Text>().text = "+0";
        GetPiecePointRed.GetComponent<Text>().text = "+0";
        BluePoint = 0;
        RedPoint = 0;
    }

    // Update is called once per frame
    void Update () {
        if(bdManager.BluePieceCut)
        {
            if (bdManager.PieceBlueCoord.Count != BluePoint)
            {
                GetPiecePointRed.GetComponent<Text>().text = "+" + (RemoveAlpha(GetPiecePointRed.GetComponent<Text>().text.ToString()) + (BluePoint - bdManager.PieceBlueCoord.Count)).ToString();
                BluePoint = bdManager.PieceBlueCoord.Count;
            }
            bdManager.BluePieceCut = false;
        }
		else if(bdManager.RedPieceCut)
        {
            if (bdManager.PieceRedCoord.Count != RedPoint)
            {
                GetPiecePointBlue.GetComponent<Text>().text = "+" + (RemoveAlpha(GetPiecePointBlue.GetComponent<Text>().text.ToString()) + (RedPoint - bdManager.PieceRedCoord.Count)).ToString();
                RedPoint = bdManager.PieceRedCoord.Count;
            }
            bdManager.RedPieceCut = false;
        }
        else
        {
            RedPoint = bdManager.PieceRedCoord.Count;
            BluePoint = bdManager.PieceBlueCoord.Count;
        }
	}

    public int RemoveAlpha(string str)
    {
        return int.Parse(Regex.Replace(str, @"\D", ""));
    }
}
