using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;


public class TileInteraction : MonoBehaviour
{
    [Header("Tilemaps and tiles to interact")]
    [SerializeField] private Tilemap[] map;
    [SerializeField] private Tilemap indicatorMap;
    [SerializeField] private Tile tileGreen;
    [SerializeField] private Tile tileYellow;
    [SerializeField] private Tile indicator;
    [SerializeField] private List<TileData> tileDatas;

    [HideInInspector] public Vector3Int gridPositionMem;
    [HideInInspector] public Vector3Int gridPosition;

    [Header("UI refrences")]
    [SerializeField] private GameObject tileDetailsUI;
    [SerializeField] private TextMeshProUGUI tileTypeUI;
    [SerializeField] private TextMeshProUGUI tilePositionUI;
    [SerializeField] private TextMeshProUGUI tilePathableUI;

    
    private Dictionary<TileBase,TileData>dataFromTiles;

    [HideInInspector] public TileBase clickedTile;
    [HideInInspector] public TileBase clickedTileMem;



    private void Awake()
    {
        //get tile data from scriptableObjects
        dataFromTiles = new Dictionary<TileBase, TileData>();

        foreach (var tileData in tileDatas)
        {
            foreach (var tile in tileData.tiles)
            {
                dataFromTiles.Add(tile,tileData);
            }
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CloseUI();
            //saving last position and tile data
            clickedTileMem = clickedTile;
            gridPositionMem = gridPosition;

            foreach (Tilemap tilemap in map) 
            {
                Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                gridPosition = tilemap.WorldToCell(worldPosition);

                clickedTile = tilemap.GetTile(gridPosition);

                //logic to prevent gray and blue tiles from inspecting
                if (clickedTile != null && (clickedTile == tileGreen || clickedTile == tileYellow))
                {
                    indicatorMap.SetTile(gridPositionMem, null);
                    Debug.Log("At position " + gridPosition + " there is a " + clickedTile+"\n");

                    indicatorMap.SetTile(gridPosition, indicator);
                    
                    TurnOnUI(clickedTile, gridPosition);
                    break;
                }
                else
                {
                    indicatorMap.SetTile(gridPositionMem, null);
                    
                }
            }
        }
    }

    public void CloseUI()
    {
        tileDetailsUI.SetActive(false);
    }

    private void TurnOnUI(TileBase clickedTile, Vector3Int gridPosition)
    {
        //UI setup
        tileDetailsUI.SetActive(true);
        tileTypeUI.SetText("Type of tile: " + clickedTile.name);
        tilePositionUI.SetText("x=" + gridPosition.x + " y=" + gridPosition.y);
        tilePathableUI.SetText("Is pathable?: " + dataFromTiles[clickedTile].walkable.ToString());
    }
}
