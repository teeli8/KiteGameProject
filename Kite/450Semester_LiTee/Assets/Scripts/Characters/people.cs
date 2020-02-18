using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class people : MonoBehaviour
{

    public Transform mainCharacterPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 d = mainCharacterPosition.position - transform.position;
        if(d.magnitude <= 3f && Mathf.Abs(d.x) <= 2.2f && Mathf.Abs(d.y) <= 2.2f)
        {
            if(mainCharacterPosition.position.x <= transform.position.x)
            {
                //look at the other direction
                transform.rotation = new Quaternion(0, 180, 0, 0);
            }
            else
            {
                transform.rotation = Quaternion.identity;
            }
        }
    }
}
