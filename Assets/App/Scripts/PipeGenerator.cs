using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PipeGenerator : MonoBehaviour
{
    [SerializeField] private GameObject pipePrefab;

    private List<Pipe> pipes;


    private void OnEnable()
    {
        pipes = new List<Pipe>();

        ConnectEvents();
    }

    private void OnDisable()
    {
        DisconnectEvents();
    }


    private async void StartGeneratingPipes()
    {
        while (GameData.IsPlaying)
        {
            Pipe pipe = Instantiate(pipePrefab, new Vector3(300f, 0f, 0f), Quaternion.identity, transform).
                                    GetComponent<Pipe>();
            pipe.InitializePipe();

            pipes.Add(pipe);

            await Awaitable.WaitForSecondsAsync(1f);
        }

        foreach (Pipe pipe in pipes)
        {
            if (pipe != null)
                Destroy(pipe.gameObject);
        }
        pipes.Clear();
    }



    #region Events Manager

    private void ConnectEvents()
    {
        EventsManager.NewGame_Event += OnNewGame;
    }

    private void DisconnectEvents()
    {
        EventsManager.NewGame_Event -= OnNewGame;
    }

    private void OnNewGame()
    {
        StartGeneratingPipes();
    }


    #endregion

}
