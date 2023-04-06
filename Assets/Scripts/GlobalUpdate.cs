using UnityEngine;

public class GlobalUpdate : MonoBehaviour
{
    private void Update()
    {
        for (var i = 0; i < MonoCache.AllUpdates.Count; i++)
        {
            MonoCache.AllUpdates[i].Tick();
        }

        MonoCache.Timer = Time.deltaTime;
    }
}