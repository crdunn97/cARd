//Resources:
//https://stackoverflow.com/questions/49798067/arcore-for-unity-save-camera-image
//https://developers.google.com/ar/reference/unity/struct/GoogleARCore/CameraImageBytes#structGoogleARCore_1_1CameraImageBytes
//https://docs.microsoft.com/en-us/dotnet/api/system.runtime.interopservices.marshal.copy?view=netframework-4.7.2#System_Runtime_InteropServices_Marshal_Copy_System_IntPtr___System_Int32_System_IntPtr_System_Int32_
//https://stackoverflow.com/questions/49579334/save-acquirecameraimagebytes-from-unity-arcore-to-storage-as-an-image
//https://stackoverflow.com/questions/50879047/how-to-get-arcore-acquirecameraimagebytes-in-color
//https://en.wikipedia.org/wiki/YUV
//


//-----------------------------------------------------------------------
// <copyright file="ARCoreBackgroundRenderer.cs" company="Google">
//
// Copyright 2017 Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
//-----------------------------------------------------------------------

namespace GoogleARCore
{
    using System.Collections;
    using System.Collections.Generic;
    using GoogleARCoreInternal;
    using UnityEngine;
    using UnityEngine.XR;
    using ZXing;
    using ZXing.QrCode;

    /// <summary>
    /// Renders the device's camera as a background to the attached Unity camera component.
    /// </summary>
    [RequireComponent(typeof(Camera))]
    [HelpURL("https://developers.google.com/ar/reference/unity/class/GoogleARCore/ARCoreBackgroundRenderer")]
    public class ARCoreBackgroundRenderer : MonoBehaviour
    {
        /// <summary>
        /// A material used to render the AR background image.
        /// </summary>
        [Tooltip("A material used to render the AR background image.")]
        public Material BackgroundMaterial;

        private Camera m_Camera;

        private ARBackgroundRenderer m_BackgroundRenderer;

        public static bool QRScanned = false;
        public static string QRText = "";
        private int counter = 0;
        public GUIStyle labelStyle;
        public Texture2D _texture = null;
        Color32[] pixels = null;
        IBarcodeReader barcodeReader = new BarcodeReader();

        private void OnEnable()
        {
            if (m_BackgroundRenderer == null)
            {
                m_BackgroundRenderer = new ARBackgroundRenderer();
            }

            if (BackgroundMaterial == null)
            {
                Debug.LogError("ArCameraBackground:: No material assigned.");
                return;
            }

            m_Camera = GetComponent<Camera>();
            Debug.Log("Height and Scaled Height: " + m_Camera.pixelHeight + ", " + m_Camera.scaledPixelHeight);
            m_BackgroundRenderer.backgroundMaterial = BackgroundMaterial;
            m_BackgroundRenderer.camera = m_Camera;
            m_BackgroundRenderer.mode = ARRenderMode.MaterialAsBackground;
        }

        private void OnDisable()
        {
            Disable();
        }

        /*private void OnGUI()
        {
            GUI.Label(new Rect(300, 200, 200, 200), QRText, labelStyle);
            GUI.Label(new Rect(500, 400, 400, 400), counter.ToString(), labelStyle);
        } */

        private void Update()
        {
            if (!QRScanned)
            {
                counter++;
                if (true)//if (counter % 1 == 0)
                {
                    using (var image = Frame.CameraImage.AcquireCameraImageBytes())
                    {
                        if (image.IsAvailable)
                        {
                            byte[] m_EdgeImage = null;
                            if (_texture == null || m_EdgeImage == null || _texture.width != image.Width || _texture.height != image.Height)
                            {
                                _texture = new Texture2D(image.Width, image.Height, TextureFormat.RGBA32, false, false);
                                m_EdgeImage = new byte[image.Width * image.Height * 4];
                            }
                            System.Runtime.InteropServices.Marshal.Copy(image.Y, m_EdgeImage, 0, image.Width * image.Height);
                            _texture.LoadRawTextureData(m_EdgeImage);
                            _texture.Apply();
                            pixels = _texture.GetPixels32();
                            Destroy(_texture);
                            try
                            {
                                var result = barcodeReader.Decode(pixels, image.Width, image.Height);
                                QRText = result.Text;
                                QRScanned = true;
                            }
                            catch (System.Exception ex) { Debug.LogWarning(ex.Message); }
                            image.Release();
                            image.Dispose();
                        }
                    }
                }

                /*
                var YUVImage = Frame.CameraImage.AcquireCameraImageBytes();
                System.Runtime.InteropServices.Marshal.Copy(pixelBuffer, m_EdgeImage, 0, bufferSize);
                try
                {
                    var image = Frame.CameraImage.AcquireCameraImageBytes();
                    IBarcodeReader barcodeReader = new BarcodeReader();
                    var result = barcodeReader.Decode(image, image.Width, image.Height);
                }
                catch (System.Exception ex) { Debug.LogWarning(ex.Message); } */
            }
            if (BackgroundMaterial == null)
            {
                return;
            }

            Texture backgroundTexture = Frame.CameraImage.Texture;
            if (backgroundTexture == null)
            {
                return;
            }

            const string mainTexVar = "_MainTex";
            const string topLeftRightVar = "_UvTopLeftRight";
            const string bottomLeftRightVar = "_UvBottomLeftRight";

            BackgroundMaterial.SetTexture(mainTexVar, backgroundTexture);

            var uvQuad = Frame.CameraImage.DisplayUvCoords;
            BackgroundMaterial.SetVector(topLeftRightVar,
                new Vector4(uvQuad.TopLeft.x, uvQuad.TopLeft.y, uvQuad.TopRight.x, uvQuad.TopRight.y));
            BackgroundMaterial.SetVector(bottomLeftRightVar,
                new Vector4(uvQuad.BottomLeft.x, uvQuad.BottomLeft.y, uvQuad.BottomRight.x, uvQuad.BottomRight.y));

            m_Camera.projectionMatrix = Frame.CameraImage.GetCameraProjectionMatrix(
                m_Camera.nearClipPlane, m_Camera.farClipPlane);
        }

        private void Disable()
        {
            if (m_BackgroundRenderer != null)
            {
                m_BackgroundRenderer.mode = ARRenderMode.StandardBackground;
                m_BackgroundRenderer.camera = null;
            }
        }
    }
}
