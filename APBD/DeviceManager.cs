using System.Linq.Expressions;
using APBD.Factories;

namespace APBD;

public class DeviceManager
{
    public readonly List<Device> devices = new();
    private readonly DeviceFileManager fileManager;
    private readonly Dictionary<string, DeviceFactory> factories = new();

    public DeviceManager(DeviceFileManager fileManager, Dictionary<string, DeviceFactory> factories)
    {
        this.fileManager = fileManager;
        this.factories = factories;

        InitializeDevices();
    }

    private void InitializeDevices()
    {
        var lines = fileManager.ReadDeviceData();
        foreach (var line in lines)
        {
            string id = line[0];
            string prefix = id.Split('-')[0];

            if (factories.ContainsKey(prefix))
            {
                try
                {
                    var factory = factories[prefix];
                    Device device = CreateDeviceFromData(line, factory);
                    if (devices.Count < 15)
                        devices.Add(device);
                    else
                        throw new Exception("Too many devices");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error creating device: {e.Message}");
                }
            }
        }
    }

    private Device CreateDeviceFromData(string[] data, DeviceFactory factory)
    {
        string id = data[0];
        string name = data[1];
        bool isTurnedOn = bool.Parse(data[2]);

        switch (id.Split('-')[0])
        {
            case "SW": // Smartwatch
                int battery = int.Parse(data[3].Replace("%", ""));
                return factory.CreateDevice(id, name, isTurnedOn, battery);
            case "P": // PersonalComputer
                string system = data.Length > 3 ? data[3] : null;
                return factory.CreateDevice(id, name, isTurnedOn, system: system);
            case "ED": // EmbeddedDevice
                string ipAddress = data[2];
                string networkName = data[3];
                return factory.CreateDevice(id, name, false, ipAddress: ipAddress, networkName: networkName);
            default:
                throw new ArgumentException("Unknown device type");
        }
    }

    public void ShowAllDevices()
    {
        foreach (var device in devices)
        {
            Console.WriteLine(device.ToString());
        }
    }

    public void AddDevice(Device device)
    {
        if (devices.Count < 15) devices.Add(device);
        else Console.WriteLine("Too many devices");
    }

    public void RemoveDevice(Device device)
    {
        if (devices.Contains(device)) devices.Remove(device);
        else Console.WriteLine("Device not found");
    }

    public void TurnOnDevice(Device device)
    {
        device.turnOn();
    }

    public void TurnOffDevice(Device device)
    {
        device.turnOff();
    }

    public void SaveDataToFile()
    {
        fileManager.SaveDeviceData(devices);
    }

    public void EditDeviceData(Device device, string newName = null, bool? isTurnedOn = null, int? battery = null, 
        string newSystem = null, string newIpAddress = null, string newNetworkName = null)
    {
        try
        {
            if (!devices.Contains(device))
                throw new Exception("Device not found");

            if (!string.IsNullOrEmpty(newName))
                device.Name = newName;

            if (isTurnedOn.HasValue)
            {
                if (isTurnedOn.Value) device.turnOn();
                else device.turnOff();
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
                if (!string.IsNullOrEmpty(newIpAddress)) ed.IpAddress = newIpAddress;
                if (!string.IsNullOrEmpty(newNetworkName)) ed.NetworkName = newNetworkName;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
