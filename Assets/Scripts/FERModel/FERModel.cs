using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TensorFlowLite;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FERModel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI emocionText;
    [SerializeField]
    private Image emocionSpriteColor;
    [SerializeField]
    private ComputeShader compute;
    [SerializeField, FilePopup("*.tflite")]
    private string filePath = "FerModel.tflite";
    [SerializeField]
    private Image smallEmocionSprite;

    private Interpreter interpreter;
    private ComputeBuffer inputBuffer;
    private float[] input = new float[48 * 48];
    private float[] output = new float[7];
    private byte[] modelFile;
    private string[] emotions = { "Enojo", "Disgusto", "Miedo", "Feliz", "Neutral", "Triste", "Sorpresa" };

    private bool canUseGPU = false;

    [field: SerializeField]
    public Emotion.EEmotion PredictedEmotion { get; private set; }

    private void Awake()
    {
        ValidateGPU();
        modelFile = FileUtil.LoadFile(filePath);

        var options = new InterpreterOptions()
        {
            threads = 2,
        };
        interpreter = new Interpreter(modelFile, options);
        interpreter.AllocateTensors();
        inputBuffer = new ComputeBuffer(48 * 48, sizeof(float));
    }

    protected void ValidateGPU()
    {
        canUseGPU = false;

        if (SystemInfo.supportsComputeShaders)
        {
            Debug.Log("FERModel::ValidateGPU supportsComputeShaders " + SystemInfo.graphicsDeviceName);
            //canUseGPU = true;
        }

#if UNITY_EDITOR
#endif
        Debug.Log("FERModel::ValidateGPU graphicsDeviceType " + SystemInfo.graphicsDeviceType);
        if (SystemInfo.graphicsDeviceType != UnityEngine.Rendering.GraphicsDeviceType.Null)
        {
            //if (SystemInfo.graphicsDeviceType)
            UnityEngine.Rendering.GraphicsDeviceType[] unSupportedGDT =
            {
                UnityEngine.Rendering.GraphicsDeviceType.Direct3D11,
                //UnityEngine.Rendering.GraphicsDeviceType.Direct3D12,
                //UnityEngine.Rendering.GraphicsDeviceType.Vulkan,
                //UnityEngine.Rendering.GraphicsDeviceType.Metal,
            };
            if (!System.Array.Exists(unSupportedGDT, a => a == SystemInfo.graphicsDeviceType))
            {
                Debug.Log("FERModel::ValidateGPU Dedicated or capable integrated GPU detected");
                canUseGPU = true;
            }
            else
            {
                Debug.Log("FERModel::ValidateGPU Software rendering or limited GPU detected");
            }
        }
    }

    //public void ChangeSprite(Texture2D texture)
    //{
    //    ExecuteModel();
    //}

    public void Execute(RawImage faceImage)
    {
        if (canUseGPU)
        {
            compute.SetTexture(0, "InputTexture", faceImage.texture);
            compute.SetBuffer(0, "OutputTensor", inputBuffer);
            compute.Dispatch(0, 48 / 4, 48 / 4, 1);
            inputBuffer.GetData(input);
        }
        else
        {
            //Debug.Log("FERModel::Execute executing without valid GPU");
            input = TextureToBuffer(faceImage);
        }

        float startTime = Time.realtimeSinceStartup;
        interpreter.SetInputTensorData(0, input);
        interpreter.Invoke();
        interpreter.GetOutputTensorData(0, output);
        float finishTime = Time.realtimeSinceStartup;

        float maxValue = output.Max();
        int maxIndex = output.ToList().IndexOf(maxValue);

        var gm = GameManager.Instance;
        PredictedEmotion = (Emotion.EEmotion)maxIndex;
        emocionText.text = gm.Emotions[PredictedEmotion].Name;
        emocionSpriteColor.sprite = gm.Emotions[PredictedEmotion].SpriteColor;
        if (smallEmocionSprite != null)
            smallEmocionSprite.sprite = emocionSpriteColor.sprite;

        //Debug.Log("FERModel::Execute t " + (finishTime - startTime) + " \te " + PredictedEmotion + " \tmv " + maxValue + " - mi" + maxIndex);
    }

    private void OnDestroy()
    {
        interpreter?.Dispose();
        inputBuffer?.Dispose();
    }

    protected float[] TextureToBuffer(RawImage rawImage)
    {
        int w = rawImage.texture.width;
        int h = rawImage.texture.height;

        Texture2D texture = rawImage.texture as Texture2D;
        Color[] pixels = texture.GetPixels(); // or GetPixels32() if preferred
        float[] tensor = new float[w * h];

        for (int y = 0; y < h; y++)
        {
            for (int x = 0; x < w; x++)
            {
                int idx = (h - 1 - y) * w + x;
                Color pixel = pixels[idx];

                tensor[idx] = pixel.r; //grayscale
            }
        }
        //Debug.Log(texture + " \twxh " + (w * h) + " \tc" + pixels[w * h / 2] + " \tt" + tensor[w * h / 2]);
        return tensor;
    }
}
