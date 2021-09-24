using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Text.RegularExpressions;

public class BoardManager : MonoBehaviour {
    
    private GridLayoutGroup grid;
    private GameObject SquareEmpty;
    private GameObject SquareBlue;
    private GameObject SquareRed;

    public GameObject CurPiece;
    public int indexInformation = -1;
    public List<int> CurIndex;

    private List<GameObject> SquareList = new List<GameObject>();
    private GameObject[,] SquareTile = new GameObject[8,8];

    private List<int[]> BasicMap = new List<int[]>();
    private int[] toHorn = new int[] {0,1,8 };


    private void Update()
    {
        //Debug.Log("undate");
        if(indexInformation != -1)
        {
            if(CurPiece.name.Contains("Pawn"))
            {
                //Debug.Log("Pawn");
                //Debug.Log("index = "+indexInformation);
                //Debug.Log("name = " + CurPiece.GetComponent<Image>().sprite.name);
                
                CurIndex = new List<int>();
                if (CurPiece.GetComponent<Image>().sprite.name.Contains("White"))
                {
                    if(indexInformation >= 9 && (indexInformation - 9) / 8 == (indexInformation - 8) / 8 && SquareList[indexInformation - 9].GetComponent<Image>().sprite.name != "BattleSquareBlock")
                    {
                        CurIndex.Add(indexInformation - 9);

                    }

                    if (indexInformation >= 7 && (indexInformation - 7) / 8 == (indexInformation - 8) / 8 && SquareList[indexInformation - 7].GetComponent<Image>().sprite.name != "BattleSquareBlock")
                    {
                        CurIndex.Add(indexInformation - 7);
                    }
                }
                else
                {
                    if (indexInformation + 9 <= 63 && (indexInformation + 9) / 8 == (indexInformation + 8) / 8 && SquareList[indexInformation + 9].GetComponent<Image>().sprite.name != "BattleSquareBlock")
                    {
                        CurIndex.Add(indexInformation + 9);
                    }

                    if (indexInformation + 7 <= 63 && (indexInformation + 7) / 8 == (indexInformation + 8) / 8 && SquareList[indexInformation + 7].GetComponent<Image>().sprite.name != "BattleSquareBlock")
                    {
                        CurIndex.Add(indexInformation + 7);
                    }
                }
                    
            }
            else if (CurPiece.name.Contains("Knight"))
            {
                CurIndex = new List<int>();
                if (indexInformation >= 10 && (indexInformation - 10) / 8 == (indexInformation - 8) / 8 && SquareList[indexInformation - 10].GetComponent<Image>().sprite.name != "BattleSquareBlock")
                {
                    CurIndex.Add(indexInformation - 10);
                }
                if (indexInformation >= 17 && (indexInformation - 17) / 8 == (indexInformation - 16) / 8 && SquareList[indexInformation - 17].GetComponent<Image>().sprite.name != "BattleSquareBlock")
                {
                    CurIndex.Add(indexInformation - 17);
                }
                if (indexInformation >= 15 && (indexInformation - 15) / 8 == (indexInformation - 16) / 8 && SquareList[indexInformation - 15].GetComponent<Image>().sprite.name != "BattleSquareBlock")
                {
                    CurIndex.Add(indexInformation - 15);
                }
                if (indexInformation >= 8 && (indexInformation - 6) / 8 == (indexInformation - 8) / 8 && SquareList[indexInformation - 6].GetComponent<Image>().sprite.name != "BattleSquareBlock")
                {
                    CurIndex.Add(indexInformation - 6);
                }

                if (indexInformation + 10 <= 63 && (indexInformation + 10) / 8 == (indexInformation + 8) / 8 && SquareList[indexInformation + 10].GetComponent<Image>().sprite.name != "BattleSquareBlock")
                {
                    CurIndex.Add(indexInformation + 10);
                }
                if (indexInformation + 17 <= 63 && (indexInformation + 17) / 8 == (indexInformation + 16) / 8 && SquareList[indexInformation + 17].GetComponent<Image>().sprite.name != "BattleSquareBlock")
                {
                    CurIndex.Add(indexInformation + 17);
                }
                if (indexInformation + 15 <= 63 && (indexInformation + 15) / 8 == (indexInformation + 16) / 8 && SquareList[indexInformation + 15].GetComponent<Image>().sprite.name != "BattleSquareBlock")
                {
                    CurIndex.Add(indexInformation + 15);
                }
                if (indexInformation + 6 <= 63 && (indexInformation + 6) / 8 == (indexInformation + 8) / 8 && SquareList[indexInformation + 6].GetComponent<Image>().sprite.name != "BattleSquareBlock")
                {
                    CurIndex.Add(indexInformation + 6);
                }
            }
            else if (CurPiece.name.Contains("Rook"))
            {
                CurIndex = new List<int>();
                int i = 1;
                while(indexInformation >= i && (indexInformation - i) / 8 == (indexInformation / 8) && SquareList[indexInformation - i].GetComponent<Image>().sprite.name != "BattleSquareBlock")
                {
                    CurIndex.Add(indexInformation - i);
                    i++;
                }
                i = 1;
                while (indexInformation + i <= 63 && (indexInformation + i) / 8 == (indexInformation / 8) && SquareList[indexInformation + i].GetComponent<Image>().sprite.name != "BattleSquareBlock")
                {
                    CurIndex.Add(indexInformation + i);
                    i++;
                }
                i = 8;
                while (indexInformation >= i && (indexInformation - i) % 8 == (indexInformation % 8) && SquareList[indexInformation - i].GetComponent<Image>().sprite.name != "BattleSquareBlock")
                {
                    CurIndex.Add(indexInformation - i);
                    i += 8;
                }
                i = 8;
                while (indexInformation + i <= 63 && (indexInformation + i) % 8 == (indexInformation % 8) && SquareList[indexInformation + i].GetComponent<Image>().sprite.name != "BattleSquareBlock")
                {
                    CurIndex.Add(indexInformation + i);
                    i += 8;
                }
            }
            else if (CurPiece.name.Contains("Bishop"))
            {
                CurIndex = new List<int>();
                int i = 1;
                while (indexInformation >= i*9 && (indexInformation - 9*i) / 8 == (indexInformation / 8) - i && SquareList[indexInformation -9*i].GetComponent<Image>().sprite.name != "BattleSquareBlock") // 좌상
                {
                    CurIndex.Add(indexInformation - i*9);
                    i++;
                }
                i = 1;
                while (indexInformation + i*9 <= 63 && (indexInformation + 9*i) / 8 == (indexInformation / 8) + i && SquareList[indexInformation + 9*i].GetComponent<Image>().sprite.name != "BattleSquareBlock") // 우하
                {
                    CurIndex.Add(indexInformation + i*9);
                    i++;
                }
                i = 1;
                while (indexInformation +7*i <= 63 && (indexInformation + 7*i) / 8 == (indexInformation / 8) + i && SquareList[indexInformation + 7*i].GetComponent<Image>().sprite.name != "BattleSquareBlock") // 좌하
                {
                    CurIndex.Add(indexInformation + i * 7);
                    i++;
                }
                i = 1;
                while (indexInformation >= 7*i && (indexInformation - 7*i) / 8 == (indexInformation / 8) - i && SquareList[indexInformation - 7*i].GetComponent<Image>().sprite.name != "BattleSquareBlock") // 우상
                {
                    CurIndex.Add(indexInformation - i * 7);
                    i++;
                }
            }
            else if (CurPiece.name.Contains("Queen"))
            {
                CurIndex = new List<int>();
                int i = 1;
                while (indexInformation >= i * 9 && (indexInformation - 9 * i) / 8 == (indexInformation / 8) - i && SquareList[indexInformation - 9 * i].GetComponent<Image>().sprite.name != "BattleSquareBlock") // 좌상
                {
                    CurIndex.Add(indexInformation - i * 9);
                    i++;
                }
                i = 1;
                while (indexInformation + i * 9 <= 63 && (indexInformation + 9 * i) / 8 == (indexInformation / 8) + i && SquareList[indexInformation + 9 * i].GetComponent<Image>().sprite.name != "BattleSquareBlock") // 우하
                {
                    CurIndex.Add(indexInformation + i * 9);
                    i++;
                }
                i = 1;
                while (indexInformation + 7 * i <= 63 && (indexInformation + 7 * i) / 8 == (indexInformation / 8) + i && SquareList[indexInformation + 7 * i].GetComponent<Image>().sprite.name != "BattleSquareBlock") // 좌하
                {
                    CurIndex.Add(indexInformation + i * 7);
                    i++;
                }
                i = 1;
                while (indexInformation >= 7 * i && (indexInformation - 7 * i) / 8 == (indexInformation / 8) - i && SquareList[indexInformation - 7 * i].GetComponent<Image>().sprite.name != "BattleSquareBlock") // 우상
                {
                    CurIndex.Add(indexInformation - i * 7);
                    i++;
                }
                i = 1;
                while (indexInformation >= i && (indexInformation - i) / 8 == (indexInformation / 8) && SquareList[indexInformation - i].GetComponent<Image>().sprite.name != "BattleSquareBlock")
                {
                    CurIndex.Add(indexInformation - i);
                    i++;
                }
                i = 1;
                while (indexInformation + i <= 63 && (indexInformation + i) / 8 == (indexInformation / 8) && SquareList[indexInformation + i].GetComponent<Image>().sprite.name != "BattleSquareBlock")
                {
                    CurIndex.Add(indexInformation + i);
                    i++;
                }
                i = 8;
                while (indexInformation >= i && (indexInformation - i) % 8 == (indexInformation % 8) && SquareList[indexInformation - i].GetComponent<Image>().sprite.name != "BattleSquareBlock")
                {
                    CurIndex.Add(indexInformation - i);
                    i += 8;
                }
                i = 8;
                while (indexInformation + i <= 63 && (indexInformation + i) % 8 == (indexInformation % 8) && SquareList[indexInformation + i].GetComponent<Image>().sprite.name != "BattleSquareBlock")
                {
                    CurIndex.Add(indexInformation + i);
                    i += 8;
                }
            }

            for (int i = 0; i < CurIndex.Count; i++)
            {
                if (CurPiece.GetComponent<Image>().sprite.name.Contains("White"))
                {
                    SquareList[CurIndex[i]].GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/BattleSquareBlue");
                    SquareList[CurIndex[i]].GetComponent<Animator>().SetInteger("State",1);
                    
                       
                }
                else if (CurPiece.GetComponent<Image>().sprite.name.Contains("Black"))
                {
                    SquareList[CurIndex[i]].GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/BattleSquareRed");
                    SquareList[CurIndex[i]].GetComponent<Animator>().SetInteger("State", 1);
                }

            }
           
            CurPiece.GetComponent<Image>().sprite = Resources.Load<Sprite>("ChessPiece/Used" + RemoveNumber(CurPiece.name));
            CurPiece.GetComponent<Image>().raycastTarget = false;
            CurPiece = null;
            Invoke("SquareInitialize", 1);
        }
        indexInformation = -1;
        
    }

