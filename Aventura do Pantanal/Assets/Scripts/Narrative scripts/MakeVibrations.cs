using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeVibrations : MonoBehaviour
{
    public void PlayVibration()
    {
        long[] pattern = { 0, 50, 50, 50 };
        Vibrator.Vibrate(pattern, -1);
    }
    public void PlayVibrationpattern1234repeatSeparadoPorVirgula(string pattern1234repeat)
    {
        //int repeat = int.Parse(stringRepeat);
        long[] patternsSlipted = new long[5];
        patternsSlipted[0] = int.Parse(splitPatternRepeat(pattern1234repeat.Split(','), 0));
        patternsSlipted[1] = int.Parse(splitPatternRepeat(pattern1234repeat.Split(','), 1));
        patternsSlipted[2] = int.Parse(splitPatternRepeat(pattern1234repeat.Split(','), 2));
        patternsSlipted[3] = int.Parse(splitPatternRepeat(pattern1234repeat.Split(','), 3));
        patternsSlipted[4] = int.Parse(splitPatternRepeat(pattern1234repeat.Split(','), 4));

        long[] pattern = { patternsSlipted[0], patternsSlipted[1], patternsSlipted[2], patternsSlipted[3]};
        Vibrator.Vibrate(pattern, (int)patternsSlipted[4]);
    }

    /*public void PlayVibration(long milliseconds = 250)
       {
           Vibrator.Vibrate(milliseconds);
       }*/

    public void StopVibration()
    {
        Vibrator.Cancel();
    }

    private string splitPatternRepeat(string[] pattern1234repeat, int indice)
    {
        string patternOrRepeat = pattern1234repeat[indice];

        return patternOrRepeat;
    }
}
