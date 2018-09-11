using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class Main : MonoBehaviour {

	private DatabaseReference db;
	// Use this for initialization
	void Start () {
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://realtime-chat-unity.firebaseio.com/");
		this.db = FirebaseDatabase.DefaultInstance.RootReference;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
