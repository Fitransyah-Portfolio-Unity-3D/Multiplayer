using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.AI;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private NavMeshAgent agent = null;
    private Camera mainCamera;

    #region Server
    [Command]
    private void CmdMove(Vector3 position)
    {
        // Checking if navmesh receive hit or no
        if (!NavMesh.SamplePosition(position, out NavMeshHit hit, 1f, NavMesh.AllAreas)) return;
        
        // move the agent based on that hit position
        agent.SetDestination(hit.position);
    }
    #endregion

    #region Client
    // start method on client that has the object
    public override void OnStartAuthority()
    {
        mainCamera = Camera.main;
    }
    [ClientCallback]
    private void Update()
    {
        // checking if has authority
        if (!hasAuthority) return;

        // checking if right mouse clicked
        if (!Input.GetMouseButtonDown(1)) return;

        // check if player clicking somewhere in the scene
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity)) return;

        // call the move mthode on server
        CmdMove(hit.point);
    }
    #endregion
}
