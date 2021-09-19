using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BoardManager : MonoBehaviour {
    
    private GridLayoutGroup grid;
    public Transform parent;
    private GameObject ImagePrefab;

    private void Awake()
    {
        grid = gameObject.GetComponent<GridLayoutGroup>();
    }

    public void SetDynamicGrid()
    {
        grid.cellSize = new Vector2(75, 75);

        grid.spacing = new Vector2(10, 10);
        grid.startCorner = GridLayoutGroup.Corner.UpperLeft;
        grid.startAxis = GridLayoutGroup.Axis.Horizontal;
        grid.constraint = GridLayoutGroup.Constraint.Flexible;

        for(int i = 0; i < 64; i++)
        {
            GameObject imageObject = Instantiate(ImagePrefab);
           
            imageObject.transform.parent = transform;
            imageObject.transform.localScale = Vector3.one;
            imageObject.transform.position = transform.position;
        }
        
        



    }

    // Use this for initialization
    void Start () {

        ImagePrefab = Resources.Load<GameObject>("Prefab/ImagePrefab");
        if(ImagePrefab == null)
        {
            Debug.Log("ImagePrefab == null");
        }

        SetDynamicGrid();
	}
}
