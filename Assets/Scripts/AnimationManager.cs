using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.Media;
using System.IO;
using Unity.Collections;
using UnityEngine.SceneManagement;
using TMPro;


public class AnimationManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    TextMeshPro displaytextCuerpo;

    [SerializeField]
    public TextMeshPro displaytextCabecera;


    [SerializeField]
      Animator anim;



    [SerializeField]
     Animator anim2;

    int a;

    [SerializeField]
     GameObject ItemSelector;




     public   List<GameObject> ItemList = new List<GameObject>();


    [SerializeField]
    int Yspace;

    [SerializeField]
    int Xspace;

    [SerializeField]
    Canvas canvas;



    Transform trans;


    int o = 0;



    float cliplenght;

    float tiempo;

    public RenderTexture CamaraTexture;
    public Camera Camara;
    public List<Texture2D> Textures = new List<Texture2D>();
    string path1 = @"C:\Users\ElConchesumadre\Documents";

    void Start()
    {
      

        trans = GetComponent<Transform>();


        displaytextCuerpo.transform.position = new Vector2(0,250);

        displaytextCabecera.transform.position = new Vector2(0, 250);


    }


    public void Preview()
    {
        canvas.enabled = false;

        tiempo = 0;

        

        StartCoroutine(preview());
    }

    void DetenerRutinas()
    {
        StopAllCoroutines();
    }

    public void AgregarDropDown()
    {
        o++;
        GameObject item = Instantiate(ItemSelector, new Vector2(0, 0), Quaternion.identity) as GameObject;


        item.transform.SetParent(trans.transform);
        item.transform.localScale = new Vector3(1, 1, 1);
        item.transform.position = new Vector2(trans.position.x, trans.position.y - (o * Yspace));



        ItemList.Add(item);



    }


    IEnumerator preview()
    {
        foreach (GameObject scene in ItemList)
        {
            anim.SetInteger("Anim", scene.transform.GetChild(0).GetComponent<Dropdown>().value);
            cliplenght = anim.GetComponent<Animation>().clip.length;
            displaytextCuerpo.transform.position = scene.GetComponent<Item>().posiciontextoCuerpo;
            displaytextCuerpo.rectTransform.sizeDelta = scene.GetComponent<Item>().textWidthHeightCuerpo;
            displaytextCuerpo.text = scene.transform.GetChild(2).GetComponent<InputField>().text;

            displaytextCabecera.transform.position = scene.GetComponent<Item>().posiciontextoCabecera;
            displaytextCabecera.rectTransform.sizeDelta = scene.GetComponent<Item>().textWidthHeightCabecera;
            displaytextCabecera.text = scene.transform.GetChild(3).GetComponent<InputField>().text;

            Debug.Log(cliplenght);
            yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime * anim.GetComponent<Animation>().clip.length >= cliplenght - 0.2);
            yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime * anim.GetComponent<Animation>().clip.length >= cliplenght - 0.5);
            anim2.Play("Idle");
            anim2.SetInteger("Anim", scene.transform.GetChild(1).GetComponent<Dropdown>().value);
            yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime * anim.GetComponent<Animation>().clip.length >= cliplenght);
            displaytextCuerpo.transform.position = new Vector2(0, 25);
            anim.SetTrigger("Idle");
        }
        canvas.enabled = true;
     
        anim.SetInteger("Anim", 30);
        anim.Play("Idle");
        yield return null;


    }

    public void Grabar()
    {
        canvas.enabled = false;
        Application.targetFrameRate = 24;
        foreach (GameObject scene in ItemList)
        {
            if (scene.transform.GetChild(0).GetComponent<Dropdown>().value >= 6)
            {
                tiempo += 15;
            }
            else
            {
                tiempo += 9;
            }
        }

        tiempo = tiempo * 24;

        StartCoroutine(preview());
        StartCoroutine(Record());

    }
    IEnumerator Record()
    {
        Debug.Log("Comenzando Grabacion");



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
            language = "esp"
        };

        int sampleFramesPerVideoFrame = audioAttr.channelCount * audioAttr.sampleRate.numerator / videoAttr.frameRate.numerator;

        var encodedFilePath = Path.Combine(Path.GetFullPath(path1), "Video.mp4");
        for (int i = 0; i <= tiempo; i++)
        {
            Debug.Log("Frame " + i + " de " + tiempo);

            RenderTexture rendText = RenderTexture.active;
            RenderTexture.active = Camara.targetTexture;
            Camara.Render();
            Texture2D tex = new Texture2D((int)Camara.targetTexture.width, (int)Camara.targetTexture.height, TextureFormat.RGBA32, false);
            tex.ReadPixels(new Rect(0, 0, Camara.targetTexture.width, Camara.targetTexture.height), 0, 0);
            tex.Apply();
            Textures.Add(tex);
            yield return new WaitForSeconds(0.030f);
        }

        tiempo = 0;

        using (var encoder = new MediaEncoder(encodedFilePath, videoAttr, audioAttr))
        using (var audioBuffer = new NativeArray<float>(sampleFramesPerVideoFrame, Allocator.Temp))
        {
            Debug.Log("Va a preparar el video");

            foreach (Texture2D tex in Textures)
            {
                encoder.AddFrame(tex);
                encoder.AddSamples(audioBuffer);
            }
            encoder.Dispose();
            Debug.Log("Ligsto");
         
        }
        Application.targetFrameRate = 60;
        canvas.enabled = true;

        Textures.Clear();

        Scene loadedLevel = SceneManager.GetActiveScene();
        SceneManager.LoadScene(loadedLevel.buildIndex);

    }
}

    
