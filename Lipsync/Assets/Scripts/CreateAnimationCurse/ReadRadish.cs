using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ReadRadish : MonoBehaviour
{
    //This script contains all processes needed to read
    //Radish's phoneme files and convert it into a neat list.
    //
    //If you decided to use other similar program like Radish,
    //you just need to modify this script

    [SerializeField] private List<PhonemeData> phonemeDataList;

    /// <summary>
    /// Start reading text from a directory path
    /// </summary>
    /// <returns>Directory of Radish .phoneme file</returns>
    public List<PhonemeData> StartRead(string path)
    {
        //In case the list is not empty, clear it all out
        phonemeDataList.Clear();

        //Avoid error by checking if the path is blank
        if (!string.IsNullOrEmpty(path))
        {
            ReadTextFile(path);
        }
        else
        {
            Debug.LogError("Phoneme Path is Empty!");
        }

        return phonemeDataList;
    }

    /// <summary>
    /// Start the Input/Output process for .phoneme files
    /// </summary>
    /// <param name="file_path"> The path of the Radish .phoneme file</param>
    private void ReadTextFile(string file_path)
    {
        StreamReader inputStream = new StreamReader(file_path);
        bool hasSkippedMetadata = false;

        while (!inputStream.EndOfStream)
        {
            string inputLine = inputStream.ReadLine();

            //Skip the header metadata of the Radish .phoneme files
            if (hasSkippedMetadata == false)
            {
                //We only start reading after the ---- line
                if (inputLine.Contains("-----"))
                {
                    hasSkippedMetadata = true;
                    continue;
                }
            }
            else
            {
                // Do Something with the input. 
                HandleInputPerLine(inputLine);
            }
        }

        inputStream.Close();
    }

    /// <summary>
    /// Now, we handle the .phoneme file line by line
    /// </summary>
    /// <param name="inputLine"></param>
    private void HandleInputPerLine(string inputLine)
    {
        //Skip ---- line and ;_ field
        //In the .phoneme files, the ---- line separates each word
        //But I dpn't know what ;_ was supposed to mean...

        if (inputLine[0] == '-' || inputLine.Contains(";_"))
        {
            return;
        }

        //Read the Phoneme Symbol. it is usually just two letters long
        PhonemeData temp = new PhonemeData("", 0, 0);
        for (int j = 0; j < 2; j++)
        {
            if (inputLine[j] != ' ' && inputLine[j] != ';')
            {
                temp.phoneme += inputLine[j];
            }
        }

        #region Find the Start timestamp, where this phoneme is spoken
        //Skip forward until we found numbers
        int startNumber = 0;
        int i = 2;
        while (inputLine[i] != '|')
        {
            i++;
            continue;
        }
        i++;

        //Get the number
        string numberString = "";
        while (inputLine[i] != '|')
        {
            if (inputLine[i] != ' ')
            {
                numberString += inputLine[i];
            }
            i++;
        }

        //Convert string to an integer number
        int.TryParse(numberString, out startNumber);
        #endregion

        #region Find the End timestamp, where this phoneme stopped being spoken
        //Skip forward until we found numbers
        int endNumber = 0;
        while (inputLine[i] != '|')
        {
            i++;
            continue;
        }
        i++;

        //Get the number
        numberString = "";
        while (inputLine[i] != '|')
        {
            if (inputLine[i] != ' ')
            {
                numberString += inputLine[i];
            }
            i++;
        }
        i++;

        //Convert string to integer number
        int.TryParse(numberString, out endNumber);
        #endregion

        //Sometimes, there are phonemes that are read by Radish with zero duration
        //Remove any phoneme that has the same start timestamp and end timestamp
        if (startNumber != endNumber)
        {
            //Add to the list
            temp.start = startNumber;
            temp.end = endNumber;
            phonemeDataList.Add(temp);
        }

        //That's what we do in every line of the .phoneme files!
    }
}

[System.Serializable]
public struct PhonemeData
{
    //This phoneme data struct is just to make the data appear tidier

    public string phoneme;
    public int start;
    public int end;

    public PhonemeData(string _phoneme, int _start, int _end)
    {
        phoneme = _phoneme;
        start = _start;
        end = _end;
    }
}