using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireButtonCooldownUIController : MonoBehaviour
{
    [SerializeField] private Transform cooldownFilterTransform;
    [SerializeField] private float filterEndYPos;

    private void Awake()
    {
        DOTween.Init();
    }

    private void OnEnable()
    {
        Cannon.OnLocalCannonFired += StartCooldownAnimation;
    }

    private void OnDisable()
    {
        Cannon.OnLocalCannonFired -= StartCooldownAnimation;
    }
    public void StartCooldownAnimation(float seconds)
    {
        cooldownFilterTransform.localPosition = Vector3.zero;
        cooldownFilterTransform.DOLocalMoveY(filterEndYPos, seconds);
    }
}
