using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float horizontalSpeed;
    private float verticalSpeed;

    [SerializeField] private Camera cam;
    private Vector3 originPoint;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            originPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(originPoint);
        }
        if (Input.GetMouseButton(1))
        {
            float differenceY = originPoint.y - cam.ScreenToWorldPoint(Input.mousePosition).y;
            float differenceX = originPoint.x - cam.ScreenToWorldPoint(Input.mousePosition).x;
        cam.transform.position += new Vector3(differenceX,differenceY,0);
        }

    }
}
