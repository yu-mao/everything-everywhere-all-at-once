using System.Collections.Generic;
using PresentFutures.XRAI.Florence;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("AI Reference")]
    [SerializeField] private Florence2Controller aiController;
    [SerializeField] private List<string> poolOfLabels;

    private void Awake()
    {
        if (aiController != null)
        {
            aiController.OnSelectedObjectsRecognized += GetSelectedObjectsLabels;
        }
        else
        {
            Debug.LogError("AI Controller is not assigned.");
        }
    }

    private void RandomlySelectObjectsLabels()
    {
        Debug.Log("hehe");
    }
    
    private void GetSelectedObjectsLabels(List<string> labels)
    {
        Debug.Log($"~~~~~~ label count: {labels.Count}");
        if(labels.Count > 0)
            Debug.Log($"~~~~~~ label 0: {labels[0]}");
    }
}

