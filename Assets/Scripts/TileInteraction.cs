using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;


public class TileInteraction : MonoBehaviour
{
    [SerializeField] private Tilemap[] map;
    [SerializeField] private Tile tileGreen;
    [SerializeField] private Tile tileYellow;
    [SerializeField] private Tilemap indicatorMap;
    [SerializeField] private Tile indicator;
    private Vector3Int gridPositionMem;
    private Vector3Int gridPosition;


    [SerializeField] private GameObject tileDetailsUI;

    [SerializeField] private TextMeshProUGUI tileTypeUI;
    [SerializeField] private TextMeshProUGUI tilePositionUI;
    [SerializeField] private TextMeshProUGUI tilePathableUI;

    [SerializeField] private List<TileData> tileDatas;
    private Dictionary<TileBase,TileData>dataFromTiles;
    



    private void Awake()
    {
        dataFromTiles = new Dictionary<TileBase, TileData>();

        foreach (var tileData in tileDatas)
        {
            foreach (var tile in tileData.tiles)
            {
                dataFromTiles.Add(tile,tileData);
            }
        }
        /*
        foreach (var kvp in dataFromTiles)
        {
            Debug.Log(("Key = {0}, Value = {1}", kvp.Key, kvp.Value));
        }
        */
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CloseUI();
            foreach (Tilemap tilemap in map) 
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                gridPosition = tilemap.WorldToCell(mousePosition);

                TileBase clickedTile = tilemap.GetTile(gridPosition);

                if (clickedTile != null && (clickedTile == tileGreen || clickedTile == tileYellow))
                {
                    indicatorMap.SetTile(gridPositionMem, null);
                    Debug.Log("At position " + gridPosition + " there is a " + clickedTile+"\n");

                    indicatorMap.SetTile(gridPosition, indicator);
                    

                    //uruchomi? UI pokazuj?ce tile data.
                    TurnOnUI(clickedTile, gridPosition);

                    //imo: pozycja, rodzaj, pathable
                }
                else
                {
                    indicatorMap.SetTile(gridPositionMem, null);
                    
                }
            }
            gridPositionMem = gridPosition;
        }
    }

    private void CloseUI()
    {
        tileDetailsUI.SetActive(false);
    }

    private void TurnOnUI(TileBase clickedTile, Vector3Int gridPosition)
    {
        tileDetailsUI.SetActive(true);
        tileTypeUI.SetText("Type of tile: " + clickedTile.name);
        tilePositionUI.SetText("x=" + gridPosition.x + " y=" + gridPosition.y);
        tilePathableUI.SetText("Is pathable?: " + dataFromTiles[clickedTile].walkable.ToString());
    }
}