    public void SquareInitialize()
    {
        for(int i = 0; i < SquareList.Count; i++)
        {
            SquareList[i].GetComponent<Animator>().SetInteger("State", -1);
        }
        
    }

    public string RemoveNumber(string str)
    {
        return Regex.Replace(str, @"\d", "");
    }

    private void Awake()
    {
        
        grid = gameObject.GetComponent<GridLayoutGroup>();
    }

    public void SetDynamicGrid()
    {
        //grid.cellSize = new Vector2(75, 75);

        //grid.spacing = new Vector2(10, 10);
        //grid.startCorner = GridLayoutGroup.Corner.UpperLeft;
        //grid.startAxis = GridLayoutGroup.Axis.Horizontal;
        //grid.constraint = GridLayoutGroup.Constraint.Flexible;

        for(int i = 0; i < 64; i++)
        {
            GameObject imageObject = Instantiate(SquareEmpty) as GameObject;

            imageObject.transform.SetParent(transform);
            imageObject.transform.localScale = Vector3.one;
            imageObject.transform.position = transform.position;
            imageObject.name = "child" + (i);
            imageObject.GetComponent<Animator>().SetInteger("State", -1);

            SquareList.Add(imageObject);
            
            
            //imageObject.
        }
        for(int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                SquareTile[i,j] = SquareList[i*8 + j];
            }
        }

