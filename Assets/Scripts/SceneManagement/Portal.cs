using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;


namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        enum DestinationIdentifier
        {
            A,B,C,D,E
        }

        [SerializeField] DestinationIdentifier destination;
        [SerializeField] int sceneToLoad = -1;
        [SerializeField] Transform spawnPoint;
        [SerializeField] float fadeOutTime = 1f;
        [SerializeField] float fadeInTime = 2f;
        [SerializeField] float fadeWaitTime = 0.5f;

        void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "Player")
            {
                StartCoroutine(transition());
            }
        }


        IEnumerator transition()
    {
        Fader fader = FindObjectOfType<Fader>();

        yield return fader.fadeOut(fadeOutTime);    


        DontDestroyOnLoad(gameObject);
        yield return SceneManager.LoadSceneAsync(sceneToLoad);

        Portal otherPortal = GetOtherPortal();
        updatePlayer(otherPortal);


        yield return new WaitForSeconds(fadeWaitTime);

        yield return fader.fadeIn(fadeInTime);

        Destroy(gameObject);
    }

    Portal GetOtherPortal()
    {
        FindObjectsOfType<Portal>();

        foreach (Portal portal in FindObjectsOfType<Portal>())
        {
            if(portal == this)
                continue;
            
            if(portal.destination != destination)
                continue;

            return portal;
        }

        return null;
    }

    void updatePlayer(Portal otherPortal)
    {
        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<NavMeshAgent>().enabled = false;
        player.transform.position = otherPortal.spawnPoint.position;
        player.transform.rotation = otherPortal.spawnPoint.rotation;
        player.GetComponent<NavMeshAgent>().enabled = true;
    }





    }
}