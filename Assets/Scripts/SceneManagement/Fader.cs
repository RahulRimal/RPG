using UnityEngine;
using System.Collections;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup canvasGroup;


        void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public IEnumerator fadeOut(float time)
        {
            while (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += Time.deltaTime / time;
            }            
            yield return null;
        }


        public IEnumerator fadeIn(float time)
        {
            while (canvasGroup.alpha  > 0)
            {
                canvasGroup.alpha -= Time.deltaTime / (time);
            }            
            yield return null;
        }

    }
}