using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingUpGame : MonoBehaviour
{
    private GameObject objectPrefab; 
    private string[] allObjects = new string[] {"dora2", "natto", "nikuzusi", "omochi", "onigiri", 
    "sake", "takoyaki", "tenpura", "wasabi"};
    void Start()
    {
        RandomInstantiate();
        ChopsticksInstantiate();
    }

    private void ChopsticksInstantiate()
    {
        GameObject chopstick1 = Resources.Load<GameObject>("Chopstick1");
        chopstick1.GetComponent<SpriteRenderer>().enabled = false;
        Instantiate(chopstick1, chopstick1.transform.position , chopstick1.transform.rotation); 
        GameObject chopstick2 = Resources.Load<GameObject>("Chopstick2");
        chopstick2.GetComponent<SpriteRenderer>().enabled = false;
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

    // Update is called once per frame
    void Update()
    {
        
    }

}
