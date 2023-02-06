using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float horizontalSum;
    private float verticalSum;

    [SerializeField] private Camera cam;
    [SerializeField] private float mouseSensitivity = 1;
    private float mouseSensitivityAutoMode = 1;
    [SerializeField] Painter painter;
    private Vector3 originPoint;
    // Start is called before the first frame update
    void Start()
    {
        mouseSensitivityAutoMode = mouseSensitivity / 2;
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
            if (painter.autoPaint)
            {
                horizontalInput = Input.GetAxis("Mouse X") * mouseSensitivityAutoMode * Time.deltaTime;
                verticalInput = Input.GetAxis("Mouse Y") * mouseSensitivityAutoMode * Time.deltaTime;
                horizontalSum = horizontalSum + horizontalInput;
                verticalSum = verticalSum + verticalInput;

                //speed limit
                if (horizontalSum > 0.5) horizontalSum = 0.5f;
                if (horizontalSum < -0.5) horizontalSum = -0.5f;
                if (verticalSum > 0.5) verticalSum = 0.5f;
                if (verticalSum < -0.5) verticalSum = -0.5f;
            }
                
            else
            {
                horizontalInput = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
                verticalInput = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
                horizontalSum = horizontalSum + horizontalInput;
                verticalSum = verticalSum + verticalInput;
            }

            cam.transform.position += new Vector3(horizontalSum, verticalSum, 0);
            
        }
        else if (Input.GetMouseButtonUp(1))
        {
            Debug.Log("release");
            Cursor.lockState = CursorLockMode.Confined;
        }
        


    }
}
