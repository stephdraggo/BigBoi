using System.Collections.Generic;
using UnityEngine;

namespace BigBoi.AI
{
    /// <summary>
    /// Generates random targets within a range determined by mesh renderer.
    /// Tested with plane and cube, rounded objects will most likely be treated as cubes when calculating area bounds.
    /// Tickbox to allow vertical movement or keep entities grounded.
    /// Mesh renderer must exist but can be disabled.
    /// Limitations: area bounds are set at start and cannot be modified in runtime.
    /// </summary>
    [AddComponentMenu("BigBoi/AI/Movement/Group Movement/Random Movement Free Range")]
    [RequireComponent(typeof(MeshRenderer))]
    public class RandomMovementFreeRange : MonoBehaviour
    {

        [SerializeField, Tooltip("The entities that will move around in this space.")]
        private List<BasicMovement> entities;

        [SerializeField, Min(0.01f), Tooltip("The distance where the entity is 'close enough' to its target that it should choose a new target.")]
        private float distanceRange;

        [SerializeField, Tooltip("Is vertical movement allowed?")]
        private bool yMovement;

        private MeshRenderer mesh;
        private float minX, maxX, minY, maxY, minZ, maxZ;


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

                if (minX == maxX && minY == maxY && minZ == maxZ)
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
            //go through each entity and check if close enough to target
            //generate new target if needed
            foreach (BasicMovement _entity in entities)
            {
                float distance = _entity.transform.position.x - _entity.Target.x;
                distance += _entity.transform.position.z - _entity.Target.z;
                if (yMovement)
                {
                    distance += _entity.transform.position.y - _entity.Target.y;
                }
                if (Mathf.Abs(distance) < distanceRange) //is close enough to target?
                {
                    //generate new random target within area bounds
                    float xTarget = Random.Range(minX, maxX);
                    float yTarget;
                    float zTarget = Random.Range(minZ, maxZ);
                    if (yMovement)
                    {
                        yTarget = Random.Range(minY, maxY);
                    }
                    else
                    {
                        yTarget = _entity.Target.y;
                    }
                    _entity.ChangeTarget(new Vector3(xTarget, yTarget, zTarget));
                }
            }
        }

        /// <summary>
        /// Dynamically remove an entity from random movement (for example to switch state to chasing something)
        /// </summary>
        public void RemoveFromList(BasicMovement _entity)
        {
            entities.Remove(_entity);
        }

        /// <summary>
        /// Dynamically add an entity to random movement (for example to switch state to wandering)
        /// </summary>
        public void AddToList(BasicMovement _entity)
        {
            entities.Add(_entity);
        }
    }
}