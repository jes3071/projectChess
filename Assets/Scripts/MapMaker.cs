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
    public List<int> WhiteTile;

    private void Awake()
    {
        MapName = new string[] { "기본", "한국", "우로보로스", "투혼", "구역", "침입", "우주전쟁" };
    }

    private void OnEnable()
    {
        Map.GetComponent<Text>().text = "기본";
        MapInitialize();
    }

    // Use this for initialization
    void Start () {
        
        
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
        BlueTile = new List<int>();
        RedTile = new List<int>();
        BlockTile = new List<int>();
        WhiteTile = new List<int>();
        switch (mapName)
        {
            case "기본":
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
            case "우로보로스":
                RedTile.AddRange(new int[] {0,1,8});
                BlueTile.AddRange(new int[] {55, 62, 63});
                for(int i = 0; i < 4; i++)
                {
                    BlockTile.AddRange(new int[] { 18 + i*8, 19 + i*8, 20 + i*8, 21 + i*8 });
                }
                for(int i = 0; i < 64; i++)
                {
                    if(!RedTile.Contains(i) && !BlueTile.Contains(i) && !BlockTile.Contains(i))
                    {
                        WhiteTile.Add(i);
                    }
                }
                
                break;
        }
    }

    public void ToggleButton()
    {
        AudioManager.instance.SelectSound();
    }
}
