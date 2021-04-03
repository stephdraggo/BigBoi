using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BigBoi.GameDisplays
{
    public class WorldSpaceUIFaceCam : MonoBehaviour
    {
        private Camera cam;

        private void Start()
        {
            cam = Camera.main;
        }

        private void Update()
        {
            FaceCam();
        }

        void FaceCam()
        {
            //find distance between camera and text
            Vector3 camToText = cam.transform.position - transform.position;

            //send that distance back past the text
            Vector3 inverseCam = transform.position - camToText;

            //look behind text (text would face backwards otherwise)
            transform.LookAt(inverseCam);
        }
    }
}