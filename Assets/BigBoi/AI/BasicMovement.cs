using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BigBoi.AI
{
    /// <summary>
    /// Basic movement class which moves an object using transform.position linearly towards its target.
    /// </summary>
    [AddComponentMenu("BigBoi/AI/Movement/Single Movement/Basic")]
    public class BasicMovement : MonoBehaviour
    {
        private Vector3 target;
        /// <summary>
        /// Entity currently moves towards this position.
        /// </summary>
        public Vector3 Target => target;

        [SerializeField,Tooltip("Speed multiplier for this entity.")]
        private float speed;

        void Update()
        {
            Move();
        }

        /// <summary>
        /// Give this entity a new target.
        /// </summary>
        public void ChangeTarget(Vector3 _target)
        {
            target = _target;
        }

        /// <summary>
        /// Calculate direction towards target.
        /// </summary>
        protected Vector3 Direction()
        {
            Vector3 direction = target - transform.position;

            return direction.normalized;
        }

        /// <summary>
        /// Move towards target at given speed using transform.position.
        /// </summary>
        protected void Move()
        {
            transform.position += Direction() * Time.deltaTime * speed;
        }
    }
}