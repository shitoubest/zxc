using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Target : MonoBehaviour {

    private NavMeshAgent[] navAgents;
    public Transform targetMarker;


	// Use this for initialization
	void Start () {
        navAgents = FindObjectsOfType<NavMeshAgent>();

    }
	
    void UpdateTargets(Vector3 targetPosition)
    {
        foreach (var item in navAgents)
        {
            item.destination = targetPosition;
        }
    }

	// Update is called once per frame
	void Update () {
        int button = 0;
        if (Input.GetMouseButtonUp(button))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray.origin,ray.direction,out hitInfo))
            {
                Vector3 targetPosition = hitInfo.point;
                UpdateTargets(targetPosition);
                targetMarker.position = targetPosition + new Vector3(0, 5, 0);
            }
        }


	}
}
