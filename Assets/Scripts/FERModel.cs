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
    private Image emocionSprite;
    [SerializeField]
    private RawImage faceImage;
    [SerializeField]
    private Material grayMaterial;
    [SerializeField]
    private ComputeShader compute;
    [SerializeField, FilePopup("*.tflite")]
    private string filePath = "FerModel.tflite";

    private Interpreter interpreter;
    private ComputeBuffer inputBuffer;
    private float[] input = new float[48 * 48];
    private float[] output = new float[7];
    private byte[] modelFile;
    private string[] emotions = { "Enojo", "Disgusto", "Miedo", "Feliz", "Neutral", "Triste", "Sorpresa" };

    private void Awake()
    {
        faceImage.material = grayMaterial;
        modelFile = FileUtil.LoadFile(filePath);

        var options = new InterpreterOptions()
        {
            threads = 2,
        };
        interpreter = new Interpreter(modelFile, options);
        interpreter.AllocateTensors();
        inputBuffer = new ComputeBuffer(48 * 48, sizeof(float));
    }

    public void ChangeSprite(Texture2D texture)
    {
        faceImage.texture = texture;
        ExecuteModel();
    }

    public void ExecuteModel()
    {
        compute.SetTexture(0, "InputTexture", faceImage.texture);
        compute.SetBuffer(0, "OutputTensor", inputBuffer);
        compute.Dispatch(0, 48 / 4, 48 / 4, 1);
        inputBuffer.GetData(input);

        float startTime = Time.realtimeSinceStartup;
        interpreter.SetInputTensorData(0, input);
        interpreter.Invoke();
        interpreter.GetOutputTensorData(0, output);
        float finishTime = Time.realtimeSinceStartup;

        float maxValue = output.Max();
        int maxIndex = output.ToList().IndexOf(maxValue);

        emocionText.text = emotions[maxIndex];
        emocionSprite.sprite = GameManager.Instance.GetEmotionSprites()[maxIndex];
    }

    private void OnDestroy()
    {
        interpreter?.Dispose();
        inputBuffer?.Dispose();
    }
}
