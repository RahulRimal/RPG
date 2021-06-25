using UnityEngine;
using RPG.Movement;
using RPG.Combat;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {

        void Update()
        {

            if(interactWithCombat())
                return;
            
            if(interactWithMovement())
                return;
        }

        bool interactWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(getMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                
                if(!GetComponent<Fighter>().canAttack(target))
                    continue;
                
                if(Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>().attack(target);
                }

                return true;
            }

            return false;
        }

        bool interactWithMovement()
        {
            RaycastHit hit;

            bool hasHit = Physics.Raycast(getMouseRay(), out hit);

            if(hasHit)
            {
                if(Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().startMoveAction(hit.point);
                }
                return true ;
            }

            return false;
        }

        public Ray getMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        
    }
}