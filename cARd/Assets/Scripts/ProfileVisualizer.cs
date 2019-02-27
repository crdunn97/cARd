namespace GoogleARCore.Examples.AugmentedImage
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using GoogleARCore;
    using GoogleARCoreInternal;
    using UnityEngine;
    using System.Text;

    /// <summary>
    /// Uses 4 frame corner objects to visualize an AugmentedImage.
    /// </summary>
    public class ProfileVisualizer : MonoBehaviour
    {
        /// <summary>
        /// The AugmentedImage to visualize.
        /// </summary>
        public AugmentedImage Image;


        public GameObject box;
        public TextMesh words;

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
                string profile = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum purus leo, viverra vel diam vitae, fringilla blandit ante. Nunc ac ultrices libero. Nullam consectetur lacus est, vitae finibus justo tempor ac. Praesent nibh nulla, tincidunt vulputate erat quis, tempus ullamcorper erat. Mauris pharetra iaculis tellus ut blandit. Maecenas sagittis et ex sit amet iaculis. Nullam condimentum accumsan consectetur. Sed ut eros ante. Ut semper mollis dui, at dapibus ex aliquet eget.";
                string output = SpliceNoteText(profile, 85);
                words.text = output;
                box.SetActive(true);

                return;
            }
            words.text = "";
            box.SetActive(false);




        }
        private string SpliceNoteText(string text, int maxWidth)
        {
            StringBuilder sb = new StringBuilder();
            string[] parts = text.Split(' ');
            int lineLength = 0;
            for (int i = 0; i < parts.Length; i++)
            {
                lineLength += parts[i].Length;
                if (lineLength <= maxWidth)
                {
                    sb.Append(parts[i]);
                    sb.Append(" ");
                    lineLength++;


                }
                else if (lineLength > maxWidth)
                {
                    sb.Append("\n");
                    sb.Append(parts[i]);
                    sb.Append(" ");
                    lineLength = parts[i].Length + 1;
                }

            }

            return sb.ToString();
        }



    }
}