using System;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    [SerializeField] private GameObject BirdGameObject;


    private void OnEnable()
    {
        ConnectEvents();
    }

    private void OnDisable()
    {
        DisconnectEvents();
    }


    #region Events Manager

    private void ConnectEvents()
    {
        EventsManager.NewGame_Event += OnNewGame;
        EventsManager.LoseGame_Event += OnLoseGame;
    }

    private void DisconnectEvents()
    {
        EventsManager.NewGame_Event -= OnNewGame;
        EventsManager.LoseGame_Event -= OnLoseGame;
    }

    private void OnNewGame()
    {
        BirdGameObject.transform.localPosition = new Vector3(-15f, 0f, 0f);
        BirdGameObject.transform.localRotation = Quaternion.identity;
        //await Awaitable.WaitForSecondsAsync(3f);
        GameData.IsPlaying = true;
        BirdGameObject.SetActive(true);
        BirdGameObject.GetComponent<Rigidbody2D>().gravityScale = 5f;
        GameData.Points = 0;
    }

    private async void OnLoseGame()
    {
        GameData.IsPlaying = false;
        //await Awaitable.WaitForSecondsAsync(3f);
        await Awaitable.WaitForSecondsAsync(1f);
        BirdGameObject.SetActive(false);
        GameData.Points = 0;
    }

    #endregion
}
