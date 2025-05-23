using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

        public Transform laserOrigin;
        public LineRenderer lineRenderer;
        public float maxLaserDistance = 100f;

        private int solidObjectsLayer;
        private Vector3 upDirection;
        private bool isReflecting = false;
        private Vector3 reflectionPoint;

        private void Start()
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, laserOrigin.position);

            solidObjectsLayer = LayerMask.NameToLayer("Default");
            upDirection = transform.up;
        }

        private void Update()
        {
            if (!isReflecting)
            {
                RaycastHit2D hit = Physics2D.Raycast(laserOrigin.position, upDirection, maxLaserDistance);
                Vector2 inbound = new Vector2( 0, -1);   // straight down

                Vector2 normal = new Vector2( 1, 1).normalized;   // normal up-right

                // I prefer to name my arguments when they are identical types, in order to keep me honest
                Vector2 outbound = Vector2.Reflect( inDirection: inbound, inNormal: normal);

                Debug.Log( outbound);

                if (hit.collider)
                {
                    lineRenderer.SetPosition(1, hit.point);

                    if (hit.collider.gameObject.layer == solidObjectsLayer)
                    {
                        isReflecting = true;
                        reflectionPoint = hit.point;
                    }
                }
                else
                {
                    lineRenderer.SetPosition(1, laserOrigin.position + (upDirection * maxLaserDistance));
                }
            }
            else
            {
                lineRenderer.SetPosition(1, reflectionPoint);

                if (Vector3.Distance(laserOrigin.position, reflectionPoint) >= 0.01f)
                {
                    laserOrigin.position = Vector3.MoveTowards(laserOrigin.position, reflectionPoint, Time.deltaTime * maxLaserDistance);
                }
                else
                {
                    Vector3 reflectionDirection = Vector3.Reflect(upDirection, (reflectionPoint - laserOrigin.position).normalized);
                    upDirection = reflectionDirection;
                    isReflecting = false;
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == solidObjectsLayer)
            {
                lineRenderer.enabled = false;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.layer == solidObjectsLayer)
            {
                lineRenderer.enabled = true;
            }
        }
    }

