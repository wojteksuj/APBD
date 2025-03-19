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
                if (line[0].StartsWith("SW-"))
                {
                    char lastChar = line[0][^1];
                    if (int.TryParse(lastChar.ToString(), out int result))
                    {
                        int id = result;
                        string name = line[1];
                        bool isTurnedOn = bool.Parse(line[2]);
                        int battery = int.Parse(line[3].Replace("%",""));
                        Device newSw = new Smartwatch(id, name, isTurnedOn, battery);
                        if(devices.Count < 15) devices.Add(newSw);
                        else throw new Exception("Too many devices");
                    }
                    else
                    {
                        throw new Exception("Wrong input format");
                    }
                }
                else if (line[0].StartsWith("P-"))
                {
                    char lastChar = line[0][^1];
                    if (int.TryParse(lastChar.ToString(), out int result))
                    {
                        int id = result;
                        string name = line[1];
                        bool isTurnedOn = bool.Parse(line[2]);
                        string system = null;
                        if (line.Length > 3) system = line[3];
                        Device newPc = new PersonalComputer(id, name, isTurnedOn, system);
                        if(devices.Count < 15) devices.Add(newPc);
                        else throw new Exception("Too many devices");
                    }
                    else
                    {
                        throw new Exception("Wrong input format");
                    }
                }
                else if (line[0].StartsWith("ED-"))
                {
                    char lastChar = line[0][^1];
                    if (int.TryParse(lastChar.ToString(), out int result))
                    {
                        int id = result;
                        string name = line[1];
                        string ipAddress = line[2];
                        string networkName = line[3];
                        Device newEd = new EmbeddedDevices(id, name, false, ipAddress, networkName);
                        if(devices.Count < 15) devices.Add(newEd);
                        else throw new Exception("Too many devices");
                    }
                    else
                    {
                        throw new Exception("Wrong input format");
                    }
                    
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