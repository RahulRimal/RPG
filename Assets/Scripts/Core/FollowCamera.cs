using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class FollowCamera : MonoBehaviour
    {

        [SerializeField] GameObject target;
        [SerializeField] Vector3 offset;
        // Update is called once per frame
        void LateUpdate()
        {
            
            transform.localPosition = target.transform.localPosition - offset;
        }
    }
}
