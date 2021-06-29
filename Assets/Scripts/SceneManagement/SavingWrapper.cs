using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {

        const string  defaultSaveFile = "save";

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.L))
            {
                load();
            }

            if(Input.GetKeyDown(KeyCode.S))
            {
                save();
            }
        }

        void save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }


        void load()
        {
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }









    }

}