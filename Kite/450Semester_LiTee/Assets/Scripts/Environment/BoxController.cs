using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{

    enum KeyColor { red = 0, yellow = 1, green = 2, blue = 3 };

    public GameObject hat;
    public GameObject wrong;
    public Box[] boxes;
    public GameObject[] keys;
    public Vector3[] keyPositions;

    // Start is called before the first frame update
    void Start()
    {
        wrong.SetActive(false);
        if (hat != null)
        {
            hat.SetActive(false);
        }
        keyPositions = new Vector3[keys.Length];
        for(int i = 0; i < keys.Length; i++)
        {
            keyPositions[i] = keys[i].transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Box b in boxes)
        {
            if (!b.HasAKey())
            {
                return;
            }
        }
        if (CheckAnswer())
        {
            if (hat != null)
            {
                hat.SetActive(true);
            }
        }
        else
        {
            foreach (Box b in boxes)
            {
                b.KeyLeft();
            }
            StartCoroutine(ShowWrong());
        }

    }

    bool CheckAnswer()
    {
        foreach(Box b in boxes)
        {
            if (!b.IsCorrect())
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator ShowWrong()
    {
        wrong.SetActive(true);
        yield return new WaitForSeconds(1f);
        wrong.SetActive(false);
  //      yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < keys.Length; i++)
        {
            Vector3 oldPosition = keyPositions[i];
            keys[i].transform.position = oldPosition;
        }
    }

}
