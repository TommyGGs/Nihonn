using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndScript : MonoBehaviour
{
    public TMP_Text resultText;
    
    void Start()
    { 
      resultText.text = SettingUpGame.result;
    }

		public void OnClick(){
			SceneManager.LoadScene("StartPage");
		}

    // Update is called once per frame
    void Update()
    {
        
    }
}
