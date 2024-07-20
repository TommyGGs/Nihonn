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
    public bool chopsticksOpen = false; 
    private bool secondTurn = false;
    public bool gameOver;
    public bool youWin;
    void Start()
    {
        randomTurnFunc();
        RandomInstantiate();
        ChopsticksInstantiate();
    }

    void Update()
    {
        if (gameOver)
        {
            Debug.Log(youWin? "You won!": "You lost");
            gameOver = false;
        }
        if (chopsticksOpen)
        {
            Debug.Log("Chopstick Open" + chopsticksOpen);
            chopsticksOpen = false;
            Debug.Log("Chopstick closed" + chopsticksOpen);
            ChangeTurn();
        }
    }


    private void ChangeTurn()
    {
        if (secondTurn == false) 
        {
            secondTurn = true;
        }
        isMyTurn = !isMyTurn;
        Debug.Log("changing turn");
        SeeIfMyTurn();
        StartCoroutine(ChopTimer("food"));
        ChopsticksDestroy();
    }

    private void ChopsticksDestroy()
    {
        Debug.Log("destroying chopsticks");

        // changed here
        GameObject oldChopstick1 = GameObject.Find("Chopstick1(Clone)");
        GameObject oldChopstick2 = GameObject.Find("Chopstick2(Clone)");
        if (oldChopstick1 != null)
            Destroy(oldChopstick1, 2f);
        if (oldChopstick2 != null)
            Destroy(oldChopstick2, 2f);

        StartCoroutine(ChopTimer("chop"));
    }

    private IEnumerator ChopTimer(string usage)
    {
        Debug.Log("Timer started!");

        // Wait for 2 seconds
        yield return new WaitForSeconds(2f);

        Debug.Log("2 seconds have passed!");

        if (usage == "chop")
        {  
            ChopsticksInstantiate();
        } else if (usage == "food")
        {
            RandomInstantiate();
        }
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
        GameObject chopstick2 = Resources.Load<GameObject>("Chopstick2");

        // Initially disable the sprite renderer
        chopstick1.GetComponent<SpriteRenderer>().enabled = false;
        chopstick2.GetComponent<SpriteRenderer>().enabled = false;

        // Apply positions
        Vector3 chopPos1 = chopstick1.transform.position;
        Vector3 chopPos2 = chopstick2.transform.position;
        chopPos1.x = (isMyTurn || secondTurn) ? -chopPos1.x : chopPos1.x;
        chopPos2.x = (isMyTurn || secondTurn) ? -chopPos2.x : chopPos2.x;

        chopstick1.transform.position = chopPos1;
        chopstick2.transform.position = chopPos2;

        // Apply rotations
        chopstick1.transform.rotation = returnQuaternion1();
        chopstick2.transform.rotation = returnQuaternion2();

        Debug.Log("chopstick1 position & rotation" + chopstick1.transform.position + chopstick1.transform.rotation);
        Debug.Log("chopstick2 position & rotation" + chopstick2.transform.position + chopstick2.transform.rotation);

        Instantiate(chopstick1, chopstick1.transform.position , chopstick1.transform.rotation); 
        Instantiate(chopstick2, chopstick2.transform.position , chopstick2.transform.rotation);
    }

    private Quaternion returnQuaternion1()
    {
        return Quaternion.Euler(0f, isMyTurn ? 180f: 0f, 0f);
    }

    private Quaternion returnQuaternion2()
    {
        // Assuming the degrees for rotation are provided correctly
        return Quaternion.Euler(0f, isMyTurn ? 180f: 0f, 0f);
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
            objectPrefab.AddComponent<StickToObject>();
            Instantiate(objectPrefab, startPosition, startRotation); 
        }
    }

}
