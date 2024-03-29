
using UnityEngine;
using System;

public class AdapterData
{
    public static void GetPlayer(PlayerData data, ref Player player)
    {
        player.transform.position = data.position;
        player.SetAmountBlock(data.amountBlock);
    }
    public static PlayerData GetPlayerData(Player player)
    {
        PlayerData data= new PlayerData();
        data.position= player.transform.position;
        data.amountBlock= player.GetAmountBolock();
        return data;
    }
     public static void GetBlock(BlockData data, ref Block block)
    {
        block.transform.position = data.position;
    }
    public static BlockData GetBlockData(Block block)
    {
        BlockData data= new BlockData();
        data.position= block.transform.position;
        return data;
    }

}

[Serializable]
public class PlayerData
{
    public Vector3 position;
    public int  amountBlock;
}

[Serializable]
public class BlockData
{
    public Vector3 position;
}
[Serializable]
public class SceneData
{
    public PlayerData player;
    public BlockData[] blocks;


}

