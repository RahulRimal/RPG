using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        const float wayPointGizmoRadius = 0.3f;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            
            for (int i = 0; i < transform.childCount; i++)
            {
                Gizmos.DrawSphere(getWayPoint(i), wayPointGizmoRadius);
                Gizmos.DrawLine(getWayPoint(i), getWayPoint(getNextIndex(i)));
            }    
        }

        public int getNextIndex(int i)
        {
            if(i + 1 == transform.childCount)
                return 0;

            return i + 1;
        }

        public Vector3 getWayPoint(int i)
        {
            return transform.GetChild(i).position;
        }
    }
}