        for(int j = 0; j < BasicMap.Count; j++)
        {
            if(j % 2 == 1)
            {
                for (int k = 0; k < BasicMap[j].Length; k++)
                {
                    SquareList[BasicMap[j][k]].GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/BattleSquareBlue");
                }
            }
            else
            {
                for (int k = 0; k < BasicMap[j].Length; k++)
                {
                    SquareList[BasicMap[j][k]].GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/BattleSquareRed");
                }
            }
        }

        SquareList[30].GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/BattleSquareBlock");
        SquareList[35].GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/BattleSquareBlock");
        SquareList[40].GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/BattleSquareBlock");
        SquareList[45].GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/BattleSquareBlock");







    }

    // Use this for initialization
    void Start () {

        SquareEmpty = Resources.Load<GameObject>("Prefab/SquareEmpty");
        SquareBlue = Resources.Load<GameObject>("Prefab/SquareBlue");
        SquareRed = Resources.Load<GameObject>("Prefab/SquareRed");

        if (SquareEmpty == null || SquareBlue == null || SquareRed == null)
        {
            Debug.Log("Square SomeThing == null");
        }

        int[] tempValue = new int[3];
        tempValue[0] = 0;
        tempValue[1] = 1;
        tempValue[2] = 8;

        int[] tempValue1 = new int[3];
        tempValue1[0] = 55;
        tempValue1[1] = 62;
        tempValue1[2] = 63;

        //BasicMap.Add(tempValue);
        BasicMap.Add(toHorn);
        BasicMap.Add(tempValue1);


        SetDynamicGrid();
	}
}
