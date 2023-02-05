using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Painter : MonoBehaviour
{
    [Header("Tiles and Tilemaps setup")]
    [SerializeField] private Tile tileGreen;
    [SerializeField] private Tile tileBlue;
    [SerializeField] private Tile tileYellow;
    [SerializeField] private Tile tileGray;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tilemap tilemapUnwalkable;


    [Header("Grid specifications")]
    [SerializeField] private Vector3Int position;
    [SerializeField] private int hight;
    [SerializeField] private int width;



    private void Start()
    {
        position = new Vector3Int(0, 0, 0);
        hight = 1000;
        width = 1000;
    }
    [ContextMenu("Paint")]
    void Paint()
    {
        DePaint();
        for (int i=0; i< width; i++)
        {
            for (int j=0; j< hight; j++)
            {
                Tile generatedTile = GenerateTile();
                if(generatedTile == tileBlue || generatedTile == tileYellow)
                {
                    tilemap.SetTile(position + new Vector3Int(i, j, 0),generatedTile);
                }
                else
                {
                    tilemapUnwalkable.SetTile(position + new Vector3Int(i, j, 0), generatedTile);
                }
            }
        }
        
        for (int i=position.x-1; i<width+1; i++)
        {
            tilemapUnwalkable.SetTile(position + new Vector3Int(i, position.y-1, 0), tileGray);
            tilemapUnwalkable.SetTile(position + new Vector3Int(i, position.y + hight, 0), tileGray);
        }
        for (int i = position.y - 1; i < hight + 1; i++)
        {
            tilemapUnwalkable.SetTile(position + new Vector3Int(position.x - 1, i , 0), tileGray);
            tilemapUnwalkable.SetTile(position + new Vector3Int(position.x + width, i, 0), tileGray);
        }
        //tilemap.SetTile(position, tile);
    }
    [ContextMenu("DePaint")]
    void DePaint()
    {
        for (int i = position.x - 1; i < width + 1; i++)
        {
            for (int j = position.y - 1; j < hight + 1; j++)
            {
                tilemap.SetTile(position + new Vector3Int(i, j, 0), null);
                tilemapUnwalkable.SetTile(position + new Vector3Int(i, j, 0), null);
            }
        }

    }

    private Tile GenerateTile()
    {
        int r = Random.Range(0, 20);
        if (r <= 12)
        {
            return tileBlue;
        }
        else if (r > 12 && r <=14)
        {
            return tileGreen;
        }
        else if (r > 14 && r <= 15)
        {
            return tileYellow;
        }
        else
        {
            return tileGray;
        }

    }
}
