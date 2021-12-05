using System.Collections;
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
            if (bdManager.BlueCoord.Count > bdManager.RedCoord.Count)
            {
                BlueText.GetComponent<Text>().text = "승리";
                RedText.GetComponent<Text>().text = "패배";

                BlueText.GetComponent<Text>().fontSize = 130;
                RedText.GetComponent<Text>().fontSize = 130;

                BlueText.GetComponent<Text>().color = new Color(0 / 255f, 119 / 255f, 215 / 255f);
                RedText.GetComponent<Text>().color = new Color(158 / 255f, 0 / 255f, 0 / 255f);
            }
            else if (bdManager.BlueCoord.Count < bdManager.RedCoord.Count)
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

            BlueNum.GetComponent<Text>().text = "차지한 영토 : " + bdManager.BlueCoord.Count.ToString();
            RedNum.GetComponent<Text>().text = "차지한 영토 : " + bdManager.RedCoord.Count.ToString();

            BluePiece.GetComponent<Text>().text = "포획한 기물 : " + ppCounting.GetPiecePointBlue.GetComponent<Text>().text; // bdManager.PieceBlueCoord.Count.ToString();
            RedPiece.GetComponent<Text>().text = "포획한 기물 : " + ppCounting.GetPiecePointRed.GetComponent<Text>().text; // bdManager.PieceRedCoord.Count.ToString();
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


}
