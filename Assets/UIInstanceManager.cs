using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInstanceManager : MonoBehaviour
{
    [SerializeField] private Transform canvasTransform;
    [SerializeField] private GameObject debugUIPrefab;

    private List<GameObject> instantiatedPrefabs = new List<GameObject>();

    private void OnEnable()
    {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        ToggleDebugUI(true, debugUIPrefab);
#else
        ToggleDebugUI(false, debugUIPrefab);
#endif
    }

    private void ToggleDebugUI(bool enable, GameObject debugObject)
    {
        if (enable)
        {
            debugObject.SetActive(true);
            return;
        }

        debugObject.SetActive(false);

        Destroy(debugObject);
    }
}
