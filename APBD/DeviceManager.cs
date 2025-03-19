namespace APBD;

public class DeviceManager
{
    List<Device> devices = new List<Device>();

    public DeviceManager(string filepath)
    {
        if (File.Exists(filepath)) { 
            string[] lines = File.ReadAllLines(filepath);
            foreach(string ln in lines)
            {
                string[] line;
                line = ln.Split(',');
                if (line[0].StartsWith("SW"))
                {
                    
                }
                else if (line[0].StartsWith("P"))
                {
                    
                }
                else if (line[0].StartsWith("ED"))
                {
                    
                }
                else continue;
            }
        }
        else
        {
            throw new FileNotFoundException("File not found");
        }
        
        
        
        
    }
}