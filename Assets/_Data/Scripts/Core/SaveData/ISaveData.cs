
public interface ISaveData
{
    public T GetData<T>();
    public void SetData<T>(T data);
    public void LoadData();
}