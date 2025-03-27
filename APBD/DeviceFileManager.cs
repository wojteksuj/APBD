namespace APBD;

public class DeviceFileManager
{
    public string FilePath;

    public string getFilePath()
    {
        return FilePath;
    }

    public DeviceFileManager(string filePath)
    {
        if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
        {
            throw new FileNotFoundException("File not found");
        }
        FilePath = filePath;
    }

    public List<string[]> ReadDeviceData()
    {
        try
        {
            return File.ReadAllLines(FilePath).Select(line => line.Split(',')).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return new List<string[]>();
        }
    }

    public void SaveDeviceData(List<Device> devices)
    {
        try
        {
            foreach (var device in devices)
            {
                string line = device.ToString() + '\n';
                File.AppendAllText(FilePath, line);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}