using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BigBoi.AI
{
    /// <summary>
    /// Basic movement class which moves an object using transform.position linearly towards its target.
    /// Implemented variations on randomising speed.
    /// </summary>
    [AddComponentMenu("BigBoi/AI/Movement/Single Movement/Basic Movement")]
    public class BasicMovement : MonoBehaviour
    {
        protected Vector3 target;
        /// <summary>
        /// Entity currently moves towards this position.
        /// </summary>
        public Vector3 Target => target;

        #region Speed Variables
        [SerializeField, Tooltip("Speed multiplier for this entity.")]
        protected float speed;

        [SerializeField, Tooltip("Randomise speed?")]
        protected bool randomiseSpeed = false;

        [SerializeField, Tooltip("Range of random speed.")]
        protected Vector2 range = new Vector2(1, 20);

        [SerializeField, Tooltip("When should speed be randomly set?")]
        protected SpeedChangeWhen speedChange;

        [SerializeField, Tooltip("Timed interval for changing speed.")]
        protected float interval;
        protected float timer;

        /// <summary>
        /// 
        /// </summary>
        public enum SpeedChangeWhen
        {
            OnStartOnly,
            OnTargetChange,
            OnTimedInterval,
        }
        #endregion

        


        protected void Start()
        {
            if (randomiseSpeed)
            {
                if (speedChange == SpeedChangeWhen.OnStartOnly)
                {
                    speed = range.RanFloat();
                }
                timer = 0;
            }
        }

        protected void Update()
        {
            //if random speed is timed, do that
            if (randomiseSpeed && speedChange == SpeedChangeWhen.OnTimedInterval)
            {
                if (timer >= interval)
                {
                    speed = range.RanFloat();
                    timer = 0;
                }
                else timer += Time.deltaTime;
            }



            Move();
        }

        /// <summary>
        /// Give this entity a new target.
        /// </summary>
        public void ChangeTarget(Vector3 _target)
        {
            target = _target;

            if (randomiseSpeed && speedChange == SpeedChangeWhen.OnTargetChange)
            {
                speed = range.RanFloat();
            }
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