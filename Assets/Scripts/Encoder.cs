using System.Collections;
using System.Collections.Generic;
using UnityEditor.Media;
using UnityEngine;
using Unity.Collections;
using System.IO;

public class Encoder : MonoBehaviour
{
    // Start is called before the first frame update
   public
RenderTexture CamaraTexture;  
public Camera Camara;
public List<Texture2D> Textures = new List<Texture2D>();
string path1 = @"C:\Users\ElConchesumadre\Documents";
void Start()
{
        Application.targetFrameRate = 24;
}

public void Grabar()
{
    StartCoroutine(Record());
}
IEnumerator Record()
{
    Debug.Log("Va,p aa grabar");
        
    var videoAttr = new VideoTrackAttributes
    {
        frameRate = new MediaRational(24),
        width = 1280,
        height = 720,
        includeAlpha = false
        };

    var audioAttr = new AudioTrackAttributes
    {
        sampleRate = new MediaRational(48000),
        channelCount = 2,
        language = "fr"
    };

        int sampleFramesPerVideoFrame = audioAttr.channelCount * audioAttr.sampleRate.numerator / videoAttr.frameRate.numerator;

        var encodedFilePath = Path.Combine(Path.GetFullPath(path1), "Video.mp4");
         for(int i = 0; i <=216 ; i++)
            {
                RenderTexture rendText= RenderTexture.active;
                RenderTexture.active = Camara.targetTexture;
                Camara.Render();
                Texture2D tex= new Texture2D((int)Camara.targetTexture.width, (int)Camara.targetTexture.height, TextureFormat.RGBA32, false);
                tex.ReadPixels(new Rect(0, 0, Camara.targetTexture.width, Camara.targetTexture.height), 0, 0);
                tex.Apply();
                Textures.Add(tex);
                yield return new WaitForEndOfFrame();
                Debug.Log("Frame Number" + i);

            }

        using (var encoder = new MediaEncoder(encodedFilePath, videoAttr, audioAttr))
        using (var audioBuffer = new NativeArray<float>(sampleFramesPerVideoFrame, Allocator.Temp))
        {
            foreach(Texture2D tex in Textures)
            {
                encoder.AddFrame(tex);
                encoder.AddSamples(audioBuffer);
            }
                encoder.Dispose();
                Debug.Log("Ligsto");
        }
    
  
}
}

