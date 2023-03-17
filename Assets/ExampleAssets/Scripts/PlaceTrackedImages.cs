using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARTrackedImageManager))]
public class PlaceTrackedImages : MonoBehaviour
{

    private ARTrackedImageManager trackedImageManager;
    public GameObject[] ArPrefabs;
    private readonly Dictionary<string, GameObject> instantiatedPrefabs = new Dictionary<string, GameObject>();
    // Start is called before the first frame update

    private void Awake()
    {
        trackedImageManager= GetComponent<ARTrackedImageManager>();
    }

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs) 
    {
        foreach(var trackedImage in eventArgs.added)
        {
            var imageName = trackedImage.referenceImage.name;

            foreach(var curPrefab in ArPrefabs) 
            {
                if(string.Compare(curPrefab.name, imageName, System.StringComparison.OrdinalIgnoreCase) == 0 
                    && !instantiatedPrefabs.ContainsKey(imageName))
                {
                    var newPrefab = Instantiate(curPrefab, trackedImage.transform);
                    instantiatedPrefabs[imageName] = newPrefab;
                }
            }
        }

        foreach(var trackedImage in eventArgs.updated) 
        {
            instantiatedPrefabs[trackedImage.referenceImage.name].SetActive(trackedImage.trackingState == TrackingState.Tracking);
        }

        foreach (var trackedImage in eventArgs.removed)
        {
            Destroy(instantiatedPrefabs[trackedImage.referenceImage.name]);
            instantiatedPrefabs.Remove(trackedImage.referenceImage.name);
        }
    }
}
