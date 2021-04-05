using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BigBoi.AI
{
    /// <summary>
    /// Group controller for modifying basic movement info on a set of entites at once.
    /// </summary>
    [AddComponentMenu("BigBoi/AI/Movement/Group Movement/Basic Movement (Grouped)")]
    public class GroupBasicMovement : MonoBehaviour
    {
        [SerializeField, Tooltip("Which entities should be affected by these settings?\nRemember to add a movement script to each entity.")]
        protected BasicMovement[] entities;

        [SerializeField, Tooltip("Speed multiplier for these entities.")]
        protected float speed;

        [SerializeField, Tooltip("Randomise speed for these entities?")]
        protected bool randomiseSpeed = false;

        [SerializeField, Tooltip("Range of random speed.")]
        protected Vector2 range = new Vector2(1, 20);

        [SerializeField, Tooltip("When should speed be randomly set?")]
        protected BasicMovement.SpeedChangeWhen speedChange;

        [SerializeField, Tooltip("Timed interval for changing speed.")]
        protected float interval;


        private void Awake()
        {
            foreach (BasicMovement _entity in entities)
            {
                _entity.ChangeSetup(speed, range, randomiseSpeed, speedChange, interval);
            }
        }

        void Start()
        {

        }

        void Update()
        {

        }
    }
}