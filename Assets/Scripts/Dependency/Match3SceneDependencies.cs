using UnityEngine;

public class Match3SceneDependencies : MonoBehaviour
{
    [SerializeField] private PieceCounter pieceCounter;
    [SerializeField] private PieceColorDictionary pieceColorDictionary;
    [SerializeField] private BoosterDictionary boosterDictionary;
    [SerializeField] private SpecifierRequiredPiecesOfType specifierRequiredPiecesOfType;
    [SerializeField] private SpecifierRequiredBooster specifierRequiredBooster;
    [SerializeField] private UIMatch3LevelPanel levelPanel;
    [SerializeField] private TaskInfoCounter taskInfoCounter;

    [SerializeField] private PieceMatrixController matrixController;
    [SerializeField] private PiecesSpawnerController spawnerController;
    [SerializeField] private Match3Level level;

    [SerializeField] private FieldController fieldController;
    [SerializeField] private PieceControl pieceControl;

    private void Awake()
    {
        MonoBehaviour[] monoInScene = FindObjectsOfType<MonoBehaviour>();  
        
        for (int i = 0; i < monoInScene.Length; i++)
        {
            Bind(monoInScene[i]);
        }
    }

    private void Bind(MonoBehaviour mono)
    {
        (mono as IDependency<PieceCounter>)?.Construct(pieceCounter);
        (mono as IDependency<PieceColorDictionary>)?.Construct(pieceColorDictionary);
        (mono as IDependency<BoosterDictionary>)?.Construct(boosterDictionary);
        (mono as IDependency<SpecifierRequiredPiecesOfType>)?.Construct(specifierRequiredPiecesOfType);
        (mono as IDependency<SpecifierRequiredBooster>)?.Construct(specifierRequiredBooster);
        (mono as IDependency<UIMatch3LevelPanel>)?.Construct(levelPanel);
        (mono as IDependency<TaskInfoCounter>)?.Construct(taskInfoCounter);

        (mono as IDependency<PieceMatrixController>)?.Construct(matrixController);
        (mono as IDependency<PiecesSpawnerController>)?.Construct(spawnerController);
        (mono as IDependency<Match3Level>)?.Construct(level);
        (mono as IDependency<FieldController>)?.Construct(fieldController);
        (mono as IDependency<PieceControl>)?.Construct(pieceControl);
    }
}
