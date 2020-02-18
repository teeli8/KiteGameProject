using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{

    public float anglePerFrame;
    public Transform targetPosition;
    Vector3 rotateEuler;
    // Start is called before the first frame update
    void Start()
    {
        rotateEuler = new Vector3(0, 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotateEuler, anglePerFrame);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            collision.gameObject.transform.position = targetPosition.position;
            GameController.instance.switchCam();
            if (collision.gameObject.GetComponent<Rigidbody2D>().gravityScale == 0f)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1f;
            }
            else
            {
                collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0f;
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            }
        }
    }

}
