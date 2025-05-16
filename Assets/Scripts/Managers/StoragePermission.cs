using UnityEngine;
using UnityEngine.Android;

public class StoragePermission : MonoBehaviour
{
    public void RequestStoragePermission(System.Action onGranted, System.Action onDenied = null)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        if (Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        {
            onGranted?.Invoke();
        }
        else
        {
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);
            StartCoroutine(WaitForPermission(onGranted, onDenied));
        }
#else
        onGranted?.Invoke();
#endif
    }

#if UNITY_ANDROID && !UNITY_EDITOR
    private System.Collections.IEnumerator WaitForPermission(System.Action onGranted, System.Action onDenied)
    {
        while (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        {
            yield return null; // wait for user to respond
        }

        if (Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        {
            onGranted?.Invoke();
        }
        else
        {
            onDenied?.Invoke();
        }
    }
#endif
}
