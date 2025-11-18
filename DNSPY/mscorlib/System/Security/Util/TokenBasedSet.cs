using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;

namespace System.Security.Util
{
	// Token: 0x0200037E RID: 894
	[Serializable]
	internal class TokenBasedSet
	{
		// Token: 0x06002C67 RID: 11367 RVA: 0x000A56FC File Offset: 0x000A38FC
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			this.OnDeserializedInternal();
		}

		// Token: 0x06002C68 RID: 11368 RVA: 0x000A5704 File Offset: 0x000A3904
		private void OnDeserializedInternal()
		{
			if (this.m_objSet != null)
			{
				if (this.m_cElt == 1)
				{
					this.m_Obj = this.m_objSet[this.m_maxIndex];
				}
				else
				{
					this.m_Set = this.m_objSet;
				}
				this.m_objSet = null;
			}
		}

		// Token: 0x06002C69 RID: 11369 RVA: 0x000A5750 File Offset: 0x000A3950
		[OnSerializing]
		private void OnSerializing(StreamingContext ctx)
		{
			if ((ctx.State & ~(StreamingContextStates.Clone | StreamingContextStates.CrossAppDomain)) != (StreamingContextStates)0)
			{
				if (this.m_cElt == 1)
				{
					this.m_objSet = new object[this.m_maxIndex + 1];
					this.m_objSet[this.m_maxIndex] = this.m_Obj;
					return;
				}
				if (this.m_cElt > 0)
				{
					this.m_objSet = this.m_Set;
				}
			}
		}

		// Token: 0x06002C6A RID: 11370 RVA: 0x000A57B9 File Offset: 0x000A39B9
		[OnSerialized]
		private void OnSerialized(StreamingContext ctx)
		{
			if ((ctx.State & ~(StreamingContextStates.Clone | StreamingContextStates.CrossAppDomain)) != (StreamingContextStates)0)
			{
				this.m_objSet = null;
			}
		}

		// Token: 0x06002C6B RID: 11371 RVA: 0x000A57D4 File Offset: 0x000A39D4
		internal bool MoveNext(ref TokenBasedSetEnumerator e)
		{
			int cElt = this.m_cElt;
			if (cElt == 0)
			{
				return false;
			}
			if (cElt != 1)
			{
				do
				{
					int num = e.Index + 1;
					e.Index = num;
					if (num > this.m_maxIndex)
					{
						goto Block_5;
					}
					e.Current = Volatile.Read<object>(ref this.m_Set[e.Index]);
				}
				while (e.Current == null);
				return true;
				Block_5:
				e.Current = null;
				return false;
			}
			if (e.Index == -1)
			{
				e.Index = this.m_maxIndex;
				e.Current = this.m_Obj;
				return true;
			}
			e.Index = (int)((short)(this.m_maxIndex + 1));
			e.Current = null;
			return false;
		}

		// Token: 0x06002C6C RID: 11372 RVA: 0x000A587C File Offset: 0x000A3A7C
		internal TokenBasedSet()
		{
			this.Reset();
		}

		// Token: 0x06002C6D RID: 11373 RVA: 0x000A589C File Offset: 0x000A3A9C
		internal TokenBasedSet(TokenBasedSet tbSet)
		{
			if (tbSet == null)
			{
				this.Reset();
				return;
			}
			if (tbSet.m_cElt > 1)
			{
				object[] set = tbSet.m_Set;
				int num = set.Length;
				object[] array = new object[num];
				Array.Copy(set, 0, array, 0, num);
				this.m_Set = array;
			}
			else
			{
				this.m_Obj = tbSet.m_Obj;
			}
			this.m_cElt = tbSet.m_cElt;
			this.m_maxIndex = tbSet.m_maxIndex;
		}

		// Token: 0x06002C6E RID: 11374 RVA: 0x000A5926 File Offset: 0x000A3B26
		internal void Reset()
		{
			this.m_Obj = null;
			this.m_Set = null;
			this.m_cElt = 0;
			this.m_maxIndex = -1;
		}

