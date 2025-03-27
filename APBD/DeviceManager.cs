using System.Linq.Expressions;
using APBD.Factories;

namespace APBD
{
    /// <summary>
    /// Manages a collection of devices, allowing for device creation, modification, and storage.
    /// </summary>
    public class DeviceManager
    {
        /// <summary>
        /// A list of devices managed by the device manager.
        /// </summary>
        public readonly List<Device> devices = new();

        private readonly DeviceFileManager fileManager;
        private readonly Dictionary<string, DeviceFactory> factories = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceManager"/> class with file and factory dependencies.
        /// </summary>
        /// <param name="fileManager">The file manager used to handle device data.</param>
        /// <param name="factories">A dictionary mapping device prefixes to corresponding factories.</param>
        public DeviceManager(DeviceFileManager fileManager, Dictionary<string, DeviceFactory> factories)
        {
            this.fileManager = fileManager;
            this.factories = factories;

            InitializeDevices();
        }

        /// <summary>
        /// Initializes the device list from the file data, using appropriate factories for device creation.
        /// </summary>
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

        /// <summary>
        /// Creates a device from provided data using the specified factory.
        /// </summary>
        /// <param name="data">Array of string data that contains device details.</param>
        /// <param name="factory">The factory to be used for creating the device.</param>
        /// <returns>The created device.</returns>
        /// <exception cref="ArgumentException">Thrown when an unknown device type is encountered.</exception>
        private Device CreateDeviceFromData(string[] data, DeviceFactory factory)
        {
            string id = data[0];
            string name = data[1];
            bool isTurnedOn = bool.Parse(data[2]);

            switch (id.Split('-')[0])
            {
                case "SW":
                    int battery = int.Parse(data[3].Replace("%", ""));
                    return factory.CreateDevice(id, name, isTurnedOn, battery);
                case "P":
                    string system = data.Length > 3 ? data[3] : null;
                    return factory.CreateDevice(id, name, isTurnedOn, system: system);
                case "ED":
                    string ipAddress = data[2];
                    string networkName = data[3];
                    return factory.CreateDevice(id, name, false, ipAddress: ipAddress, networkName: networkName);
                default:
                    throw new ArgumentException("Unknown device type");
            }
        }

        /// <summary>
        /// Displays all devices in the collection.
        /// </summary>
        public void ShowAllDevices()
        {
            foreach (var device in devices)
            {
                Console.WriteLine(device.ToString());
            }
        }

        /// <summary>
        /// Adds a new device to the collection if the limit of 15 devices is not exceeded.
        /// </summary>
        /// <param name="device">The device to be added.</param>
        public void AddDevice(Device device)
        {
            if (devices.Count < 15) 
                devices.Add(device);
            else 
                Console.WriteLine("Too many devices");
        }

        /// <summary>
        /// Removes a device from the collection.
        /// </summary>
        /// <param name="device">The device to be removed.</param>
        public void RemoveDevice(Device device)
        {
            if (devices.Contains(device)) 
                devices.Remove(device);
            else 
                Console.WriteLine("Device not found");
        }

        /// <summary>
        /// Turns on the specified device.
        /// </summary>
        /// <param name="device">The device to be turned on.</param>
        public void TurnOnDevice(Device device)
        {
            device.turnOn();
        }

        /// <summary>
        /// Turns off the specified device.
        /// </summary>
        /// <param name="device">The device to be turned off.</param>
        public void TurnOffDevice(Device device)
        {
            device.turnOff();
        }

        /// <summary>
        /// Saves the current list of devices to a file using the <see cref="DeviceFileManager"/>.
        /// </summary>
        public void SaveDataToFile()
        {
            fileManager.SaveDeviceData(devices);
        }

        /// <summary>
        /// Edits the properties of a device, such as name, power state, battery, system, IP address, and network name.
        /// </summary>
        /// <param name="device">The device to be edited.</param>
        /// <param name="newName">The new name of the device (optional).</param>
        /// <param name="isTurnedOn">The new power state of the device (optional).</param>
        /// <param name="battery">The new battery percentage for a smartwatch device (optional).</param>
        /// <param name="newSystem">The new operating system for a personal computer (optional).</param>
        /// <param name="newIpAddress">The new IP address for an embedded device (optional).</param>
        /// <param name="newNetworkName">The new network name for an embedded device (optional).</param>
        /// <exception cref="Exception">Thrown if the device is not found in the collection.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the battery percentage is outside the valid range (0-100).</exception>
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
}
