using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapMaker : MonoBehaviour {

    public GameObject Map;

    public string[] MapName;
    public int MapCount = 0;

    public List<int> BlueTile;
    public List<int> RedTile;
    public List<int> BlockTile;

	// Use this for initialization
	void Start () {
        MapName = new string[] { "우로보로스", "한국", "우주전쟁" };
        MapInitialize();
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
        gameObject.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Map" + MapCount);
        MapInitialize();
    }

    private void MapInitialize()
    {
        string mapName = Map.GetComponent<Text>().text;
        BlueTile = new List<int>();
        RedTile = new List<int>();
        BlockTile = new List<int>();
        switch (mapName)
        {
            case "우로보로스":
                for (int i = 0; i < 32; i++)
                {
                    BlueTile.Add(i + 32);
                    RedTile.Add(i);
                }
                break;
            case "한국":
                int[] tiles = new int[8] { 1, 6, 8, 15,
                                        48, 55, 57, 62 };
                BlockTile.AddRange(tiles);
                for (int i = 0; i < 24; i++)
                {
                    if (!BlockTile.Contains(i))
                    {
                        RedTile.Add(i);
                    }
                    if (!BlockTile.Contains(i + 40))
                    {
                        BlueTile.Add(i + 40);
                    }

                }
                int[] rTiles = new int[] { 24, 25, 26, 27, 28, 31, 33, 34 };
                int[] bTiles = new int[] { 29, 30, 32, 35, 36, 37, 38, 39 };
                RedTile.AddRange(rTiles);
                BlueTile.AddRange(bTiles);
                break;
        }
    }


}
