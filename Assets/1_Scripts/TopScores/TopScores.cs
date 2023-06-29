using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "High Scores", menuName = "Scores")]
public class TopScores : ScriptableObject
{
    [SerializeField] private int _scoresCount;
    [SerializeField] private List<int> _scores;

    public List<int> Scores
    {
        get
        {
            _scores.Sort();
            _scores.Reverse();
            
            while(_scores.Count < _scoresCount) _scores.Add(0);
            while (_scores.Count > _scoresCount) _scores.RemoveAt(_scores.Count - 1);
            
            return _scores;
        }
    }
}
