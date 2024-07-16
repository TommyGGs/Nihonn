using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreGame : MonoBehaviour
{
    // Start is called before the first frame update
    int mouseClicks = 0;

    void Spin()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint
        (Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = rotation;

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        transform.position += new Vector3(horizontal/100, 0, vertical/100);

        if (Input.GetMouseButtonDown(0))
        {
            mouseClicks ++;
        }

        if (mouseClicks == 0)
        {
            Spin(); 
        } 

        if (Input.GetKey(KeyCode.Space))
        {
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }

        if (transform.position.y <= -4)
        {
            Debug.Log("gameOver!!");
            Destroy(gameObject);
        }
    }
}
