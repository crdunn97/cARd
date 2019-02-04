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
    public class InternetVisualizer : MonoBehaviour
    {
        /// <summary>
        /// The AugmentedImage to visualize.
        /// </summary>
        public AugmentedImage Image;


        public GameObject internet;


        public void Start()
        {

        }
        public void Update()
        {

            if (Image == null || Image.TrackingState != TrackingState.Tracking)
            {
                internet.SetActive(false);
                return;
            }
            
            if (MailVisualizer.mail == true)
            {
                internet.SetActive(false);
                return;
            }
            internet.SetActive(true);
            ButtonPress();
        }

        public void ButtonPress()
        {


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
                if (hit.collider.CompareTag("Internet"))
                {
                    //Opens ULR in web browser
                    string url = "https://www.cpp.edu";
                    Application.OpenURL(url);
                    
                }
            }




        }

    }
}
