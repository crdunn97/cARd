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
    public class TextboxVisualizer : MonoBehaviour
    {
        /// <summary>
        /// The AugmentedImage to visualize.
        /// </summary>
        public AugmentedImage Image;


        public GameObject box;
        public TextMesh words;
        public int pressed = 0;
        public void Start()
        {
            words.text = "Pushed: " + pressed;
        }
        public void Update()
        {
            if (Image == null || Image.TrackingState != TrackingState.Tracking)
            {
                box.SetActive(false);
                pressed = 0;

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
                if (hit.collider.CompareTag("Box"))
                {
                    pressed++;
                    words.text = "Pushed: " + pressed;

                    //words.characterSize += 1;     //increased fontsize when pressed for testing
                 }
            }
        }

    }
}