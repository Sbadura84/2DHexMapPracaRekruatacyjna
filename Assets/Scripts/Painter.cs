using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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


    [Header("Grid specifications, specify width in one direction if autoPaint)")]
    [SerializeField] private Vector3Int position;
    [SerializeField] public int hight;
    [SerializeField] public int width;

    [Header("Program settings")]
    [SerializeField] public bool autoPaint;
    [SerializeField] private bool paintAtBeginning;
    [SerializeField] public bool pathing;

    [SerializeField] GameObject user;

    private Vector3Int anchorPoint;

    [SerializeField] private Tilemap[] map;
    private Vector3Int userTilePosition;

    private bool autoPaintStart;
    private int maxHight = 1000;
    private int maxWidth = 1000;
    private int boundaryX;
    private int boundaryY;

    private void Start()
    {
        autoPaintStart = autoPaint;
        if (autoPaint == true)
        {
            position = new Vector3Int(0, 0, 0);
            Paint();
            anchorPoint = tilemap.WorldToCell(new Vector3(user.transform.position.x, user.transform.position.y, 0));
        }
        else
        {
            if(paintAtBeginning == true) Paint();
        }
    }

    private void Update()
    {
        if(autoPaint != autoPaintStart)
        {
            EditorUtility.DisplayDialog("AutoPaint change","Can't change in runtime","ok");
            autoPaint = autoPaintStart;
        }

        if (autoPaint)
        {
            hight = 25;
            width = 25;
            userTilePosition = tilemap.WorldToCell(new Vector3(user.transform.position.x, user.transform.position.y, 0));
            if (Mathf.Abs(anchorPoint.x - userTilePosition.x) > 0 || Mathf.Abs(anchorPoint.y - userTilePosition.y) > 0)
            {
                UpdatePaint();
            }
        }
        
        
    }

    private void UpdatePaint()
    {
       int signX = anchorPoint.x - userTilePosition.x;
       int signY = anchorPoint.y - userTilePosition.y;
        if (Mathf.Sign(signX) == -1)
        {
            boundaryX = anchorPoint.x + (width / 2) + 1;
        }
        else
        {
            boundaryX = anchorPoint.x - (width / 2) - 1;
        }
        if (Mathf.Sign(signY) == -1)
        {
            boundaryY = anchorPoint.y + (hight / 2) + 1;
        }
        else
        {
            boundaryY = anchorPoint.y - (hight / 2) - 1;
        }

        

        for (int i = anchorPoint.y - (hight / 2) -1; i < anchorPoint.y + (hight / 2) + 1; i++)
        {
            for(int j = -1; j<1; j++)
            {
                if (tilemap.HasTile(new Vector3Int(boundaryX+j, i, 0)) == true || tilemapUnwalkable.HasTile(new Vector3Int(boundaryX + j, i, 0)) == true)
                {
                    continue;
                }


                Tile generatedTile = GenerateTile();
                if (generatedTile == tileBlue || generatedTile == tileYellow)
                {
                    tilemap.SetTile(new Vector3Int(boundaryX + j, i, 0), generatedTile);
                }
                else
                {
                    tilemapUnwalkable.SetTile(new Vector3Int(boundaryX + j, i, 0), generatedTile);
                }
            }
        }
        for (int i = anchorPoint.x - (width / 2) - 1; i < anchorPoint.x + (width / 2) + 1; i++)
        {
            for (int j = -1; j < 1; j++)
            {

                if (tilemap.HasTile(new Vector3Int(i, boundaryY+j, 0)) == true || tilemapUnwalkable.HasTile(new Vector3Int(i, boundaryY + j, 0)) == true)
                {
                    continue;
                }

                Tile generatedTile = GenerateTile();

                if (generatedTile == tileBlue || generatedTile == tileYellow)
                {
                    tilemap.SetTile(new Vector3Int(i, boundaryY + j, 0), generatedTile);
                }
                else
                {
                    tilemapUnwalkable.SetTile(new Vector3Int(i, boundaryY + j, 0), generatedTile);
                }
            }
        }
        anchorPoint = userTilePosition;
    }


    [ContextMenu("Paint")]
    void Paint()
    {
        DePaint();
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < hight; j++)
            {
                Tile generatedTile = GenerateTile();
                if (generatedTile == tileBlue || generatedTile == tileYellow)
                {
                    tilemap.SetTile(position + new Vector3Int(i, j, 0), generatedTile);
                }
                else
                {
                    tilemapUnwalkable.SetTile(position + new Vector3Int(i, j, 0), generatedTile);
                }
            }
        }

        if (autoPaint){
            for (int i = -1; i < maxWidth + 1; i++)
            {
                tilemapUnwalkable.SetTile(position + new Vector3Int(i, - 1, 0), tileGray);
                tilemapUnwalkable.SetTile(position + new Vector3Int(i, maxHight, 0), tileGray);
            }
            for (int i = - 1; i < maxHight + 1; i++)
            {
                tilemapUnwalkable.SetTile(position + new Vector3Int( - 1, i, 0), tileGray);
                tilemapUnwalkable.SetTile(position + new Vector3Int(maxWidth, i, 0), tileGray);
            }
        }
        else
        {
            for (int i = position.x - 1; i < width + 1; i++)
            {
                tilemapUnwalkable.SetTile(position + new Vector3Int(i, position.y - 1, 0), tileGray);
                tilemapUnwalkable.SetTile(position + new Vector3Int(i, position.y + hight, 0), tileGray);
            }
            for (int i = position.y - 1; i < hight + 1; i++)
            {
                tilemapUnwalkable.SetTile(position + new Vector3Int(position.x - 1, i, 0), tileGray);
                tilemapUnwalkable.SetTile(position + new Vector3Int(position.x + width, i, 0), tileGray);
            }
        }
        
        //tilemap.SetTile(position, tile);
    }
    [ContextMenu("DePaint")]
    void DePaint()
    {
        //Clears grid with parameters set by user
        for (int i = position.x - 1; i < width + 1; i++)
        {
            for (int j = position.y - 1; j < hight + 1; j++)
            {
                tilemap.SetTile(position + new Vector3Int(i, j, 0), null);
                tilemapUnwalkable.SetTile(position + new Vector3Int(i, j, 0), null);
            }
        }
        //Clears grid from (-1,-1) to (1001,1001)
        for (int i = - 1; i < maxWidth + 1; i++)
        {
            for (int j = 1; j < maxHight + 1; j++)
            {
                tilemap.SetTile(new Vector3Int(i, j, 0), null);
                tilemapUnwalkable.SetTile(new Vector3Int(i, j, 0), null);
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
        else if (r > 12 && r <= 14)
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