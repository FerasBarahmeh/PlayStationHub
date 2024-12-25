namespace PlayStationHub.Utilities.Interfaces.BasicOperation.Sync;

public interface IIsExist
{
    bool IsExist(int ID);
}

public interface IIsExistByContent
{
    bool IsExist(string content);
}

