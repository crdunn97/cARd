// Copyright 2016 Google Inc. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Firebase.Sample.Database
{
    using Firebase;
    using Firebase.Database;
    using Firebase.Unity.Editor;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;

    // Handler for UI buttons on the scene.  Also performs some
    // necessary setup (initializing the firebase app, etc) on
    // startup.
    public class DatabaseHandler : MonoBehaviour
    {

        public Text nameText;
        public Text companyText;
        public Text titleText;
        public Text phoneText;
        public Text emailText;
        public Text webpageText;
        public Text linkedinText;
        public Text imageText;
        public Text profileText;




        private string logText = "";

        private string name1 = "";
        private string company = "";
        private string title = "";
        private string phone = "";
        private string email = "";
        private string webpage = "";
        private string linkedin = "";
        private string image = "";
        private string profile = "";


        protected bool UIEnabled = true;

        const int kMaxLogSize = 16382;
        DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;
        protected bool isFirebaseInitialized = false;

        // When the app starts, check to make sure that we have
        // the required dependencies to use Firebase, and if not,
        // add them if possible.
        protected virtual void Start()
        {


            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                dependencyStatus = task.Result;
                if (dependencyStatus == DependencyStatus.Available)
                {
                    InitializeFirebase();
                }
                else
                {
                    Debug.LogError(
                      "Could not resolve all Firebase dependencies: " + dependencyStatus);
                }
            });

        }

        // Initialize the Firebase database:
        protected virtual void InitializeFirebase()
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            // NOTE: You'll need to replace this url with your Firebase App's database
            // path in order for the database connection to work correctly in editor.
            app.SetEditorDatabaseUrl("https://card-c1238.firebaseio.com/");
            if (app.Options.DatabaseUrl != null)
                app.SetEditorDatabaseUrl(app.Options.DatabaseUrl);

            isFirebaseInitialized = true;
        }

        public void loadNextScene()
        {
        
            SceneManager.LoadSceneAsync("cARd");
        }


        // Exit if escape (or back, on mobile) is pressed.
        protected virtual void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }

        // Output text to the debug log text field, as well as the console.
        public void DebugLog(string s)
        {
            Debug.Log(s);
            logText += s + "\n";

            while (logText.Length > kMaxLogSize)
            {
                int index = logText.IndexOf("\n");
                logText = logText.Substring(index + 1);
            }
        }

       


        public void AddProfile()
        {

            name1 = nameText.text;
            company = companyText.text;
            title = titleText.text;
            phone = phoneText.text;
            email = emailText.text;
            webpage = webpageText.text;
            linkedin = linkedinText.text;
            image = imageText.text;
            profile = profileText.text;

            DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("Profiles");

            DebugLog("Running Transaction...");
            // Use a transaction to ensure that we do not encounter issues with
            // simultaneous updates that otherwise might create more than MaxScores top scores.
            reference.RunTransaction(AddProfileTransaction)
              .ContinueWith(task =>
              {
                  if (task.Exception != null)
                  {
                      DebugLog(task.Exception.ToString());
                  }
                  else if (task.IsCompleted)
                  {
                      DebugLog("Transaction complete.");
;
                  }
              });

        }


        // A realtime database transaction receives MutableData which can be modified
        // and returns a TransactionResult which is either TransactionResult.Success(data) with
        // modified data or TransactionResult.Abort() which stops the transaction with no changes.
        TransactionResult AddProfileTransaction(MutableData mutableData)
        {
            List<object> profiles = mutableData.Value as List<object>;

            if (profiles == null)
            {
                profiles = new List<object>();
                
            }
                  

                // Now we add the new score as a new entry that contains the email address and score.
                Dictionary<string, object> newProfile = new Dictionary<string, object>();
                newProfile["name"] = name1;
                newProfile["email"] = email;
                newProfile["title"] = title;
                newProfile["phone"] = phone;
                newProfile["email"] = email;
                newProfile["webpage"] = webpage;
                newProfile["linkedin"] = linkedin;
                newProfile["image"] = image;
                newProfile["profile"] = profile;
                profiles.Add(newProfile);

                // You must set the Value to indicate data at that location has changed.
                mutableData.Value = profiles;
                return TransactionResult.Success(mutableData);
            

        }
    }
}

