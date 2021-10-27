using System.Collections.Generic;

[System.Serializable]
public class User
{
    public int sellCount;
    public int coin;

    public List<Shovel> shovels;

    public Shovel userShovel;
}