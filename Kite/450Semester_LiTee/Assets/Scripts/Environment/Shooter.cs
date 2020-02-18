using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    public GameObject shooter;
    public GameObject bulletPrefab;
    Quaternion rot;


    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject newBullet = Instantiate(bulletPrefab);
        newBullet.transform.position = shooter.transform.position;
        newBullet.transform.rotation = rot;
    }

    void Start()
    {
        Vector3 direction = transform.position - shooter.transform.position;
        float radiansToHazard = Mathf.Atan2(direction.y, direction.x);
        float angleToHazard = radiansToHazard * 180f / Mathf.PI;
        rot = Quaternion.Euler(0, 0, angleToHazard);
    }

}
