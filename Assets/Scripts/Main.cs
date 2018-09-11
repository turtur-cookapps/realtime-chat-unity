using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class Main : MonoBehaviour {

	private FirebaseDatabase db;
	// Use this for initialization

	public GameObject text;
	private string userName = "영진";
	private string avatar = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT-x6umATOcMVdzb8JkfGhafTqmNM1hFy3AT_X9VUUtJsmyjph_";

	public GameObject scrollContainer;

	private List<ChatData> chatDatas = new List<ChatData>();

	void Start () {
		Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
		Firebase.Auth.FirebaseUser user = auth.CurrentUser;
		this.userName = user.DisplayName;
		this.avatar = user.PhotoUrl.ToString();

		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://realtime-chat-unity.firebaseio.com/");
		this.db = FirebaseDatabase.DefaultInstance;
		this.db.GetReference("chats").LimitToLast(10).ChildAdded += this.ChildAdded;
	}

	private void ChildAdded(object sender, ChildChangedEventArgs args) {
      	Dictionary<string, object> chatData = (Dictionary<string, object>)args.Snapshot.Value;
	  	GameObject chat;
		if (chatData["userName"].ToString() == this.userName)
			chat = (GameObject)Instantiate(Resources.Load("ChatRight"));
		else 
			chat = (GameObject)Instantiate(Resources.Load("ChatLeft"));
		
		chat.GetComponentInChildren<Text>().text = chatData["text"].ToString();
		chat.GetComponentInChildren<UserImage>().LoadImage(chatData["avatar"].ToString());
		chat.transform.SetParent(this.scrollContainer.GetComponent<ScrollRect>().content.transform);
		
		Invoke("ScrollToBottom", 0.1f);
    }

	void ScrollToBottom () {
		this.scrollContainer.GetComponent<ScrollRect>().verticalNormalizedPosition = 0f;
		Canvas.ForceUpdateCanvases();
	}
	// Update is called once per frame
	void Update () {
		if(this.text.GetComponent<InputField>().text.Length <= 0) return;
		if((Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter))) {
			
			ChatData chatData = new ChatData();
			chatData.userName = this.userName;
			chatData.text = this.text.GetComponent<InputField>().text;
			chatData.avatar = this.avatar;
			chatDatas.Add(chatData);
			
			string id = this.db.GetReference("chats").Push().Key;
			this.db.GetReference("chats").Child(id).SetRawJsonValueAsync(JsonUtility.ToJson(chatData));
			
			this.text.GetComponent<InputField>().text = "";
		}
	}
}

public class ChatData {
	public string userName;
	public string text;
	public string avatar;
}