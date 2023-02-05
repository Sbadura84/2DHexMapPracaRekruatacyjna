using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;


public class PointToPoint : MonoBehaviour
{
    [Header("Logic setup")]
    [SerializeField] private TileInteraction tileInteraction;
    [SerializeField] private TileBase pathFindTileColor;
    [SerializeField] private Grid grid;
    [SerializeField] NavMeshAgent agent;

    private GameObject startPos;
    private GameObject endPos;

    [Header("Visual setup")]
    [SerializeField] private LineRenderer lineRenderer;

    private TileBase clickedTile;
    private TileBase clickedTileMem;


    private void Start()
    {
        //Getting data of NavMeshAgent
        startPos = gameObject.transform.GetChild(0).gameObject;
        endPos = gameObject.transform.GetChild(1).gameObject;

        //Removing initial line
        lineRenderer.positionCount = 0;
    }
    private void Update()
    {

        //Gating successive line rendering
        if (Input.GetMouseButtonDown(0) && lineRenderer.positionCount == 0)
        {
            clickedTile = tileInteraction.clickedTile;
            clickedTileMem = tileInteraction.clickedTileMem;
            Debug.Log("Clicked tile = " + clickedTile + "\n ClickedTileMem = " + clickedTileMem);
        }
        else if(Input.GetMouseButtonDown(0) && lineRenderer.positionCount > 0)
        {
            clickedTile = null;
            ClearPath();
            Debug.Log("Clicked tile = " + clickedTile + "\n ClickedTileMem = " + clickedTileMem);
        }

        //Gating line drawing untill two tiles of same type are chosen
        if (clickedTile == clickedTileMem 
            && clickedTile == pathFindTileColor 
            && tileInteraction.gridPosition != tileInteraction.gridPositionMem)
        {
            //set start and end point of NavMeshAgent
            startPos.transform.position = grid.CellToWorld(tileInteraction.gridPosition);
            endPos.transform.position = grid.CellToWorld(tileInteraction.gridPositionMem);

            startPos.SetActive(true);
            endPos.SetActive(true);

            agent.SetDestination(endPos.transform.position);

            //line rendereing
            DrawPath();
        }
        else
        {
            ClearPath();
            startPos.SetActive(false);
            endPos.SetActive(false);
        }


    }


    void DrawPath()
    {
        lineRenderer.positionCount = agent.path.corners.Length;
        lineRenderer.SetPosition(0, startPos.transform.position);

        if (agent.path.corners.Length < 2)
        {
            return;
        }

        for (int i = 0; i < agent.path.corners.Length; i++)
        {
            Vector3 pointPosition = new Vector3(agent.path.corners[i].x, agent.path.corners[i].y, 0.1f);
            lineRenderer.SetPosition(i, pointPosition);
        }
    }
    void ClearPath()
    {
        lineRenderer.positionCount = 0;
    }
}
