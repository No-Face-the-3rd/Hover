using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;



[System.Serializable]
public class scoreHi
{
    public string name = "";
    public float value = 0.0f;


    public scoreHi(string inName = "", float inValue = 0.0f)
    {
        name = inName;
        value = inValue;
    }

    public static implicit operator string(scoreHi score)
    {
        return score.name + "\t \t \t \t \t \t" + Mathf.FloorToInt(score.value).ToString();
    }
    public static string scoresToString(scoreHi[] scores)
    {
        string ret = "";
        for (int i = 0; i < scores.Length; i++)
        {
            ret += scores[i] + "\n";
        }
        return ret;
    }
}


public class HighScoreManager : MonoBehaviour {

    public static HighScoreManager manager;

    public scoreHi[] scores;

	// Use this for initialization
	void Start () {
        if(manager == null)
        {
            manager = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(manager != this)
        {
            Destroy(gameObject);
        }
        initialize();
        loadScores();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void initialize()
    {
        scores = new scoreHi[10];
        for (int i = 0; i < scores.Length; i++)
        {
            scores[i] = new scoreHi();
        }
    }

    public void loadScores()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/sc.sc", FileMode.OpenOrCreate, FileAccess.ReadWrite);
        scores = (scoreHi[])formatter.Deserialize(file);
        file.Close();
    }

    public void checkScore(string name, float score)
    {
        int index = scores.Length;
        for (int i = scores.Length - 1; i >= 0; i--)
        {
            if (score > scores[i].value)
                index--;
            else
                break;
        }
        if (index < scores.Length)
        {
            for (int i = scores.Length - 1; i >= index + 1; i--)
            {
                scores[i] = scores[i - 1];
            }
            scores[index] = new scoreHi(name, score);
        }
        saveScores();
    }

    public void saveScores()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/sc.sc");
        formatter.Serialize(file, scores);
        file.Close();
    }

    public scoreHi[] getScores()
    {
        return scores;
    }


}
