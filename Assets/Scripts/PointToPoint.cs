using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;


public class PointToPoint : MonoBehaviour
{
    [SerializeField] private TileInteraction tileInteraction;
    [SerializeField] private Tile pathFindTileColor;


    private Vector3Int startTile;
    private Vector3Int endTile;

    private GameObject startPos;
    private GameObject endPos;
    [SerializeField] private LineRenderer lineRenderer;

    private List<Vector3> wayPoints;
    private void Start()
    {
        startPos = this.gameObject.transform.GetChild(0).gameObject;
        endPos = this.gameObject.transform.GetChild(1).gameObject;

    }
    private void Update()
    {
        if(tileInteraction.clickedTileMem == tileInteraction.clickedTile && tileInteraction.clickedTile == pathFindTileColor)
        {
            startTile = tileInteraction.gridPosition;
            endTile = tileInteraction.gridPositionMem;
            startPos.transform.position = startTile;
            endPos.transform.position = endTile;
            startPos.SetActive(true);
            endPos.SetActive(true);

        }
        else
        {
            startPos.SetActive(false);
            endPos.SetActive(false);
        }


    }
}
