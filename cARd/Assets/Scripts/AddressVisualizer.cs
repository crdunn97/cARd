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
    public class AddressVisualizer : MonoBehaviour
    {
        /// <summary>
        /// The AugmentedImage to visualize.
        /// </summary>
        public AugmentedImage Image;


        public GameObject box;
        public TextMesh words;
        private string streetnumber;
        private string streetname;
        private string city;
        private string state;
        private string zip;
        public void Start()
        {
            words.text = "";
        }
        public void Update()
        {
            streetnumber = "12345";
            streetname = "Test Ave";
            city = "Pomona";
            state = "CA";
            zip = "91768";
            if (Image == null || Image.TrackingState != TrackingState.Tracking)
            {
                box.SetActive(false);


                return;
            }
            if (MailVisualizer.mail == true)
            {
                box.SetActive(true);
                words.text = streetnumber + " " + streetname + "\n" + city + ", " + state + ", " + zip;
                return;
            }
            words.text = "";
            box.SetActive(false);




        }



    }
}