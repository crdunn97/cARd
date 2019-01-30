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
    public class TitleVisualizer : MonoBehaviour
    {
        /// <summary>
        /// The AugmentedImage to visualize.
        /// </summary>
        public AugmentedImage Image;


        public GameObject box;
        public TextMesh words;

        public void Start()
        {
            words.text = "Title";
        }
        public void Update()
        {
            if (Image == null || Image.TrackingState != TrackingState.Tracking)
            {
                box.SetActive(false);


                return;
            }

            box.SetActive(true);




        }



    }
}