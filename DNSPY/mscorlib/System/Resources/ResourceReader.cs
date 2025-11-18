using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration.Assemblies;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Text;

namespace System.Resources
{
	// Token: 0x02000399 RID: 921
	[ComVisible(true)]
	public sealed class ResourceReader : IResourceReader, IEnumerable, IDisposable
	{
		// Token: 0x06002D4C RID: 11596 RVA: 0x000AB964 File Offset: 0x000A9B64
		[SecuritySafeCritical]
		public ResourceReader(string fileName)
		{
			this._resCache = new Dictionary<string, ResourceLocator>(FastResourceComparer.Default);
			this._store = new BinaryReader(new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.RandomAccess, Path.GetFileName(fileName), false), Encoding.UTF8);
			try
			{
				this.ReadResources();
			}
			catch
			{
				this._store.Close();
				throw;
			}
		}

		// Token: 0x06002D4D RID: 11597 RVA: 0x000AB9D8 File Offset: 0x000A9BD8
		[SecurityCritical]
		public ResourceReader(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanRead)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_StreamNotReadable"));
			}
			this._resCache = new Dictionary<string, ResourceLocator>(FastResourceComparer.Default);
			this._store = new BinaryReader(stream, Encoding.UTF8);
			this._ums = stream as UnmanagedMemoryStream;
			this.ReadResources();
		}

		// Token: 0x06002D4E RID: 11598 RVA: 0x000ABA44 File Offset: 0x000A9C44
		[SecurityCritical]
		internal ResourceReader(Stream stream, Dictionary<string, ResourceLocator> resCache)
		{
			this._resCache = resCache;
			this._store = new BinaryReader(stream, Encoding.UTF8);
			this._ums = stream as UnmanagedMemoryStream;
			this.ReadResources();
		}

		// Token: 0x06002D4F RID: 11599 RVA: 0x000ABA76 File Offset: 0x000A9C76
		public void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x06002D50 RID: 11600 RVA: 0x000ABA7F File Offset: 0x000A9C7F
		public void Dispose()
		{
			this.Close();
		}

		// Token: 0x06002D51 RID: 11601 RVA: 0x000ABA88 File Offset: 0x000A9C88
		[SecuritySafeCritical]
		private void Dispose(bool disposing)
		{
			if (this._store != null)
			{
				this._resCache = null;
				if (disposing)
				{
					BinaryReader store = this._store;
					this._store = null;
					if (store != null)
					{
						store.Close();
					}
				}
				this._store = null;
				this._namePositions = null;
				this._nameHashes = null;
				this._ums = null;
				this._namePositionsPtr = null;
				this._nameHashesPtr = null;
			}
		}

		// Token: 0x06002D52 RID: 11602 RVA: 0x000ABAEC File Offset: 0x000A9CEC
		[SecurityCritical]
		internal unsafe static int ReadUnalignedI4(int* p)
		{
			return (int)(*(byte*)p) | ((int)((byte*)p)[1] << 8) | ((int)((byte*)p)[2] << 16) | ((int)((byte*)p)[3] << 24);
		}

		// Token: 0x06002D53 RID: 11603 RVA: 0x000ABB14 File Offset: 0x000A9D14
		private void SkipInt32()
		{
			this._store.BaseStream.Seek(4L, SeekOrigin.Current);
		}

		// Token: 0x06002D54 RID: 11604 RVA: 0x000ABB2C File Offset: 0x000A9D2C
		private void SkipString()
		{
			int num = this._store.Read7BitEncodedInt();
			if (num < 0)
			{
				throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_NegativeStringLength"));
			}
			this._store.BaseStream.Seek((long)num, SeekOrigin.Current);
		}

		// Token: 0x06002D55 RID: 11605 RVA: 0x000ABB6D File Offset: 0x000A9D6D
		[SecuritySafeCritical]
		private int GetNameHash(int index)
		{
			if (this._ums == null)
			{
				return this._nameHashes[index];
			}
			return ResourceReader.ReadUnalignedI4(this._nameHashesPtr + index);
		}

		// Token: 0x06002D56 RID: 11606 RVA: 0x000ABB94 File Offset: 0x000A9D94
		[SecuritySafeCritical]
		private int GetNamePosition(int index)
		{
			int num;
			if (this._ums == null)
			{
				num = this._namePositions[index];
			}
			else
			{
				num = ResourceReader.ReadUnalignedI4(this._namePositionsPtr + index);
			}
			if (num < 0 || (long)num > this._dataSectionOffset - this._nameSectionOffset)
			{
				throw new FormatException(Environment.GetResourceString("BadImageFormat_ResourcesNameInvalidOffset", new object[] { num }));
			}
			return num;
		}

		// Token: 0x06002D57 RID: 11607 RVA: 0x000ABBFB File Offset: 0x000A9DFB
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06002D58 RID: 11608 RVA: 0x000ABC03 File Offset: 0x000A9E03
		public IDictionaryEnumerator GetEnumerator()
		{
			if (this._resCache == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("ResourceReaderIsClosed"));
			}
			return new ResourceReader.ResourceEnumerator(this);
		}

		// Token: 0x06002D59 RID: 11609 RVA: 0x000ABC23 File Offset: 0x000A9E23
		internal ResourceReader.ResourceEnumerator GetEnumeratorInternal()
		{
			return new ResourceReader.ResourceEnumerator(this);
		}

		// Token: 0x06002D5A RID: 11610 RVA: 0x000ABC2C File Offset: 0x000A9E2C
		internal int FindPosForResource(string name)
		{
			int num = FastResourceComparer.HashFunction(name);
			int i = 0;
			int num2 = this._numResources - 1;
			int num3 = -1;
			bool flag = false;
			while (i <= num2)
			{
				num3 = i + num2 >> 1;
				int nameHash = this.GetNameHash(num3);
				int num4;
				if (nameHash == num)
				{
					num4 = 0;
				}
				else if (nameHash < num)
				{
					num4 = -1;
				}
				else
				{
					num4 = 1;
				}
				if (num4 == 0)
				{
					flag = true;
					break;
				}
				if (num4 < 0)
				{
					i = num3 + 1;
				}
				else
				{
					num2 = num3 - 1;
				}
			}
			if (!flag)
			{
				return -1;
			}
			if (i != num3)
			{
				i = num3;
				while (i > 0 && this.GetNameHash(i - 1) == num)
				{
					i--;
				}
			}
			if (num2 != num3)
			{
				num2 = num3;
				while (num2 < this._numResources - 1 && this.GetNameHash(num2 + 1) == num)
				{
					num2++;
				}
			}
			lock (this)
			{
				int j = i;
				while (j <= num2)
				{
					this._store.BaseStream.Seek(this._nameSectionOffset + (long)this.GetNamePosition(j), SeekOrigin.Begin);
					if (this.CompareStringEqualsName(name))
					{
						int num5 = this._store.ReadInt32();
						if (num5 < 0 || (long)num5 >= this._store.BaseStream.Length - this._dataSectionOffset)
						{
							throw new FormatException(Environment.GetResourceString("BadImageFormat_ResourcesDataInvalidOffset", new object[] { num5 }));
						}
						return num5;
					}
					else
					{
						j++;
					}
				}
			}
			return -1;
		}

		// Token: 0x06002D5B RID: 11611 RVA: 0x000ABDA0 File Offset: 0x000A9FA0
		[SecuritySafeCritical]
		private unsafe bool CompareStringEqualsName(string name)
		{
			int num = this._store.Read7BitEncodedInt();
			if (num < 0)
			{
				throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_NegativeStringLength"));
			}
			if (this._ums == null)
			{
				byte[] array = new byte[num];
				int num2;
				for (int i = num; i > 0; i -= num2)
				{
					num2 = this._store.Read(array, num - i, i);
					if (num2 == 0)
					{
						throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourceNameCorrupted"));
					}
				}
				return FastResourceComparer.CompareOrdinal(array, num / 2, name) == 0;
			}
			byte* positionPointer = this._ums.PositionPointer;
			this._ums.Seek((long)num, SeekOrigin.Current);
			if (this._ums.Position > this._ums.Length)
			{
				throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesNameTooLong"));
			}
			return FastResourceComparer.CompareOrdinal(positionPointer, num, name) == 0;
		}

		// Token: 0x06002D5C RID: 11612 RVA: 0x000ABE6C File Offset: 0x000AA06C
		[SecurityCritical]
		private unsafe string AllocateStringForNameIndex(int index, out int dataOffset)
		{
			long num = (long)this.GetNamePosition(index);
			int num2;
			byte[] array;
			lock (this)
			{
				this._store.BaseStream.Seek(num + this._nameSectionOffset, SeekOrigin.Begin);
				num2 = this._store.Read7BitEncodedInt();
				if (num2 < 0)
				{
					throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_NegativeStringLength"));
				}
				if (this._ums != null)
				{
					if (this._ums.Position > this._ums.Length - (long)num2)
					{
						throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesIndexTooLong", new object[] { index }));
					}
					char* positionPointer = (char*)this._ums.PositionPointer;
					string text = new string(positionPointer, 0, num2 / 2);
					this._ums.Position += (long)num2;
					dataOffset = this._store.ReadInt32();
					if (dataOffset < 0 || (long)dataOffset >= this._store.BaseStream.Length - this._dataSectionOffset)
					{
						throw new FormatException(Environment.GetResourceString("BadImageFormat_ResourcesDataInvalidOffset", new object[] { dataOffset }));
					}
					return text;
				}
				else
				{
					array = new byte[num2];
					int num3;
					for (int i = num2; i > 0; i -= num3)
					{
						num3 = this._store.Read(array, num2 - i, i);
						if (num3 == 0)
						{
							throw new EndOfStreamException(Environment.GetResourceString("BadImageFormat_ResourceNameCorrupted_NameIndex", new object[] { index }));
						}
					}
					dataOffset = this._store.ReadInt32();
					if (dataOffset < 0 || (long)dataOffset >= this._store.BaseStream.Length - this._dataSectionOffset)
					{
						throw new FormatException(Environment.GetResourceString("BadImageFormat_ResourcesDataInvalidOffset", new object[] { dataOffset }));
					}
				}
			}
			return Encoding.Unicode.GetString(array, 0, num2);
		}

		// Token: 0x06002D5D RID: 11613 RVA: 0x000AC06C File Offset: 0x000AA26C
		private object GetValueForNameIndex(int index)
		{
			long num = (long)this.GetNamePosition(index);
			object obj;
			lock (this)
			{
				this._store.BaseStream.Seek(num + this._nameSectionOffset, SeekOrigin.Begin);
				this.SkipString();
				int num2 = this._store.ReadInt32();
				if (num2 < 0 || (long)num2 >= this._store.BaseStream.Length - this._dataSectionOffset)
				{
					throw new FormatException(Environment.GetResourceString("BadImageFormat_ResourcesDataInvalidOffset", new object[] { num2 }));
				}
				if (this._version == 1)
				{
					obj = this.LoadObjectV1(num2);
				}
				else
				{
					ResourceTypeCode resourceTypeCode;
					obj = this.LoadObjectV2(num2, out resourceTypeCode);
				}
			}
			return obj;
		}

		// Token: 0x06002D5E RID: 11614 RVA: 0x000AC138 File Offset: 0x000AA338
		internal string LoadString(int pos)
		{
			this._store.BaseStream.Seek(this._dataSectionOffset + (long)pos, SeekOrigin.Begin);
			string text = null;
			int num = this._store.Read7BitEncodedInt();
			if (this._version == 1)
			{
				if (num == -1)
				{
					return null;
				}
				if (this.FindType(num) != typeof(string))
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ResourceNotString_Type", new object[] { this.FindType(num).FullName }));
				}
				text = this._store.ReadString();
			}
			else
			{
				ResourceTypeCode resourceTypeCode = (ResourceTypeCode)num;
				if (resourceTypeCode != ResourceTypeCode.String && resourceTypeCode != ResourceTypeCode.Null)
				{
					string text2;
					if (resourceTypeCode < ResourceTypeCode.StartOfUserTypes)
					{
						text2 = resourceTypeCode.ToString();
					}
					else
					{
						text2 = this.FindType(resourceTypeCode - ResourceTypeCode.StartOfUserTypes).FullName;
					}
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ResourceNotString_Type", new object[] { text2 }));
				}
				if (resourceTypeCode == ResourceTypeCode.String)
				{
					text = this._store.ReadString();
				}
			}
			return text;
		}

		// Token: 0x06002D5F RID: 11615 RVA: 0x000AC224 File Offset: 0x000AA424
		internal object LoadObject(int pos)
		{
			if (this._version == 1)
			{
				return this.LoadObjectV1(pos);
			}
			ResourceTypeCode resourceTypeCode;
			return this.LoadObjectV2(pos, out resourceTypeCode);
		}

		// Token: 0x06002D60 RID: 11616 RVA: 0x000AC24C File Offset: 0x000AA44C
		internal object LoadObject(int pos, out ResourceTypeCode typeCode)
		{
			if (this._version == 1)
			{
				object obj = this.LoadObjectV1(pos);
				typeCode = ((obj is string) ? ResourceTypeCode.String : ResourceTypeCode.StartOfUserTypes);
				return obj;
			}
			return this.LoadObjectV2(pos, out typeCode);
		}

		// Token: 0x06002D61 RID: 11617 RVA: 0x000AC284 File Offset: 0x000AA484
		internal object LoadObjectV1(int pos)
		{
			object obj;
			try
			{
				obj = this._LoadObjectV1(pos);
			}
			catch (EndOfStreamException ex)
			{
				throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_TypeMismatch"), ex);
			}
			catch (ArgumentOutOfRangeException ex2)
			{
				throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_TypeMismatch"), ex2);
			}
			return obj;
		}

		// Token: 0x06002D62 RID: 11618 RVA: 0x000AC2DC File Offset: 0x000AA4DC
		[SecuritySafeCritical]
		private object _LoadObjectV1(int pos)
		{
			this._store.BaseStream.Seek(this._dataSectionOffset + (long)pos, SeekOrigin.Begin);
			int num = this._store.Read7BitEncodedInt();
			if (num == -1)
			{
				return null;
			}
			RuntimeType runtimeType = this.FindType(num);
			if (runtimeType == typeof(string))
			{
				return this._store.ReadString();
			}
			if (runtimeType == typeof(int))
			{
				return this._store.ReadInt32();
			}
			if (runtimeType == typeof(byte))
			{
				return this._store.ReadByte();
			}
			if (runtimeType == typeof(sbyte))
			{
				return this._store.ReadSByte();
			}
			if (runtimeType == typeof(short))
			{
				return this._store.ReadInt16();
			}
			if (runtimeType == typeof(long))
			{
				return this._store.ReadInt64();
			}
			if (runtimeType == typeof(ushort))
			{
				return this._store.ReadUInt16();
			}
			if (runtimeType == typeof(uint))
			{
				return this._store.ReadUInt32();
			}
			if (runtimeType == typeof(ulong))
			{
				return this._store.ReadUInt64();
			}
			if (runtimeType == typeof(float))
			{
				return this._store.ReadSingle();
			}
			if (runtimeType == typeof(double))
			{
				return this._store.ReadDouble();
			}
			if (runtimeType == typeof(DateTime))
			{
				return new DateTime(this._store.ReadInt64());
			}
			if (runtimeType == typeof(TimeSpan))
			{
				return new TimeSpan(this._store.ReadInt64());
			}
			if (runtimeType == typeof(decimal))
			{
				int[] array = new int[4];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this._store.ReadInt32();
				}
				return new decimal(array);
			}
			return this.DeserializeObject(num);
		}

		// Token: 0x06002D63 RID: 11619 RVA: 0x000AC534 File Offset: 0x000AA734
		internal object LoadObjectV2(int pos, out ResourceTypeCode typeCode)
		{
			object obj;
			try
			{
				obj = this._LoadObjectV2(pos, out typeCode);
			}
			catch (EndOfStreamException ex)
			{
				throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_TypeMismatch"), ex);
			}
			catch (ArgumentOutOfRangeException ex2)
			{
				throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_TypeMismatch"), ex2);
			}
			return obj;
		}

		// Token: 0x06002D64 RID: 11620 RVA: 0x000AC590 File Offset: 0x000AA790
		[SecuritySafeCritical]
		private object _LoadObjectV2(int pos, out ResourceTypeCode typeCode)
		{
			this._store.BaseStream.Seek(this._dataSectionOffset + (long)pos, SeekOrigin.Begin);
			typeCode = (ResourceTypeCode)this._store.Read7BitEncodedInt();
			switch (typeCode)
			{
			case ResourceTypeCode.Null:
				return null;
			case ResourceTypeCode.String:
				return this._store.ReadString();
			case ResourceTypeCode.Boolean:
				return this._store.ReadBoolean();
			case ResourceTypeCode.Char:
				return (char)this._store.ReadUInt16();
			case ResourceTypeCode.Byte:
				return this._store.ReadByte();
			case ResourceTypeCode.SByte:
				return this._store.ReadSByte();
			case ResourceTypeCode.Int16:
				return this._store.ReadInt16();
			case ResourceTypeCode.UInt16:
				return this._store.ReadUInt16();
			case ResourceTypeCode.Int32:
				return this._store.ReadInt32();
			case ResourceTypeCode.UInt32:
				return this._store.ReadUInt32();
			case ResourceTypeCode.Int64:
				return this._store.ReadInt64();
			case ResourceTypeCode.UInt64:
				return this._store.ReadUInt64();
			case ResourceTypeCode.Single:
				return this._store.ReadSingle();
			case ResourceTypeCode.Double:
				return this._store.ReadDouble();
			case ResourceTypeCode.Decimal:
				return this._store.ReadDecimal();
			case ResourceTypeCode.DateTime:
			{
				long num = this._store.ReadInt64();
				return DateTime.FromBinary(num);
			}
			case ResourceTypeCode.TimeSpan:
			{
				long num2 = this._store.ReadInt64();
				return new TimeSpan(num2);
			}
			case ResourceTypeCode.ByteArray:
			{
				int num3 = this._store.ReadInt32();
				if (num3 < 0)
				{
					throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourceDataLengthInvalid", new object[] { num3 }));
				}
				if (this._ums == null)
				{
					if ((long)num3 > this._store.BaseStream.Length)
					{
						throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourceDataLengthInvalid", new object[] { num3 }));
					}
					return this._store.ReadBytes(num3);
				}
				else
				{
					if ((long)num3 > this._ums.Length - this._ums.Position)
					{
						throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourceDataLengthInvalid", new object[] { num3 }));
					}
					byte[] array = new byte[num3];
					int num4 = this._ums.Read(array, 0, num3);
					return array;
				}
				break;
			}
			case ResourceTypeCode.Stream:
			{
				int num5 = this._store.ReadInt32();
				if (num5 < 0)
				{
					throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourceDataLengthInvalid", new object[] { num5 }));
				}
				if (this._ums == null)
				{
					byte[] array2 = this._store.ReadBytes(num5);
					return new PinnedBufferMemoryStream(array2);
				}
				if ((long)num5 > this._ums.Length - this._ums.Position)
				{
					throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourceDataLengthInvalid", new object[] { num5 }));
				}
				return new UnmanagedMemoryStream(this._ums.PositionPointer, (long)num5, (long)num5, FileAccess.Read, true);
			}
			}
			if (typeCode < ResourceTypeCode.StartOfUserTypes)
			{
				throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_TypeMismatch"));
			}
			int num6 = typeCode - ResourceTypeCode.StartOfUserTypes;
			return this.DeserializeObject(num6);
		}

		// Token: 0x06002D65 RID: 11621 RVA: 0x000AC918 File Offset: 0x000AAB18
		[SecurityCritical]
		private object DeserializeObject(int typeIndex)
		{
			RuntimeType runtimeType = this.FindType(typeIndex);
			if (this._safeToDeserialize == null)
			{
				this.InitSafeToDeserializeArray();
			}
			object obj;
			if (this._safeToDeserialize[typeIndex])
			{
				this._objFormatter.Binder = this._typeLimitingBinder;
				this._typeLimitingBinder.ExpectingToDeserialize(runtimeType);
				obj = this._objFormatter.UnsafeDeserialize(this._store.BaseStream, null);
			}
			else
			{
				this._objFormatter.Binder = null;
				obj = this._objFormatter.Deserialize(this._store.BaseStream);
			}
			if (obj.GetType() != runtimeType)
			{
				throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResType&SerBlobMismatch", new object[]
				{
					runtimeType.FullName,
					obj.GetType().FullName
				}));
			}
			return obj;
		}

		// Token: 0x06002D66 RID: 11622 RVA: 0x000AC9DC File Offset: 0x000AABDC
		[SecurityCritical]
		private void ReadResources()
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.File | StreamingContextStates.Persistence));
			this._typeLimitingBinder = new ResourceReader.TypeLimitingDeserializationBinder();
			binaryFormatter.Binder = this._typeLimitingBinder;
			this._objFormatter = binaryFormatter;
			try
			{
				this._ReadResources();
			}
			catch (EndOfStreamException ex)
			{
				throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesHeaderCorrupted"), ex);
			}
			catch (IndexOutOfRangeException ex2)
			{
				throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesHeaderCorrupted"), ex2);
			}
		}

		// Token: 0x06002D67 RID: 11623 RVA: 0x000ACA60 File Offset: 0x000AAC60
		[SecurityCritical]
		private unsafe void _ReadResources()
		{
			int num = this._store.ReadInt32();
			if (num != ResourceManager.MagicNumber)
			{
				throw new ArgumentException(Environment.GetResourceString("Resources_StreamNotValid"));
			}
			int num2 = this._store.ReadInt32();
			int num3 = this._store.ReadInt32();
			if (num3 < 0 || num2 < 0)
			{
				throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesHeaderCorrupted"));
			}
			if (num2 > 1)
			{
				this._store.BaseStream.Seek((long)num3, SeekOrigin.Current);
			}
			else
			{
				string text = this._store.ReadString();
				AssemblyName assemblyName = new AssemblyName(ResourceManager.MscorlibName);
				if (!ResourceManager.CompareNames(text, ResourceManager.ResReaderTypeName, assemblyName))
				{
					throw new NotSupportedException(Environment.GetResourceString("NotSupported_WrongResourceReader_Type", new object[] { text }));
				}
				this.SkipString();
			}
			int num4 = this._store.ReadInt32();
			if (num4 != 2 && num4 != 1)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_ResourceFileUnsupportedVersion", new object[] { 2, num4 }));
			}
			this._version = num4;
			this._numResources = this._store.ReadInt32();
			if (this._numResources < 0)
			{
				throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesHeaderCorrupted"));
			}
			int num5 = this._store.ReadInt32();
			if (num5 < 0)
			{
				throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesHeaderCorrupted"));
			}
			this._typeTable = new RuntimeType[num5];
			this._typeNamePositions = new int[num5];
			for (int i = 0; i < num5; i++)
			{
				this._typeNamePositions[i] = (int)this._store.BaseStream.Position;
				this.SkipString();
			}
			long position = this._store.BaseStream.Position;
			int num6 = (int)position & 7;
			if (num6 != 0)
			{
				for (int j = 0; j < 8 - num6; j++)
				{
					this._store.ReadByte();
				}
			}
			if (this._ums == null)
			{
				this._nameHashes = new int[this._numResources];
				for (int k = 0; k < this._numResources; k++)
				{
					this._nameHashes[k] = this._store.ReadInt32();
				}
			}
			else
			{
				if (((long)this._numResources & (long)((ulong)(-536870912))) != 0L)
				{
					throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesHeaderCorrupted"));
				}
				int num7 = 4 * this._numResources;
				this._nameHashesPtr = (int*)this._ums.PositionPointer;
				this._ums.Seek((long)num7, SeekOrigin.Current);
				byte* positionPointer = this._ums.PositionPointer;
			}
			if (this._ums == null)
			{
				this._namePositions = new int[this._numResources];
				for (int l = 0; l < this._numResources; l++)
				{
					int num8 = this._store.ReadInt32();
					if (num8 < 0)
					{
						throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesHeaderCorrupted"));
					}
					this._namePositions[l] = num8;
				}
			}
			else
			{
				if (((long)this._numResources & (long)((ulong)(-536870912))) != 0L)
				{
					throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesHeaderCorrupted"));
				}
				int num9 = 4 * this._numResources;
				this._namePositionsPtr = (int*)this._ums.PositionPointer;
				this._ums.Seek((long)num9, SeekOrigin.Current);
				byte* positionPointer2 = this._ums.PositionPointer;
			}
			this._dataSectionOffset = (long)this._store.ReadInt32();
			if (this._dataSectionOffset < 0L)
			{
				throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesHeaderCorrupted"));
			}
			this._nameSectionOffset = this._store.BaseStream.Position;
			if (this._dataSectionOffset < this._nameSectionOffset)
			{
				throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesHeaderCorrupted"));
			}
		}

		// Token: 0x06002D68 RID: 11624 RVA: 0x000ACDEC File Offset: 0x000AAFEC
		private RuntimeType FindType(int typeIndex)
		{
			if (typeIndex < 0 || typeIndex >= this._typeTable.Length)
			{
				throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_InvalidType"));
			}
			if (this._typeTable[typeIndex] == null)
			{
				long position = this._store.BaseStream.Position;
				try
				{
					this._store.BaseStream.Position = (long)this._typeNamePositions[typeIndex];
					string text = this._store.ReadString();
					this._typeTable[typeIndex] = (RuntimeType)Type.GetType(text, true);
				}
				finally
				{
					this._store.BaseStream.Position = position;
				}
			}
			return this._typeTable[typeIndex];
		}

		// Token: 0x06002D69 RID: 11625 RVA: 0x000ACEA0 File Offset: 0x000AB0A0
		[SecurityCritical]
		private void InitSafeToDeserializeArray()
		{
			this._safeToDeserialize = new bool[this._typeTable.Length];
			int i = 0;
			while (i < this._typeTable.Length)
			{
				long position = this._store.BaseStream.Position;
				string text;
				try
				{
					this._store.BaseStream.Position = (long)this._typeNamePositions[i];
					text = this._store.ReadString();
				}
				finally
				{
					this._store.BaseStream.Position = position;
				}
				RuntimeType runtimeType = (RuntimeType)Type.GetType(text, false);
				if (runtimeType == null)
				{
					AssemblyName assemblyName = null;
					string text2 = text;
					goto IL_E5;
				}
				if (!(runtimeType.BaseType == typeof(Enum)))
				{
					string text2 = runtimeType.FullName;
					AssemblyName assemblyName = new AssemblyName();
					RuntimeAssembly runtimeAssembly = (RuntimeAssembly)runtimeType.Assembly;
					assemblyName.Init(runtimeAssembly.GetSimpleName(), runtimeAssembly.GetPublicKey(), null, null, runtimeAssembly.GetLocale(), AssemblyHashAlgorithm.None, AssemblyVersionCompatibility.SameMachine, null, AssemblyNameFlags.PublicKey, null);
					goto IL_E5;
				}
				this._safeToDeserialize[i] = true;
				IL_11B:
				i++;
				continue;
				IL_E5:
				foreach (string text3 in ResourceReader.TypesSafeForDeserialization)
				{
					AssemblyName assemblyName;
					string text2;
					if (ResourceManager.CompareNames(text3, text2, assemblyName))
					{
						this._safeToDeserialize[i] = true;
					}
				}
				goto IL_11B;
			}
		}

		// Token: 0x06002D6A RID: 11626 RVA: 0x000ACFEC File Offset: 0x000AB1EC
		public void GetResourceData(string resourceName, out string resourceType, out byte[] resourceData)
		{
			if (resourceName == null)
			{
				throw new ArgumentNullException("resourceName");
			}
			if (this._resCache == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("ResourceReaderIsClosed"));
			}
			int[] array = new int[this._numResources];
			int num = this.FindPosForResource(resourceName);
			if (num == -1)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_ResourceNameNotExist", new object[] { resourceName }));
			}
			lock (this)
			{
				for (int i = 0; i < this._numResources; i++)
				{
					this._store.BaseStream.Position = this._nameSectionOffset + (long)this.GetNamePosition(i);
					int num2 = this._store.Read7BitEncodedInt();
					if (num2 < 0)
					{
						throw new FormatException(Environment.GetResourceString("BadImageFormat_ResourcesNameInvalidOffset", new object[] { num2 }));
					}
					this._store.BaseStream.Position += (long)num2;
					int num3 = this._store.ReadInt32();
					if (num3 < 0 || (long)num3 >= this._store.BaseStream.Length - this._dataSectionOffset)
					{
						throw new FormatException(Environment.GetResourceString("BadImageFormat_ResourcesDataInvalidOffset", new object[] { num3 }));
					}
					array[i] = num3;
				}
				Array.Sort<int>(array);
				int num4 = Array.BinarySearch<int>(array, num);
				long num5 = ((num4 < this._numResources - 1) ? ((long)array[num4 + 1] + this._dataSectionOffset) : this._store.BaseStream.Length);
				int num6 = (int)(num5 - ((long)num + this._dataSectionOffset));
				this._store.BaseStream.Position = this._dataSectionOffset + (long)num;
				ResourceTypeCode resourceTypeCode = (ResourceTypeCode)this._store.Read7BitEncodedInt();
				if (resourceTypeCode < ResourceTypeCode.Null || resourceTypeCode >= ResourceTypeCode.StartOfUserTypes + this._typeTable.Length)
				{
					throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_InvalidType"));
				}
				resourceType = this.TypeNameFromTypeCode(resourceTypeCode);
				num6 -= (int)(this._store.BaseStream.Position - (this._dataSectionOffset + (long)num));
				byte[] array2 = this._store.ReadBytes(num6);
				if (array2.Length != num6)
				{
					throw new FormatException(Environment.GetResourceString("BadImageFormat_ResourceNameCorrupted"));
				}
				resourceData = array2;
			}
		}

		// Token: 0x06002D6B RID: 11627 RVA: 0x000AD24C File Offset: 0x000AB44C
		private string TypeNameFromTypeCode(ResourceTypeCode typeCode)
		{
			if (typeCode < ResourceTypeCode.StartOfUserTypes)
			{
				return "ResourceTypeCode." + typeCode.ToString();
			}
			int num = typeCode - ResourceTypeCode.StartOfUserTypes;
			long position = this._store.BaseStream.Position;
			string text;
			try
			{
				this._store.BaseStream.Position = (long)this._typeNamePositions[num];
				text = this._store.ReadString();
			}
			finally
			{
				this._store.BaseStream.Position = position;
			}
			return text;
		}

		// Token: 0x06002D6C RID: 11628 RVA: 0x000AD2D8 File Offset: 0x000AB4D8
		// Note: this type is marked as 'beforefieldinit'.
		static ResourceReader()
		{
		}

		// Token: 0x0400125A RID: 4698
		private const int DefaultFileStreamBufferSize = 4096;

		// Token: 0x0400125B RID: 4699
		private BinaryReader _store;

		// Token: 0x0400125C RID: 4700
		internal Dictionary<string, ResourceLocator> _resCache;

		// Token: 0x0400125D RID: 4701
		private long _nameSectionOffset;

		// Token: 0x0400125E RID: 4702
		private long _dataSectionOffset;

		// Token: 0x0400125F RID: 4703
		private int[] _nameHashes;

		// Token: 0x04001260 RID: 4704
		[SecurityCritical]
		private unsafe int* _nameHashesPtr;

		// Token: 0x04001261 RID: 4705
		private int[] _namePositions;

		// Token: 0x04001262 RID: 4706
		[SecurityCritical]
		private unsafe int* _namePositionsPtr;

		// Token: 0x04001263 RID: 4707
		private RuntimeType[] _typeTable;

		// Token: 0x04001264 RID: 4708
		private int[] _typeNamePositions;

		// Token: 0x04001265 RID: 4709
		private BinaryFormatter _objFormatter;

		// Token: 0x04001266 RID: 4710
		private int _numResources;

		// Token: 0x04001267 RID: 4711
		private UnmanagedMemoryStream _ums;

		// Token: 0x04001268 RID: 4712
		private int _version;

		// Token: 0x04001269 RID: 4713
		private bool[] _safeToDeserialize;

		// Token: 0x0400126A RID: 4714
		private ResourceReader.TypeLimitingDeserializationBinder _typeLimitingBinder;

		// Token: 0x0400126B RID: 4715
		private static readonly string[] TypesSafeForDeserialization = new string[]
		{
			"System.String[], mscorlib, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.DateTime[], mscorlib, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Bitmap, System.Drawing, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Imaging.Metafile, System.Drawing, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Point, System.Drawing, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.PointF, System.Drawing, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Size, System.Drawing, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.SizeF, System.Drawing, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Font, System.Drawing, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Icon, System.Drawing, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
			"System.Drawing.Color, System.Drawing, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Windows.Forms.Cursor, System.Windows.Forms, Culture=neutral, PublicKeyToken=b77a5c561934e089", "System.Windows.Forms.Padding, System.Windows.Forms, Culture=neutral, PublicKeyToken=b77a5c561934e089", "System.Windows.Forms.LinkArea, System.Windows.Forms, Culture=neutral, PublicKeyToken=b77a5c561934e089", "System.Windows.Forms.ImageListStreamer, System.Windows.Forms, Culture=neutral, PublicKeyToken=b77a5c561934e089", "System.Windows.Forms.ListViewGroup, System.Windows.Forms, Culture=neutral, PublicKeyToken=b77a5c561934e089", "System.Windows.Forms.ListViewItem, System.Windows.Forms, Culture=neutral, PublicKeyToken=b77a5c561934e089", "System.Windows.Forms.ListViewItem+ListViewSubItem, System.Windows.Forms, Culture=neutral, PublicKeyToken=b77a5c561934e089", "System.Windows.Forms.ListViewItem+ListViewSubItem+SubItemStyle, System.Windows.Forms, Culture=neutral, PublicKeyToken=b77a5c561934e089", "System.Windows.Forms.OwnerDrawPropertyBag, System.Windows.Forms, Culture=neutral, PublicKeyToken=b77a5c561934e089",
			"System.Windows.Forms.TreeNode, System.Windows.Forms, Culture=neutral, PublicKeyToken=b77a5c561934e089"
		};

		// Token: 0x02000B69 RID: 2921
		internal sealed class TypeLimitingDeserializationBinder : SerializationBinder
		{
			// Token: 0x17001248 RID: 4680
			// (get) Token: 0x06006C22 RID: 27682 RVA: 0x00175DF8 File Offset: 0x00173FF8
			// (set) Token: 0x06006C23 RID: 27683 RVA: 0x00175E00 File Offset: 0x00174000
			internal ObjectReader ObjectReader
			{
				get
				{
					return this._objectReader;
				}
				set
				{
					this._objectReader = value;
				}
			}

			// Token: 0x06006C24 RID: 27684 RVA: 0x00175E09 File Offset: 0x00174009
			internal void ExpectingToDeserialize(RuntimeType type)
			{
				this._typeToDeserialize = type;
			}

			// Token: 0x06006C25 RID: 27685 RVA: 0x00175E14 File Offset: 0x00174014
			[SecuritySafeCritical]
			public override Type BindToType(string assemblyName, string typeName)
			{
				AssemblyName assemblyName2 = new AssemblyName(assemblyName);
				bool flag = false;
				foreach (string text in ResourceReader.TypesSafeForDeserialization)
				{
					if (ResourceManager.CompareNames(text, typeName, assemblyName2))
					{
						flag = true;
						break;
					}
				}
				Type type = this.ObjectReader.FastBindToType(assemblyName, typeName);
				if (type.IsEnum)
				{
					flag = true;
				}
				if (flag)
				{
					return null;
				}
				throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResType&SerBlobMismatch", new object[]
				{
					this._typeToDeserialize.FullName,
					typeName
				}));
			}

			// Token: 0x06006C26 RID: 27686 RVA: 0x00175E9B File Offset: 0x0017409B
			public TypeLimitingDeserializationBinder()
			{
			}

			// Token: 0x04003453 RID: 13395
			private RuntimeType _typeToDeserialize;

			// Token: 0x04003454 RID: 13396
			private ObjectReader _objectReader;
		}

		// Token: 0x02000B6A RID: 2922
		internal sealed class ResourceEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x06006C27 RID: 27687 RVA: 0x00175EA3 File Offset: 0x001740A3
			internal ResourceEnumerator(ResourceReader reader)
			{
				this._currentName = -1;
				this._reader = reader;
				this._dataPosition = -2;
			}

			// Token: 0x06006C28 RID: 27688 RVA: 0x00175EC4 File Offset: 0x001740C4
			public bool MoveNext()
			{
				if (this._currentName == this._reader._numResources - 1 || this._currentName == -2147483648)
				{
					this._currentIsValid = false;
					this._currentName = int.MinValue;
					return false;
				}
				this._currentIsValid = true;
				this._currentName++;
				return true;
			}

			// Token: 0x17001249 RID: 4681
			// (get) Token: 0x06006C29 RID: 27689 RVA: 0x00175F20 File Offset: 0x00174120
			public object Key
			{
				[SecuritySafeCritical]
				get
				{
					if (this._currentName == -2147483648)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
					}
					if (!this._currentIsValid)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
					}
					if (this._reader._resCache == null)
					{
						throw new InvalidOperationException(Environment.GetResourceString("ResourceReaderIsClosed"));
					}
					return this._reader.AllocateStringForNameIndex(this._currentName, out this._dataPosition);
				}
			}

			// Token: 0x1700124A RID: 4682
			// (get) Token: 0x06006C2A RID: 27690 RVA: 0x00175F96 File Offset: 0x00174196
			public object Current
			{
				get
				{
					return this.Entry;
				}
			}

			// Token: 0x1700124B RID: 4683
			// (get) Token: 0x06006C2B RID: 27691 RVA: 0x00175FA3 File Offset: 0x001741A3
			internal int DataPosition
			{
				get
				{
					return this._dataPosition;
				}
			}

			// Token: 0x1700124C RID: 4684
			// (get) Token: 0x06006C2C RID: 27692 RVA: 0x00175FAC File Offset: 0x001741AC
			public DictionaryEntry Entry
			{
				[SecuritySafeCritical]
				get
				{
					if (this._currentName == -2147483648)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
					}
					if (!this._currentIsValid)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
					}
					if (this._reader._resCache == null)
					{
						throw new InvalidOperationException(Environment.GetResourceString("ResourceReaderIsClosed"));
					}
					object obj = null;
					ResourceReader reader = this._reader;
					string text;
					lock (reader)
					{
						Dictionary<string, ResourceLocator> resCache = this._reader._resCache;
						lock (resCache)
						{
							text = this._reader.AllocateStringForNameIndex(this._currentName, out this._dataPosition);
							ResourceLocator resourceLocator;
							if (this._reader._resCache.TryGetValue(text, out resourceLocator))
							{
								obj = resourceLocator.Value;
							}
							if (obj == null)
							{
								if (this._dataPosition == -1)
								{
									obj = this._reader.GetValueForNameIndex(this._currentName);
								}
								else
								{
									obj = this._reader.LoadObject(this._dataPosition);
								}
							}
						}
					}
					return new DictionaryEntry(text, obj);
				}
			}

			// Token: 0x1700124D RID: 4685
			// (get) Token: 0x06006C2D RID: 27693 RVA: 0x001760DC File Offset: 0x001742DC
			public object Value
			{
				get
				{
					if (this._currentName == -2147483648)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
					}
					if (!this._currentIsValid)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
					}
					if (this._reader._resCache == null)
					{
						throw new InvalidOperationException(Environment.GetResourceString("ResourceReaderIsClosed"));
					}
					return this._reader.GetValueForNameIndex(this._currentName);
				}
			}

			// Token: 0x06006C2E RID: 27694 RVA: 0x0017614C File Offset: 0x0017434C
			public void Reset()
			{
				if (this._reader._resCache == null)
				{
					throw new InvalidOperationException(Environment.GetResourceString("ResourceReaderIsClosed"));
				}
				this._currentIsValid = false;
				this._currentName = -1;
			}

			// Token: 0x04003455 RID: 13397
			private const int ENUM_DONE = -2147483648;

			// Token: 0x04003456 RID: 13398
			private const int ENUM_NOT_STARTED = -1;

			// Token: 0x04003457 RID: 13399
			private ResourceReader _reader;

			// Token: 0x04003458 RID: 13400
			private bool _currentIsValid;

			// Token: 0x04003459 RID: 13401
			private int _currentName;

			// Token: 0x0400345A RID: 13402
			private int _dataPosition;
		}
	}
}
