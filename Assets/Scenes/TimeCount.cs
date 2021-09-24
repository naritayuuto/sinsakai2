using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class TimeCount : MonoBehaviour
{
	[SerializeField] string m_sceneNameToBeLoaded = "SceneNameToBeLoaded";
	private float time = 60;
	void Start()
	{
		
		//初期値60を表示
		//float型からint型へCastし、String型に変換して表示
		GetComponent<Text>().text = ((int)time).ToString();
	}

	void Update()
	{
		Text text = this.GetComponent<Text>();
		//1秒に1ずつ減らしていく
		time -= Time.deltaTime;
		//マイナスは表示しない
		if (time < 0) time = 0;
		GetComponent<Text>().text = ((int)time).ToString();
		if (time < 30)
		{
			text.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
		}
		if (time == 0)
        {
			SceneManager.LoadScene(m_sceneNameToBeLoaded);
		}
	}
}
