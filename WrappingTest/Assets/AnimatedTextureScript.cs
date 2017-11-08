// http://www.41post.com/4742/programming/unity-animated-texture-from-image-sequence-part-2

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedTextureScript : MonoBehaviour
{
    // outputs animation
    private Texture texture;
    // allows storage of reference to game object material
    private Material goMaterial;
    // advances frames
    private int frameCounter = 1;

    // holds name of folder containing image sequence
    public string folderName;
    // name of image sequence
    public string imageSequenceName;
    // number of frames in animation
    public int numberOfFrames;
    // amount of time between frames
    public float delay;
    // digits in number of images in sequence
    private int numberOfDigits;

    // base name of files in sequence
    private string baseName;

    void Awake()
    {
        // get reference to game object material script is attached to
        this.goMaterial = this.GetComponent<Renderer>().material;

        // get base path of images
        this.baseName = this.folderName + "/" + this.imageSequenceName;

        int temp = numberOfFrames;
        numberOfDigits = 0;
        while (temp > 0)
        {
            temp /= 10;
            numberOfDigits++;
        }
    }

    void Start()
    {
        // set initial frame as first texture and load it from first image
        texture = (Texture) Resources.Load(baseName + new string('0', numberOfDigits - 1) + frameCounter.ToString(), typeof(Texture));
    }

    void Update()
    {
        // start Play method as coroutine with delay  
        StartCoroutine("Play", delay);

        // set texture of material to current value of frameCounter variable
        goMaterial.mainTexture = this.texture;
    }

    // return IEnumerator to loop animation
    IEnumerator Play(float d)
    {
        // wait for defined time in parameter
        yield return new WaitForSeconds(d);

        if (frameCounter < numberOfFrames)
        {
            // advance one frame
            frameCounter++;

            // load current frame
            this.texture = (Texture) Resources.Load(baseName + frameCounter.ToString("D" + numberOfDigits.ToString()), typeof(Texture));
        }

        // stop coroutine
        StopCoroutine("Play");
    }
}
