using System;
using UnityEngine;
using UnityEngine.UIElements;

public class UI : MonoBehaviour
{
    private PanelRenderer panelRenderer;
    private int currentVersion = -1;

    private VisualElement mainPage_VisualElement;
    private Label newGame_Label;
    private Label exit_Label;

    private VisualElement gamePage_VisualElement;
    private Label points_Label;


    private void OnEnable()
    {
        panelRenderer = GetComponent<PanelRenderer>();
        panelRenderer.RegisterUIReloadCallback(OnUIReloadCallback);
    }

    private void OnDisable()
    {
        panelRenderer.UnregisterUIReloadCallback(OnUIReloadCallback);

        DisconnectEvents();
        RemoveFunctionality();
    }

    private void OnUIReloadCallback(PanelRenderer panelRenderer, VisualElement root, int version)
    {
        if (currentVersion == version)
            return;
        currentVersion = version;

        mainPage_VisualElement = root.Q<VisualElement>("MainPage_VisualElement");
        newGame_Label = mainPage_VisualElement.Q<Label>("NewGame_Label");
        exit_Label = mainPage_VisualElement.Q<Label>("Exit_Label");

        gamePage_VisualElement = root.Q<VisualElement>("GamePage_VisualElement");
        points_Label = gamePage_VisualElement.Q<Label>("Points_Label");

        AddFunctionality();
        ConnectEvents();

        SetPageActive(mainPage_VisualElement);
    }



    #region Functionality

    private void AddFunctionality()
    {
        newGame_Label.RegisterCallback<ClickEvent>(OnNewGameSelected);
        exit_Label.RegisterCallback<ClickEvent>(OnExitSelected);
    }

    private void RemoveFunctionality()
    {
        newGame_Label.UnregisterCallback<ClickEvent>(OnNewGameSelected);
        exit_Label.UnregisterCallback<ClickEvent>(OnExitSelected);
    }


    private void OnNewGameSelected(ClickEvent clickEvent)
    {
        SetPageActive(gamePage_VisualElement);
        EventsManager.InvokeNewGame_Event();
    }

    private void OnExitSelected(ClickEvent clickEvent)
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    #endregion


    #region Events Manager

    private void ConnectEvents()
    {
        EventsManager.LoseGame_Event += OnLoseGame;
        EventsManager.PointsChanged_Event += OnPointsChanged;
    }

    private void DisconnectEvents()
    {
        EventsManager.LoseGame_Event -= OnLoseGame;
        EventsManager.PointsChanged_Event -= OnPointsChanged;
    }

    private void OnLoseGame()
    {
        SetPageActive(mainPage_VisualElement);
    }

    private void OnPointsChanged()
    {
        points_Label.text = $"Points: {GameData.Points}";
    }

    #endregion



    private void SetPageActive(VisualElement page)
    {
        mainPage_VisualElement.style.display = DisplayStyle.None;
        gamePage_VisualElement.style.display = DisplayStyle.None;

        if (page == null)
        {
            return;
        }

        page.style.display = DisplayStyle.Flex;
    }
}
