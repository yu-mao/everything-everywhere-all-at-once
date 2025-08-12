// Copyright (c) Meta Platforms, Inc. and affiliates.

using System.Collections;
using Meta.XR.Samples;
using UnityEngine;
using UnityEngine.UI;

namespace PassthroughCameraSamples.CameraToWorld
{
    [MetaCodeSample("PassthroughCameraApiSamples-CameraToWorld")]
    public class CameraToWorldCameraCanvas : MonoBehaviour
    {
        [SerializeField] private WebCamTextureManager m_webCamTextureManager;
        [SerializeField] private Text m_debugText;
        [SerializeField] private RawImage m_image;
        private Texture2D m_cameraSnapshot;
        private Color32[] m_pixelsBuffer;

        public void MakeCameraSnapshot()
        {
            var webCamTexture = m_webCamTextureManager.WebCamTexture;
            if (webCamTexture == null || !webCamTexture.isPlaying)
                return;

            if (m_cameraSnapshot == null)
            {
                m_cameraSnapshot = new Texture2D(webCamTexture.width, webCamTexture.height, TextureFormat.RGBA32, false);
            }

            // Copy the last available image from WebCamTexture to a separate object
            m_pixelsBuffer ??= new Color32[webCamTexture.width * webCamTexture.height];
            _ = m_webCamTextureManager.WebCamTexture.GetPixels32(m_pixelsBuffer);
            m_cameraSnapshot.SetPixels32(m_pixelsBuffer);
            m_cameraSnapshot.Apply();

            m_image.texture = m_cameraSnapshot;
        }

        public void ResumeStreamingFromCamera()
        {
            m_image.texture = m_webCamTextureManager.WebCamTexture;
        }

        private IEnumerator Start()
        {
            while (m_webCamTextureManager.WebCamTexture == null)
            {
                yield return null;
            }
            if (m_debugText) m_debugText.text = "WebCamTexture Object ready and playing.";
            ResumeStreamingFromCamera();
        }

        private void Update()
        {
            if (PassthroughCameraPermissions.HasCameraPermission != true)
            {
                if (m_debugText) m_debugText.text = "No permission granted.";
            }
        }
    }
}
