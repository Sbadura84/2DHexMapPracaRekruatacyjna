using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileInteraction : MonoBehaviour
{
    [SerializeField] private Tilemap[] map;
    [SerializeField] private Tile tileGreen;
    [SerializeField] private Tile tileYellow;
    [SerializeField]  private Tilemap indicatorMap;
    [SerializeField]  private Tile indicator;
    private Vector3Int gridPositionMem;
    private Vector3Int gridPosition;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
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
                    
                    Debug.Log(gridPositionMem);
                    
                    //uruchomi? UI pokazuj?ce tile data.
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
}
