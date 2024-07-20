using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingUpGame : MonoBehaviour
{
    private GameObject objectPrefab; 
    private string[] allObjects = new string[] {"dora2", "natto", "nikuzusi", "omochi", "onigiri", 
    "sake", "takoyaki", "tenpura", "wasabi"};
    public bool isMyTurn; 
    private string yesOrNo;

    void Start()
    {
        randomTurnFunc();
        RandomInstantiate();
        ChopsticksInstantiate();
    }

    // private void onCollision(){ naahhh it should be in core game file
        // when collides with plate: changeturn()
    // }

    private void ChangeTurn()
    {
        isMyTurn = !isMyTurn;
        SeeIfMyTurn();
    }

    private void SeeIfMyTurn()
    {
        if (isMyTurn)
        {
            Debug.Log("its ur turn");
        } else 
        {
            Debug.Log("not ur turn buddy");
        }
    }

    private void randomTurnFunc()
    {
        int randomTurn = UnityEngine.Random.Range(0, 2);
        if (randomTurn == 0)
        {
            isMyTurn = true;
        } else
        {
            isMyTurn = false;
        }
        SeeIfMyTurn();
    }

    private void ChopsticksInstantiate()
    {
        GameObject chopstick1 = Resources.Load<GameObject>("Chopstick1");
        chopstick1.GetComponent<SpriteRenderer>().enabled = false;
        GameObject chopstick2 = Resources.Load<GameObject>("Chopstick2");
        chopstick2.GetComponent<SpriteRenderer>().enabled = false;
        if (!isMyTurn)
        {
            Debug.Log("not my turn, changing chopsticks position");
            Vector3 chopPos1 = chopstick1.transform.position;
            chopPos1.x = -chopPos1.x;
            chopstick1.transform.position = chopPos1;

            Quaternion chopRot1 = chopstick1.transform.rotation;
            chopRot1.y = chopRot1.y + 180;
            chopstick1.transform.rotation = chopRot1;

            Vector3 chopPos2 = chopstick2.transform.position;
            chopPos2.x = -chopPos2.x;
            chopstick2.transform.position = chopPos2;

            Quaternion chopRot2 = chopstick2.transform.rotation;
            chopRot2.y = chopRot2.y + 180;
            chopstick2.transform.rotation = chopRot2;
        }
        Instantiate(chopstick1, chopstick1.transform.position , chopstick1.transform.rotation); 
        Instantiate(chopstick2, chopstick2.transform.position , chopstick2.transform.rotation);
    }


    private void RandomInstantiate()
    {
        int randomInt = UnityEngine.Random.Range(0, allObjects.Length);
        string randomObject = allObjects[randomInt];
        objectPrefab = Resources.Load<GameObject>(randomObject);
        if (objectPrefab != null)
        {
            Vector3 startPosition = new Vector3(0, 3, -3);
            Quaternion startRotation = Quaternion.Euler(0, 0, 0);
            objectPrefab.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            objectPrefab.GetComponent<SpriteRenderer>().sortingOrder = 2;
            objectPrefab.AddComponent<CoreGame>();
            Instantiate(objectPrefab, startPosition, startRotation); 
        }
    }

}
