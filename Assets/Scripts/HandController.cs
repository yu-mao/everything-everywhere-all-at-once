using System;
using UnityEngine;

public class HandController : MonoBehaviour
{
    [Header("OVR Hand Reference")]
    [SerializeField] private OVRHand rightHand;
    [SerializeField] private OVRSkeleton rightHandSkeleton;
    [SerializeField] private GameObject indexFingerTipVisual;

    public event Action<Vector3> OnThumbUpRecognized;

    public void TriggerRoomObjectRecognition()
    {
        if (rightHand.IsTracked && rightHandSkeleton.IsInitialized)
        {
            Vector3 indexFingerTipPos = GetIndexFingerTipPos();
            OnThumbUpRecognized?.Invoke(indexFingerTipPos);
        }
    }

    private Vector3 GetIndexFingerTipPos()
    {
        var indexTip = rightHandSkeleton.Bones[(int)OVRSkeleton.BoneId.Hand_IndexTip];
        return indexTip.Transform.position;
    }

    private void Update()
    {
        if (rightHand.IsTracked && rightHandSkeleton.IsInitialized)
        {
            indexFingerTipVisual.SetActive(true);
            indexFingerTipVisual.transform.position = GetIndexFingerTipPos();
        }
        else
        {
            indexFingerTipVisual.SetActive(false);
        }
    }
}
