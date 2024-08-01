using System.Collections.Generic;
using UnityEngine;

namespace HlStudio
{
    public class WallBuilder
    {
        private readonly float _borderThickness; 
        private readonly GameObject _wallPrefab;
        private readonly Vector3 _areaSize;

        public List<GameObject> Walls { get; private set; } = new List<GameObject>();
        
        public WallBuilder(GameObject wallPrefab, float borderThickness, Vector3 areaSize)
        {
            _wallPrefab = wallPrefab;
            _borderThickness = borderThickness;
            _areaSize = areaSize;
        }
        
        public void SpawnAll()
        {
            for (int i = 0; i < Walls.Count; i++)
            {
                Object.Destroy(Walls[i]);
            }
            Walls.Clear();
            
            // Calculate the half sizes
            float halfWidth = _areaSize.x / 2;
            float halfHeight = _areaSize.z / 2;
            float wallHeight = _areaSize.y;
            float wallThickness = _borderThickness;

            // Create walls around the border
            CreateWall(new Vector3(0, wallHeight / 2, halfHeight - wallThickness / 2), new Vector3(halfWidth * 2, wallHeight, wallThickness)); // Top wall
            CreateWall(new Vector3(0, wallHeight / 2, -halfHeight + wallThickness / 2), new Vector3(halfWidth * 2, wallHeight, wallThickness)); // Bottom wall
            CreateWall(new Vector3(halfWidth - wallThickness / 2, wallHeight / 2, 0), new Vector3(wallThickness, wallHeight, halfHeight * 2)); // Right wall
            CreateWall(new Vector3(-halfWidth + wallThickness / 2, wallHeight / 2, 0), new Vector3(wallThickness, wallHeight, halfHeight * 2)); // Left wall

            // Create the floor (bottom)
            CreateWall(new Vector3(0, wallThickness / 2, 0),
                new Vector3(_areaSize.x, wallThickness, _areaSize.z)); // Bottom

            // Create the ceiling
            CreateWall(new Vector3(0, wallHeight - wallThickness / 2, 0),
                new Vector3(_areaSize.x, wallThickness, _areaSize.z)); // Ceiling
        }

        private void CreateWall(Vector3 position, Vector3 size)
        {
            GameObject wall = Object.Instantiate(_wallPrefab, position, Quaternion.identity);
            wall.transform.localScale = size;
            
            Walls.Add(wall);
        }
    }
}