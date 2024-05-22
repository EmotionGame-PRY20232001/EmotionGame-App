using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance { get { return _instance; } }

    [SerializeField]
    private GameObject UICanvas;
    [SerializeField]
    private GameObject newPlayerTemplate;
    [SerializeField]
    private GameObject scrollViewPrefab;
    [SerializeField]
    private GameObject newPlayerButton;
    [SerializeField]
    private GameObject newPlayerButton2;
    [SerializedDictionary("Name", "Game Object")]
    public SerializedDictionary<PanelScript.PanelType, GameObject> templates;

    private GameObject scrollViewRef;
    private GameObject newPlayerButtonRef;
    private GameObject newPlayerButtonRef2;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        RefreshSelectPlayerMenu();
    }

    public void ShowPanelTemplate(PanelScript.PanelType panelType, Player player = null)
    {
        var template = templates[panelType];
        if (template == null) { return; }

        template.SetActive(true);
        if (panelType == PanelScript.PanelType.EditPlayer ||
            panelType == PanelScript.PanelType.DeletePlayer ||
            panelType == PanelScript.PanelType.ChooseEmotions)
        {
            template.GetComponent<PanelScript>().GetPlayerInfo(player);
        }
    }

    public void ClosePanel(PanelScript.PanelType panelType)
    {
        var template = templates[panelType];
        if (template == null) { return; }
        template.GetComponent<PopUp>().Close();
    }

    public void RefreshSelectPlayerMenu()
    {
        EraseObjects();
        var players = DBManager.Instance.GetPlayersFromDb();
        if (players.Count > 0)
        {
            scrollViewRef = Instantiate(scrollViewPrefab, UICanvas.transform);
            scrollViewRef.GetComponent<ScrollView>().ShowPlayers(players);
            newPlayerButtonRef2 = Instantiate(newPlayerButton2, UICanvas.transform);
            return;
        }
        newPlayerButtonRef = Instantiate(newPlayerButton, UICanvas.transform);
    }

    private void EraseObjects()
    {
        Destroy(scrollViewRef);
        Destroy(newPlayerButtonRef);
        Destroy(newPlayerButtonRef2);
    }
}
