using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float xAngle, yAngle, zAngle;
    
    public GameObject CubeRed, CubeBlue;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        CubeRed.GetComponent<Renderer>().material.color = Color.red;

        CubeBlue.GetComponent<Renderer>().material.color = Color.blue;
    }


    // Update is called once per frame
    void Update()
    {
        CubeRed.transform.Rotate(xAngle, yAngle, zAngle, Space.Self);
        CubeBlue.transform.Rotate(xAngle, yAngle, zAngle, Space.World);
    }

   public void OnClick()
    {
        xAngle += 1;
        yAngle += 1;
        zAngle += 1;
    }
}
