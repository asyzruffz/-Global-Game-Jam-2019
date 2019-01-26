using System.Collections.Generic;
using UnityEngine;

public class Grid {

    public int Width { get; private set; }
    public int Height { get; private set; }
    public Tile[,] Tiles { get; private set; }

    Vector2 offset;

    public Grid (Vector2Int size, Vector2 offset) {
        Width = size.x;
        Height = size.y;
        this.offset = offset;

        Tiles = new Tile[Width, Height];

        for (int y = 0; y < Height; y++) {
            for (int x = 0; x < Width; x++) {
                Tiles[x, y] = new Tile ();
                Tiles[x, y].Position = new Vector2Int (x, y);
            }
        }
    }

    public Tile TileAt (Vector3 position) {
        int x = Mathf.RoundToInt (position.x - offset.x);
        int y = Mathf.RoundToInt (position.y - offset.y);
        if (IsValidTileAt (x, y)) {
            return Tiles[x, y];
        }
        return null;
    }

    public bool FloodFill (Vector3 position, out List<Vector3> area) {
        int startX = Mathf.RoundToInt (position.x - offset.x);
        int startY = Mathf.RoundToInt (position.y - offset.y);
        Vector2Int start = new Vector2Int (startX, startY);

        area = new List<Vector3> ();

        if (!IsValidTileAt (startX, startY)) {
            // Started from an invalid position
            return false;
        }

        if (Tiles[startX, startY].ForeTile != null) {
            // Started from a blocked position
            return false;
        }

        HashSet<Vector2Int> visited = new HashSet<Vector2Int> ();
        Queue<Vector2Int> queue = new Queue<Vector2Int> ();

        visited.Add (start);
        queue.Enqueue (start);

        while (queue.Count > 0) {
            Vector2Int n = queue.Dequeue ();
            Vector2Int see;

            // Check left
            if (IsValidTileAt(n.x - 1, n.y)) {
                see = new Vector2Int (n.x - 1, n.y);
                if (Tiles[see.x, see.y].ForeTile == null && !visited.Contains (see)) {
                    visited.Add (see);
                    queue.Enqueue (see);
                }
            } else {
                // No left wall
                return false;
            }

            // Check right
            if (IsValidTileAt (n.x + 1, n.y)) {
                see = new Vector2Int (n.x + 1, n.y);
                if (Tiles[see.x, see.y].ForeTile == null && !visited.Contains (see)) {
                    visited.Add (see);
                    queue.Enqueue (see);
                }
            } else {
                // No right wall
                return false;
            }

            // Check up
            if (IsValidTileAt (n.x, n.y + 1)) {
                see = new Vector2Int (n.x, n.y + 1);
                if (Tiles[see.x, see.y].ForeTile == null && !visited.Contains (see)) {
                    visited.Add (see);
                    queue.Enqueue (see);
                }
            } else {
                // No ceiling
                return false;
            }

            // Check down
            if (IsValidTileAt (n.x, n.y - 1)) {
                see = new Vector2Int (n.x, n.y - 1);
                if (Tiles[see.x, see.y].ForeTile == null && !visited.Contains (see)) {
                    visited.Add (see);
                    queue.Enqueue (see);
                } // We allow no floor
            }
        }

        // Return the list of offset position in the area
        foreach (Vector2Int t in visited) {
            area.Add (new Vector3 (t.x + offset.x, t.y + offset.y));
        }
        return true;
    }

    bool IsValidTileAt (int x, int y) {
        return x >= 0 && x < Width && y >= 0 && y < Height;
    }
}
