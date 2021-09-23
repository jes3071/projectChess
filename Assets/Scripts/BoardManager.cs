using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BoardManager : MonoBehaviour {
    
    private GridLayoutGroup grid;
    private GameObject SquareEmpty;
    private GameObject SquareBlue;
    private GameObject SquareRed;

    public GameObject CurPiece;

    private List<GameObject> SquareList = new List<GameObject>();

    private List<int[]> BasicMap = new List<int[]>();
    private int[] toHorn = new int[] {0,1,8 };


    private void Update()
    {
        
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
            imageObject.name = "child" + (i+1);

            SquareList.Add(imageObject);

            //imageObject.
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
