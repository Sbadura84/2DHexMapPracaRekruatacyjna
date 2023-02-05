using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NavMeshBuilder = UnityEngine.AI.NavMeshBuilder;

public class AreaFloorBaker : MonoBehaviour
{
    [SerializeField] private NavMeshSurface surface;
    [SerializeField] private GameObject player;
    [SerializeField] private float updateRate = 0.1f;
    [SerializeField] private float movementThreshold = 3;
    [SerializeField] private Vector3 navMeshSize = new Vector3(20, 20, 1);

    private Vector3 worldAnchor;
    private NavMeshData navMeshData;
    private List<NavMeshBuildSource> buildSources = new List<NavMeshBuildSource>();



    private void Start()
    {
        navMeshData = new NavMeshData();
        NavMesh.AddNavMeshData(navMeshData);
        BuildNavMesh(false);
        StartCoroutine(CheckPlayerMovement());
    }

    private IEnumerator CheckPlayerMovement()
    {
        WaitForSeconds Wait = new WaitForSeconds(updateRate);
        while (true)
        {
            if(Vector3.Distance(worldAnchor,player.transform.position) > movementThreshold)
            {
                BuildNavMesh(true);
                worldAnchor = player.transform.position;
            }
            yield return Wait;
        }
    }


    private void BuildNavMesh(bool Async)
    {
        Debug.Log("Rebuilding NavMesh");
        Bounds navMeshBounds = new Bounds(player.transform.position,navMeshSize);
        List<NavMeshBuildMarkup> markups = new List<NavMeshBuildMarkup>();

        List<NavMeshModifier> modifiers;

        if(surface.collectObjects == CollectObjects.Children)
        {
            modifiers = new List<NavMeshModifier>(surface.GetComponentsInChildren<NavMeshModifier>());
        }
        else
        {
            modifiers = NavMeshModifier.activeModifiers;
        }
        for (int i = 0; i<modifiers.Count; i++)
        {
            if(((surface.layerMask & (1<< modifiers[i].gameObject.layer))==1) 
                && modifiers[i].AffectsAgentType(surface.agentTypeID))
            {
                markups.Add(new NavMeshBuildMarkup()
                {
                    root = modifiers[i].transform,
                    overrideArea = modifiers[i].overrideArea,
                    area = modifiers[i].area,
                    ignoreFromBuild = modifiers[i].ignoreFromBuild
                });
            }
        }
        if(surface.collectObjects == CollectObjects.Children)
        {
            NavMeshBuilder.CollectSources(surface.transform, surface.layerMask, surface.useGeometry, surface.defaultArea, markups, buildSources);
        }
        else
        {
            NavMeshBuilder.CollectSources(navMeshBounds, surface.layerMask, surface.useGeometry, surface.defaultArea, markups, buildSources);
        }

        if (Async)
        {
            NavMeshBuilder.UpdateNavMeshDataAsync(navMeshData, surface.GetBuildSettings(), buildSources, new Bounds(player.transform.position,navMeshSize));
        }
        else
        {
            NavMeshBuilder.UpdateNavMeshData(navMeshData, surface.GetBuildSettings(), buildSources, new Bounds(player.transform.position, navMeshSize));
        }
    }
}
