using UnityEngine;
using System.Collections.Generic;


public class Pathfinding : MonoBehaviour
{
    private Map grid;

    private class Node
    {
        public Vector2Int Position;
        public Node parent;
        public int gCost;
        public int hCost;
        public int fCost => gCost + hCost;

        public Node(Vector2Int position, Node parent = null, int gCost = 0, int hCost = 0)
        {
            Position = position;
            this.parent = parent;
            this.gCost = gCost;
            this.hCost = hCost;
        }

        public override bool Equals(object obj)
        {
            if(obj is Node n)
                {
                return n.Position == Position;
                }
            return false;
        }

        public override int GetHashCode() => Position.GetHashCode();
    }

    public void Initialize(Map grid)
    {
        this.grid = grid;
    }


    public List<Vector2Int> FindPath(Vector2Int start, Vector2Int target)
    {
        List<Node> open = new List<Node>();
        HashSet<Vector2Int> closed = new HashSet<Vector2Int>(); 
        Node startNode = new Node(start, null, 0, GetDistance(start, target));
        open.Add(startNode);

        while (open.Count > 0)
        {
            open.Sort((a, b) => (a.fCost.CompareTo(b.fCost)));
            Node current = open[0];

            if (current.Position == target)
            {
                return RetracePath(current);
            }

            open.Remove(current);
            closed.Add(current.Position); 

            foreach (var neighbourPos in GetNeighbours(current.Position))
            {
                if (closed.Contains(neighbourPos))
                {
                    continue;
                }
                if (!grid.IsWalkable(neighbourPos))
                {
                    closed.Add(neighbourPos);
                    continue;
                }

                int newCost = current.gCost + GetDistance(current.Position, neighbourPos);

                Node neighbour = null;
                foreach (var node in open)
                {
                    if (node.Position == neighbourPos)
                    {
                        neighbour = node;
                        break;
                    }
                }

                if (neighbour == null)
                {
                    neighbour = new Node(neighbourPos, current, newCost, GetDistance(neighbourPos, target));
                    open.Add(neighbour);
                }
                else if (newCost < neighbour.gCost)
                {
                    neighbour.gCost = newCost;
                    neighbour.parent = current;
                }
            }
        }
        return null;
    }

    private int GetDistance(Vector2Int a, Vector2Int b)
    { 
    return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }

    private List<Vector2Int> RetracePath(Node endNode)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        Node current = endNode;
        while (current != null)
        {
            path.Add(current.Position);
            current = current.parent;
        }
        path.Reverse();
        return path;
    }

    private List<Vector2Int> GetNeighbours(Vector2Int pos)
    {
        return new List<Vector2Int>()
    {
        // Cardinal directions
       // new Vector2Int(pos.x + 1, pos.y),     // Right
       // new Vector2Int(pos.x - 1, pos.y),     // Left
       // new Vector2Int(pos.x, pos.y + 1),     // Up
      //  new Vector2Int(pos.x, pos.y - 1),     // Down
        
        // Diagonal directions (often needed for isometric)
        new Vector2Int(pos.x + 1, pos.y + 1), // Top-right
        new Vector2Int(pos.x - 1, pos.y + 1), // Top-left
        new Vector2Int(pos.x + 1, pos.y - 1), // Bottom-right
        new Vector2Int(pos.x - 1, pos.y - 1), // Bottom-left
    };
    }

}

