using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private NavMeshAgent agent = null;
    private Camera mainCamera;
    
    #region server
    [Command]
    private void CmdMove(Vector3 position)
    {
        if (!NavMesh.SamplePosition(position, out NavMeshHit hit, 1f, NavMesh.AllAreas)) return;
        agent.SetDestination(hit.position);
    }
    #endregion
    
    #region client

    public override void OnStartAuthority()
    {
        base.OnStartAuthority();
        mainCamera = Camera.main;
    }

    [ClientCallback]
    private void Update()
    {
        if(!hasAuthority) return;
        if(!Input.GetMouseButtonDown(0))  return;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity)) return;
        
        CmdMove(hit.point);
    }

    #endregion
}
