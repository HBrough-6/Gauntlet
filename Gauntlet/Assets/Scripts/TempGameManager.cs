using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempGameManager : Singleton<TempGameManager>
{
    private int numLevels = 3;
    LevelManager[] levels;

    public int currentLevel = 1;

    public void addLevel(LevelManager level)
    {
        if (levels == null)
        {
            levels = new LevelManager[numLevels];
        }

        levels[level.LevelNumber - 1] = level;
    }

    // takes players to the specified level
    public void GotoLevel(int levelNum)
    {
        levels[levelNum - 1].EnterLevel();

        levels[currentLevel - 1].ExitLevel();
        currentLevel = levelNum - 1;
    }
}
