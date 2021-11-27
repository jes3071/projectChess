using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;
using System.Text.RegularExpressions;

public class BoardManager : MonoBehaviour {

    private int BLUE_TURN = 0;
    private int RED_TURN = 1;
    
    private GridLayoutGroup grid;
    private GameObject parent;
    private Animator turnAnim;
    private GameObject SquareEmpty;

    public MapMaker mapMaker;

    public GameObject CurPiece;
    public int indexInformation;
    private List<int> CurIndex;

    private int CurAnimation;
    public bool turnChange;

    private int RedChange;
    private int BlueChange;
    private int turnCount;

    public BtnEvent ResultOpener;

    private int Pawn = 1;
    private int Rook = 2;
    private int Knight = 3;
    private int Bishop = 4;
    private int Queen = 5;
    private int King = 6;
    private int Apearance = 10;

    public List<int> PieceBlueCoord;
    public List<int> PieceRedCoord;

    private List<GameObject> SquareList = new List<GameObject>();
    private GameObject[,] SquareTile;

    public List<int> BasicMap;

    public Toggle FirstTurn;
    public Toggle TurnTimeToggle;

    private int curTurn;
    private int turnTime;

    public GameObject TimerOne;
    public GameObject TimerTwo;


    public int kingBluePoint;
    public int kingRedPoint;

    public List<int> BlueCoord;
    public List<int> RedCoord;

    public AudioManager audioManager;

    IEnumerator timerCoroutine;

    public bool CR_update = false;

    void StartTimer()
    {
        timerCoroutine = Timer(turnTime);
        if(isActiveAndEnabled)
        {
            StartCoroutine(timerCoroutine);
        }
    }

    void StopTimer()
    {
        if(timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
        }
    }


    IEnumerator Timer(float turnTime)
    {

        float duration = turnTime; // 카운팅에 걸리는 시간 설정. 
        float target = 0;
        float current = 1f;

        float offset = (target - current) / duration;

        if(curTurn == BLUE_TURN)
        {
            while (current > target)
            {
                float time = Time.deltaTime;
                current += offset * time;
                
                TimerOne.gameObject.GetComponent<Image>().fillAmount = current;

                yield return null;
            }
        }
        else if(curTurn == RED_TURN)
        {
            while (current > target)
            {
                float time = Time.deltaTime;
                current += offset * time;
                
                TimerTwo.gameObject.GetComponent<Image>().fillAmount = current;

                yield return null;
            }
        }

        turnChange = true;
        if(CurPiece != null)
        {
            CurPiece.GetComponent<Image>().raycastTarget = false;
            CurPiece = null;
        }
        
        Invoke("SquareInitialize", 1);

        turnCount++;

        StopTimer();

        if (curTurn == BLUE_TURN)
        {
            turnAnim.SetInteger("AIState", 1);
            curTurn = RED_TURN;
            TimerOne.gameObject.GetComponent<Image>().fillAmount = 0f;
            TimerTwo.gameObject.GetComponent<Image>().fillAmount = 1f;
        }
        else if (curTurn == RED_TURN)
        {
            turnAnim.SetInteger("AIState", -1);
            curTurn = BLUE_TURN;
            TimerOne.gameObject.GetComponent<Image>().fillAmount = 1f;
            TimerTwo.gameObject.GetComponent<Image>().fillAmount = 0f;
        }

        StartTimer();
        CR_update = false;
    }

    IEnumerator BlueChanger(int i)
    {
        SquareList[CurIndex[i]].GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/BattleSquareBlue");
        SquareList[CurIndex[i]].GetComponent<Animator>().SetInteger("State", CurAnimation);
        SquareList[CurIndex[i]].transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(0 / 255f, 119 / 255f, 215 / 255f);

        yield return new WaitForFixedUpdate();
    }

        private void Update()
    {
        if(mapMaker.RedTile.Count == 0 || mapMaker.BlueTile.Count == 0)
        {
            StopTimer();
            //ResultOpener.Invoke("BattleResult", 2);
            ResultOpener.BattleResult();
        }
        if (indexInformation != -1 && !CR_update)
        {
            CR_update = true;
            StartCoroutine(UpdateChessBoard());
        }
        indexInformation = -1;
    }

