using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using GoogleARCore;

public class DatabaseReciever : MonoBehaviour {

    public static string company;
    public static string name1;
    public static string title;
    public static string phone;
    public static string email;
    public static string webpage;
    public static string linkedin;
    public static string profile;
    public static string image;
    private bool dataRecieved = false;

	// Use this for initialization
	void Start () {
        // Set up the Editor before calling into the realtime database.
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://card-c1238.firebaseio.com/");

        // Get the root reference location of the database.
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
    }
	
	// Update is called once per frame
	void Update () {
		if(GoogleARCore.ARCoreBackgroundRenderer.QRScanned == true && dataRecieved == false)
        {
            FirebaseDatabase.DefaultInstance
             .GetReference("Profiles")
             .GetValueAsync().ContinueWith(task => {
          if (task.IsFaulted)
          {
              // Handle the error...
          }
          else if (task.IsCompleted)
          {
              DataSnapshot snapshot = task.Result;
                     // Do something with snapshot...
                     //hard coded for testing
                     /*
                     name1 = snapshot.Child("rv8BmT4nA7baV3br7rKrG6t0Ww83").Child("0").Child("name").Value.ToString();
                     company = snapshot.Child("rv8BmT4nA7baV3br7rKrG6t0Ww83").Child("0").Child("company").Value.ToString();
                     title = snapshot.Child("rv8BmT4nA7baV3br7rKrG6t0Ww83").Child("0").Child("title").Value.ToString();
                     phone = snapshot.Child("rv8BmT4nA7baV3br7rKrG6t0Ww83").Child("0").Child("phone").Value.ToString();
                     email = snapshot.Child("rv8BmT4nA7baV3br7rKrG6t0Ww83").Child("0").Child("email").Value.ToString();
                     webpage = snapshot.Child("rv8BmT4nA7baV3br7rKrG6t0Ww83").Child("0").Child("webpage").Value.ToString();
                     linkedin = snapshot.Child("rv8BmT4nA7baV3br7rKrG6t0Ww83").Child("0").Child("linkedin").Value.ToString();
                     profile = snapshot.Child("rv8BmT4nA7baV3br7rKrG6t0Ww83").Child("0").Child("profile").Value.ToString();
                     image = snapshot.Child("rv8BmT4nA7baV3br7rKrG6t0Ww83").Child("0").Child("image").Value.ToString();
                    */
                     string childLatest = ((int)(snapshot.Child(ARCoreBackgroundRenderer.QRText).ChildrenCount) - 1).ToString();
                     name1 = snapshot.Child(ARCoreBackgroundRenderer.QRText).Child(childLatest).Child("name").Value.ToString();
                     
                     company = snapshot.Child(ARCoreBackgroundRenderer.QRText).Child(childLatest).Child("company").Value.ToString();
                     title = snapshot.Child(ARCoreBackgroundRenderer.QRText).Child(childLatest).Child("title").Value.ToString();
                     phone = snapshot.Child(ARCoreBackgroundRenderer.QRText).Child(childLatest).Child("phone").Value.ToString();
                     email = snapshot.Child(ARCoreBackgroundRenderer.QRText).Child(childLatest).Child("email").Value.ToString();
                     webpage = snapshot.Child(ARCoreBackgroundRenderer.QRText).Child(childLatest).Child("webpage").Value.ToString();
                     linkedin = snapshot.Child(ARCoreBackgroundRenderer.QRText).Child(childLatest).Child("linkedin").Value.ToString();
                     profile = snapshot.Child(ARCoreBackgroundRenderer.QRText).Child(childLatest).Child("profile").Value.ToString();
                     image = snapshot.Child(ARCoreBackgroundRenderer.QRText).Child(childLatest).Child("image").Value.ToString();

                     dataRecieved = true;
                 }
      });
        }
	}
}
