using Common.Core.Table.Util;
using Zenject;

public class TableLoader : IInitializable
{
    public void Initialize()
    {
        TableReader.Read("Assets/Resources/Table/");
    }
}