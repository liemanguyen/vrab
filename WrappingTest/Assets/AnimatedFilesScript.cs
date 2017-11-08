using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedFilesScript : MonoBehaviour
{
    // outputs animation
    private Texture texture;
    // allows storage of reference to game object material
    private Material goMaterial;
    // advances frames
    private int frameCounter = 1;

    // name of image sequence
    public string imageSequenceName;
    // number of frames in animation
    public int numberOfFrames;
    // amount of time between frames
    public float delay;
    // digits in number of images in sequence
    private int numberOfDigits;

    // complete path of files in sequence
    private string path;

    void Awake()
    {
        // get reference to game object material script is attached to
        this.goMaterial = this.GetComponent<Renderer>().material;

        // get full path of images
        this.path = "file://C:/Users/admin/Pictures/FreeVideoToJPGConverter/" + this.imageSequenceName;

        int temp = numberOfFrames;
        numberOfDigits = 0;
        while (temp > 0)
        {
            temp /= 10;
            numberOfDigits++;
        }
    }

    IEnumerator Start()
    {
        // request and download local image file
        WWW www = new WWW(path + new string('0', numberOfDigits - 1) + frameCounter.ToString() + ".jpg");
        yield return www;
        texture = www.texture;
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
            WWW www = new WWW(path + frameCounter.ToString("D" + numberOfDigits.ToString()) + ".jpg");
            yield return www;
            this.texture = www.texture;
        }

        // stop coroutine
        StopCoroutine("Play");
    }
}
