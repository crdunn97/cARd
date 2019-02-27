using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class DatabaseReciever : MonoBehaviour {

    public static string company;
    public static string name1;
    public static string title;
    public static string phone;
    public static string email;
    public static string webpage;
    public static string linkedin;
    public static string profile;


	// Use this for initialization
	void Start () {
        // Set up the Editor before calling into the realtime database.
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://card-c1238.firebaseio.com/");

        // Get the root reference location of the database.
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
    }
	
	// Update is called once per frame
	void Update () {
		if(GoogleARCore.ARCoreBackgroundRenderer.QRScanned == true)
        {
            FirebaseDatabase.DefaultInstance
             .GetReference("user")
             .GetValueAsync().ContinueWith(task => {
          if (task.IsFaulted)
          {
              // Handle the error...
          }
          else if (task.IsCompleted)
          {
              DataSnapshot snapshot = task.Result;
                     // Do something with snapshot...
              DataSnapshot nameSnap = snapshot.Child(name);
              name1 = nameSnap.GetRawJsonValue();
          }
      });
        }
	}
}