		// Token: 0x06002C6F RID: 11375 RVA: 0x000A594C File Offset: 0x000A3B4C
		internal void SetItem(int index, object item)
		{
			if (item == null)
			{
				this.RemoveItem(index);
				return;
			}
			int cElt = this.m_cElt;
			if (cElt == 0)
			{
				this.m_cElt = 1;
				this.m_maxIndex = (int)((short)index);
				this.m_Obj = item;
				return;
			}
			if (cElt != 1)
			{
				object[] array = this.m_Set;
				if (index >= array.Length)
				{
					object[] array2 = new object[index + 1];
					Array.Copy(array, 0, array2, 0, this.m_maxIndex + 1);
					this.m_maxIndex = (int)((short)index);
					array2[index] = item;
					this.m_Set = array2;
					this.m_cElt++;
					return;
				}
				if (array[index] == null)
				{
					this.m_cElt++;
				}
				array[index] = item;
				if (index > this.m_maxIndex)
				{
					this.m_maxIndex = (int)((short)index);
				}
				return;
			}
			else
			{
				if (index == this.m_maxIndex)
				{
					this.m_Obj = item;
					return;
				}
				object obj = this.m_Obj;
				int num = Math.Max(this.m_maxIndex, index);
				object[] array = new object[num + 1];
				array[this.m_maxIndex] = obj;
				array[index] = item;
				this.m_maxIndex = (int)((short)num);
				this.m_cElt = 2;
				this.m_Set = array;
				this.m_Obj = null;
				return;
			}
		}

		// Token: 0x06002C70 RID: 11376 RVA: 0x000A5A80 File Offset: 0x000A3C80
		internal object GetItem(int index)
		{
			int cElt = this.m_cElt;
			if (cElt == 0)
			{
				return null;
			}
			if (cElt != 1)
			{
				if (index < this.m_Set.Length)
				{
					return Volatile.Read<object>(ref this.m_Set[index]);
				}
				return null;
			}
			else
			{
				if (index == this.m_maxIndex)
				{
					return this.m_Obj;
				}
				return null;
			}
		}

		// Token: 0x06002C71 RID: 11377 RVA: 0x000A5AD8 File Offset: 0x000A3CD8
		internal object RemoveItem(int index)
		{
			object obj = null;
			int cElt = this.m_cElt;
			if (cElt != 0)
			{
				if (cElt != 1)
				{
					if (index < this.m_Set.Length && (obj = Volatile.Read<object>(ref this.m_Set[index])) != null)
					{
						Volatile.Write<object>(ref this.m_Set[index], null);
						this.m_cElt--;
						if (index == this.m_maxIndex)
						{
							this.ResetMaxIndex(this.m_Set);
						}
						if (this.m_cElt == 1)
						{
							this.m_Obj = Volatile.Read<object>(ref this.m_Set[this.m_maxIndex]);
							this.m_Set = null;
						}
					}
				}
				else if (index != this.m_maxIndex)
				{
					obj = null;
				}
				else
				{
					obj = this.m_Obj;
					this.Reset();
				}
			}
			else
			{
				obj = null;
			}
			return obj;
		}

		// Token: 0x06002C72 RID: 11378 RVA: 0x000A5BBC File Offset: 0x000A3DBC
		private void ResetMaxIndex(object[] aObj)
		{
			for (int i = aObj.Length - 1; i >= 0; i--)
			{
				if (aObj[i] != null)
				{
					this.m_maxIndex = (int)((short)i);
					return;
				}
			}
			this.m_maxIndex = -1;
		}

		// Token: 0x06002C73 RID: 11379 RVA: 0x000A5BF2 File Offset: 0x000A3DF2
		internal int GetStartingIndex()
		{
			if (this.m_cElt <= 1)
			{
				return this.m_maxIndex;
			}
			return 0;
		}

		// Token: 0x06002C74 RID: 11380 RVA: 0x000A5C07 File Offset: 0x000A3E07
		internal int GetCount()
		{
			return this.m_cElt;
		}

		// Token: 0x06002C75 RID: 11381 RVA: 0x000A5C0F File Offset: 0x000A3E0F
		internal int GetMaxUsedIndex()
		{
			return this.m_maxIndex;
		}

