﻿namespace GoogleARCore.Examples.AugmentedImage
{
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using GoogleARCore;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Controller for AugmentedImage example.
    /// </summary>
    public class EmailController : MonoBehaviour
    {
        /// <summary>
        /// A prefab for visualizing an AugmentedImage.
        /// </summary>
        public EmailVisualizer EmailPrefab;


        /// <summary>
        /// The overlay containing the fit to scan user guide.
        /// </summary>

        private Dictionary<int, EmailVisualizer> m_Visualizers
            = new Dictionary<int, EmailVisualizer>();

        private List<AugmentedImage> m_TempAugmentedImages = new List<AugmentedImage>();

        /// <summary>
        /// The Unity Update method.
        /// </summary>
        public void Update()
        {
            // Exit the app when the 'back' button is pressed.
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }

            // Check that motion tracking is tracking.
            if (Session.Status != SessionStatus.Tracking)
            {
                return;
            }

            // Get updated augmented images for this frame.
            Session.GetTrackables<AugmentedImage>(m_TempAugmentedImages, TrackableQueryFilter.Updated);

            // Create visualizers and anchors for updated augmented images that are tracking and do not previously
            // have a visualizer. Remove visualizers for stopped images.
            foreach (var image in m_TempAugmentedImages)
            {
                EmailVisualizer visualizer = null;

                m_Visualizers.TryGetValue(image.DatabaseIndex, out visualizer);



                if (image.TrackingState == TrackingState.Tracking && visualizer == null)
                {
                    // Create an anchor to ensure that ARCore keeps tracking this augmented image.
                    Anchor anchor = image.CreateAnchor(image.CenterPose);
                    visualizer = (EmailVisualizer)Instantiate(EmailPrefab, anchor.transform);
                    visualizer.Image = image;
                    m_Visualizers.Add(image.DatabaseIndex, visualizer);
                }
                else if (image.TrackingState == TrackingState.Stopped && visualizer != null)
                {
                    m_Visualizers.Remove(image.DatabaseIndex);
                    GameObject.Destroy(visualizer.gameObject);
                }


            }


        }
    }
}

