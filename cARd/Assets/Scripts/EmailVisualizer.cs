namespace GoogleARCore.Examples.AugmentedImage
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using GoogleARCore;
    using GoogleARCoreInternal;
    using UnityEngine;

    /// <summary>
    /// Uses 4 frame corner objects to visualize an AugmentedImage.
    /// </summary>
    public class EmailVisualizer : MonoBehaviour
    {
        /// <summary>
        /// The AugmentedImage to visualize.
        /// </summary>
        public AugmentedImage Image;


        public GameObject email;
        

        public void Start()
        {
            
        }
        public void Update()
        {
            
            if (Image == null || Image.TrackingState != TrackingState.Tracking)
            {
                email.SetActive(false);
                return;
            }
            email.SetActive(true);

            ButtonPress();
        }

        public void ButtonPress(){


            Touch touch;
            if (Input.touchCount != 1 ||
                (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
            {
                return;
            }

           
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Email"))
                {
                    //Code to open email application and enter email, subject and body
                    string address = "test@example.com";
                    string subject = "";
                    string body = "";
                    Application.OpenURL("mailto:" + address + "?subject=" + subject + "&body=" + body);
                }
            }

            
            
            
        }

    }
}
