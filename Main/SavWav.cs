using System;
using System.IO;
using System.Text;
using UnityEngine;

// Token: 0x02000339 RID: 825
public static class SavWav
{
	// Token: 0x06000E5D RID: 3677 RVA: 0x0003AD34 File Offset: 0x00038F34
	public static void Save(string filepath, AudioClip clip, float start, float duration)
	{
		if (!filepath.ToLower().EndsWith(".wav"))
		{
			filepath += ".wav";
		}
		Directory.CreateDirectory(Path.GetDirectoryName(filepath));
		using (FileStream fileStream = new FileStream(filepath, FileMode.Create))
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
			{
				uint count;
				byte[] wav = SavWav.GetWav(clip, out count, start, duration);
				binaryWriter.Write(wav, 0, (int)count);
			}
		}
	}

	// Token: 0x06000E5E RID: 3678 RVA: 0x0003ADC4 File Offset: 0x00038FC4
	public static byte[] GetWav(AudioClip clip, out uint length, float start, float duration)
	{
		uint samples;
		byte[] array = SavWav.ConvertAndWrite(clip, out length, out samples, start, duration);
		SavWav.WriteHeader(array, clip, length, samples);
		return array;
	}

	// Token: 0x06000E5F RID: 3679 RVA: 0x0003ADE8 File Offset: 0x00038FE8
	private static byte[] ConvertAndWrite(AudioClip clip, out uint length, out uint samplesAfterTrimming, float start, float duration)
	{
		float[] array = new float[clip.samples * clip.channels];
		clip.GetData(array, 0);
		int num = array.Length;
		int num2 = (int)(start * (float)clip.frequency * (float)clip.channels);
		int num3 = Math.Min(array.Length - 1, num2 + (int)(duration * (float)clip.frequency * (float)clip.channels));
		byte[] array2 = new byte[(long)(num * 2) + 44L];
		uint num4 = 44U;
		for (int i = num2; i <= num3; i++)
		{
			short num5 = (short)(array[i] * 32767f);
			array2[(int)num4++] = (byte)num5;
			array2[(int)num4++] = (byte)(num5 >> 8);
		}
		length = num4;
		samplesAfterTrimming = (uint)(num3 - num2 + 1);
		return array2;
	}

	// Token: 0x06000E60 RID: 3680 RVA: 0x0003AE9C File Offset: 0x0003909C
	private static void AddDataToBuffer(byte[] buffer, ref uint offset, byte[] addBytes)
	{
		foreach (byte b in addBytes)
		{
			uint num = offset;
			offset = num + 1U;
			buffer[(int)num] = b;
		}
	}

	// Token: 0x06000E61 RID: 3681 RVA: 0x0003AECC File Offset: 0x000390CC
	private static void WriteHeader(byte[] stream, AudioClip clip, uint length, uint samples)
	{
		int frequency = clip.frequency;
		ushort num = (ushort)clip.channels;
		uint num2 = 0U;
		byte[] bytes = Encoding.UTF8.GetBytes("RIFF");
		SavWav.AddDataToBuffer(stream, ref num2, bytes);
		byte[] bytes2 = BitConverter.GetBytes(length - 8U);
		SavWav.AddDataToBuffer(stream, ref num2, bytes2);
		byte[] bytes3 = Encoding.UTF8.GetBytes("WAVE");
		SavWav.AddDataToBuffer(stream, ref num2, bytes3);
		byte[] bytes4 = Encoding.UTF8.GetBytes("fmt ");
		SavWav.AddDataToBuffer(stream, ref num2, bytes4);
		byte[] bytes5 = BitConverter.GetBytes(16U);
		SavWav.AddDataToBuffer(stream, ref num2, bytes5);
		byte[] bytes6 = BitConverter.GetBytes(1);
		SavWav.AddDataToBuffer(stream, ref num2, bytes6);
		byte[] bytes7 = BitConverter.GetBytes(num);
		SavWav.AddDataToBuffer(stream, ref num2, bytes7);
		byte[] bytes8 = BitConverter.GetBytes((uint)frequency);
		SavWav.AddDataToBuffer(stream, ref num2, bytes8);
		byte[] bytes9 = BitConverter.GetBytes((uint)(frequency * (int)num * 2));
		SavWav.AddDataToBuffer(stream, ref num2, bytes9);
		ushort value = (ushort)(num * 2);
		SavWav.AddDataToBuffer(stream, ref num2, BitConverter.GetBytes(value));
		byte[] bytes10 = BitConverter.GetBytes(16);
		SavWav.AddDataToBuffer(stream, ref num2, bytes10);
		byte[] bytes11 = Encoding.UTF8.GetBytes("data");
		SavWav.AddDataToBuffer(stream, ref num2, bytes11);
		byte[] bytes12 = BitConverter.GetBytes(samples * 2U);
		SavWav.AddDataToBuffer(stream, ref num2, bytes12);
	}

	// Token: 0x04000EB9 RID: 3769
	private const uint HeaderSize = 44U;

	// Token: 0x04000EBA RID: 3770
	private const float RescaleFactor = 32767f;
}
