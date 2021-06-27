using UnityEngine;
using UnityEngine.Playables;
using RPG.Core;
using RPG.Control;

namespace RPG.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {
        GameObject player;

        void Start()
        {
            player = GameObject.FindWithTag("Player");

            GetComponent<PlayableDirector>().played += disableControl;
            GetComponent<PlayableDirector>().stopped += enableControl;
        }

        void disableControl(PlayableDirector pd)
        { 
            player.GetComponent<ActionScheduler>().cancelCurrentAction();
            player.GetComponent<PlayerController>().enabled = false;
        }

        void enableControl(PlayableDirector pd)
        {
            player.GetComponent<PlayerController>().enabled = true;
        }

    }    
}