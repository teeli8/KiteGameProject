using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public int boxColor;
    public int answer;
    int currentKey;
    bool hasKey;

    void Start()
    {
        hasKey = false;
        currentKey = -1;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("objects"))
        { 
            Key thisKey = collision.GetComponent<Key>();
            if (thisKey != null)
            {
                collision.gameObject.transform.position = gameObject.transform.position;
                hasKey = true;
                currentKey = thisKey.color;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("objects"))
        {
            KeyLeft();
        }
    }

    public bool HasAKey()
    {
        return hasKey;
    }

    public void KeyLeft()
    {
        hasKey = false;
        currentKey = -1;
    }

    public bool IsCorrect()
    {
        return answer == currentKey;
    }
}
