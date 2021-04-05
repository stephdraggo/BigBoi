using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains classes related to AI movement.
/// </summary>
namespace BigBoi.AI
{
    /// <summary>
    /// Basic movement class which moves an object using transform.position linearly towards its target.
    /// Implemented variations on randomising speed.
    /// </summary>
    [AddComponentMenu("BigBoi/AI/Movement/Single Movement/Basic Movement (Single)")]
    public class BasicMovement : MonoBehaviour
    {
        protected Vector3 target;
        /// <summary>
        /// Entity currently moves towards this position.
        /// </summary>
        public Vector3 Target => target;

        protected Vector3 directionModifier;

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
        /// Under what circumstances the speed should be randomly changed.
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
            //if random speed on start only, do that
            //also, set timer to 0 if for some reason it isn't
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



            Move(); //call move method
        }

        /// <summary>
        /// Give this entity a new target.
        /// </summary>
        public void ChangeTarget(Vector3 _target)
        {
            target = _target;

            //if random speed on change target, do that
            if (randomiseSpeed && speedChange == SpeedChangeWhen.OnTargetChange)
            {
                speed = range.RanFloat();
            }
        }

        public void ChangeSetup(float _speed, Vector2 _range, bool _random = false, SpeedChangeWhen _when = SpeedChangeWhen.OnStartOnly, float _interval = 5)
        {
            randomiseSpeed = _random;
            speed = _speed;
            range = _range;
            speedChange = _when;
            interval = _interval;
        }

        /// <summary>
        /// Calculate direction towards target.
        /// Base is shortest linear path, no pathfinding.
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
            transform.position += (Direction() + directionModifier) * Time.deltaTime * speed;
        }

        protected void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out BasicMovement _other))
            {
                directionModifier = (transform.position - _other.transform.position).normalized * 2;
            }
        }
        protected void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out BasicMovement _other))
            {
                directionModifier = Vector3.zero;
                //Vector3 startMod = directionModifier;
                //StartCoroutine(DirectionModifierOverTime(startMod));
            }
        }


        protected IEnumerator DirectionModifierOverTime(Vector3 _start)
        {
            while (directionModifier.magnitude > 0.5f)
            {

                directionModifier -= _start * 0.1f;
                yield return new WaitForFixedUpdate();
            }

            directionModifier = Vector3.zero;
        }
    }
}