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
    public class MailVisualizer : MonoBehaviour
    {
        /// <summary>
        /// The AugmentedImage to visualize.
        /// </summary>
        public AugmentedImage Image;
        public static bool mail = false;

        public GameObject box;


        public void Start()
        {

        }
        public void Update()
        {

            if (Image == null || Image.TrackingState != TrackingState.Tracking)
            {
                box.SetActive(false);
                return;
            }
            if (NextVisualizer.next == true)
            {
                box.SetActive(false);

                return;
            }
            box.SetActive(true);
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
                if (hit.collider.CompareTag("Mail"))
                {
                    mail ^= true;
                }
            }




        }



    }
}
