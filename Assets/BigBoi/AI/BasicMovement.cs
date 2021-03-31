using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BigBoi.AI
{
    public class BasicMovement : MonoBehaviour
    {
        [SerializeField]
        private Vector3 target;
        public Vector3 Target => target;

        [SerializeField] private float speed;
        void Start()
        {

        }

        void Update()
        {
            //this isn't right bc it adds a direction rather than pointing it towards a point
            transform.position += Target * Time.deltaTime * speed;
        }

        public void ChangeTarget(Vector3 _target)
        {
            target = _target;
        }
    }
}