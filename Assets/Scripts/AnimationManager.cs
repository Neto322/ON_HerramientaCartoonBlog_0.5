using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.Media;
using System.IO;
using Unity.Collections;


public class AnimationManager : MonoBehaviour
{
    // Start is called before the first frame update


    [SerializeField]
    Animator anim;



    [SerializeField]
    Animator anim2;



    [SerializeField]
    GameObject dropdownitem;

    [SerializeField]
    GameObject dropdownitem2;


    public List<GameObject> sceneslist = new List<GameObject>();



    public List<GameObject> transitionlist = new List<GameObject>();

    [SerializeField]
    int Yspace;

    [SerializeField]
    int Xspace;

    [SerializeField]
    Canvas canvas;



    Transform trans;


    int o = 0;

    float tiempo = 0;


    float cliplenght;

    public RenderTexture CamaraTexture;
    public Camera Camara;
    public List<Texture2D> Textures = new List<Texture2D>();
    string path1 = @"C:\Users\ElConchesumadre\Documents";

    List<float> cliplengths = new List<float>();

    void Start()
    {

        trans = GetComponent<Transform>();

        Application.targetFrameRate = 24;

    }


    public void Preview()
    {
        canvas.enabled = false;

        StartCoroutine(preview());
    }


    public void AgregarDropDown()
    {
        o++;
        GameObject item = Instantiate(dropdownitem, new Vector2(0, 0), Quaternion.identity) as GameObject;


        item.transform.SetParent(trans.transform);
        item.transform.localScale = new Vector3(1, 1, 1);
        item.transform.position = new Vector2(trans.position.x, trans.position.y - (o * Yspace));



        sceneslist.Add(item);


        GameObject item2 = Instantiate(dropdownitem2, new Vector2(0, 0), Quaternion.identity) as GameObject;


        item2.transform.SetParent(trans.transform);
        item2.transform.localScale = new Vector3(1, 1, 1);
        item2.transform.position = new Vector2(trans.position.x + Xspace, trans.position.y - (o * Yspace));



        transitionlist.Add(item2);

    }
    IEnumerator preview()
    {
    
        foreach(GameObject scene in sceneslist)
        {
            cliplenght = anim.GetComponent<Animation>().clip.length;
            cliplengths.Add(cliplenght);
            anim.SetInteger("Anim", scene.GetComponent<Dropdown>().value);
            yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime * anim.GetComponent<Animation>().clip.length >= cliplenght * 0.2);
            yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime * anim.GetComponent<Animation>().clip.length >= cliplenght * 0.9);
            Debug.Log(cliplenght * 0.8);
            anim2.Play("Idle");
            anim2.SetInteger("Anim", transitionlist[sceneslist.IndexOf(scene)].GetComponent<Dropdown>().value);
            yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime * anim.GetComponent<Animation>().clip.length >= cliplenght);
            Debug.Log(cliplenght);
            anim.SetTrigger("Idle");
        }
        anim.SetInteger("Anim", 30);
        anim.Play("Idle");
        yield return null;


    }

    public void Grabar()
    {
        canvas.enabled = false;
        foreach(float lenght in cliplengths)
        {
            tiempo =  cliplengths[cliplengths.IndexOf(lenght)] * 24;
        }
        StartCoroutine(preview());
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
        for (int i = 0; i <= tiempo; i++)
        {
            RenderTexture rendText = RenderTexture.active;
            RenderTexture.active = Camara.targetTexture;
            Camara.Render();
            Texture2D tex = new Texture2D((int)Camara.targetTexture.width, (int)Camara.targetTexture.height, TextureFormat.RGBA32, false);
            tex.ReadPixels(new Rect(0, 0, Camara.targetTexture.width, Camara.targetTexture.height), 0, 0);
            tex.Apply();
            Textures.Add(tex);
            yield return new WaitForEndOfFrame();
            Debug.Log("Frame Number" + i);

        }

        using (var encoder = new MediaEncoder(encodedFilePath, videoAttr, audioAttr))
        using (var audioBuffer = new NativeArray<float>(sampleFramesPerVideoFrame, Allocator.Temp))
        {
            foreach (Texture2D tex in Textures)
            {
                encoder.AddFrame(tex);
                encoder.AddSamples(audioBuffer);
            }
            encoder.Dispose();
            Debug.Log("Ligsto");
        }
}


    }
