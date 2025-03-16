using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Tracer : MonoBehaviour
{
    [SerializeField, Space(10)] Material tracerMaterial;
    [SerializeField] RenderTexture tracerTexture;
    RenderTexture bufferTexture;

    [SerializeField] PendulumData penData;
    [SerializeField] PendulumData paperData;

    [SerializeField, Space(10)] ActionHolder drawToggleHolder;

    Renderer render;
    bool isDrawing = false;

    Vector2 oldPos = Vector2.zero;

    void Start()
    {
        ResetTexture();
        render = GetComponent<Renderer>();
        drawToggleHolder.Subscribe(ToggleTracer);
    }

    void ResetTexture()
    {
        bufferTexture = new RenderTexture(tracerTexture.width, tracerTexture.height, tracerTexture.depth, tracerTexture.format);
        var initialTexture = new Texture2D(tracerTexture.width, tracerTexture.height, TextureFormat.RGBA32, false);
        var pixels = initialTexture.GetPixels();
        for (var i = 0; i < pixels.Length; i++) pixels[i] = new Color(0f, 0f, 0f, 0f);
        initialTexture.Apply();
        Graphics.Blit(initialTexture, tracerTexture);
    }

    void ToggleTracer()
    {
        isDrawing = !isDrawing;
        if (isDrawing)
        {
            render.sharedMaterial.SetInteger("_IsDrawing", 1);
            ResetMaterialVars();
            ResetTexture();
        }
        else
        {
            render.sharedMaterial.SetInteger("_IsDrawing", 0);
            ResetMaterialVars();
            ResetTexture();
        }
    }

    void Update()
    {
        SetMaterialVars();
        IterateTexture();
    }

    void IterateTexture()
    {
        Graphics.Blit(tracerTexture, bufferTexture, tracerMaterial);
        Graphics.Blit(bufferTexture, tracerTexture);
    }

    void ResetMaterialVars()
    {
        var pos = penData.Position - paperData.Position;
        oldPos = pos;

        render.sharedMaterial.SetFloat("_X", oldPos.x);
        render.sharedMaterial.SetFloat("_Y", oldPos.y);

        render.sharedMaterial.SetFloat("_DX", 0f);
        render.sharedMaterial.SetFloat("_DY", 0f);

        render.sharedMaterial.SetFloat("_L", 0f);
    }

    void SetMaterialVars()
    {
        var pos = penData.Position - paperData.Position;
        var diff = pos - oldPos;
        var normedDiff = diff.normalized;

        if (diff.magnitude > 0.01f)
        {
            ResetMaterialVars();
            return;
        }

        render.sharedMaterial.SetFloat("_X", oldPos.x);
        render.sharedMaterial.SetFloat("_Y", oldPos.y);
        
        render.sharedMaterial.SetFloat("_DX", normedDiff.x);
        render.sharedMaterial.SetFloat("_DY", normedDiff.y);

        render.sharedMaterial.SetFloat("_L", diff.magnitude);

        oldPos = pos;
    }
}
