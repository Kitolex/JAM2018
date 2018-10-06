using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldTVEffect : MonoBehaviour {

    public float scanlineIntensity = 100;
    public int scanlineWidth = 1;
    private Material material_Scanlines;
    public float shift = 10;
    private Texture texture;
    private Material material;
    public enum Movement { JUMPING_FullOnly, SCROLLING_FullOnly, STATIC };
    public Movement movement = Movement.STATIC;
    public float speed = 1;
    private float position = 0;
    private Material material2;

    // Use this for initialization
    void Start () {
        material = new Material(Shader.Find("Hidden/Distortion"));
        material2 = new Material(Shader.Find("Hidden/VUnsync"));
        material_Scanlines = new Material(Shader.Find("Hidden/Scanlines"));

        texture = Resources.Load<Texture>("Checkerboard-big");

        InvokeRepeating("LaunchEffectCRT", 2.0f, 0.3f);
        InvokeRepeating("LaunchEffectUnsync", 2.0f, 0.3f);
        InvokeRepeating("LaunchEffectCorruptedVram", 2.0f, 0.3f);

    }

    // Update is called once per frame
    //void LaunchEffectCRT() {
    //    material_Scanlines.SetFloat("_Intensity", scanlineIntensity * 0.01f);
    //    material_Scanlines.SetFloat("_ValueX", scanlineWidth);

    //    Graphics.Blit(source, destination, material_Scanlines);
    //}

    //void LaunchEffectUnsync()
    //{
    //    position = speed * 0.1f;

    //    material.SetFloat("_ValueX", position);
    //    Graphics.Blit(source, destination, material);
    //}

    //void LaunchEffectCorruptedVram()
    //{
    //    material.SetFloat("_ValueX", shift);
    //    material.SetTexture("_Texture", texture);
    //    Graphics.Blit(source, destination, material);
    //}

}
