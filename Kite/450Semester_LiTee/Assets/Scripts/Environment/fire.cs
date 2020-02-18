using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, -2f*Time.deltaTime, 0);
    }


    void OnBecameInvisible()
    {
        Destroy(gameObject);   
    }



}
