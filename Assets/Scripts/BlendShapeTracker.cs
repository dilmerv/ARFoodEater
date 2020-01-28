using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARKit;
using DilmerGames.Core.Singletons;

[RequireComponent(typeof(ARFace))]
public class BlendShapeTracker : Singleton<BlendShapeTracker>
{
    [SerializeField]
    public GameObject collisionArea;

    [SerializeField]
    private Vector3 collisionAreaOffset;
    
    [SerializeField]
    private ARKitBlendShapeLocation blendShapeToTrack = ARKitBlendShapeLocation.JawOpen;

    private Dictionary<ARKitBlendShapeLocation, float> cacheBlendShape;

    private ARKitFaceSubsystem faceSubsystem;

    private ARFace face;

    public float Coefficient
    {
        private set;
        get;
    }

    void Awake() 
    { 
        face = GetComponent<ARFace>();
        cacheBlendShape = new Dictionary<ARKitBlendShapeLocation, float>();
        cacheBlendShape.Add(blendShapeToTrack, 0);
    }

    void OnEnable()
    {
        var faceManager = FindObjectOfType<ARFaceManager>();

        if (faceManager != null)
        {
            faceSubsystem = (ARKitFaceSubsystem)faceManager.subsystem;
        }

        face.updated += OnUpdated;
    }

    void OnDisable()
    {
        face.updated -= OnUpdated;
    }

    void OnUpdated(ARFaceUpdatedEventArgs eventArgs)
    {
        UpdateFaceFeatures();
        collisionArea.transform.position = transform.position - collisionAreaOffset;
    }
    
    void UpdateFaceFeatures()
    {
        using (var blendShapes = faceSubsystem.GetBlendShapeCoefficients(face.trackableId, Allocator.Temp))
        {
            foreach (var featureCoefficient in blendShapes)
            {
                if(cacheBlendShape.TryGetValue(featureCoefficient.blendShapeLocation, out float coefficient))
                {
                    Coefficient = coefficient;
                    Logger.Instance.LogInfo(featureCoefficient.blendShapeLocation.ToString() + " " + Coefficient);
                    cacheBlendShape[featureCoefficient.blendShapeLocation] = featureCoefficient.coefficient;
                }
            }
        }
    }
}