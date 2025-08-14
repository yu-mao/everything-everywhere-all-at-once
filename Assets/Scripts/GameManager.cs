using System;
using System.Collections;
using System.Collections.Generic;
using PresentFutures.XRAI.Florence;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameManager Instance { get; private set; }
    
    [Header("Riddle Reference")]
    [SerializeField] private DatabaseController databaseController;
    [SerializeField] private TMP_Text riddleText;

    [Header("Hand Pose Reference")]
    [SerializeField] private GameObject handController;
    
    [Header("AI Reference")]
    [SerializeField] private Florence2Controller aiController;
    [SerializeField] private List<string> poolOfLabels;
    
    // Hard-coded riddles
    private string riddle = "A monitor, a vase, a flower";

    // Hard-coded room objects associated to each riddles
    private List<List<string>> targetObjectLabels = new List<List<string>>
    {
        new List<string> { "monitor", "television"},
        new List<string> { "vase" },
        new List<string> { "flower" }
    };
    
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        
        try
        {
            aiController.OnSelectedObjectsRecognized += GetSelectedObjectsLabels;
            riddleText.text = riddle;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Game Manager awake() exception: {ex.Message}");
        }
    }

    
    private void GetSelectedObjectsLabels(List<string> labels)
    {
        Debug.Log($"~~~~~~ label count: {labels.Count}");
    }
}

