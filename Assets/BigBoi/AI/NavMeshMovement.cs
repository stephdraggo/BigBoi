using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BigBoi.AI
{
    /// <summary>
    /// 
    /// </summary>
    [AddComponentMenu("BigBoi/AI/Movement/Single Movement/NavMesh Movement (Single)")]
    [RequireComponent(typeof(NavMeshAgent))]
    public class NavMeshMovement : FreeMovement
    {
        private NavMeshAgent agent;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();

        }
        protected override void Start()
        {
            
            agent.autoRepath = true;
            agent.speed = speed;

            base.Start();

            agent.destination = target;
        }

        protected override void Move()
        {
            //base.Move();
        }

        public override void ChangeTarget(Vector3 _target)
        {

            target = _target;
            agent.destination = target;

            //if random speed on change target, do that
            if (randomiseSpeed && speedChange == SpeedChangeWhen.OnTargetChange)
            {
                speed = range.RanFloat();
            }
        }


    }
}