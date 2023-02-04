using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float horizontalSum;
    private float verticalSum;
    private float horizontalSpeed;
    private float verticalSpeed;

    [SerializeField] private Camera cam;
    private Vector3 originPoint;
    [SerializeField] private float mouseSensitivity = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            originPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            horizontalSum = 0;
            verticalSum = 0;

        } else if (Input.GetMouseButton(1)){
            Cursor.lockState = CursorLockMode.Locked;
            horizontalInput = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            verticalInput = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
            horizontalSum = horizontalSum + horizontalInput;
            verticalSum = verticalSum + verticalInput;
            cam.transform.position += new Vector3(horizontalSum, verticalSum, 0);
            
        }
        else if (Input.GetMouseButtonUp(1))
        {
            Debug.Log("release");
            Cursor.lockState = CursorLockMode.Confined;
        }
        

        /*
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
        */
    }
}
