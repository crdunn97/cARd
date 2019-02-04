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
    public class PictureVisualizer : MonoBehaviour
    {
        /// <summary>
        /// The AugmentedImage to visualize.
        /// </summary>
        public AugmentedImage Image;

        public GameObject frame;




        /// <summary>
        /// The Unity Update method.
        /// </summary>
        /// 
        public void Start()
        {

        }
        public void Update()
        {
            if (Image == null || Image.TrackingState != TrackingState.Tracking)
            {
                frame.SetActive(false);


                return;
            }
            if (MailVisualizer.mail == true)
            {
                frame.SetActive(false);
                return;
            }

            frame.SetActive(true);




        }

    }
}
