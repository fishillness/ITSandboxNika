using System;
using UnityEngine;
using UnityEngine.Events;

public class Match3LevelManager : MonoBehaviour
{
    public UnityEvent<int> OnLevelUp;

    [Serializable]
    public class LevelData
    {
        public int CurrentLevel;
    }

    [SerializeField] private LevelList levelList;

    private int currentLevel;

    public LevelList LevelList => levelList;
    public int CurrentLevel => currentLevel;
    public LevelInfo CurrentLevelInfo => levelList.Levels[currentLevel];
    public bool HaveUnCompletedLevels => currentLevel < levelList.Levels.Length;

    private void Awake()
    {
        LoadLevelData();
    }

    public void LevelUp()
    {
        Debug.Log("level up");
        if (!HaveUnCompletedLevels) return;  

        currentLevel++;

        if (!HaveUnCompletedLevels && levelList.Replayable)
            currentLevel = 0;

        OnLevelUp?.Invoke(currentLevel);

        SaveLevelData();
    }

    public void SetLevel(int number)
    {
        if (number < 0) number = 0;
        if (number >= levelList.Levels.Length) number = levelList.Levels.Length - 1;

        currentLevel = number;
        SaveLevelData();
    }

    private void SaveLevelData()
    {
        LevelData levelData = new LevelData();

        levelData.CurrentLevel = currentLevel;
        
        Saver<LevelData>.Save(SaverFilenames.LevelFilaname, levelData);
    }

    private void LoadLevelData()
    {
        LevelData levelData = new LevelData();

        if (Saver<LevelData>.TryLoad(SaverFilenames.LevelFilaname, ref levelData) == false)
        {
            currentLevel = 0;
        }
        else
        {
            currentLevel = levelData.CurrentLevel;
        }
    }

    public int GetNextLevelNumber()
    {
        if (!HaveUnCompletedLevels)
        {
            if (!levelList.Replayable)
                return -1;
            else
                return 0;
        }

        return currentLevel + 1;
    }
}
