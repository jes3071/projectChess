using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapMaker : MonoBehaviour {

    public GameObject Map;

    public string[] MapName;
    public int MapCount = 0;

    // red = 0, blue = 1, white = 2, block = 3, boom = 4
    public int[] BasicMap;
    public List<int> RedTile;
    public List<int> BlueTile;


    private void Awake()
    {
        MapName = new string[] { "기본", "한국", "우로보로스", "투혼", "구역", "침입", "우주전쟁" };
    }

    private void OnEnable()
    {
        
        MapInitialize();
    }

    // Use this for initialization
    void Start () {
        Map.GetComponent<Text>().text = "기본";

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LeftMapButton()
    {
        if(Map.GetComponent<Text>().text == MapName[0])
        {
            Map.GetComponent<Text>().text = MapName[MapCount = MapName.Length - 1];
        }
        else
        {
            Map.GetComponent<Text>().text = MapName[--MapCount];
        }

        AudioManager.instance.SelectSound();
        gameObject.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Map" + MapCount);
        MapInitialize();
    }

    public void RightMapButton()
    {
        if (Map.GetComponent<Text>().text == MapName[MapName.Length - 1])
        {
            Map.GetComponent<Text>().text = MapName[MapCount = 0];
        }
        else
        {
            Map.GetComponent<Text>().text = MapName[++MapCount];
        }

        AudioManager.instance.SelectSound();
        gameObject.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Map" + MapCount);
        MapInitialize();
    }

    private void MapInitialize()
    {
        string mapName = Map.GetComponent<Text>().text;
        RedTile = new List<int>();
        BlueTile = new List<int>();

        switch (mapName)
        {
            case "기본":

                BasicMap = new int[]{
                    0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,
                    2,2,2,2,2,2,2,2,
                    2,2,2,2,2,2,2,2,
                    2,2,2,2,2,2,2,2,
                    2,2,2,2,2,2,2,2,
                    1,1,1,1,1,1,1,1,
                    1,1,1,1,1,1,1,1};
                break;
            case "한국":

                BasicMap = new int[]{
                    0,3,0,0,0,0,3,0,
                    3,0,0,0,0,0,0,3,
                    0,0,0,0,0,0,0,0,
                    0,0,0,0,0,1,1,0,
                    1,0,0,1,1,1,1,1,
                    1,1,1,1,1,1,1,1,
                    3,1,1,1,1,1,1,3,
                    1,3,1,1,1,1,3,1};
                break;
            case "우로보로스":

                BasicMap = new int[]{
                    0,0,2,2,2,2,2,2,
                    0,2,2,2,2,2,2,2,
                    2,2,3,3,3,3,2,2,
                    2,2,3,3,3,3,2,2,
                    2,2,3,3,3,3,2,2,
                    2,2,3,3,3,3,2,2,
                    2,2,2,2,2,2,2,1,
                    2,2,2,2,2,2,1,1};
                break;
            case "투혼":

                BasicMap = new int[]{
                    0,0,0,3,2,2,2,4,
                    0,0,3,2,2,2,2,2,
                    0,2,2,2,2,2,2,2,
                    3,2,2,2,2,2,3,2,
                    2,3,2,2,2,2,2,3,
                    2,2,2,2,2,2,2,1,
                    2,2,2,2,2,3,1,1,
                    4,2,2,2,3,1,1,1};
                break;
            case "구역":
                BasicMap = new int[]{
                    3,3,0,2,2,2,3,3,
                    3,0,2,2,2,2,2,3,
                    0,2,2,2,2,2,2,2,
                    2,2,2,2,2,2,2,2,
                    2,2,2,2,2,2,2,2,
                    2,2,2,2,2,2,2,1,
                    3,2,2,2,2,2,1,3,
                    3,3,2,2,2,1,3,3};
                break;
            case "침입":
                BasicMap = new int[]{
                    2,2,2,0,3,2,2,2,
                    2,2,2,0,3,2,2,2,
                    2,2,2,2,2,2,2,2,
                    3,3,2,2,2,2,1,1,
                    0,0,2,2,2,2,3,3,
                    2,2,2,2,2,2,2,2,
                    2,2,2,3,1,2,2,2,
                    2,2,2,3,1,2,2,2};
                break;
            case "우주전쟁":
                BasicMap = new int[]{
                0,0,2,2,2,2,2,2,
                0,2,2,2,2,2,3,2,
                2,2,2,2,2,3,2,2,
                2,2,2,2,3,2,2,2,
                2,2,2,3,2,2,2,2,
                2,2,3,2,2,2,2,2,
                2,3,2,2,2,2,2,1,
                2,2,2,2,2,2,1,1};
                break;
        }
        SetTiles();
    }

    public void SetTiles()
    {
        for (int i = 0; i < 64; i++)
        {
            if (BasicMap[i] == 0)
            {
                RedTile.Add(i);
            }
            else if (BasicMap[i] == 1)
            {
                BlueTile.Add(i);
            }
        }
    }

    public void ToggleButton()
    {
        AudioManager.instance.SelectSound();
    }
}
