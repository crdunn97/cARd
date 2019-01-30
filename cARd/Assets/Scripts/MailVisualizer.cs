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
            internet.SetActive(true);

      
        }



    }
}
