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

    [Header("UI")]
    [SerializeField] private GameObject correctIcon;
    [SerializeField] private GameObject wrongIcon;
    [SerializeField] private GameObject unclearIcon;
    [SerializeField] private GameObject riddleDisplay;
    
    // Hard-coded riddles
    private string riddle = "I can show you worlds without a door,\n" +
        "I hold a bloom but not on the floor.\n" +
        "I sit and wait on a table or stand—\n" +
        "What am I, held by your hand?";

    // Hard-coded room objects associated to each riddles
    private List<string> targetObjectLabels = new List<string>
    {
        "television", "vase" , "flower"
    };

    private List<string> completedObjectLabels = new List<string>();
    
    public void ToggleRiddleDisplay()
    {
        riddleDisplay.SetActive(!riddleDisplay.activeSelf);
    }
    
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
            correctIcon.SetActive(false);
            wrongIcon.SetActive(false);
            riddleText.text = riddle;
            
            aiController.OnSelectedObjectsRecognized += GetSelectedObjectsLabels;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Game Manager awake() exception: {ex.Message}");
        }
    }

    
    private void GetSelectedObjectsLabels(List<string> recognizedLabels)
    {
        if (recognizedLabels.Count == 0) GetNonLabel();
        
        foreach (var targetLabel in targetObjectLabels)
        {
            if (recognizedLabels.Contains(targetLabel))
            {
                StartCoroutine(GetACorrectLabel());
                targetObjectLabels.Remove(targetLabel);
                
                completedObjectLabels.Add(targetLabel);
                
                return;
            }
        }
        StartCoroutine(GetAWrongLabel());
    }

    private IEnumerator GetNonLabel()
    {
        unclearIcon.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        unclearIcon.SetActive(false);
    }
    
    private IEnumerator GetACorrectLabel()
    {
        correctIcon.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        correctIcon.SetActive(false);
    }

    private IEnumerator GetAWrongLabel()
    {
        wrongIcon.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        wrongIcon.SetActive(false);
    }
}

