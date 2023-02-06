using NavMeshPlus.Components;
using System.Collections;
using UnityEditor;
using UnityEngine;


public class AreaFloorBaker : MonoBehaviour
{
    [SerializeField] private NavMeshSurface surface;
    [SerializeField] private Painter painter;

    void Start()
    {
        StartCoroutine(LateStart(2));
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if ((painter.width < 300 && painter.hight < 300) 
            && painter.autoPaint == false)
        {
            EditorUtility.DisplayDialog("Line renderer", "Baking AI mesh, might take a while", "ok");
            BuildMesh();
        }
    }
    public void BuildMesh()
    {
        
        surface.BuildNavMesh();

    }
}