    IEnumerator UpdateChessBoard()
    {
        
        AudioManager.instance.BaseSound();

        if (CurPiece.name.Contains("Pawn"))
        {
            AudioManager.instance.PawnSound();

            CurIndex = new List<int>();
            if (CurPiece.GetComponent<Image>().sprite.name.Contains("White"))
            {
                if (indexInformation >= 9 && (indexInformation - 9) / 8 == (indexInformation - 8) / 8 && SquareList[indexInformation - 9].GetComponent<Image>().sprite.name != "BattleSquareBlock")
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
            AudioManager.instance.KnightSound();
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
            AudioManager.instance.RookSound();
            CurIndex = new List<int>();
            int i = 1;
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
            CurAnimation = Rook;
        }
        else if (CurPiece.name.Contains("Bishop"))
        {
            AudioManager.instance.BishopSound();
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
            CurAnimation = Bishop;
        }
        else if (CurPiece.name.Contains("Queen"))
        {
            AudioManager.instance.QueenSound();
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
            AudioManager.instance.KingSound();
            CurIndex = new List<int>();

            if (FirstTurn.isOn) // 1p first
            {
                if (curTurn == BLUE_TURN)
                {
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
                }
                else if (curTurn == RED_TURN)
                {
                    if (indexInformation >= 7 && (indexInformation - 7) / 8 == (indexInformation - 8) / 8 && SquareList[indexInformation - 7].GetComponent<Image>().sprite.name != "BattleSquareBlock" && !BlueCoord.Contains(indexInformation - 7))
                    {
                        CurIndex.Add(indexInformation - 7);
                    }
                    if (indexInformation >= 8 && SquareList[indexInformation - 8].GetComponent<Image>().sprite.name != "BattleSquareBlock" && !BlueCoord.Contains(indexInformation - 8))
                    {
                        CurIndex.Add(indexInformation - 8);
                    }
                    if (indexInformation >= 9 && (indexInformation - 9) / 8 == (indexInformation - 8) / 8 && SquareList[indexInformation - 9].GetComponent<Image>().sprite.name != "BattleSquareBlock" && !BlueCoord.Contains(indexInformation - 9))
                    {
                        CurIndex.Add(indexInformation - 9);
                    }
                    if (indexInformation >= 1 && (indexInformation - 1) / 8 == (indexInformation) / 8 && SquareList[indexInformation - 1].GetComponent<Image>().sprite.name != "BattleSquareBlock" && !BlueCoord.Contains(indexInformation - 1))
                    {
                        CurIndex.Add(indexInformation - 1);
                    }
                    if (indexInformation + 1 <= 63 && (indexInformation + 1) / 8 == (indexInformation) / 8 && SquareList[indexInformation + 1].GetComponent<Image>().sprite.name != "BattleSquareBlock" && !BlueCoord.Contains(indexInformation + 1))
                    {
                        CurIndex.Add(indexInformation + 1);
                    }
                    if (indexInformation + 7 <= 63 && (indexInformation + 7) / 8 == (indexInformation + 8) / 8 && SquareList[indexInformation + 7].GetComponent<Image>().sprite.name != "BattleSquareBlock" && !BlueCoord.Contains(indexInformation + 7))
                    {
                        CurIndex.Add(indexInformation + 7);
                    }
                    if (indexInformation + 8 <= 63 && SquareList[indexInformation + 8].GetComponent<Image>().sprite.name != "BattleSquareBlock" && !BlueCoord.Contains(indexInformation + 8))
                    {
                        CurIndex.Add(indexInformation + 8);
                    }
                    if (indexInformation + 9 <= 63 && (indexInformation + 9) / 8 == (indexInformation + 8) / 8 && SquareList[indexInformation + 9].GetComponent<Image>().sprite.name != "BattleSquareBlock" && !BlueCoord.Contains(indexInformation + 9))
                    {
                        CurIndex.Add(indexInformation + 9);
                    }
                }
            }
            else if (!FirstTurn.isOn) // 2p first
            {
                if (curTurn == BLUE_TURN)
                {
                    if (indexInformation >= 7 && (indexInformation - 7) / 8 == (indexInformation - 8) / 8 && SquareList[indexInformation - 7].GetComponent<Image>().sprite.name != "BattleSquareBlock" && !RedCoord.Contains(indexInformation - 7))
                    {
                        CurIndex.Add(indexInformation - 7);
                    }
                    if (indexInformation >= 8 && SquareList[indexInformation - 8].GetComponent<Image>().sprite.name != "BattleSquareBlock" && !RedCoord.Contains(indexInformation - 8))
                    {
                        CurIndex.Add(indexInformation - 8);
                    }
                    if (indexInformation >= 9 && (indexInformation - 9) / 8 == (indexInformation - 8) / 8 && SquareList[indexInformation - 9].GetComponent<Image>().sprite.name != "BattleSquareBlock" && !RedCoord.Contains(indexInformation - 9))
                    {
                        CurIndex.Add(indexInformation - 9);
                    }
                    if (indexInformation >= 1 && (indexInformation - 1) / 8 == (indexInformation) / 8 && SquareList[indexInformation - 1].GetComponent<Image>().sprite.name != "BattleSquareBlock" && !RedCoord.Contains(indexInformation - 1))
                    {
                        CurIndex.Add(indexInformation - 1);
                    }
                    if (indexInformation + 1 <= 63 && (indexInformation + 1) / 8 == (indexInformation) / 8 && SquareList[indexInformation + 1].GetComponent<Image>().sprite.name != "BattleSquareBlock" && !RedCoord.Contains(indexInformation + 1))
                    {
                        CurIndex.Add(indexInformation + 1);
                    }
                    if (indexInformation + 7 <= 63 && (indexInformation + 7) / 8 == (indexInformation + 8) / 8 && SquareList[indexInformation + 7].GetComponent<Image>().sprite.name != "BattleSquareBlock" && !RedCoord.Contains(indexInformation + 7))
                    {
                        CurIndex.Add(indexInformation + 7);
                    }
                    if (indexInformation + 8 <= 63 && SquareList[indexInformation + 8].GetComponent<Image>().sprite.name != "BattleSquareBlock" && !RedCoord.Contains(indexInformation + 8))
                    {
                        CurIndex.Add(indexInformation + 8);
                    }
                    if (indexInformation + 9 <= 63 && (indexInformation + 9) / 8 == (indexInformation + 8) / 8 && SquareList[indexInformation + 9].GetComponent<Image>().sprite.name != "BattleSquareBlock" && !RedCoord.Contains(indexInformation + 9))
                    {
                        CurIndex.Add(indexInformation + 9);
                    }

                }
                else if (curTurn == RED_TURN)
                {
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
                }
            }


            CurAnimation = King;

        }

        if (mapMaker.Map.GetComponent<Text>().text.Equals("투혼"))
        {
            if (CurIndex.Contains(7) && SquareList[7].GetComponent<Image>().sprite.name == "BattleSquareMulti")
            {
                CurIndex.AddRange(new int[9] { 5, 6, 7, 13, 14, 15, 21, 22, 23 });
                CurIndex = CurIndex.Distinct().ToList();
            }
            if (CurIndex.Contains(56) && SquareList[56].GetComponent<Image>().sprite.name == "BattleSquareMulti")
            {
                CurIndex.AddRange(new int[9] { 40, 41, 42, 48, 49, 50, 56, 57, 58 });
                CurIndex = CurIndex.Distinct().ToList();
            }
        }
            

        for (int i = 0; i < CurIndex.Count; i++)
        {
            if (CurPiece.GetComponent<Image>().sprite.name.Contains("White"))
            {

                if (i == 0)
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
                for (int j = 0; j < PieceBlueCoord.Count; j++)
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
                    SquareList[CurIndex[i]].transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(0 / 255f, 119 / 255f, 215 / 255f);
                    yield return null;
                    //if(mapMaker.BasicMap[CurIndex[i]] != 1)
                    //{
                    //    mapMaker.BasicMap[CurIndex[i]] = 1;
                    //}
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
                    SquareList[CurIndex[i]].transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(158 / 255f, 0 / 255f, 0 / 255f);
                    yield return null;
                    //if (mapMaker.BasicMap[CurIndex[i]] != 0)
                    //{
                    //    mapMaker.BasicMap[CurIndex[i]] = 0;
                    //}
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

        if (curTurn == BLUE_TURN && CurAnimation == King) // 1p
        {
            Debug.Log("blue");
            BlueCalcul(BoardClickable.kingBlueCoord);
            for (int i = 0; i < BlueCoord.Count; i++)
            {
                SquareList[BlueCoord[i]].GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/BattleSquareBlueStempLand");
                SquareList[BlueCoord[i]].GetComponent<Animator>().SetInteger("State", CurAnimation);
                SquareList[BlueCoord[i]].transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(0 / 255f, 119 / 255f, 215 / 255f);
                yield return null;
                //SquareList[BlueCoord[i]].GetComponent<Animator>().SetInteger("State", CurAnimation);

            }
        }
        else if (curTurn == RED_TURN && CurAnimation == King)
        {
            Debug.Log("red");
            RedCalcul(BoardClickable.kingRedCoord);
            for (int i = 0; i < RedCoord.Count; i++)
            {
                SquareList[RedCoord[i]].GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/BattleSquareRedStempLand");
                SquareList[RedCoord[i]].GetComponent<Animator>().SetInteger("State", CurAnimation);
                SquareList[RedCoord[i]].transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(158 / 255f, 0 / 255f, 0 / 255f);
                yield return null;
                //SquareList[RedCoord[i]].GetComponent<Animator>().SetInteger("State", CurAnimation);

            }
        }


        turnChange = true;
        CurPiece.GetComponent<Image>().raycastTarget = false;
        CurPiece = null;
        Invoke("SquareInitialize", 1);

        turnCount++;

        if (turnCount >= 3) // 막 턴 되면 킹 선택 가능하게 30
        {
            GameObject BlueKingPiece = GameObject.Find("PlayerStateBoard/PieceList").transform.Find("King0").gameObject;
            BlueKingPiece.GetComponent<Image>().raycastTarget = true;
            GameObject RedKingPiece = GameObject.Find("EnemyStateBoard/PieceList").transform.Find("King0").gameObject;
            RedKingPiece.GetComponent<Image>().raycastTarget = true;
        }

        if (turnCount >= 32) // 게임 끝
        {
            ResultOpener.Invoke("BattleResult", 2);
            //ResultOpener.BattleResult();
            Debug.Log("blue king = " + BlueCoord.Count);
            Debug.Log("red king = " + RedCoord.Count);
            //Debug.Log("blue Piece = " + PieceBlueCoord.Count);
            //Debug.Log("red Piece = " + PieceRedCoord.Count);
            //Debug.Log("blue tile = " + mapMaker.BlueTile.Count);
            //Debug.Log("red tile = " + mapMaker.RedTile.Count);
        }

        StopTimer();

        if (mapMaker.Map.GetComponent<Text>().text.Equals("우로보로스"))
        {
            if(turnCount == 5)
            {
                yield return new WaitForSeconds(0.5f);
                for (int i = 0; i < 3; i++)
                {
                    yield return new WaitForSeconds(0.3f);
                    UroborosOutMap();
                }
            }

            if(turnCount == 10)
            {
                yield return new WaitForSeconds(0.5f);
                for (int i = 0; i < 5; i++)
                {
                    yield return new WaitForSeconds(0.3f);
                    UroborosInMap();
                }
            }
            
            
        }
        

        if (curTurn == BLUE_TURN)
        {
            turnAnim.SetInteger("AIState", 1);
            curTurn = RED_TURN;
            TimerOne.gameObject.GetComponent<Image>().fillAmount = 0f;
            TimerTwo.gameObject.GetComponent<Image>().fillAmount = 1f;
        }
        else if (curTurn == RED_TURN)
        {
            turnAnim.SetInteger("AIState", -1);
            curTurn = BLUE_TURN;
            TimerOne.gameObject.GetComponent<Image>().fillAmount = 1f;
            TimerTwo.gameObject.GetComponent<Image>().fillAmount = 0f;
        }

        if(turnCount < 32)// 게임끝 전까지만
            StartTimer();
        CR_update = false;
    }

    public void BlueCalcul(int kingPoint)
    {
        if (!BlueCoord.Contains(kingPoint) && (SquareList[kingPoint].GetComponent<Image>().sprite == Resources.Load<Sprite>("UI/BattleSquareBlue")
             || SquareList[kingPoint].GetComponent<Image>().sprite == Resources.Load<Sprite>("UI/BattleSquareBlueStempKing")
             || SquareList[kingPoint].GetComponent<Image>().sprite == Resources.Load<Sprite>("UI/BattleSquareBlueStempQueen")
             || SquareList[kingPoint].GetComponent<Image>().sprite == Resources.Load<Sprite>("UI/BattleSquareBlueStempBishop")
             || SquareList[kingPoint].GetComponent<Image>().sprite == Resources.Load<Sprite>("UI/BattleSquareBlueStempRook")
             || SquareList[kingPoint].GetComponent<Image>().sprite == Resources.Load<Sprite>("UI/BattleSquareBlueStempKnight")
             || SquareList[kingPoint].GetComponent<Image>().sprite == Resources.Load<Sprite>("UI/BattleSquareBlueStempPawn")))
        {
            Debug.Log("kingPoint = " + kingPoint);
            BlueCoord.Add(kingPoint);

            if(kingPoint % 8 != 0 && kingPoint > 0)
            {
                BlueCalcul(kingPoint - 1); // 왼쪽
            }
            if (kingPoint % 8 != 7 && kingPoint < 63)
            {
                BlueCalcul(kingPoint + 1); // 오른쪽
            }
            if (kingPoint / 8 >= 1 && kingPoint >= 8)
            {
                BlueCalcul(kingPoint - 8); // 위쪽
            }
            if (kingPoint / 8 <= 7 && kingPoint <= 55)
            {
                BlueCalcul(kingPoint + 8); // 아래쪽
            }
            
        }
    }

    public void RedCalcul(int kingPoint)
    {
        if (!RedCoord.Contains(kingPoint) && (SquareList[kingPoint].GetComponent<Image>().sprite == Resources.Load<Sprite>("UI/BattleSquareRed")
             || SquareList[kingPoint].GetComponent<Image>().sprite == Resources.Load<Sprite>("UI/BattleSquareRedStempKing")
             || SquareList[kingPoint].GetComponent<Image>().sprite == Resources.Load<Sprite>("UI/BattleSquareRedStempQueen")
             || SquareList[kingPoint].GetComponent<Image>().sprite == Resources.Load<Sprite>("UI/BattleSquareRedStempBishop")
             || SquareList[kingPoint].GetComponent<Image>().sprite == Resources.Load<Sprite>("UI/BattleSquareRedStempRook")
             || SquareList[kingPoint].GetComponent<Image>().sprite == Resources.Load<Sprite>("UI/BattleSquareRedStempKnight")
             || SquareList[kingPoint].GetComponent<Image>().sprite == Resources.Load<Sprite>("UI/BattleSquareRedStempPawn")))
        {
            Debug.Log("kingPoint = " + kingPoint);
            RedCoord.Add(kingPoint);
            if (kingPoint % 8 != 0 && kingPoint > 0)
            {
                RedCalcul(kingPoint - 1); // 왼쪽
            }
            if (kingPoint % 8 != 7 && kingPoint < 63)
            {
                RedCalcul(kingPoint + 1); // 오른쪽
            }
            if (kingPoint / 8 >= 1 && kingPoint >= 8)
            {
                RedCalcul(kingPoint - 8); // 위쪽
            }
            if (kingPoint / 8 <= 7 && kingPoint <= 55)
            {
                RedCalcul(kingPoint + 8); // 아래쪽
            }
        }
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

        SquareEmpty = Resources.Load<GameObject>("Prefab/SquareEmpty");
        
    }

    IEnumerator SetTile(List<GameObject> tempList)
    {

        for (int i = 0; i < 64; i++)
        {
            tempList[i].GetComponent<Animator>().SetInteger("State", 10); // appear state
            
            yield return null;
        }

        Invoke("SquareInitialize", 1);

    }

    public void SetDynamicGrid()
    {


        for (int i = 0; i < 64; i++)
        {
            GameObject imageObject = Instantiate(SquareEmpty) as GameObject;

            imageObject.transform.SetParent(transform);
            imageObject.transform.localScale = Vector3.one;
            imageObject.transform.position = transform.position;
            imageObject.name = "child" + (i);
            //imageObject.GetComponent<Animator>().SetInteger("State", 10);
            imageObject.GetComponent<Animator>().SetInteger("State", 11); // ready to appear state

            SquareList.Add(imageObject);
            
            
            //imageObject.
        }
        
        

        TileColoring();

        StartCoroutine(SetTile(SquareList));

    }

    // Use this for initialization
    void Start () {
        
        if (SquareEmpty == null)
        {
            Debug.Log("SquareEmpty == null");
        }

        GameObject BlueKingPiece = GameObject.Find("PlayerStateBoard/PieceList").transform.Find("King0").gameObject;
        BlueKingPiece.GetComponent<Image>().raycastTarget = false;
        GameObject RedKingPiece = GameObject.Find("EnemyStateBoard/PieceList").transform.Find("King0").gameObject;
        RedKingPiece.GetComponent<Image>().raycastTarget = false;

        //SetDynamicGrid();
        //StartCoroutine("SetTile");
    }

    private void OnEnable()
    {
        Init();

        //int[] redTile = mapMaker.RedTile.ToArray();
        //int[] blueTile = mapMaker.BlueTile.ToArray();
        //int[] blockTile = mapMaker.BlockTile.ToArray();
        //int[] whiteTile = mapMaker.WhiteTile.ToArray();

        //BasicMap.Add(redTile);
        //BasicMap.Add(blueTile);
        //BasicMap.Add(blockTile);
        //BasicMap.Add(whiteTile);

        kingBluePoint = 0;
        kingRedPoint = 0;

        CR_update = false;

        BlueCoord = new List<int>();
        RedCoord = new List<int>();


        if (gameObject.transform.childCount > 0)
        {
            TileColoring();
            PieceReset();
            Invoke("SquareInitialize", 1);
        }

        Invoke("StartTimer", 2);

        SetDynamicGrid();

    }

    public void Init()
    {
        Debug.Log("Init");
        indexInformation = -1;
        CurAnimation = -1;
        turnChange = true;

        RedChange = 0;
        BlueChange = 0;
        turnCount = 0;

        Pawn = 1;
        Rook = 2;
        Knight = 3;
        Bishop = 4;
        Queen = 5;
        King = 6;
        Apearance = 10;

        PieceBlueCoord = new List<int>();
        PieceRedCoord = new List<int>();

        SquareList = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        transform.DetachChildren();
        


        SquareTile = new GameObject[8, 8];

        BasicMap = new List<int>();

        if (FirstTurn.isOn)
        {
            curTurn = BLUE_TURN;
            turnAnim.SetInteger("AIState", -1); // 1p
            TimerOne.gameObject.GetComponent<Image>().fillAmount = 1f;
            TimerTwo.gameObject.GetComponent<Image>().fillAmount = 0f;
        }
        else
        {
            turnAnim.SetInteger("AIState", 1); // 2p
            curTurn = RED_TURN;
            TimerOne.gameObject.GetComponent<Image>().fillAmount = 0f;
            TimerTwo.gameObject.GetComponent<Image>().fillAmount = 1f;
        }

        if (TurnTimeToggle.isOn)
        {
            turnTime = 15;
        }
        else
        {
            turnTime = 30;
        }
        
    }

    public void PieceReset()
    {
        for(int i = 0; i < 4; i++)
        {
            GameObject p1 = GameObject.Find("PlayerStateBoard/PieceList").transform.Find("Pawn" + i.ToString()).gameObject;
            p1.GetComponent<Image>().sprite = Resources.Load<Sprite>("ChessPiece/White" + "Pawn");
            p1.GetComponent<Image>().raycastTarget = true;

            GameObject p2 = GameObject.Find("EnemyStateBoard/PieceList").transform.Find("Pawn" + i.ToString()).gameObject;
            p2.GetComponent<Image>().sprite = Resources.Load<Sprite>("ChessPiece/Black" + "Pawn");
            p2.GetComponent<Image>().raycastTarget = true;
        }

        for (int i = 0; i < 4; i++)
        {
            GameObject p1 = GameObject.Find("PlayerStateBoard/PieceList").transform.Find("Pawn" + (i+4).ToString()).gameObject;
            p1.GetComponent<Image>().sprite = Resources.Load<Sprite>("ChessPiece/White" + "Pawn");
            p1.GetComponent<Image>().raycastTarget = true;

            GameObject p2 = GameObject.Find("EnemyStateBoard/PieceList").transform.Find("Pawn" + (i+4).ToString()).gameObject;
            p2.GetComponent<Image>().sprite = Resources.Load<Sprite>("ChessPiece/Black" + "Pawn");
            p2.GetComponent<Image>().raycastTarget = true;
        }

        for (int i = 0; i < 2; i++)
        {
            if(i == 0)
            {
                GameObject p1 = GameObject.Find("PlayerStateBoard/PieceList").transform.Find("Rook" + i.ToString()).gameObject;
                p1.GetComponent<Image>().sprite = Resources.Load<Sprite>("ChessPiece/White" + "Rook");

                GameObject p2 = GameObject.Find("PlayerStateBoard/PieceList").transform.Find("Knight" + i.ToString()).gameObject;
                p2.GetComponent<Image>().sprite = Resources.Load<Sprite>("ChessPiece/White" + "Knight");

                GameObject p3 = GameObject.Find("PlayerStateBoard/PieceList").transform.Find("Bishop" + i.ToString()).gameObject;
                p3.GetComponent<Image>().sprite = Resources.Load<Sprite>("ChessPiece/White" + "Bishop");

                GameObject p4 = GameObject.Find("PlayerStateBoard/PieceList").transform.Find("Queen" + i.ToString()).gameObject;
                p4.GetComponent<Image>().sprite = Resources.Load<Sprite>("ChessPiece/White" + "Queen");



                GameObject p5 = GameObject.Find("EnemyStateBoard/PieceList").transform.Find("Rook" + i.ToString()).gameObject;
                p5.GetComponent<Image>().sprite = Resources.Load<Sprite>("ChessPiece/Black" + "Rook");

                GameObject p6 = GameObject.Find("EnemyStateBoard/PieceList").transform.Find("Knight" + i.ToString()).gameObject;
                p6.GetComponent<Image>().sprite = Resources.Load<Sprite>("ChessPiece/Black" + "Knight");

                GameObject p7 = GameObject.Find("EnemyStateBoard/PieceList").transform.Find("Bishop" + i.ToString()).gameObject;
                p7.GetComponent<Image>().sprite = Resources.Load<Sprite>("ChessPiece/Black" + "Bishop");

                GameObject p8 = GameObject.Find("EnemyStateBoard/PieceList").transform.Find("Queen" + i.ToString()).gameObject;
                p8.GetComponent<Image>().sprite = Resources.Load<Sprite>("ChessPiece/Black" + "Queen");

                p1.GetComponent<Image>().raycastTarget = true;
                p2.GetComponent<Image>().raycastTarget = true;
                p3.GetComponent<Image>().raycastTarget = true;
                p4.GetComponent<Image>().raycastTarget = true;
                p5.GetComponent<Image>().raycastTarget = true;
                p6.GetComponent<Image>().raycastTarget = true;
                p7.GetComponent<Image>().raycastTarget = true;
                p8.GetComponent<Image>().raycastTarget = true;
            }
            else
            {
                GameObject p1 = GameObject.Find("PlayerStateBoard/PieceList").transform.Find("Rook" + i.ToString()).gameObject;
                p1.GetComponent<Image>().sprite = Resources.Load<Sprite>("ChessPiece/White" + "Rook");

                GameObject p2 = GameObject.Find("PlayerStateBoard/PieceList").transform.Find("Knight" + i.ToString()).gameObject;
                p2.GetComponent<Image>().sprite = Resources.Load<Sprite>("ChessPiece/White" + "Knight");

                GameObject p3 = GameObject.Find("PlayerStateBoard/PieceList").transform.Find("Bishop" + i.ToString()).gameObject;
                p3.GetComponent<Image>().sprite = Resources.Load<Sprite>("ChessPiece/White" + "Bishop");

                GameObject p4 = GameObject.Find("PlayerStateBoard/PieceList").transform.Find("King0").gameObject;
                p4.GetComponent<Image>().sprite = Resources.Load<Sprite>("ChessPiece/White" + "King");



                GameObject p5 = GameObject.Find("EnemyStateBoard/PieceList").transform.Find("Rook" + i.ToString()).gameObject;
                p5.GetComponent<Image>().sprite = Resources.Load<Sprite>("ChessPiece/Black" + "Rook");

                GameObject p6 = GameObject.Find("EnemyStateBoard/PieceList").transform.Find("Knight" + i.ToString()).gameObject;
                p6.GetComponent<Image>().sprite = Resources.Load<Sprite>("ChessPiece/Black" + "Knight");

                GameObject p7 = GameObject.Find("EnemyStateBoard/PieceList").transform.Find("Bishop" + i.ToString()).gameObject;
                p7.GetComponent<Image>().sprite = Resources.Load<Sprite>("ChessPiece/Black" + "Bishop");

                GameObject p8 = GameObject.Find("EnemyStateBoard/PieceList").transform.Find("King0").gameObject;
                p8.GetComponent<Image>().sprite = Resources.Load<Sprite>("ChessPiece/Black" + "King");

                p1.GetComponent<Image>().raycastTarget = true;
                p2.GetComponent<Image>().raycastTarget = true;
                p3.GetComponent<Image>().raycastTarget = true;
                p4.GetComponent<Image>().raycastTarget = false;
                p5.GetComponent<Image>().raycastTarget = true;
                p6.GetComponent<Image>().raycastTarget = true;
                p7.GetComponent<Image>().raycastTarget = true;
                p8.GetComponent<Image>().raycastTarget = false;
            }
            
            
        }
        

        
    }

    public void TileColoring()
    {
        for(int k = 0; k < mapMaker.BasicMap.Length; k++)
        {
            switch (mapMaker.BasicMap[k])
            {
                case 0:
                    SquareList[k].GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/BattleSquareRed");
                    break;
                case 1:
                    SquareList[k].GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/BattleSquareBlue");
                    break;
                case 2:
                    SquareList[k].GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/BattleSquareWhite");
                    break;
                case 3:
                    SquareList[k].GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/BattleSquareBlock");
                    break;
                case 4:
                    SquareList[k].GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/BattleSquareMulti");
                    break;
            }
            
        }
    }

    public void SetMap()
    {
        BasicMap = new List<int>();

        for (int k = 0; k < 64; k++)
        {
            BasicMap.Add(-1);
            Debug.Log("string = " + SquareList[k].GetComponent<Image>().sprite.ToString());
            switch (SquareList[k].GetComponent<Image>().sprite.name)
            {
                case "BattleSquareRed":
                    BasicMap[k] = 0;
                    break;
                case "BattleSquareBlue":
                    BasicMap[k] = 1;
                    break;
                case "BattleSquareWhite":
                    BasicMap[k] = 2;
                    break;
                case "BattleSquareBlock":
                    BasicMap[k] = 3;
                    break;
                case "BattleSquareRedStempPawn":
                    BasicMap[k] = 4;
                    break;
                case "BattleSquareRedStempKnight":
                    BasicMap[k] = 5;
                    break;
                case "BattleSquareRedStempBishop":
                    BasicMap[k] = 6;
                    break;
                case "BattleSquareRedStempRook":
                    BasicMap[k] = 7;
                    break;
                case "BattleSquareRedStempQueen":
                    BasicMap[k] = 8;
                    break;
                case "BattleSquareRedStempKing":
                    BasicMap[k] = 9;
                    break;
                case "BattleSquareBlueStempPawn":
                    BasicMap[k] = 10;
                    break;
                case "BattleSquareBlueStempKnight":
                    BasicMap[k] = 11;
                    break;
                case "BattleSquareBlueStempBishop":
                    BasicMap[k] = 12;
                    break;
                case "BattleSquareBlueStempRook":
                    BasicMap[k] = 13;
                    break;
                case "BattleSquareBlueStempQueen":
                    BasicMap[k] = 14;
                    break;
                case "BattleSquareBlueStempKing":
                    BasicMap[k] = 15;
                    break;

                default:
                    break;
            }
        }
    }

    public void AfterSetMap()
    {
        for (int k = 0; k < 64; k++)
        {
            switch (BasicMap[k])
            {
                case 0:
                    SquareList[k].GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/BattleSquareRed");
                    break;
                case 1:
                    SquareList[k].GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/BattleSquareBlue");
                    break;
                case 2:
                    SquareList[k].GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/BattleSquareWhite");
                    break;
                case 3:
                    SquareList[k].GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/BattleSquareBlock");
                    break;
                case 4:
                    SquareList[k].GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/BattleSquareRedStempPawn");
                    break;
                case 5:
                    SquareList[k].GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/BattleSquareRedStempKnight");
                    break;
                case 6:
                    SquareList[k].GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/BattleSquareRedStempBishop");
                    break;
                case 7:
                    SquareList[k].GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/BattleSquareRedStempRook");
                    break;
                case 8:
                    SquareList[k].GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/BattleSquareRedStempQueen");
                    break;
                case 9:
                    SquareList[k].GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/BattleSquareRedStempKing");
                    break;
                case 10:
                    SquareList[k].GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/BattleSquareBlueStempPawn");
                    break;
                case 11:
                    SquareList[k].GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/BattleSquareBlueStempKnight");
                    break;
                case 12:
                    SquareList[k].GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/BattleSquareBlueStempBishop");
                    break;
                case 13:
                    SquareList[k].GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/BattleSquareBlueStempRook");
                    break;
                case 14:
                    SquareList[k].GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/BattleSquareBlueStempQueen");
                    break;
                case 15:
                    SquareList[k].GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/BattleSquareBlueStempKing");
                    break;

                default:
                    break;
            }
        }
    }

    public void UroborosOutMap() // 시계방향 최외각
    {
        //0123456 -> right
        //8 배수 -> up
        //8 배수 - 1 -> down
        //57 58 59 60 61 62 63 -> left

        SetMap();

        List<int> tempList = new List<int>();
        tempList.AddRange(BasicMap);

        for (int i = 0; i < 64; i++)
        {
            if(i >= 0 && i < 7) // right
            {
                BasicMap[i + 1] = tempList[i];
            }
            else if(i % 8 == 0 && i != 0) // up
            {
                BasicMap[i - 8] = tempList[i];
            }
            else if(i > 56 && i <= 63) // left
            {
                BasicMap[i - 1] = tempList[i];
            }
            else if (i % 8 == 7 && i != 63) // down
            {
                BasicMap[i + 8] = tempList[i];
            }
        }

        AfterSetMap();

    }

    public void UroborosInMap() // 반시계방향
    {
        //9 10 11 12 13 14
        //17            22
        //25            30


        //49 50 51 52 53 54

        SetMap();

        List<int> tempList = new List<int>();
        tempList.AddRange(BasicMap);

        for (int i = 0; i < 64; i++)
        {
            if (i >= 49 && i < 54) // right
            {
                BasicMap[i + 1] = tempList[i];
            }
            else if (i % 8 == 6 && i != 6 && i != 14 && i != 62) // up
            {
                BasicMap[i - 8] = tempList[i];
            }
            else if (i > 9 && i <= 14) // left
            {
                BasicMap[i - 1] = tempList[i];
            }
            else if (i % 8 == 1 && i != 49 && i != 57 && i != 1) // down
            {
                BasicMap[i + 8] = tempList[i];
            }
        }

        AfterSetMap();

    }
}
