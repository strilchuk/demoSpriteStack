using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SpriteStackEntity : MonoBehaviour
{
    [SerializeField] private SpriteStackSO spriteStackSO;
    [SerializeField] public Vector3 offset = new Vector3(0, 0.02f, 0);
    [SerializeField] public bool generateAtRuntime = true;
    [HideInInspector] public List<GameObject> partList;
    [HideInInspector] public Vector3 rotation;

    [SerializeField] public bool isStatic = true;
    [SerializeField] public Material sharedMaterial;
    [HideInInspector] public String sortingLayerName;

    public enum RotationStyle
    {
        ByValue,
        Constant,
        Targeted
    }

    [HideInInspector] public GameObject rotationTarget;
    [SerializeField] public RotationStyle rotationStyle;
    [HideInInspector] public float rotationSpeed = 100;

    void Start()
    {
        if (generateAtRuntime)
        {
            GenerateStack();
        }
    }

    void Update()
    {
        if (!isStatic)
            DrawStack();
    }

    void PartClear()
    {
        partList.Clear();

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            if (child.name == "Layers")
            {
#if UNITY_EDITOR
                DestroyImmediate(child);
#else
                Destroy(child);
#endif
            }
        }
    }

    public void GenerateStack()
    {
        PartClear();

        GameObject parts = new GameObject("Layers");
        parts.transform.SetParent(transform);
        parts.transform.localPosition = Vector3.zero;
        parts.transform.rotation = Quaternion.Euler(45, 0, 0);

        for (int i = 0; i < spriteStackSO.stack.Count; i++)
        {
            GameObject stackPart = new GameObject("Layer" + i);
            SpriteRenderer sp = stackPart.AddComponent<SpriteRenderer>();
            sp.sprite = spriteStackSO.stack[i];
            sp.sortingOrder = i;
            sp.sortingLayerName = sortingLayerName;
            sp.sharedMaterial = sharedMaterial;

            stackPart.transform.SetParent(parts.transform);
            partList.Add(stackPart);
        }

        generateAtRuntime = false;
        DrawStack();
    }

    public void DrawStack()
    {
        Vector3 v = Vector3.zero;

        foreach (GameObject part in partList)
        {
            if (rotationStyle == RotationStyle.Constant)
            {
                part.transform.Rotate(new Vector3(0f, 0f, rotationSpeed * Time.deltaTime));
                // Debug.Log(string.Format("Speed: {0}, Delta: {1}, Rotation: {2}", rotationSpeed, Time.deltaTime, rotationSpeed * Time.deltaTime));
            }
            
            if (rotationStyle == RotationStyle.Targeted)
            {
                Vector3 targetPosition = rotationTarget.transform.position;
                Vector3 direction = targetPosition - gameObject.transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                part.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward * Time.deltaTime);
            }

            if (rotationStyle == RotationStyle.ByValue)
            {
                part.transform.localRotation = Quaternion.Euler(rotation);
            }

            part.transform.localPosition = v;
            v += offset;
        }
    }
}