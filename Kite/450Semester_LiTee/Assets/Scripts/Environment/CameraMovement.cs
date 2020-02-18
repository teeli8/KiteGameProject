using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update

    Camera mainCamera;
    public Vector3 offset;
    
    void Start()
    {
        mainCamera = GetComponentInParent<Camera>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Vector3 pos = mainCamera.WorldToViewportPoint(collision.gameObject.transform.position);
        if(pos.x < 0f ||
            pos.x > 1f ||
            pos.y < 0f ||
            pos.y > 1f)
        {
            mainCamera.transform.position += offset;
        }
        
    }

}
