using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Painter : MonoBehaviour
{
    public Tile tileGreen;
    public Tile tileBlue;
    public Tile tileYellow;
    public Tile tileGray;
    public Tilemap tilemap;
    public Tilemap tilemapUnwalkable;
    public Vector3Int position;


    [SerializeField] private int hight;
    [SerializeField] private int width;

    [ContextMenu("Paint")]
   

    void Paint()
    {
        
        for(int i=0; i<hight; i++)
        {
            for (int j=0; j<width; j++)
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
        
        //tilemap.SetTile(position, tile);
    }
    [ContextMenu("DePaint")]
    void DePaint()
    {
        for (int i = 0; i < hight; i++)
        {
            for (int j = 0; j < width; j++)
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
