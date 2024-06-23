using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowField
{
    public Cell[,] grid { get; private set; }
    public Vector2Int gridSize { get; private set; }
    public float cellRadius { get; private set; }
    
    public Cell destination;
    public Terrain terrain;
    
    private float cellDiameter;
    
    public FlowField(Vector2Int gridSize, float cellRadius, Terrain terrain)
    {
        this.gridSize = gridSize;
        this.terrain = terrain;
        this.cellRadius = cellRadius;
        cellDiameter = cellRadius * 2;
        
        CreateGrid();
    }

    public void CreateGrid()
    {
        grid = new Cell[gridSize.x, gridSize.y];
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector3 worldPos2D = new Vector3(x * cellDiameter + cellRadius, 0, y * cellDiameter + cellRadius);
                float terrainHeight = terrain.SampleHeight(worldPos2D);
                Vector3 worldPosition = new Vector3(x * cellDiameter + cellRadius, terrainHeight - 10, y * cellDiameter + cellRadius);
                Vector2Int gridPosition = new Vector2Int(x, y);
                
                grid[x, y] = new Cell(worldPosition, gridPosition);
            }
        }
        CreateCostField();
    }

    public void CreateCostField()
    {
        foreach (Cell cell in grid)
        {
            float height = Mathf.Abs(cell.height);
            
            if (height > 1)
            {
                byte cellCost = (byte)(height * 50);
                cell.IncreaseCost(cellCost);
            }
            
        }
    }
    
    public void CreateIntegrationField(Cell _destinationCell)
    {
        destination = _destinationCell;
        
        destination.cost = 0;
        destination.bestCost = 0;
        
        Queue<Cell> cellsToCheck = new Queue<Cell>();
        cellsToCheck.Enqueue(destination);

        while (cellsToCheck.Count > 0)
        {
            Cell currentCell = cellsToCheck.Dequeue();
            List<Cell> neighbours = GetNeighbourCells(currentCell.gridPosition, GridDirection.CardinalDirections);

            foreach (Cell neighbour in neighbours)
            {
                if (neighbour.cost == byte.MaxValue)
                {
                    continue;
                }

                if (neighbour.cost + currentCell.bestCost < neighbour.bestCost)
                {
                    neighbour.bestCost = (ushort)(neighbour.cost + currentCell.bestCost);
                    cellsToCheck.Enqueue(neighbour);
                }
            }
        }
    }

    public void CreateFlowField()
    {
        foreach (Cell cell in grid)
        {
            List<Cell> neighbours = GetNeighbourCells(cell.gridPosition, GridDirection.AllDirections);

            int bestCost = cell.bestCost;
            foreach (Cell neighbour in neighbours)
            {
                if (neighbour.bestCost < bestCost)
                {
                    bestCost = neighbour.bestCost;
                    cell.bestDirection = GridDirection.GetDirectionFromV2I(neighbour.gridPosition - cell.gridPosition);
                }
            }
        }
    }
    
    private List<Cell> GetNeighbourCells(Vector2Int pos, List<GridDirection> directions)
    {
        List<Cell> neighbours = new List<Cell>();
        
        foreach (Vector2Int direction in directions)
        {
            Cell neighbour = GetCellAtRelativePosition(pos, direction);
            if (neighbour != null)
            {
                neighbours.Add(neighbour);
            }
        }
        
        return neighbours;
    }
    
    private Cell GetCellAtRelativePosition(Vector2Int pos, Vector2Int relativePos)
    {
        Vector2Int newPos = pos + relativePos;
        
        if (newPos.x < 0 || newPos.x >= gridSize.x || newPos.y < 0 || newPos.y >= gridSize.y)
        {
            return null;
        }
        
        return grid[newPos.x, newPos.y];
    }
    
    public Cell GetCellAtWorldPosition(Vector3 worldPosition)
    {
        Vector2Int gridPosition = new Vector2Int(Mathf.FloorToInt(worldPosition.x / cellDiameter), Mathf.FloorToInt(worldPosition.z / cellDiameter));
        
        if (gridPosition.x < 0 || gridPosition.x >= gridSize.x || gridPosition.y < 0 || gridPosition.y >= gridSize.y)
        {
            return null;
        }
        
        return grid[gridPosition.x, gridPosition.y];
    }
    
    public void Reset()
    {
        foreach (Cell cell in grid)
        {
            cell.bestCost = ushort.MaxValue;
            cell.bestDirection = GridDirection.None;
        }
    }
}
