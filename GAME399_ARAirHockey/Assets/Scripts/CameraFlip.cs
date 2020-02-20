using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlip : MonoBehaviour
{
    public Camera thecamera;
    public bool flipHorizontal;


    // Start is called before the first frame update
    void Start()
    {
        flipHorizontal = true;
    }

  
    void Update()
    {
        thecamera.ResetWorldToCameraMatrix();
        thecamera.ResetProjectionMatrix();
        Vector3 scale = new Vector3(flipHorizontal ? -1 : 1, 1, 1);
        thecamera.projectionMatrix = thecamera.projectionMatrix * Matrix4x4.Scale(scale);
    }

       void OnPreRender()
    {
        GL.invertCulling = flipHorizontal;
    }
    void OnPostRender()
    {
        GL.invertCulling = false;
    }
}
