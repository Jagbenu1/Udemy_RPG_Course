using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private SpriteRenderer sr;

    [Header("Flash Fx")]
    [SerializeField] private float flashDuration;
    [SerializeField] private Material hitMaterial;
    private Material originalMat;

    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMat = sr.material;
    }

    private IEnumerator FlashFX()
    {
        sr.material = hitMaterial;

        yield return new WaitForSeconds(flashDuration);

        sr.material = originalMat;

    }

    private void RedColorBlink()
    {
        if (sr.color != Color.white)
            sr.color = Color.white;
        else
            sr.color = Color.red;
    }

    private void CancelRedBlink()
    {
        CancelInvoke();
        sr.color = Color.white;
    }
}
