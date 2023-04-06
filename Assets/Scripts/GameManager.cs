using System;
using Configs;
using Events;
using Interfaces;
using Models;
using Services;
using Services.FileServices;
using SimpleEventBus.Disposables;
using UnityEngine;

public class GameManager : MonoBehaviour, IDisposable
{
    [SerializeField] private IncomeService _incomeService;
    [SerializeField] private ConfigService _configService;
    [SerializeField] private BusinessService _businessService;

    private ISaveFileSystem _saveFileSystem;
    private ILoadFileSystem _loadFileSystem;
    private IDeleteFileSystem _deleteFileSystem;
    private StartBalanceConfig _balanceConfig;

    private CompositeDisposable _subscriptions;

    public void Dispose()
    {
        _subscriptions?.Dispose();
    }
    
    private void Awake()
    {
        InitializeFileSystems();

        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<SaveGameEvent>(InitializeSaveGame),
            EventStreams.Game.Subscribe<NewGameEvent>(InitializeNewGame)
        };

        StartGame();
    }

    private void StartGame()
    {
        var saveData = _loadFileSystem.Load();
        BusinessModel[] models;

        if (saveData == null)
        {
            _incomeService.Initialize(0);
            _configService.Initialize();
            models = _configService.GetBusinessModels();
        }
        else
        {
            models = saveData.BusinessModels;
            _configService.Initialize(models);
            _incomeService.Initialize(saveData.Balance);
        }

        _businessService.Initialize(models);
    }

    private void InitializeFileSystems()
    {
        _saveFileSystem = new JsonFileFileService();
        _loadFileSystem = new JsonFileFileService();
        _deleteFileSystem = new JsonFileFileService();
    }
    
    private void InitializeSaveGame(SaveGameEvent eventData)
    {
        var businessModels = _configService.GetBusinessModels();
        var playerBalance = _incomeService.GetPlayerBalance();
        var saveData = new SaveData(playerBalance, businessModels);
        _saveFileSystem.Save(saveData);
    }

    private void InitializeNewGame(NewGameEvent eventData)
    {
        _deleteFileSystem.Delete();
        _businessService.ResetControllers();
        StartGame();
    }
}