using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class AnswersCsvExporter : MonoBehaviour
{
    [SerializeField]
    protected Button BtnDownload;
    [SerializeField]
    protected PopUp DownloadPopUp;
    [SerializeField]
    protected ReportManager Manager;

    [SerializeField]
    protected TMPro.TMP_Text txtTitle;
    [SerializeField]
    protected TMPro.TMP_Text txtFileLocation;
    [SerializeField]
    protected Button BtnCancel;
    [SerializeField]
    protected Button BtnCheck;

    [SerializeField]
    protected RectTransform LoadingSpin;
    [SerializeField]
    protected Image DownloadState;
    [SerializeField]
    protected Sprite DownloadingIcon;
    [SerializeField]
    protected Sprite CompletedIcon;

    private Coroutine exportCoroutine;
    private int spinTweenId = -1;

    protected void Awake()
    {
        if (BtnDownload != null)
        {
            BtnDownload.onClick.AddListener(StartCsvAnswersExport);

            if (DownloadPopUp != null)
                BtnDownload.onClick.AddListener(DownloadPopUp.Open);
        }

        if (BtnCancel != null)
            BtnCancel.onClick.AddListener(CancelExport);
    }

    protected void SwitchButtons(bool isDownloading)
    {
        if (BtnCancel != null)
            BtnCancel.interactable = isDownloading;

        if (BtnCheck != null)
            BtnCheck.interactable = !isDownloading;
    }

    protected void StartSpinner()
    {
        spinTweenId = LeanTween.rotateAround(LoadingSpin.gameObject, Vector3.forward, -360f, 1f)
                                .setLoopClamp()
                                .setEase(LeanTweenType.linear)
                                .id;
    }

    protected void StopSpinner()
    {
        if (LeanTween.isTweening(spinTweenId))
        {
            LeanTween.cancel(spinTweenId);
            spinTweenId = -1;
        }
    }

    public void StartCsvAnswersExport()
    {
        if (Manager == null) return;

        string filePath = FilesManager.GetDefaultFilePathName(FilesManager.CFolders.ANSWERS_CSV);
        StartCsvExport(Manager.Responses, filePath);
    }

    public void StartCsvExport(List<ReportManager.FullResponse> dataList, string filePath)
    {
        exportCoroutine = StartCoroutine(ExportToCsvCoroutine(dataList, filePath));
        if (DownloadState != null) DownloadState.sprite = DownloadingIcon;
        if (txtTitle != null) txtTitle.text = "Descargando archivo...";
        if (txtFileLocation != null) txtFileLocation.text = filePath;
        SwitchButtons(true);
        StartSpinner();
    }

    public void CancelExport()
    {
        if (exportCoroutine != null)
        {
            StopCoroutine(exportCoroutine);
            exportCoroutine = null;
            if (txtTitle != null) txtTitle.text = "Descarga cancelada";
            if (txtFileLocation != null) txtFileLocation.text = "Exportación de CSV cancelado.";
        }

        SwitchButtons(false);
        StopSpinner();
    }

    protected IEnumerator ExportToCsvCoroutine(List<ReportManager.FullResponse> dataList, string filePath)
    {
        StringBuilder csvContent = new StringBuilder();

        // Header
        csvContent.AppendLine("Actividad,Emoción Correcta,Emoción Seleccionada,Es correcto,Fecha de realización,Duración (s)");

        // Data
        var gm = GameManager.Instance;
        Dictionary<bool, string> bs = new Dictionary<bool, string> { { true, "VERDADERO" }, { false, "FALSO" } };
        foreach (var data in dataList)
        {
            csvContent.AppendLine($"" +
                $"{gm.Games[data.exercise.ActivityId].Name}," +
                $"{gm.Emotions[data.idCont.emotion].Name}," +
                $"{gm.Emotions[data.response.ResponseEmotionId].Name}," +
                $"{bs[data.response.IsCorrect]}," +
                $"{data.response.CompletedAt.ToLocalTime()}," +
                $"{data.response.SecondsToSolve}");
            yield return null; // Yield per line in case of large lists
        }

        // Save file
        File.WriteAllText(filePath, csvContent.ToString());

        exportCoroutine = null; // Clear coroutine reference
        if (DownloadState != null) DownloadState.sprite = CompletedIcon;
        if (txtTitle != null) txtTitle.text = "Descarga completa";
        if (txtFileLocation != null) txtFileLocation.text = filePath;
        SwitchButtons(false);
        StopSpinner();
    }
}
