using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BigBoi.AI
{
    [AddComponentMenu("BigBoi/AI/Movement/Random 2D Movement")]
    [RequireComponent(typeof(MeshRenderer))]
    public class RandomMovementPlane : MonoBehaviour
    {

        [SerializeField, Tooltip("The entities that will move around in this space.")]
        private List<BasicMovement> entities;

        [SerializeField, Min(0.01f), Tooltip("The distance where the entity is 'close enough' to its target that it should choose a new target.")]
        private float distance;

        [SerializeField, Tooltip("Is 3D movement allowed?")]
        private bool yMovement;

        private MeshRenderer mesh;
        private float minX, maxX, minY, maxY, minZ, maxZ;
        private Vector3 target;


        void Start()
        {
            //get the bounds from the shape
            if (TryGetComponent(out mesh)) //check if there is a mesh renderer
            {
                Vector3 offset = mesh.bounds.center;
                float xSize = mesh.bounds.size.x * 0.5f;
                float ySize = mesh.bounds.size.y * 0.5f;
                float zSize = mesh.bounds.size.z * 0.5f;
                minX = offset.x - xSize;
                maxX = offset.x + xSize;
                minY = offset.y - ySize;
                maxY = offset.y + ySize;
                minZ = offset.z - zSize;
                maxZ = offset.z + zSize;

                if (minX == maxX && minZ == maxZ && minY == maxY)
                {
                    Debug.LogError("Mesh renderer lacks required size. Min and max bounds values should not be equal.");
                    enabled = false;
                    return;
                }
            }
            else
            {
                Debug.LogError("No mesh renderer attached to this object.");
                enabled = false;
                return;
            }

            if (entities.Count < 1)
            {
                Debug.LogWarning("There are no entities attached to this movement area.");
            }
            else
            {
                foreach (BasicMovement _entity in entities)
                {
                    _entity.ChangeTarget(_entity.transform.position);
                }
            }
        }

        void Update()
        {
            foreach (BasicMovement _entity in entities)
            {
                float xDistance = _entity.transform.position.x - target.x;
                float zDistance = _entity.transform.position.z - target.z;
                if ((xDistance + zDistance) < distance) //is close enough to target?
                {

                }
            }
        }
    }
}