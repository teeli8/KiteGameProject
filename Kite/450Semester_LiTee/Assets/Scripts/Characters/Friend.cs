using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Friend : MonoBehaviour
{
    public Transform[] destinations;
    public Transform mainCharacterPosition;

    enum Talk { dontTalk = 0, first = 1, end = 2 };

    bool startLeading;
    bool continueLeading;
    Talk doITalk;
    int nextDestination;

    public Image icon;
    public string[] dialogs;
    public string[] finalDialogs;
    // Start is called before the first frame update
    void Start()
    {
        startLeading = false;
        continueLeading = false;
        doITalk = Talk.first;
        nextDestination = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (ReachedDestination())
        {
            //reach final destination
            startLeading = false;
            doITalk = Talk.end;
            FaceCharacter();
        }
        if (isMainCharacterFollowing())
        {
            continueLeading = true;
        }
        if (NeedToMove())
        {
            MoveToNextDesination();           
        }
    }

     void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            TalkToMainCharacter();           
        }

    }

    void TalkToMainCharacter()
    {
        switch (doITalk)
        {
            case Talk.first:
                GameController.instance.switchDialogBox(true);
                GameController.instance.icon = icon;
                StartCoroutine(WaitForDialog(dialogs));
                break;
            case Talk.end:
                GameController.instance.switchDialogBox(true);
                GameController.instance.icon = icon;
                StartCoroutine(WaitForDialog(finalDialogs));
                break;
            default:
                break;
        }
    }

    bool isMainCharacterFollowing()
    {
        Vector3 d = transform.position - mainCharacterPosition.position;
        return d.magnitude <= 4f;
    }

    IEnumerator WaitForDialog(string[] dialogs)
    {
        MainMovement.inConversation = true;
        Text dialog = GameController.instance.dialog;
        for (int j = 0; j < dialogs.Length; j++)
        {
            //dialog.text = dialogs[j];
            StartCoroutine(DisplayDialog(dialogs[j], dialog));
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            StopCoroutine("DisplayDialog");
        }
        GameController.instance.switchDialogBox(false);
        startLeading = true;
        continueLeading = true;
        switch (doITalk)
        {
            case Talk.first:
                doITalk = Talk.dontTalk;
                break;
            default:
                doITalk = Talk.end;
                break;
        }
        MainMovement.inConversation = false;
    }

    IEnumerator DisplayDialog(string dialogToPrint,Text textbox)
    {
        for(int i = 1; i <= dialogToPrint.Length; i++)
        {
            textbox.text = dialogToPrint.Substring(0, i);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    bool ReachedDestination()
    {
        return nextDestination >= destinations.Length;
    }

    void FaceCharacter()
    {
        if (mainCharacterPosition.position.x <= transform.position.x)
        {
            transform.rotation = Quaternion.identity;
        }
        else
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
        }
    }

    bool NeedToMove()
    {
        return startLeading && continueLeading;
    }

    void MoveToNextDesination()
    {
        Transform nextPoint = destinations[nextDestination];
        float xDist = nextPoint.position.x - transform.position.x;
        float yDist = nextPoint.position.y - transform.position.y;
        if (xDist != 0)
        {
            float speed = Mathf.Min(Mathf.Abs(xDist), 8f * Time.deltaTime);
            Quaternion rot = new Quaternion(0, 180, 0, 0);
            if (xDist < 0)
            {
                speed = -speed;
                rot.y = 0;
            }
            transform.position += new Vector3(speed, 0, 0);
            transform.rotation = rot;
        }
        else if (yDist != 0)
        {
            float speed = Mathf.Min(Mathf.Abs(yDist), 8f * Time.deltaTime);
            if (yDist < 0)
            {
                speed = -speed;
            }
            transform.position += new Vector3(0, speed, 0);
        }
        else if (xDist == 0 && yDist == 0)
        {
            continueLeading = false;
            nextDestination++;

        }
    }

}
