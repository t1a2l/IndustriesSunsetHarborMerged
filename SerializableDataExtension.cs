using ICities;
using System;

namespace IndustriesSunsetHarborMerged
{
  public class SerializableDataExtension : ISerializableDataExtension
  {
    public static SerializableDataExtension instance;
    private ISerializableData _serializableData;
    private bool _loaded;
    private const string DATA_ID = "MarketData";

    public ISerializableData SerializableData
    {
      get
      {
        return _serializableData;
      }
    }

    public bool Loaded
    {
      get
      {
        return _loaded;
      }
      set
      {
        _loaded = value;
      }
    }

    public event SerializableDataExtension.SaveDataEventHandler EventSaveData;

    public void OnCreated(ISerializableData serializedData)
    {
      SerializableDataExtension.instance = this;
      _serializableData = serializedData;
    }

    public void OnLoadData()
    {

    }

    public void OnSaveData()
    {
        if (!_loaded || EventSaveData == null)
            return;
        EventSaveData();
    }

    public void OnReleased()
    {
      SerializableDataExtension.instance = (SerializableDataExtension) null;
    }

    public static void WriteByte(byte value, FastList<byte> data)
    {
      data.Add(value);
    }

    public static byte ReadByte(byte[] data, ref int index)
    {
      int num = (int) data[index];
      index = index + 1;
      return (byte) num;
    }

    public static void WriteBool(bool value, FastList<byte> data)
    {
      SerializableDataExtension.AddToData(BitConverter.GetBytes(value), data);
    }

    public static bool ReadBool(byte[] data, ref int index)
    {
      int num = BitConverter.ToBoolean(data, index) ? 1 : 0;
      index = index + 1;
      return num != 0;
    }

    public static void WriteUInt16(ushort value, FastList<byte> data)
    {
      SerializableDataExtension.AddToData(BitConverter.GetBytes(value), data);
    }

    public static ushort ReadUInt16(byte[] data, ref int index)
    {
      int uint16 = (int) BitConverter.ToUInt16(data, index);
      index = index + 2;
      return (ushort) uint16;
    }

    public static void WriteInt32(int value, FastList<byte> data)
    {
      SerializableDataExtension.AddToData(BitConverter.GetBytes(value), data);
    }

    public static int ReadInt32(byte[] data, ref int index)
    {
      int int32 = BitConverter.ToInt32(data, index);
      index = index + 4;
      return int32;
    }

    public static void WriteFloat(float value, FastList<byte> data)
    {
      SerializableDataExtension.AddToData(BitConverter.GetBytes(value), data);
    }

    public static float ReadFloat(byte[] data, ref int index)
    {
      double single = (double) BitConverter.ToSingle(data, index);
      index = index + 4;
      return (float) single;
    }

    public static void WriteString(string s, FastList<byte> data)
    {
      char[] charArray = s.ToCharArray();
      SerializableDataExtension.WriteInt32(charArray.Length, data);
      for (ushort index = 0; (int) index < charArray.Length; ++index)
        SerializableDataExtension.AddToData(BitConverter.GetBytes(charArray[(int) index]), data);
    }

    public static string ReadString(byte[] data, ref int index)
    {
      string empty = string.Empty;
      int num = SerializableDataExtension.ReadInt32(data, ref index);
      for (int index1 = 0; index1 < num; ++index1)
      {
        empty += BitConverter.ToChar(data, index).ToString();
        index = index + 2;
      }
      return empty;
    }

    public static void WriteFloatArray(float[] array, FastList<byte> data)
    {
      SerializableDataExtension.WriteInt32(array.Length, data);
      for (ushort index = 0; (int) index < array.Length; ++index)
        SerializableDataExtension.WriteFloat(array[(int) index], data);
    }

    public static float[] ReadFloatArray(byte[] data, ref int index)
    {
      int length = SerializableDataExtension.ReadInt32(data, ref index);
      float[] numArray = new float[length];
      for (int index1 = 0; index1 < length; ++index1)
        numArray[index1] = SerializableDataExtension.ReadFloat(data, ref index);
      return numArray;
    }

    public static void AddToData(byte[] bytes, FastList<byte> data)
    {
      foreach (byte num in bytes)
        data.Add(num);
    }

    public delegate void SaveDataEventHandler();
  }
}