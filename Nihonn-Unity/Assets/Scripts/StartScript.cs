using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    	Debug.Log("Hello World!"); 
    }

		public void OnClick(){
			Debug.Log("Button Clicked!");
			SceneManager.LoadScene("GamePage");
		}

    // Update is called once per frame
    void Update()
    {
        
    }
}
