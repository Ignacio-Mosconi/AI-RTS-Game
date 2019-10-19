using System.Collections.Generic;
using UnityEngine;

namespace GreenNacho.AI.Flocking
{
    public class FlockingManager : MonoBehaviour
    {
        #region Singleton

        static FlockingManager instance;

        void SetUpSingleton()
        {
            if (Instance == this)
                DontDestroyOnLoad(gameObject);
            else
                Destroy(gameObject);
        }

        public static FlockingManager Instance
        {
            get
            {
                if (!instance)
                    instance = FindObjectOfType<FlockingManager>();
                if (!instance)
                {
                    GameObject pathFindingManager = new GameObject("Pathfinding Manager");
                    instance = pathFindingManager.AddComponent<FlockingManager>();
                }

                return instance;
            }
        }

        #endregion

        
        void Awake()
        {
            SetUpSingleton();
        }
    }
}