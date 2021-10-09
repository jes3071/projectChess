using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Text.RegularExpressions;

public class BoardManager : MonoBehaviour {
    
    private GridLayoutGroup grid;
    private GameObject parent;
    private Animator turnAnim;
    private GameObject SquareEmpty;
    private GameObject SquareBlue;
    private GameObject SquareRed;

    public MapMaker mapMaker;

    public GameObject CurPiece;
    public int indexInformation = -1;
    private List<int> CurIndex;

    public int CurAnimation = -1;
    public bool[] whoseTurn;
    public bool testTurn = true;

    public int RedChange = 0;
    public int BlueChange = 0;
    public int turnCount = 0;

    private int Pawn = 1;
    private int Rook = 2;
    private int Knight = 3;
    private int Bishop = 4;
    private int Queen = 5;
    private int King = 6;
    private int Apearance = 10;

    public List<int> PieceBlueCoord = new List<int>();
    public List<int> PieceRedCoord = new List<int>();

    private List<GameObject> SquareList = new List<GameObject>();
    private GameObject[,] SquareTile = new GameObject[8,8];

    private List<int[]> BasicMap = new List<int[]>();


    private void Update()
    {
        //Debug.Log("undate");
        //if(SquareList.Count == 0)
        //{
        //    SetDynamicGrid();
        //}
        if (indexInformation != -1)
        {
            if (turnAnim.GetInteger("AIState") == -1)
            {
                turnAnim.SetInteger("AIState",1);
            }
            else
            {
                turnAnim.SetInteger("AIState", -1);
            }

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
                CurAnimation = Pawn;

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
                CurAnimation = Knight;
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
                CurAnimation = Rook;
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
                CurAnimation = Bishop;
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
                CurAnimation = Queen;
            }
            else if (CurPiece.name.Contains("King"))
            {
                CurIndex = new List<int>();
                if (indexInformation >= 7 && (indexInformation - 7) / 8 == (indexInformation - 8) / 8 && SquareList[indexInformation - 7].GetComponent<Image>().sprite.name != "BattleSquareBlock")
                {
                    CurIndex.Add(indexInformation - 7);
                }
                if (indexInformation >= 8 && SquareList[indexInformation - 8].GetComponent<Image>().sprite.name != "BattleSquareBlock")
                {
                    CurIndex.Add(indexInformation - 8);
                }
                if (indexInformation >= 9 && (indexInformation - 9) / 8 == (indexInformation - 8) / 8 && SquareList[indexInformation - 9].GetComponent<Image>().sprite.name != "BattleSquareBlock")
                {
                    CurIndex.Add(indexInformation - 9);
                }
                if (indexInformation >= 1 && (indexInformation - 1) / 8 == (indexInformation) / 8 && SquareList[indexInformation - 1].GetComponent<Image>().sprite.name != "BattleSquareBlock")
                {
                    CurIndex.Add(indexInformation - 1);
                }
                if (indexInformation + 1 <= 63 && (indexInformation + 1) / 8 == (indexInformation) / 8 && SquareList[indexInformation + 1].GetComponent<Image>().sprite.name != "BattleSquareBlock")
                {
                    CurIndex.Add(indexInformation + 1);
                }
                if (indexInformation + 7 <= 63 && (indexInformation + 7) / 8 == (indexInformation + 8) / 8 && SquareList[indexInformation + 7].GetComponent<Image>().sprite.name != "BattleSquareBlock")
                {
                    CurIndex.Add(indexInformation + 7);
                }
                if (indexInformation + 8 <= 63 && SquareList[indexInformation + 8].GetComponent<Image>().sprite.name != "BattleSquareBlock")
                {
                    CurIndex.Add(indexInformation + 8);
                }
                if (indexInformation + 9 <= 63 && (indexInformation + 9) / 8 == (indexInformation + 8) / 8 && SquareList[indexInformation + 9].GetComponent<Image>().sprite.name != "BattleSquareBlock")
                {
                    CurIndex.Add(indexInformation + 9);
                }
                CurAnimation = King;
            }

            for (int i = 0; i < CurIndex.Count; i++)
            {
                if (CurPiece.GetComponent<Image>().sprite.name.Contains("White"))
                {
                    if(i==0)
                        PieceBlueCoord.Add(indexInformation);

                    for (int j = 0; j < PieceRedCoord.Count; j++)
                    {
                        if (CurIndex[i] == PieceRedCoord[j])
                        {
                            PieceRedCoord.RemoveAt(j);
                            break;
                        }
                    }

                    bool stateFlag = false;
                    for(int j = 0; j < PieceBlueCoord.Count; j++)
                    {
                        if (CurIndex[i] == PieceBlueCoord[j])
                        {
                            stateFlag = true;
                            break;
                        }
                    }
                    if (!stateFlag)
                    {
                        SquareList[CurIndex[i]].GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/BattleSquareBlue");
                        SquareList[CurIndex[i]].GetComponent<Animator>().SetInteger("State", CurAnimation);
                        if (!mapMaker.BlueTile.Contains(CurIndex[i]))
                        {
                            mapMaker.BlueTile.Add(CurIndex[i]);
                            mapMaker.RedTile.Remove(CurIndex[i]);
                        }
                            
                    }
                    
                    
                       
                }
                else if (CurPiece.GetComponent<Image>().sprite.name.Contains("Black"))
                {
                    if (i == 0)
                        PieceRedCoord.Add(indexInformation);

                    for (int j = 0; j < PieceBlueCoord.Count; j++)
                    {
                        if (CurIndex[i] == PieceBlueCoord[j])
                        {
                            PieceBlueCoord.RemoveAt(j);
                            break;
                        }
                    }

                    bool stateFlag = false;
                    for (int j = 0; j < PieceRedCoord.Count; j++)
                    {
                        if (CurIndex[i] == PieceRedCoord[j])
                        {
                            stateFlag = true;
                            break;
                        }
                    }
                    if (!stateFlag)
                    {
                        SquareList[CurIndex[i]].GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/BattleSquareRed");
                        SquareList[CurIndex[i]].GetComponent<Animator>().SetInteger("State", CurAnimation);
                        if (!mapMaker.RedTile.Contains(CurIndex[i]))
                        {
                            mapMaker.RedTile.Add(CurIndex[i]);
                            mapMaker.BlueTile.Remove(CurIndex[i]);
                        }
                    }
                        
                }

            }
            if (CurPiece.GetComponent<Image>().sprite.name.Contains("White"))
            {
                CurPiece.GetComponent<Image>().sprite = Resources.Load<Sprite>("ChessPiece/UsedWhite" + RemoveNumber(CurPiece.name));
            }
            else if (CurPiece.GetComponent<Image>().sprite.name.Contains("Black"))
            {
                CurPiece.GetComponent<Image>().sprite = Resources.Load<Sprite>("ChessPiece/UsedBlack" + RemoveNumber(CurPiece.name));
            }


            testTurn = true;
            whoseTurn[0] = true;
            whoseTurn[1] = true;
            CurPiece.GetComponent<Image>().raycastTarget = false;
            CurPiece = null;
            Invoke("SquareInitialize", 1);

            turnCount++;

            if (turnCount >= 30)
            {
                GameObject BlueKingPiece = GameObject.Find("PlayerStateBoard/WithoutPawn").transform.Find("King0").gameObject;
                BlueKingPiece.GetComponent<Image>().raycastTarget = true;
                GameObject RedKingPiece = GameObject.Find("EnemyStateBoard/WithoutPawn").transform.Find("King0").gameObject;
                RedKingPiece.GetComponent<Image>().raycastTarget = true;
            }
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

    public void RedTileCalculate(List<int> index)
    {
        RedChange = index.Count;
        BlueChange = -index.Count;
    }

    public void BlueTileCalculate(List<int> index)
    {
        BlueChange = index.Count;
        RedChange = -index.Count;
    }

    public string RemoveNumber(string str)
    {
        return Regex.Replace(str, @"\d", "");
    }

    private void Awake()
    {
        turnAnim = gameObject.transform.parent.GetComponent<Animator>();
        parent = gameObject.transform.parent.gameObject;
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
            imageObject.GetComponent<Animator>().SetInteger("State", 10);

            SquareList.Add(imageObject);
            
            
            //imageObject.
        }

        for(int j = 0; j < BasicMap.Count; j++)
        {
            switch (j % 3)
            {
                case 0:
                    for (int k = 0; k < BasicMap[j].Length; k++)
                    {
                        SquareList[BasicMap[j][k]].GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/BattleSquareRed");
                    }
                    break;
                case 1:
                    for (int k = 0; k < BasicMap[j].Length; k++)
                    {
                        SquareList[BasicMap[j][k]].GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/BattleSquareBlue");
                    }
                    break;
                case 2:
                    for (int k = 0; k < BasicMap[j].Length; k++)
                    {
                        SquareList[BasicMap[j][k]].GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/BattleSquareBlock");
                    }
                    break;
            }
        }

        Invoke("SquareInitialize", 1);
        
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

        int[] redTile = mapMaker.RedTile.ToArray();
        int[] blueTile = mapMaker.BlueTile.ToArray();
        int[] blockTile = mapMaker.BlockTile.ToArray();

        BasicMap.Add(redTile);
        BasicMap.Add(blueTile);
        BasicMap.Add(blockTile);
        //int[] tempValue = new int[3];
        //tempValue[0] = 0;
        //tempValue[1] = 1;
        //tempValue[2] = 8;

        //int[] tempValue1 = new int[3];
        //tempValue1[0] = 55;
        //tempValue1[1] = 62;
        //tempValue1[2] = 63;

        //BasicMap.Add(tempValue);
         //BasicMap.Add(tempValue);
         //BasicMap.Add(tempValue1);

         GameObject BlueKingPiece = GameObject.Find("PlayerStateBoard/WithoutPawn").transform.Find("King0").gameObject;
        BlueKingPiece.GetComponent<Image>().raycastTarget = false;
        GameObject RedKingPiece = GameObject.Find("EnemyStateBoard/WithoutPawn").transform.Find("King0").gameObject;
        RedKingPiece.GetComponent<Image>().raycastTarget = false;


        whoseTurn = new bool[2] { true, true };

        SetDynamicGrid();
	}
}
