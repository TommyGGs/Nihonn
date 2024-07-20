using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndScript : MonoBehaviour
{
    public TMP_Text resultText;
    public SettingUpGame settingUpGame;
    void Start()
    {
      Debug.Log(settingUpGame.youWin);
      resultText.text = settingUpGame.youWin? "先手の勝ち": "後手の勝ち";
    }

		public void OnClick(){
			Debug.Log("Button Clicked!");
			SceneManager.LoadScene("StartPage");
		}

    // Update is called once per frame
    void Update()
    {
        
    }
}
