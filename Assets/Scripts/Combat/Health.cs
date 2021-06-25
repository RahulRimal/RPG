using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {

        [SerializeField] float healthPoints = 100f;

        bool isDead;


        public bool IsDead()
        {
            return isDead;
        }

        public void damage(float damage)
        {
            healthPoints = Mathf.Max(0, healthPoints - damage);
            if(healthPoints == 0)
                die();

        }

        void die()
        {
            if(isDead) return;

            GetComponent<Animator>().SetTrigger("die");
            
            isDead = true;
        }


    }
}