using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class Main : MonoBehaviour {

	private DatabaseReference db;
	// Use this for initialization

	public GameObject text;
	public string userName;
	public string avatar;

	public GameObject scrollContainer;

	public List<ChatData> chatDatas = new List<ChatData>();

	void Start () {
		// FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://realtime-chat-unity.firebaseio.com/");
		// this.db = FirebaseDatabase.DefaultInstance.RootReference;
		for(int i = 0; i < 50; i++) {
			ChatData chatData = new ChatData();
			chatData.userName = "영진";
			chatData.text = "가나다라마바사";
			chatData.avatar = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT-x6umATOcMVdzb8JkfGhafTqmNM1hFy3AT_X9VUUtJsmyjph_";
			chatDatas.Add(chatData);
		}
		
		//int topY = -30;
		//int marginY = 50;
		for(int i = 0; i < this.chatDatas.Count; i++) {
			GameObject chat = (GameObject)Instantiate(Resources.Load("Chat"));
			chat.transform.SetParent(this.scrollContainer.transform);
			//chat.transform.position = new Vector3(0, topY-(marginY*i), 0);
			//Debug.Log(topY-(marginY*i));
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(this.text.GetComponent<InputField>().text.Length <= 0) return;
		if(this.text.GetComponent<InputField>().isFocused == false) return;
		if((Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter))) {
			ChatData chatData = new ChatData();
			chatData.userName = "영진";
			chatData.text = "가나다라마바사";
			chatData.avatar = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT-x6umATOcMVdzb8JkfGhafTqmNM1hFy3AT_X9VUUtJsmyjph_";
			chatDatas.Add(chatData);
			GameObject chat = (GameObject)Instantiate(Resources.Load("Chat"));
			chat.transform.SetParent(this.scrollContainer.transform);
		}
	}
}

public class ChatData {
	public string userName;
	public string text;
	public string avatar;
}