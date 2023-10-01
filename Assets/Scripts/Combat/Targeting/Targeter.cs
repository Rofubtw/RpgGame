using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    [SerializeField] private CinemachineTargetGroup cineTargetGroup;

    public Target CurrentTarget { get; private set; }

    private List<Target> targets = new List<Target>();
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(!other.TryGetComponent<Target>(out var target)) return;

        targets.Add(target);
        target.OnDestroyed += RemoveTarget;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<Target>(out var target)) return;

        RemoveTarget(target);

    }

    public bool SelectTarget()
    {
        if(targets.Count == 0) return false;

        Target closestTarget = null;
        float closestTargetDistance = Mathf.Infinity;

        Vector2 centerScreen = new Vector2(0.5f, 0.5f);

        foreach (var target in targets)
        {
            Vector2 viewPos = mainCamera.WorldToViewportPoint(target.transform.position);

            if(viewPos.x < 0 || viewPos.x > 1 || viewPos.y < 0 || viewPos.y > 1)
            {
                continue;
            }

            Vector2 toCenter = viewPos - centerScreen;
            if (toCenter.sqrMagnitude < closestTargetDistance)
            {
                closestTarget = target;
                closestTargetDistance = toCenter.sqrMagnitude;
            }
        }
        if (closestTarget == null) return false;

        CurrentTarget = closestTarget;
        cineTargetGroup.AddMember(CurrentTarget.transform, 1f, 2f);
        return true;
    }

    public void Cancel()
    {
        if(CurrentTarget == null) return;
        cineTargetGroup.RemoveMember(CurrentTarget.transform);
        CurrentTarget = null;
    }

    private void RemoveTarget(Target target)
    {
        if(CurrentTarget == target)
        {
            cineTargetGroup.RemoveMember(CurrentTarget.transform);
            CurrentTarget = null;
        }

        target.OnDestroyed -= RemoveTarget;
        targets.Remove(target);
    }

}
