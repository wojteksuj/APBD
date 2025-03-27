using System.Linq.Expressions;
using APBD.Factories;

namespace APBD;

public class DeviceManager
{
    public List<Device> devices = new List<Device>();
    private DeviceFactory swFactory = new SmartwatchFactory();
    private DeviceFactory pcFactory = new PersonalComputerFactory();
    private DeviceFactory edFactory = new EmbeddedDeviceFactory();
    

    public DeviceManager(string filepath)
    {
        try
        {
            if (File.Exists(filepath))
            {
                string[] lines = File.ReadAllLines(filepath);
                foreach (string ln in lines)
                {
                    string[] line;
                    line = ln.Split(',');
                    if (line[0].StartsWith("SW-"))
                    {
                        string id = line[0];
                        string name = line[1];
                        bool isTurnedOn = bool.Parse(line[2]);
                        int battery = int.Parse(line[3].Replace("%", ""));
                        Device newSw = swFactory.CreateDevice(id, name, isTurnedOn, battery);
                        if (devices.Count < 15) devices.Add(newSw);
                        else throw new Exception("Too many devices");
                    }
                    else if (line[0].StartsWith("P-"))
                    {
                        string id = line[0];
                        string name = line[1];
                        bool isTurnedOn = bool.Parse(line[2]);
                        string system = null;
                        if (line.Length > 3) system = line[3];
                        Device newPc = pcFactory.CreateDevice(id, name, isTurnedOn, system: system);
                        if (devices.Count < 15) devices.Add(newPc);
                        else throw new Exception("Too many devices");
                    }
                    else if (line[0].StartsWith("ED-"))
                    {
                        string id = line[0];
                        
                            string name = line[1];
                            string ipAddress = line[2];
                            string networkName = line[3];
                            Device newEd = edFactory.CreateDevice(id, name, isTurnedOn: false, ipAddress: ipAddress, networkName: networkName);
                            if (devices.Count < 15) devices.Add(newEd);
                            else throw new Exception("Too many devices");
                        
                    }
                    else continue;
                }
            }
            else
            {
                throw new FileNotFoundException("File not found");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void showAllDevices()
    {
        foreach (Device device in devices)
        {
            Console.WriteLine(device.ToString());
        }
    }

    public void addDevice(Device device)
    {
        try
        {
            if (devices.Count < 15) devices.Add(device);
            else throw new Exception("Too many devices");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void removeDevice(Device device)
    {
        try
        {
            if (devices.Contains(device)) devices.Remove(device);
            else throw new Exception("Device not found");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void turnOnDevice(Device device)
    {
        device.turnOn();
    }

    public void turnOffDevice(Device device)
    {
        device.turnOff();
    }

    public void saveDataToFile(string filepath)
    {
        try
        {
            if (File.Exists(filepath))
            {
                foreach (Device device in devices)
                {
                    string line = device.ToString() + '\n';
                    File.AppendAllText(filepath, line);
                }
            }
            else
            {
                throw new FileNotFoundException("File not found");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void editDeviceData(Device device, string newName = null, bool? isTurnedOn = null, int? battery = null,
        string newSystem = null, string newIpAddress = null, string newNetworkName = null)
    {
        try
        {
            if (!devices.Contains(device))
            {
                throw new Exception("Device not found in the list");
            }

            if (!string.IsNullOrEmpty(newName))
            {
                device.Name = newName;
            }

            if (isTurnedOn.HasValue)
            {
                if (isTurnedOn.Value)
                    device.turnOn();
                else
                    device.turnOff();
            }

            if (device is Smartwatch smartwatch && battery.HasValue)
            {
                if (battery < 0 || battery > 100)
                    throw new ArgumentOutOfRangeException("Battery must be between 0 and 100");
                smartwatch.battery = battery.Value;
            }
            else if (device is PersonalComputer pc && !string.IsNullOrEmpty(newSystem))
            {
                pc.system = newSystem;
            }
            else if (device is EmbeddedDevices ed)
            {
                if (!string.IsNullOrEmpty(newIpAddress))
                    ed.IpAddress = newIpAddress;

                if (!string.IsNullOrEmpty(newNetworkName))
                    ed.NetworkName = newNetworkName;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}