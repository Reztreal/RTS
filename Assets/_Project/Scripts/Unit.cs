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
        while (Vector3.Distance(transform.position, destination) > stoppingDistance)
        {
            Cell currentCell = GridManager.Instance.flowField.GetCellAtWorldPosition(transform.position);
            if (currentCell == null || currentCell.bestDirection == GridDirection.None)
            {
                yield break; // No valid path found
            }

            Vector2Int direction = GridDirection.GetDirectionFromV2I(currentCell.bestDirection);
            Vector3 moveDirection = new Vector3(direction.x, 0, direction.y).normalized;

            // Apply the movement direction
            Vector3 targetPosition = transform.position + moveDirection * (speed * Time.deltaTime);
            
            // Adjust target position to follow the terrain height
            targetPosition.y = GridManager.Instance.flowField.terrain.SampleHeight(targetPosition) - 10f + 1f;

            // Move towards the target position
            rb.MovePosition(targetPosition);

            // Wait for the next frame
            yield return null;
        }
    }
}
