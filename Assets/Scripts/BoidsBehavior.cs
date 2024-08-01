using System.Collections.Generic;
using HlStudio.Jobs;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;
using Random = UnityEngine.Random;

namespace HlStudio
{
    public class BoidsBehavior : MonoBehaviour
    {
        [SerializeField] private int _numberOfEntities;
        [SerializeField] private float _destinationTheshold;

        [SerializeField] private Vector3 _areaSize;
        [SerializeField] private float _borderThickness = 1.0f; // Thickness of the walls
        [SerializeField] private GameObject _wallPrefab;

        [SerializeField] private float _velocityLimit;
        [SerializeField] private Vector3 _accelerationWeights;

        [SerializeField] private SpawnProbability _spawnProbability;

        private NativeArray<Vector3> _positions;
        private NativeArray<Vector3> _velocities;
        private NativeArray<Vector3> _accelerations;

        private TransformAccessArray _transformAccessArray;


        private void Start()
        {
            _positions = new NativeArray<Vector3>(_numberOfEntities, Allocator.Persistent);
            _velocities = new NativeArray<Vector3>(_numberOfEntities, Allocator.Persistent);
            _accelerations = new NativeArray<Vector3>(_numberOfEntities, Allocator.Persistent);

            var transforms = new Transform[_numberOfEntities];

            for (int i = 0; i < _numberOfEntities; i++)
            {
                SpawnProbability.EntityProbability selectedEntity = _spawnProbability.GetEntity();
                print("Spawned : " + selectedEntity.Title);
                transforms[i] = Instantiate(selectedEntity.Entity, transform).transform;
                _velocities[i] = Random.insideUnitSphere;
            }

            _transformAccessArray = new TransformAccessArray(transforms);
        }

        private void Update()
        {
            var boundsJob = new BoundsJob()
            {
                Accelerations = _accelerations,
                Positions = _positions,
                AreaSize = _areaSize
            };

            var accelerationJob = new AccelerationJob()
            {
                Positions = _positions,
                Velocities = _velocities,
                Accelerations = _accelerations,
                DestinationThreshold = _destinationTheshold,
                Weights = _accelerationWeights
            };

            var moveJob = new MoveJob()
            {
                Positions = _positions,
                Velocities = _velocities,
                Accelerations = _accelerations,
                DeltaTime = Time.deltaTime,
                VelocityLimit = _velocityLimit
            };

            var boundsHandle = boundsJob.Schedule(_numberOfEntities, 0);
            var accelerationHandle = accelerationJob.Schedule(_numberOfEntities, 0, boundsHandle);
            var moveHandle = moveJob.Schedule(_transformAccessArray, accelerationHandle);
            moveHandle.Complete();
        }

        private void OnDestroy()
        {
            _positions.Dispose();
            _velocities.Dispose();
            _accelerations.Dispose();
            _transformAccessArray.Dispose();
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position, _areaSize);
        }

        [ContextMenu("Spawn Walls, Floor, and Ceiling")]
        private void SpawnAll()
        {
            // Ensure that the wallPrefab is assigned
            if (_wallPrefab == null)
            {
                Debug.LogError("Wall prefab is not assigned.");
                return;
            }

            // Calculate the half sizes
            float halfWidth = _areaSize.x / 2;
            float halfHeight = _areaSize.z / 2;
            float wallHeight = _areaSize.y;
            float wallThickness = _borderThickness;

            // Create walls around the border
            CreateWall(new Vector3(0, wallHeight / 2, halfHeight - wallThickness / 2),
                new Vector3(halfWidth * 2, wallHeight, wallThickness)); // Top wall
            CreateWall(new Vector3(0, wallHeight / 2, -halfHeight + wallThickness / 2),
                new Vector3(halfWidth * 2, wallHeight, wallThickness)); // Bottom wall
            CreateWall(new Vector3(halfWidth - wallThickness / 2, wallHeight / 2, 0),
                new Vector3(wallThickness, wallHeight, halfHeight * 2)); // Right wall
            CreateWall(new Vector3(-halfWidth + wallThickness / 2, wallHeight / 2, 0),
                new Vector3(wallThickness, wallHeight, halfHeight * 2)); // Left wall

            // Create the floor (bottom)
            CreateFloor(new Vector3(0, wallThickness / 2, 0),
                new Vector3(_areaSize.x, wallThickness, _areaSize.z)); // Bottom

            // Create the ceiling
            CreateCeiling(new Vector3(0, wallHeight - wallThickness / 2, 0),
                new Vector3(_areaSize.x, wallThickness, _areaSize.z)); // Ceiling
        }

        private void CreateWall(Vector3 position, Vector3 size)
        {
            GameObject wall = Instantiate(_wallPrefab, position, Quaternion.identity);
            wall.transform.localScale = size;
        }

        private void CreateFloor(Vector3 position, Vector3 size)
        {
            GameObject floor = Instantiate(_wallPrefab, position, Quaternion.identity);
            floor.transform.localScale = size;
        }

        private void CreateCeiling(Vector3 position, Vector3 size)
        {
            GameObject ceiling = Instantiate(_wallPrefab, position, Quaternion.identity);
            ceiling.transform.localScale = size;
        }
    }
}