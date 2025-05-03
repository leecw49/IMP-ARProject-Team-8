using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PortalSpawner : MonoBehaviour
{
    // Reference to ARTrackedImageManager which manages tracked images in the AR session
    private ARTrackedImageManager imageManager;

    // Reference to the main camera
    private Camera mainCamera;

    // Array of monster prefabs that can spawn on blue portal images
    public GameObject[] bluePortalMonsters;

    // Dictionary to keep track of spawned monsters by image name
    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();

    private void Awake()
    {
        // Get required components
        imageManager = GetComponent<ARTrackedImageManager>();
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        // Subscribe to the tracked images changed event
        imageManager.trackedImagesChanged += OnImageChanged;
    }

    private void OnDisable()
    {
        // Unsubscribe when disabled
        imageManager.trackedImagesChanged -= OnImageChanged;
    }

    // Called when tracked images are added, updated, or removed
    private void OnImageChanged(ARTrackedImagesChangedEventArgs args)
    {
        // Handle added images
        foreach (var image in args.added)
        {
            HandleImage(image);
        }

        // Handle updated images
        foreach (var image in args.updated)
        {
            HandleImage(image);
        }

        // Handle removed images by destroying the associated monster prefab
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

    // Handles monster spawning logic based on the tracked image
    private void HandleImage(ARTrackedImage image)
    {
        string name = image.referenceImage.name;

        // Only respond to images named "blue_portal"
        if (name != "blue_portal") return;

        // If the image is not actively tracked, remove the monster
        if (image.trackingState != TrackingState.Tracking)
        {
            if (spawnedPrefabs.ContainsKey(name))
            {
                Destroy(spawnedPrefabs[name]);
                spawnedPrefabs.Remove(name);
            }
            return;
        }

        // If a monster hasn't been spawned for this image yet
        if (!spawnedPrefabs.ContainsKey(name))
        {
            // Pick a random monster from the list
            int randIndex = Random.Range(0, bluePortalMonsters.Length);
            GameObject selectedPrefab = bluePortalMonsters[randIndex];

            // Instantiate the monster at the image's position
            GameObject spawned = Instantiate(selectedPrefab, image.transform.position, Quaternion.identity);
            spawnedPrefabs[name] = spawned;

            // Store the selected enemy in the GameManager for use in battle
            GameManager.Instance.selectedEnemyPrefab = selectedPrefab;

            // Adjust the monster's scale based on type
            if (selectedPrefab.name.Contains("Mushroom"))
            {
                spawned.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            }
            else if (selectedPrefab.name.Contains("Cactus"))
            {
                spawned.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            }

            // Rotate the monster to face the camera
            if (mainCamera != null)
            {
                Vector3 lookPos = mainCamera.transform.position;
                lookPos.y = spawned.transform.position.y;
                spawned.transform.LookAt(lookPos);
            }
        }
        else
        {
            // If already spawned, just update position
            GameObject obj = spawnedPrefabs[name];
            obj.transform.position = image.transform.position;

            // Rotate to face the camera again
            if (mainCamera != null)
            {
                Vector3 lookPos = mainCamera.transform.position;
                lookPos.y = obj.transform.position.y;
                obj.transform.LookAt(lookPos);
            }
        }
    }
}
