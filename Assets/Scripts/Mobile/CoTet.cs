using System;
using System.Collections;
using UnityEngine;

public class CoTet : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(StopCamera("url", (string a) => { SomeText(a); }));
    }

    private void SomeText(string a)
    {
        print(a);
    }

    public IEnumerator StopCamera(string url, Action<string> callback)
    {
        yield return new WaitForSeconds(2f);
        callback.Invoke("San " + url);
    }
}