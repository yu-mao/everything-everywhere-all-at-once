using System.Collections;
using PassthroughCameraSamples;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private RawImage pcaDisplay;
    [SerializeField] private WebCamTextureManager pcaManager;

    private IEnumerator Start()
    {
        while (pcaManager.WebCamTexture == null) yield return null;
        
        pcaDisplay.texture = pcaManager.WebCamTexture;
    }
}
