using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour {

    public GameObject BlueText;
    public GameObject RedText;

    public GameObject BlueNum;
    public GameObject RedNum;

    public GameObject BlueTotal;
    public GameObject RedTotal;

    public GameObject BluePiece;
    public GameObject RedPiece;

    public BoardManager bdManager;
    public PiecePointCounting ppCounting;
    public MapMaker mapMaker;

    private void OnEnable()
    {
        if(mapMaker.BlueTile.Count == 0 || mapMaker.BlueTile.Count == bdManager.PieceBlueCoord.Count)
        {
            BlueText.GetComponent<Text>().text = "불계패";
            RedText.GetComponent<Text>().text = "불계승";

            BlueText.GetComponent<Text>().fontSize = 95;
            RedText.GetComponent<Text>().fontSize = 95;

            BlueText.GetComponent<Text>().color = new Color(158 / 255f, 0 / 255f, 0 / 255f);
            RedText.GetComponent<Text>().color = new Color(0 / 255f, 119 / 255f, 215 / 255f);

            CanNotPlayText();

        } else if(mapMaker.RedTile.Count == 0 || mapMaker.RedTile.Count == bdManager.PieceRedCoord.Count)
        {
            BlueText.GetComponent<Text>().text = "불계승";
            RedText.GetComponent<Text>().text = "불계패";

            BlueText.GetComponent<Text>().fontSize = 95;
            RedText.GetComponent<Text>().fontSize = 95;

            BlueText.GetComponent<Text>().color = new Color(0 / 255f, 119 / 255f, 215 / 255f);
            RedText.GetComponent<Text>().color = new Color(158 / 255f, 0 / 255f, 0 / 255f);

            CanNotPlayText();

        }
        else
        {

            int blueTotal = bdManager.BlueCoord.Count + RemoveAlpha(ppCounting.GetPiecePointBlue.GetComponent<Text>().text);
            int redTotal = bdManager.RedCoord.Count + RemoveAlpha(ppCounting.GetPiecePointRed.GetComponent<Text>().text);

            if (blueTotal > redTotal)
            {
                BlueText.GetComponent<Text>().text = "승리";
                RedText.GetComponent<Text>().text = "패배";

                BlueText.GetComponent<Text>().fontSize = 130;
                RedText.GetComponent<Text>().fontSize = 130;

                BlueText.GetComponent<Text>().color = new Color(0 / 255f, 119 / 255f, 215 / 255f);
                RedText.GetComponent<Text>().color = new Color(158 / 255f, 0 / 255f, 0 / 255f);

                BlueTotal.GetComponent<Text>().text = "총 점수 : " + bdManager.BlueCoord.Count.ToString() +
                " + " + RemoveAlpha(ppCounting.GetPiecePointBlue.GetComponent<Text>().text) +
                " = <B><size=60><color=#0077D7FF>" + blueTotal + "</color></size></B>";
                RedTotal.GetComponent<Text>().text = "총 점수 : " + bdManager.RedCoord.Count.ToString() +
                    " + " + RemoveAlpha(ppCounting.GetPiecePointRed.GetComponent<Text>().text) +
                    " = <B><size=60><color=#9E0000FF>" + redTotal + "</color></size></B>";
            }
            else if (blueTotal < redTotal)
            {
                BlueText.GetComponent<Text>().text = "패배";
                RedText.GetComponent<Text>().text = "승리";

                BlueText.GetComponent<Text>().fontSize = 130;
                RedText.GetComponent<Text>().fontSize = 130;

                BlueText.GetComponent<Text>().color = new Color(158 / 255f, 0 / 255f, 0 / 255f);
                RedText.GetComponent<Text>().color = new Color(0 / 255f, 119 / 255f, 215 / 255f);

                BlueTotal.GetComponent<Text>().text = "총 점수 : " + bdManager.BlueCoord.Count.ToString() +
                " + " + RemoveAlpha(ppCounting.GetPiecePointBlue.GetComponent<Text>().text) +
                " = <B><size=60><color=#9E0000FF>" + blueTotal + "</color></size></B>";
                RedTotal.GetComponent<Text>().text = "총 점수 : " + bdManager.RedCoord.Count.ToString() +
                    " + " + RemoveAlpha(ppCounting.GetPiecePointRed.GetComponent<Text>().text) +
                    " = <B><size=60><color=#0077D7FF>" + redTotal + "</color></size></B>";
            }
            else
            {
                BlueText.GetComponent<Text>().text = "무승부";
                RedText.GetComponent<Text>().text = "무승부";

                BlueText.GetComponent<Text>().fontSize = 95;
                RedText.GetComponent<Text>().fontSize = 95;


                BlueText.GetComponent<Text>().color = Color.white;
                RedText.GetComponent<Text>().color = Color.white;

                BlueTotal.GetComponent<Text>().text = "총 점수 : " + bdManager.BlueCoord.Count.ToString() +
                " + " + RemoveAlpha(ppCounting.GetPiecePointBlue.GetComponent<Text>().text) +
                " = <B><size=60><color=#FFFFFFFF>" + blueTotal + "</color></size></B>";
                RedTotal.GetComponent<Text>().text = "총 점수 : " + bdManager.RedCoord.Count.ToString() +
                    " + " + RemoveAlpha(ppCounting.GetPiecePointRed.GetComponent<Text>().text) +
                    " = <B><size=60><color=#FFFFFFFF>" + redTotal + "</color></size></B>";
            }

            BlueNum.GetComponent<Text>().text = "차지한 영토 : " + bdManager.BlueCoord.Count.ToString();
            RedNum.GetComponent<Text>().text = "차지한 영토 : " + bdManager.RedCoord.Count.ToString();

            BluePiece.GetComponent<Text>().text = "포획한 기물 : " + ppCounting.GetPiecePointBlue.GetComponent<Text>().text;
            RedPiece.GetComponent<Text>().text = "포획한 기물 : " + ppCounting.GetPiecePointRed.GetComponent<Text>().text;
        }
    }
    public void CanNotPlayText()
    {
        BlueNum.GetComponent<Text>().text = "차지한 영토 : 0";
        RedNum.GetComponent<Text>().text = "차지한 영토 : 0";

        BluePiece.GetComponent<Text>().text = "포획한 기물 : +0";
        RedPiece.GetComponent<Text>().text = "포획한 기물 : +0";
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public int RemoveAlpha(string str)
    {
        return int.Parse(Regex.Replace(str, @"\D", ""));
    }


}
