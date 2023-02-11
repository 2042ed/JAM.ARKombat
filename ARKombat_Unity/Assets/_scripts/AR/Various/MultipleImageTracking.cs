using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace gamelab
{
    [RequireComponent(typeof(ARTrackedImageManager))]
    public class MultipleImageTracking : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] placeablePrefabs;

        private Dictionary<string, GameObject> spawnedPrefabs = new();
        private ARTrackedImageManager trackedImageManager;

        private void Awake()
        {
            trackedImageManager = GetComponent<ARTrackedImageManager>();
            foreach (var prefab in placeablePrefabs)
            {
                GameObject newPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
                newPrefab.name = prefab.name;
                spawnedPrefabs.Add(prefab.name, newPrefab);
            }
        }

        private void OnEnable()
        {
            trackedImageManager.trackedImagesChanged += OnImageChanged;
        }

        private void OnDisable()
        {
            trackedImageManager.trackedImagesChanged -= OnImageChanged;
        }

        public void OnImageChanged(ARTrackedImagesChangedEventArgs args)
        {
            foreach (var addedTrackedImage in args.added)
            {
                UpdateImage(addedTrackedImage);
            }
            foreach (var updatedTrackedImage in args.updated)
            {
                UpdateImage(updatedTrackedImage);
            }
            foreach (var removedTrackedImage in args.removed)
            {
                spawnedPrefabs[removedTrackedImage.name].SetActive(false);
            }
        }

        private void UpdateImage(ARTrackedImage trackedImage)
        {
            string name = trackedImage.referenceImage.name;

            GameObject prefab = spawnedPrefabs[name];
            prefab.transform.position = trackedImage.transform.position;
            prefab.transform.rotation = trackedImage.transform.rotation;
            prefab.SetActive(true);

            foreach (var go in spawnedPrefabs.Values)
            {
                if (go.name != name)
                {
                    go.SetActive(false);
                }
            }

        }
    }
}