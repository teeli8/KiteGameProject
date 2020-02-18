using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pitHazard : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject pitPrefab;
    public Quaternion rotation;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject newPit = Instantiate(pitPrefab);
        newPit.transform.position = transform.position;
        newPit.transform.rotation = rotation;
    }
}