		// Token: 0x06002C76 RID: 11382 RVA: 0x000A5C19 File Offset: 0x000A3E19
		internal bool FastIsEmpty()
		{
			return this.m_cElt == 0;
		}

		// Token: 0x06002C77 RID: 11383 RVA: 0x000A5C24 File Offset: 0x000A3E24
		internal TokenBasedSet SpecialUnion(TokenBasedSet other)
		{
			this.OnDeserializedInternal();
			TokenBasedSet tokenBasedSet = new TokenBasedSet();
			int num;
			if (other != null)
			{
				other.OnDeserializedInternal();
				num = ((this.GetMaxUsedIndex() > other.GetMaxUsedIndex()) ? this.GetMaxUsedIndex() : other.GetMaxUsedIndex());
			}
			else
			{
				num = this.GetMaxUsedIndex();
			}
			for (int i = 0; i <= num; i++)
			{
				object item = this.GetItem(i);
				IPermission permission = item as IPermission;
				ISecurityElementFactory securityElementFactory = item as ISecurityElementFactory;
				object obj = ((other != null) ? other.GetItem(i) : null);
				IPermission permission2 = obj as IPermission;
				ISecurityElementFactory securityElementFactory2 = obj as ISecurityElementFactory;
				if (item != null || obj != null)
				{
					if (item == null)
					{
						if (securityElementFactory2 != null)
						{
							permission2 = PermissionSet.CreatePerm(securityElementFactory2, false);
						}
						PermissionToken token = PermissionToken.GetToken(permission2);
						if (token == null)
						{
							throw new SerializationException(Environment.GetResourceString("Serialization_InsufficientState"));
						}
						tokenBasedSet.SetItem(token.m_index, permission2);
					}
					else if (obj == null)
					{
						if (securityElementFactory != null)
						{
							permission = PermissionSet.CreatePerm(securityElementFactory, false);
						}
						PermissionToken token2 = PermissionToken.GetToken(permission);
						if (token2 == null)
						{
							throw new SerializationException(Environment.GetResourceString("Serialization_InsufficientState"));
						}
						tokenBasedSet.SetItem(token2.m_index, permission);
					}
				}
			}
			return tokenBasedSet;
		}

		// Token: 0x06002C78 RID: 11384 RVA: 0x000A5D3C File Offset: 0x000A3F3C
		internal void SpecialSplit(ref TokenBasedSet unrestrictedPermSet, ref TokenBasedSet normalPermSet, bool ignoreTypeLoadFailures)
		{
			int maxUsedIndex = this.GetMaxUsedIndex();
			for (int i = this.GetStartingIndex(); i <= maxUsedIndex; i++)
			{
				object item = this.GetItem(i);
				if (item != null)
				{
					IPermission permission = item as IPermission;
					if (permission == null)
					{
						permission = PermissionSet.CreatePerm(item, ignoreTypeLoadFailures);
					}
					PermissionToken token = PermissionToken.GetToken(permission);
					if (permission != null && token != null)
					{
						if (permission is IUnrestrictedPermission)
						{
							if (unrestrictedPermSet == null)
							{
								unrestrictedPermSet = new TokenBasedSet();
							}
							unrestrictedPermSet.SetItem(token.m_index, permission);
						}
						else
						{
							if (normalPermSet == null)
							{
								normalPermSet = new TokenBasedSet();
							}
							normalPermSet.SetItem(token.m_index, permission);
						}
					}
				}
			}
		}

		// Token: 0x040011D2 RID: 4562
		private int m_initSize = 24;

		// Token: 0x040011D3 RID: 4563
		private int m_increment = 8;

		// Token: 0x040011D4 RID: 4564
		private object[] m_objSet;

		// Token: 0x040011D5 RID: 4565
		[OptionalField(VersionAdded = 2)]
		private volatile object m_Obj;

		// Token: 0x040011D6 RID: 4566
		[OptionalField(VersionAdded = 2)]
		private volatile object[] m_Set;

		// Token: 0x040011D7 RID: 4567
		private int m_cElt;

		// Token: 0x040011D8 RID: 4568
		private volatile int m_maxIndex;
	}
}
