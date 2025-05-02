using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PortalSpawner : MonoBehaviour
{
    private ARTrackedImageManager imageManager;
    private Camera mainCamera;

    public GameObject[] bluePortalMonsters; // [0] 버섯, [1] 선인장

    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();

    private void Awake()
    {
        imageManager = GetComponent<ARTrackedImageManager>();
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        imageManager.trackedImagesChanged += OnImageChanged;
    }

    private void OnDisable()
    {
        imageManager.trackedImagesChanged -= OnImageChanged;
    }

    private void OnImageChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (var image in args.added)
        {
            HandleImage(image);
        }

        foreach (var image in args.updated)
        {
            HandleImage(image);
        }

        foreach (var image in args.removed)
        {
            string name = image.referenceImage.name;
            if (spawnedPrefabs.ContainsKey(name))
            {
                Destroy(spawnedPrefabs[name]);
                spawnedPrefabs.Remove(name);
            }
        }
    }

    private void HandleImage(ARTrackedImage image)
    {
        string name = image.referenceImage.name;

        if (name != "blue_portal") return;

        if (image.trackingState != TrackingState.Tracking)
        {
            if (spawnedPrefabs.ContainsKey(name))
            {
                Destroy(spawnedPrefabs[name]);
                spawnedPrefabs.Remove(name);
            }
            return;
        }

        if (!spawnedPrefabs.ContainsKey(name))
        {
            int randIndex = Random.Range(0, bluePortalMonsters.Length);
            GameObject selectedPrefab = bluePortalMonsters[randIndex];

            GameObject spawned = Instantiate(selectedPrefab, image.transform.position, Quaternion.identity);
            spawnedPrefabs[name] = spawned;

            if (selectedPrefab.name.Contains("Mushroom"))
            {
                spawned.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            }
            else if (selectedPrefab.name.Contains("Cactus"))
            {
                spawned.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            }

            if (mainCamera != null)
            {
                Vector3 lookPos = mainCamera.transform.position;
                lookPos.y = spawned.transform.position.y;
                spawned.transform.LookAt(lookPos);
            }
        }
        else
        {
            GameObject obj = spawnedPrefabs[name];
            obj.transform.position = image.transform.position;

            if (mainCamera != null)
            {
                Vector3 lookPos = mainCamera.transform.position;
                lookPos.y = obj.transform.position.y;
                obj.transform.LookAt(lookPos);
            }
        }
    }
}
