using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Unit : MonoBehaviour
{
    public float speed = 2f;
    public float stoppingDistance = 10f;
    
    private Vector3 destination;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 destination)
    {
        this.destination = destination;
        StartCoroutine(MoveToDestination());
    }

    private IEnumerator MoveToDestination()
    {
        Cell currentCell = GridManager.Instance.flowField.GetCellAtWorldPosition(transform.position);
        Cell destinationCell = GridManager.Instance.flowField.GetCellAtWorldPosition(destination);
        
        while (Vector3.Distance(transform.position, destination) > stoppingDistance)
        {
            currentCell = GridManager.Instance.flowField.GetCellAtWorldPosition(transform.position);
            Vector2Int direction = GridDirection.GetDirectionFromV2I(currentCell.bestDirection);
            
            Vector3 moveDirection = new Vector3(direction.x, 0, direction.y);
            rb.velocity = moveDirection * speed;
            
            yield return null;
        }
    }
}
