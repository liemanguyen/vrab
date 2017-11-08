using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImgSeqPlaybackScript : MonoBehaviour {

    /*public Texture2D[] frames;
    public double framesPerSecond = 10.0;
    private int index;*/

    private int frameCounter = 1;

    public string folderName;
    public string imageSequenceName;
    public int numberOfFrames;
    private Material wrappedMaterial;
    private string baseName;

    void Awake()
    {
        this.wrappedMaterial = this.GetComponent<Renderer>().material;
        this.baseName = this.folderName + "/" + this.imageSequenceName;
    }

    // Use this for initialization
    void Start () {
        wrappedMaterial.mainTexture = Resources.Load(baseName + " 60") as Texture;
    }

    // Update is called once per frame
    void Update () {
        /*index = (int) (Time.time * framesPerSecond);
        index = index % frames.Length;
        GetComponent<Renderer>.material.mainTexture = frames[index];*/
        frameCounter = (frameCounter++) % numberOfFrames;
        wrappedMaterial.mainTexture = Resources.Load(baseName + " " + frameCounter.ToString("D2")) as Texture;
    }
}
