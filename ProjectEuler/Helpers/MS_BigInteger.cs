using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;
using System.Text;
using System.Globalization;

using Microsoft.Contracts;



namespace ProjectEuler.Helpers
{
	[Serializable, StructLayout(LayoutKind.Sequential), Immutable, ComVisible(false)]
	internal struct MS_BigInteger : IFormattable, IEquatable<MS_BigInteger>, IComparable<MS_BigInteger>, IComparable
	{
		//private const int DecimalScaleFactorMask = 0xff0000;
		//private const int DecimalSignMask = -2147483648;
		//private const int BitsPerDigit = 0x20;
		//private const ulong Base = 0x100000000L;
		//private const int UpperBoundForSchoolBookMultiplicationDigits = 0x40;
		//private const int ForceSchoolBookMultiplicationThresholdDigits = 8;
		private static readonly uint[] maxCharsPerDigit;
		private static readonly uint[] groupRadixValues;
		private static readonly uint[] zeroArray;
		private readonly short _sign;
		private readonly uint[] _data;
		private int _length;

		public bool IsZero()
		{
			return (_data.Length==0 || _data[0] == 0 || this._length == 0 || this == MS_BigInteger.Zero);
		}

		public static MS_BigInteger Zero
		{
			get
			{
				return new MS_BigInteger(0, zeroArray);
			}
		}
		public static MS_BigInteger One
		{
			get
			{
				return new MS_BigInteger(1);
			}
		}
		public static MS_BigInteger MinusOne
		{
			get
			{
				return new MS_BigInteger(-1);
			}
		}

		public int LengthNum { get { return this.ToString().Length; } }

		public static MS_BigInteger MathPower(int number, int power)
		{
			MS_BigInteger result = 1;
			for (int i = 1; i <= power; i++)
			{
				result = MathMultiply(result, number);
			}
			return result;
		}

		public static MS_BigInteger MathMultiply(MS_BigInteger num1, MS_BigInteger num2)
		{
			bool isNegative = (num1.Sign==-1 ^ num2.Sign==-1);

			MS_BigInteger a = MS_BigInteger.Abs(num1);
			MS_BigInteger b = MS_BigInteger.Abs(num2);

			MS_BigInteger bigger;
			MS_BigInteger smaller;

			if (a > b)
			{
				bigger = a;
				smaller = b;
			}
			else
			{
				bigger = b;
				smaller = a;
			}

			MS_BigInteger result = 0;
			for (MS_BigInteger i = 1; i <= smaller; i++)
			{
				result += bigger;
			}

			if(isNegative) result = new MS_BigInteger(-1, result._data);

			return result;
		}



		public MS_BigInteger(int value)
		{
			if (value == 0)
			{
				_sign = 0;
				_data = new uint[0];
			}
			else if (value < 0)
			{
				_sign = -1;
				_data = new uint[] { (uint)-value };
			}
			else
			{
				_sign = 1;
				_data = new uint[] { (uint)value };
			}
			_length = -1;
		}

		public MS_BigInteger(long value)
		{
			ulong num = 0L;
			if (value < 0L)
			{
				num = (ulong)-value;
				_sign = -1;
			}
			else if (value > 0L)
			{
				num = (ulong)value;
				_sign = 1;
			}
			else
			{
				_sign = 0;
			}
			if (num >= 0x100000000L)
			{
				_data = new uint[] { (uint)num, (uint)(num >> 0x20) };
			}
			else
			{
				_data = new uint[] { (uint)num };
			}
			_length = -1;
		}

		public MS_BigInteger(uint value)
		{
			if (value == 0)
			{
				_sign = 0;
			}
			else
			{
				_sign = 1;
			}
			_data = new uint[] { value };
			_length = -1;
		}

		public MS_BigInteger(ulong value)
		{
			if (value == 0L)
			{
				_sign = 0;
			}
			else
			{
				_sign = 1;
			}
			if (value >= 0x100000000L)
			{
				_data = new uint[] { (uint)value, (uint)(value >> 0x20) };
			}
			else
			{
				_data = new uint[] { (uint)value };
			}
			_length = -1;
		}

		public MS_BigInteger(float value)
			: this((double)value)
		{
		}

		public MS_BigInteger(double value)
		{
			Contract.Requires(!double.IsInfinity(value) ? null : ((Exception)new OverflowException(Res.BigIntInfinity)));
			Contract.Requires(!double.IsNaN(value) ? null : ((Exception)new OverflowException(Res.NotANumber)));
			byte[] bytes = BitConverter.GetBytes(value);
			ulong num = Mantissa(bytes);
			if (num == 0L)
			{
				int num2 = Exponent(bytes);
				if (num2 == 0)
				{
					_sign = 0;
					_data = zeroArray;
					_length = 0;
					return;
				}
				MS_BigInteger x = IsNegative(bytes) ? Negate(One) : One;
				x = LeftShift(x, num2 - 0x3ff);
				_sign = x._sign;
				_data = x._data;
			}
			else
			{
				int num3 = Exponent(bytes);
				num |= (ulong)0x10000000000000L;
				MS_BigInteger integer2 = new MS_BigInteger(num);
				integer2 = (num3 > 0x433) ? LeftShift(integer2, num3 - 0x433) : RightShift(integer2, 0x433 - num3);
				_sign = IsNegative(bytes) ? ((short)(integer2._sign * -1)) : integer2._sign;
				_data = integer2._data;
			}
			_length = -1;
		}

		public MS_BigInteger(decimal value)
		{
			int[] bits = decimal.GetBits(decimal.Truncate(value));
			int num = 3;
			while ((num > 0) && (bits[num - 1] == 0))
			{
				num--;
			}
			_length = num;
			if (num == 0)
			{
				_sign = 0;
				_data = new uint[0];
			}
			else
			{
				uint[] numArray2 = new uint[num];
				numArray2[0] = (uint)bits[0];
				if (num > 1)
				{
					numArray2[1] = (uint)bits[1];
				}
				if (num > 2)
				{
					numArray2[2] = (uint)bits[2];
				}
				_sign = ((bits[3] & -2147483648) != 0) ? ((short)(-1)) : ((short)1);
				_data = numArray2;
			}
		}

		public MS_BigInteger(byte[] value)
			: this(value, false)
		{
		}

		public MS_BigInteger(byte[] value, bool negative)
		{
			Contract.Requires((value != null) ? null : ((Exception)new ArgumentNullException("value")));
			int index = value.Length / 4;
			int num2 = value.Length % 4;
			if (num2 > 0)
			{
				_data = new uint[index + 1];
			}
			else
			{
				_data = new uint[index];
			}
			Buffer.BlockCopy(value, 0, _data, 0, index * 4);
			if (num2 > 0)
			{
				uint num3 = 0;
				for (int i = 0; i < num2; i++)
				{
					num3 |= (uint)(value[(index * 4) + i] << (8 * i));
				}
				_data[index] = num3;
			}
			_sign = negative ? ((short)(-1)) : ((short)1);
			_length = -1;
			if (Length == 0)
			{
				_sign = 0;
				_data = zeroArray;
			}
		}

		public MS_BigInteger(int _signIn, params uint[] _dataIn)
		{
			Contract.Requires(_dataIn != null);
			Contract.Requires((_signIn >= -1) && (_signIn <= 1));
			Contract.Requires((_signIn != 0) || (GetLength(_dataIn) == 0));
			if (GetLength(_dataIn) == 0)
			{
				_sign = 0;
			}
			_data = _dataIn;
			_sign = (short)_signIn;
			_length = -1;
		}

		public static MS_BigInteger Abs(MS_BigInteger x)
		{
			if (x._sign == -1)
			{
				return (0 - x);
			}
			return x;
		}

		public static MS_BigInteger GreatestCommonDivisor(MS_BigInteger x, MS_BigInteger y)
		{
			MS_BigInteger integer;
			MS_BigInteger integer2;
			Contract.Requires((x.Sign != 0) ? null : ((Exception)new ArgumentOutOfRangeException("x", Res.MustBePositive)));
			Contract.Requires((y.Sign != 0) ? null : ((Exception)new ArgumentOutOfRangeException("y", Res.MustBePositive)));
			x = Abs(x);
			y = Abs(y);
			int num = Compare(x, y);
			if (num == 0)
			{
				return x;
			}
			if (num < 1)
			{
				integer = x;
				integer2 = y;
			}
			else
			{
				integer = y;
				integer2 = x;
			}
			do
			{
				MS_BigInteger integer3;
				MS_BigInteger integer4 = integer2;
				DivRem(integer, integer2, out integer3);
				integer2 = integer3;
				integer = integer4;
			}
			while (integer2 != 0);
			return integer;
		}

		public static MS_BigInteger Remainder(MS_BigInteger dividend, MS_BigInteger divisor)
		{
			MS_BigInteger integer;
			DivRem(dividend, divisor, out integer);
			return integer;
		}

		public static MS_BigInteger Negate(MS_BigInteger x)
		{
			MS_BigInteger integer = new MS_BigInteger(-x._sign, (x._data == null) ? zeroArray : x._data);
			integer._length = x._length;
			return integer;
		}

		public static MS_BigInteger Pow(MS_BigInteger baseValue, MS_BigInteger exponent)
		{
			Contract.Requires((exponent >= 0) ? null : ((Exception)new ArgumentOutOfRangeException("exponent", Res.NonNegative)));
			if (exponent == 0)
			{
				return One;
			}
			MS_BigInteger integer = baseValue;
			MS_BigInteger one = One;
			while (exponent > 0)
			{
				if ((exponent._data[0] & 1) != 0)
				{
					one *= integer;
				}
				if (exponent == 1)
				{
					return one;
				}
				integer = integer.Square();
				exponent = RightShift(exponent, 1);
			}
			return one;
		}

		public static MS_BigInteger ModPow(MS_BigInteger baseValue, MS_BigInteger exponent, MS_BigInteger modulus)
		{
			Contract.Requires((exponent >= 0) ? null : ((Exception)new ArgumentOutOfRangeException("exponent", Res.NonNegative)));
			if (exponent == 0)
			{
				return One;
			}
			MS_BigInteger integer = baseValue;
			MS_BigInteger one = One;
			while (exponent > 0)
			{
				if ((exponent._data[0] & 1) != 0)
				{
					one *= integer;
					one = one % modulus;// op_Modulus(one, modulus);
				}
				if (exponent == 1)
				{
					return one;
				}
				integer = integer.Square();
				exponent = RightShift(exponent, 1);
			}
			return one;
		}

		private MS_BigInteger Square()
		{
			return (this * this);
		}

		public byte[] ToByteArray()
		{
			bool flag;
			return ToByteArray(out flag);
		}

		public byte[] ToByteArray(out bool isNegative)
		{
			int length = Length;
			byte[] dst = new byte[length * 4];
			Buffer.BlockCopy(_data, 0, dst, 0, length * 4);
			isNegative = _sign == -1;
			return dst;
		}

		public int Sign
		{
			[Pure]
			get
			{
				return _sign;
			}
		}
		public static MS_BigInteger operator +(MS_BigInteger value)
		{
			return value;
		}

		public static MS_BigInteger operator -(MS_BigInteger value)
		{
			return Negate(value);
		}

		public static MS_BigInteger operator ++(MS_BigInteger value)
		{
			if (value._sign >= 0)
			{
				return new MS_BigInteger(1, add0(value._data, value.Length, new uint[] { 1 }, 1));
			}
			if ((value.Length == 1) && (value._data[0] == 1))
			{
				return Zero;
			}
			return new MS_BigInteger(-1, sub(value._data, value.Length, new uint[] { 1 }, 1));
		}

		public static MS_BigInteger operator --(MS_BigInteger value)
		{
			uint[] numArray;
			int num;
			int length = value.Length;
			if (value._sign == 1)
			{
				if ((length == 1) && (value._data[0] == 1))
				{
					return Zero;
				}
				numArray = sub(value._data, length, new uint[] { 1 }, 1);
				num = 1;
			}
			else
			{
				numArray = add0(value._data, length, new uint[] { 1 }, 1);
				num = -1;
			}
			return new MS_BigInteger(num, numArray);
		}

		public static MS_BigInteger operator %(MS_BigInteger x, MS_BigInteger y)
		{
			MS_BigInteger integer;
			if ((x._sign == y._sign) && (x.Length < y.Length))
			{
				return x;
			}
			DivRem(x, y, out integer);
			return integer;
		}

		public static explicit operator byte(MS_BigInteger value)
		{
			if (value._sign == 0)
			{
				return 0;
			}
			if (value.Length > 1)
			{
				throw new OverflowException(Res.Overflow_Byte);
			}
			if (value._data[0] > 0xff)
			{
				throw new OverflowException(Res.Overflow_Byte);
			}
			if (value._sign < 0)
			{
				throw new OverflowException(Res.Overflow_Byte);
			}
			return (byte)value._data[0];
		}

		public static explicit operator sbyte(MS_BigInteger value)
		{
			if (value._sign == 0)
			{
				return 0;
			}
			if (value.Length > 1)
			{
				throw new OverflowException(Res.Overflow_SByte);
			}
			if (value._data[0] > 0x80)
			{
				throw new OverflowException(Res.Overflow_SByte);
			}
			if ((value._data[0] == 0x80) && (value._sign == 1))
			{
				throw new OverflowException(Res.Overflow_SByte);
			}
			sbyte num = (sbyte)value._data[0];
			return (sbyte)(num * ((sbyte)value._sign));
		}

		public static explicit operator short(MS_BigInteger value)
		{
			if (value._sign == 0)
			{
				return 0;
			}
			if (value.Length > 1)
			{
				throw new OverflowException(Res.Overflow_Int16);
			}
			if (value._data[0] > 0x8000)
			{
				throw new OverflowException(Res.Overflow_Int16);
			}
			if ((value._data[0] == 0x8000) && (value._sign == 1))
			{
				throw new OverflowException(Res.Overflow_Int16);
			}
			short num = (short)value._data[0];
			return (short)(num * value._sign);
		}

		public static explicit operator ushort(MS_BigInteger value)
		{
			if (value._sign == 0)
			{
				return 0;
			}
			if (value.Length > 1)
			{
				throw new OverflowException(Res.Overflow_UInt16);
			}
			if (value._data[0] > 0xffff)
			{
				throw new OverflowException(Res.Overflow_UInt16);
			}
			if (value._sign < 0)
			{
				throw new OverflowException(Res.Overflow_UInt16);
			}
			return (ushort)value._data[0];
		}

		public static explicit operator int(MS_BigInteger value)
		{
			if (value._sign == 0)
			{
				return 0;
			}
			if (value.Length > 1)
			{
				throw new OverflowException(Res.Overflow_Int32);
			}
			if (value._data[0] > 0x80000000)
			{
				throw new OverflowException(Res.Overflow_Int32);
			}
			if ((value._data[0] == 0x80000000) && (value._sign == 1))
			{
				throw new OverflowException(Res.Overflow_Int32);
			}
			int num = (int)value._data[0];
			return (num * value._sign);
		}

		public static explicit operator uint(MS_BigInteger value)
		{
			if (value._sign == 0)
			{
				return 0;
			}
			if (value.Length > 1)
			{
				throw new OverflowException(Res.Overflow_UInt32);
			}
			if (value._sign < 0)
			{
				throw new OverflowException(Res.Overflow_UInt32);
			}
			return value._data[0];
		}

		public static explicit operator long(MS_BigInteger value)
		{
			if (value._sign == 0)
			{
				return 0L;
			}
			if (value.Length > 2)
			{
				throw new OverflowException(Res.Overflow_Int64);
			}
			if (value.Length == 1)
			{
				return (value._sign * value._data[0]);
			}
			ulong num2 = (value._data[1] << 0x20) | value._data[0];
			if (num2 > 9223372036854775808L)
			{
				throw new OverflowException(Res.Overflow_Int64);
			}
			if ((num2 == 9223372036854775808L) && (value._sign == 1))
			{
				throw new OverflowException(Res.Overflow_Int64);
			}
			return (long)((long)num2 * (long)value._sign);
		}

		public static explicit operator ulong(MS_BigInteger value)
		{
			ulong num = 0L;
			if (value._sign == 0)
			{
				return 0L;
			}
			if (value._sign < 0)
			{
				throw new OverflowException(Res.Overflow_UInt64);
			}
			if (value.Length > 2)
			{
				throw new OverflowException(Res.Overflow_UInt64);
			}
			num = value._data[0];
			if (value.Length > 1)
			{
				num |= value._data[1] << 0x20;
			}
			return num;
		}

		public static explicit operator float(MS_BigInteger value)
		{
			float num;
			NumberFormatInfo numberFormat = CultureInfo.InvariantCulture.NumberFormat;
			if (!float.TryParse(value.ToString(10, false, numberFormat), NumberStyles.Number, (IFormatProvider)numberFormat, out num))
			{
				throw new OverflowException(Res.Overflow_Single);
			}
			return num;
		}

		public static explicit operator double(MS_BigInteger value)
		{
			double num;
			NumberFormatInfo numberFormat = CultureInfo.InvariantCulture.NumberFormat;
			if (!double.TryParse(value.ToString(10, false, numberFormat), NumberStyles.Number, (IFormatProvider)numberFormat, out num))
			{
				throw new OverflowException(Res.Overflow_Double);
			}
			return num;
		}

		public static explicit operator decimal(MS_BigInteger value)
		{
			if (value._sign == 0)
			{
				return 0M;
			}
			int length = value.Length;
			if (length > 3)
			{
				throw new OverflowException(Res.Overflow_Decimal);
			}
			int lo = 0;
			int mid = 0;
			int hi = 0;
			if (length > 2)
			{
				hi = (int)value._data[2];
			}
			if (length > 1)
			{
				mid = (int)value._data[1];
			}
			if (length > 0)
			{
				lo = (int)value._data[0];
			}
			return new decimal(lo, mid, hi, value._sign < 0, 0);
		}

		public static explicit operator MS_BigInteger(float value)
		{
			return new MS_BigInteger(value);
		}

		public static explicit operator MS_BigInteger(double value)
		{
			return new MS_BigInteger(value);
		}

		public static explicit operator MS_BigInteger(decimal value)
		{
			return new MS_BigInteger(value);
		}

		public static implicit operator MS_BigInteger(byte value)
		{
			return new MS_BigInteger(value);
		}

		public static implicit operator MS_BigInteger(sbyte value)
		{
			return new MS_BigInteger(value);
		}

		public static implicit operator MS_BigInteger(short value)
		{
			return new MS_BigInteger(value);
		}

		public static implicit operator MS_BigInteger(ushort value)
		{
			return new MS_BigInteger(value);
		}

		[Pure]
		public static implicit operator MS_BigInteger(int value)
		{
			return new MS_BigInteger(value);
		}

		public static implicit operator MS_BigInteger(uint value)
		{
			return new MS_BigInteger(value);
		}

		public static implicit operator MS_BigInteger(long value)
		{
			return new MS_BigInteger(value);
		}

		public static implicit operator MS_BigInteger(ulong value)
		{
			return new MS_BigInteger(value);
		}

		private static bool IsNegative(byte[] doubleBits)
		{
			Contract.Requires(doubleBits.Length == 8);
			return ((doubleBits[7] & 0x80) != 0);
		}

		private static ushort Exponent(byte[] doubleBits)
		{
			Contract.Requires(doubleBits.Length == 8);
			return (ushort)((((ushort)(doubleBits[7] & 0x7f)) << 4) | (((ushort)(doubleBits[6] & 240)) >> 4));
		}

		private static ulong Mantissa(byte[] doubleBits)
		{
			Contract.Requires(doubleBits.Length == 8);
			uint num = (uint)(((doubleBits[0] | (doubleBits[1] << 8)) | (doubleBits[2] << 0x10)) | (doubleBits[3] << 0x18));
			uint num2 = (uint)((doubleBits[4] | (doubleBits[5] << 8)) | ((doubleBits[6] & 15) << 0x10));
			return (num | (num2 << 0x20));
		}

		public int Length
		{
			get
			{
				if (_length == -1)
				{
					_length = GetLength(_data);
				}
				return _length;
			}
		}
		private static int GetLength(uint[] _data)
		{
			if (_data == null)
			{
				return 0;
			}
			int index = _data.Length - 1;
			while ((index >= 0) && (_data[index] == 0))
			{
				index--;
			}
			return (index + 1);
		}

		private static uint[] copy(uint[] v)
		{
			uint[] destinationArray = new uint[v.Length];
			Array.Copy(v, destinationArray, v.Length);
			return destinationArray;
		}

		private static uint[] resize(uint[] v, int len)
		{
			if (v.Length == len)
			{
				return v;
			}
			uint[] destinationArray = new uint[len];
			int length = Math.Min(v.Length, len);
			Array.Copy(v, destinationArray, length);
			return destinationArray;
		}

		private static uint[] add0(uint[] x, int xl, uint[] y, int yl)
		{
			if (xl >= yl)
			{
				return InternalAdd(x, xl, y, yl);
			}
			return InternalAdd(y, yl, x, xl);
		}

		private static uint[] InternalAdd(uint[] x, int xl, uint[] y, int yl)
		{
			uint[] v = new uint[xl];
			ulong num2 = 0L;
			int index = 0;
			while (index < yl)
			{
				num2 = (num2 + x[index]) + y[index];
				v[index] = (uint)num2;
				num2 = num2 >> 0x20;
				index++;
			}
			while ((index < xl) && (num2 != 0L))
			{
				num2 += x[index];
				v[index] = (uint)num2;
				num2 = num2 >> 0x20;
				index++;
			}
			if (num2 == 0L)
			{
				while (index < xl)
				{
					v[index] = x[index];
					index++;
				}
				return v;
			}
			v = resize(v, xl + 1);
			v[index] = (uint)num2;
			return v;
		}

		private static uint[] sub(uint[] x, int xl, uint[] y, int yl)
		{
			uint[] numArray = new uint[xl];
			bool flag = false;
			int index = 0;
			while (index < yl)
			{
				uint maxValue = x[index];
				uint num3 = y[index];
				if (flag)
				{
					if (maxValue == 0)
					{
						maxValue = uint.MaxValue;
						flag = true;
					}
					else
					{
						maxValue--;
						flag = false;
					}
				}
				if (num3 > maxValue)
				{
					flag = true;
				}
				numArray[index] = maxValue - num3;
				index++;
			}
			if (flag)
			{
				while (index < xl)
				{
					uint num4 = x[index];
					numArray[index] = num4 - 1;
					if (num4 != 0)
					{
						index++;
						break;
					}
					index++;
				}
			}
			while (index < xl)
			{
				numArray[index] = x[index];
				index++;
			}
			return numArray;
		}

		[Pure]
		public static int Compare(MS_BigInteger x, MS_BigInteger y)
		{
			if (x._sign == y._sign)
			{
				int length = x.Length;
				int num2 = y.Length;
				if (length == num2)
				{
					for (int i = length - 1; i >= 0; i--)
					{
						if (x._data[i] != y._data[i])
						{
							if (x._data[i] <= y._data[i])
							{
								return -x._sign;
							}
							return x._sign;
						}
					}
					return 0;
				}
				if (length <= num2)
				{
					return -x._sign;
				}
				return x._sign;
			}
			if (x._sign <= y._sign)
			{
				return -1;
			}
			return 1;
		}

		[Pure]
		public static bool operator ==(MS_BigInteger x, MS_BigInteger y)
		{
			return (Compare(x, y) == 0);
		}

		[Pure]
		public static bool operator !=(MS_BigInteger x, MS_BigInteger y)
		{
			return (Compare(x, y) != 0);
		}

		[Pure]
		public static bool operator <(MS_BigInteger x, MS_BigInteger y)
		{
			return (Compare(x, y) < 0);
		}

		[Pure]
		public static bool operator <=(MS_BigInteger x, MS_BigInteger y)
		{
			return (Compare(x, y) <= 0);
		}

		[Pure]
		public static bool operator >(MS_BigInteger x, MS_BigInteger y)
		{
			return (Compare(x, y) > 0);
		}

		[Pure]
		public static bool operator >=(MS_BigInteger x, MS_BigInteger y)
		{
			return (Compare(x, y) >= 0);
		}

		public static MS_BigInteger Add(MS_BigInteger x, MS_BigInteger y)
		{
			return (x + y);
		}

		public static MS_BigInteger operator +(MS_BigInteger x, MS_BigInteger y)
		{
			if (x._sign == y._sign)
			{
				return new MS_BigInteger(x._sign, add0(x._data, x.Length, y._data, y.Length));
			}
			return (x - (-y));// op_UnaryNegation(-y));
		}

		public static MS_BigInteger Subtract(MS_BigInteger x, MS_BigInteger y)
		{
			return (x - y);
		}

		public static MS_BigInteger operator -(MS_BigInteger x, MS_BigInteger y)
		{
			uint[] numArray;
			int num = Compare(x, y);
			if (num != 0)
			{
				if (x._sign != y._sign)
				{
					return new MS_BigInteger(num, add0(x._data, x.Length, y._data, y.Length));
				}
				switch ((num * x._sign))
				{
					case -1:
						numArray = sub(y._data, y.Length, x._data, x.Length);
						goto Label_008F;

					case 1:
						numArray = sub(x._data, x.Length, y._data, y.Length);
						goto Label_008F;
				}
			}
			return Zero;
		Label_008F:
			return new MS_BigInteger(num, numArray);
		}

		//public static MS_BigInteger Multiply(MS_BigInteger x, MS_BigInteger y)
		//{
		//    int length = x.Length;
		//    int num2 = y.Length;
		//    if ((((length + num2) >= 0x40) && (length >= 8)) && (num2 >= 8))
		//    {
		//        return MultiplyKaratsuba(x, y);
		//    }
		//    return MultiplySchoolBook(x, y);
		//}

		[Pure]
		public static MS_BigInteger operator *(MS_BigInteger x, MS_BigInteger y)
		{
			//return Multiply(x, y);
			return MathMultiply(x, y);
		}

		private static MS_BigInteger MultiplySchoolBook(MS_BigInteger x, MS_BigInteger y)
		{
			int length = x.Length;
			int num2 = y.Length;
			int num3 = length + num2;
			uint[] numArray = x._data;
			uint[] numArray2 = y._data;
			uint[] numArray3 = new uint[num3];
			for (int i = 0; i < length; i++)
			{
				uint num5 = numArray[i];
				int index = i;
				ulong num7 = 0L;
				for (int j = 0; j < num2; j++)
				{
					num7 = (num7 + (num5 * numArray2[j])) + numArray3[index];
					numArray3[index++] = (uint)num7;
					num7 = num7 >> 0x20;
				}
				while (num7 != 0L)
				{
					num7 += numArray3[index];
					numArray3[index++] = (uint)num7;
					num7 = num7 >> 0x20;
				}
			}
			return new MS_BigInteger(x._sign * y._sign, numArray3);
		}

		private static MS_BigInteger MultiplyKaratsuba(MS_BigInteger x, MS_BigInteger y)
		{
			int numDigits = Math.Max(x.Length, y.Length) / 2;
			if (((numDigits <= 0x10) || (x.Length < 0x10)) || (y.Length < 0x10))
			{
				return MultiplySchoolBook(x, y);
			}
			int shift = 0x20 * numDigits;
			MS_BigInteger integer = RightShift(x, shift);
			MS_BigInteger integer2 = x.RestrictTo(numDigits);
			MS_BigInteger integer3 = RightShift(y, shift);
			MS_BigInteger integer4 = y.RestrictTo(numDigits);
			MS_BigInteger integer5 = MathMultiply(integer, integer3);
			MS_BigInteger integer6 = MathMultiply(integer2, integer4);
			MS_BigInteger integer8 = MathMultiply(integer + integer2, integer3 + integer4) - (integer5 + integer6);
			return (integer6 + LeftShift(integer8 + LeftShift(integer5, shift), shift));
		}

		private MS_BigInteger RestrictTo(int numDigits)
		{
			Contract.Requires(numDigits > 0);
			int num = Math.Min(numDigits, Length);
			if (num == Length)
			{
				return this;
			}
			MS_BigInteger integer = new MS_BigInteger(_sign, _data);
			integer._length = num;
			return integer;
		}

		public static MS_BigInteger Divide(MS_BigInteger dividend, MS_BigInteger divisor)
		{
			return (dividend / divisor);
		}

		public static MS_BigInteger operator /(MS_BigInteger dividend, MS_BigInteger divisor)
		{
			MS_BigInteger integer;
			return DivRem(dividend, divisor, out integer);
		}

		private static int GetNormalizeShift(uint value)
		{
			int num = 0;
			if ((value & 0xffff0000) == 0)
			{
				value = value << 0x10;
				num += 0x10;
			}
			if ((value & 0xff000000) == 0)
			{
				value = value << 8;
				num += 8;
			}
			if ((value & 0xf0000000) == 0)
			{
				value = value << 4;
				num += 4;
			}
			if ((value & 0xc0000000) == 0)
			{
				value = value << 2;
				num += 2;
			}
			if ((value & 0x80000000) == 0)
			{
				value = value << 1;
				num++;
			}
			return num;
		}

		[Conditional("DEBUG")]
		private static void TestNormalize(uint[] u, uint[] un, int shift)
		{
			new MS_BigInteger(1, u);
			MS_BigInteger x = new MS_BigInteger(1, un);
			RightShift(x, shift);
		}

		[Conditional("DEBUG")]
		private static void TestDivisionStep(uint[] un, uint[] vn, uint[] q, uint[] u, uint[] v)
		{
			int length = GetLength(v);
			int normalizeShift = GetNormalizeShift(v[length - 1]);
			MS_BigInteger integer = new MS_BigInteger(1, un);
			MS_BigInteger integer2 = new MS_BigInteger(1, vn);
			MS_BigInteger integer3 = new MS_BigInteger(1, q);
			MS_BigInteger x = new MS_BigInteger(1, u);
			MS_BigInteger integer1 = (integer2 * integer3) + integer;
			LeftShift(x, normalizeShift);
		}

		[Conditional("DEBUG")]
		private static void TestResult(uint[] u, uint[] v, uint[] q, uint[] r)
		{
			new MS_BigInteger(1, u);
			MS_BigInteger integer = new MS_BigInteger(1, v);
			MS_BigInteger integer2 = new MS_BigInteger(1, q);
			MS_BigInteger integer3 = new MS_BigInteger(1, r);
			MS_BigInteger integer4 = integer * integer2;
			MS_BigInteger integer1 = integer4 + integer3;
		}

		private static void Normalize(uint[] u, int l, uint[] un, int shift)
		{
			int num2;
			uint num = 0;
			if (shift > 0)
			{
				int num3 = 0x20 - shift;
				for (num2 = 0; num2 < l; num2++)
				{
					uint num4 = u[num2];
					un[num2] = (num4 << shift) | num;
					num = num4 >> num3;
				}
			}
			else
			{
				num2 = 0;
				while (num2 < l)
				{
					un[num2] = u[num2];
					num2++;
				}
			}
			while (num2 < un.Length)
			{
				un[num2++] = 0;
			}
			if (num != 0)
			{
				un[l] = num;
			}
		}

		private static void Unnormalize(uint[] un, out uint[] r, int shift)
		{
			int length = GetLength(un);
			r = new uint[length];
			if (shift > 0)
			{
				int num2 = 0x20 - shift;
				uint num3 = 0;
				for (int i = length - 1; i >= 0; i--)
				{
					uint num5 = un[i];
					r[i] = (num5 >> shift) | num3;
					num3 = num5 << num2;
				}
			}
			else
			{
				for (int j = 0; j < length; j++)
				{
					r[j] = un[j];
				}
			}
		}

		private static void DivModUnsigned(uint[] u, uint[] v, out uint[] q, out uint[] r)
		{
			int length = GetLength(u);
			int l = GetLength(v);
			if (l <= 1)
			{
				if (l == 0)
				{
					throw new DivideByZeroException();
				}
				ulong num3 = 0L;
				uint num4 = v[0];
				q = new uint[length];
				r = new uint[1];
				for (int i = length - 1; i >= 0; i--)
				{
					num3 *= (ulong)0x100000000L;
					num3 += u[i];
					ulong num6 = num3 / ((ulong)num4);
					num3 -= num6 * num4;
					q[i] = (uint)num6;
				}
				r[0] = (uint)num3;
			}
			else if (length >= l)
			{
				int normalizeShift = GetNormalizeShift(v[l - 1]);
				uint[] un = new uint[length + 1];
				uint[] numArray2 = new uint[l];
				Normalize(u, length, un, normalizeShift);
				Normalize(v, l, numArray2, normalizeShift);
				q = new uint[(length - l) + 1];
				r = null;
				for (int j = length - l; j >= 0; j--)
				{
					ulong num9 = (ulong)((0x100000000L * un[j + l]) + un[(j + l) - 1]);
					ulong num10 = num9 / ((ulong)numArray2[l - 1]);
					num9 -= num10 * numArray2[l - 1];
					do
					{
						if ((num10 < 0x100000000L) && ((num10 * numArray2[l - 2]) <= ((num9 * ((ulong)0x100000000L)) + un[(j + l) - 2])))
						{
							break;
						}
						num10 -= (ulong)1L;
						num9 += numArray2[l - 1];
					}
					while (num9 < 0x100000000L);
					long num12 = 0L;
					long num13 = 0L;
					int index = 0;
					while (index < l)
					{
						ulong num14 = numArray2[index] * num10;
						num13 = (un[index + j] - ((uint)num14)) - num12;
						un[index + j] = (uint)num13;
						num14 = num14 >> 0x20;
						num13 = num13 >> 0x20;
						num12 = ((long)num14) - num13;
						index++;
					}
					num13 = un[j + l] - num12;
					un[j + l] = (uint)num13;
					q[j] = (uint)num10;
					if (num13 < 0L)
					{
						q[j]--;
						ulong num15 = 0L;
						for (index = 0; index < l; index++)
						{
							num15 = (numArray2[index] + un[j + index]) + num15;
							un[j + index] = (uint)num15;
							num15 = num15 >> 0x20;
						}
						num15 += un[j + l];
						un[j + l] = (uint)num15;
					}
				}
				Unnormalize(un, out r, normalizeShift);
			}
			else
			{
				q = zeroArray;
				r = u;
			}
		}

		public static MS_BigInteger DivRem(MS_BigInteger dividend, MS_BigInteger divisor, out MS_BigInteger remainder)
		{
			uint[] numArray;
			uint[] numArray2;
			DivModUnsigned((dividend._data == null) ? zeroArray : dividend._data, (divisor._data == null) ? zeroArray : divisor._data, out numArray, out numArray2);
			remainder = new MS_BigInteger(dividend._sign, numArray2);
			return new MS_BigInteger(dividend._sign * divisor._sign, numArray);
		}

		private static MS_BigInteger LeftShift(MS_BigInteger x, int shift)
		{
			if (shift == 0)
			{
				return x;
			}
			if (shift < 0)
			{
				return RightShift(x, -shift);
			}
			int num = shift / 0x20;
			int num2 = shift - (num * 0x20);
			int length = x.Length;
			uint[] numArray = x._data;
			int num4 = (length + num) + 1;
			uint[] numArray2 = new uint[num4];
			if (num2 == 0)
			{
				for (int i = 0; i < length; i++)
				{
					numArray2[i + num] = numArray[i];
				}
			}
			else
			{
				int num6 = 0x20 - num2;
				uint num7 = 0;
				int index = 0;
				while (index < length)
				{
					uint num9 = numArray[index];
					numArray2[index + num] = (num9 << num2) | num7;
					num7 = num9 >> num6;
					index++;
				}
				numArray2[index + num] = num7;
			}
			return new MS_BigInteger(x._sign, numArray2);
		}

		private static MS_BigInteger RightShift(MS_BigInteger x, int shift)
		{
			if (shift == 0)
			{
				return x;
			}
			if (shift < 0)
			{
				return LeftShift(x, -shift);
			}
			int num = shift / 0x20;
			int num2 = shift - (num * 0x20);
			int length = x.Length;
			uint[] numArray = x._data;
			int num4 = length - num;
			if (num4 < 0)
			{
				num4 = 0;
			}
			uint[] numArray2 = new uint[num4];
			if (num2 == 0)
			{
				for (int i = length - 1; i >= num; i--)
				{
					numArray2[i - num] = numArray[i];
				}
			}
			else
			{
				int num6 = 0x20 - num2;
				uint num7 = 0;
				for (int j = length - 1; j >= num; j--)
				{
					uint num9 = numArray[j];
					numArray2[j - num] = (num9 >> num2) | num7;
					num7 = num9 << num6;
				}
			}
			return new MS_BigInteger(x._sign, numArray2);
		}

		public static MS_BigInteger Parse(string s)
		{
			return Parse(s, CultureInfo.CurrentCulture);
		}

		public static MS_BigInteger Parse(string s, IFormatProvider provider)
		{
			return Parse(s, NumberStyles.Integer, provider);
		}

		public static MS_BigInteger Parse(string s, NumberStyles style)
		{
			return Parse(s, style, CultureInfo.CurrentCulture);
		}

		public static MS_BigInteger Parse(string s, NumberStyles style, IFormatProvider provider)
		{
			MS_BigInteger integer;
			string str;
			if (!TryParse(s, style, provider, out integer, out str))
			{
				throw new FormatException(str);
			}
			return integer;
		}

		public static bool TryParse(string s, out MS_BigInteger b)
		{
			string str;
			return TryParse(s, NumberStyles.Integer, CultureInfo.CurrentCulture, out b, out str);
		}

		public static bool TryParse(string s, NumberStyles style, IFormatProvider formatProvider, out MS_BigInteger value)
		{
			string str;
			return TryParse(s, style, formatProvider, out value, out str);
		}

		private static bool TryParse(string s, NumberStyles style, IFormatProvider formatProvider, out MS_BigInteger value, out string error)
		{
			Contract.Requires((s != null) ? null : ((Exception)new ArgumentNullException("s")));
			if (formatProvider == null)
			{
				formatProvider = CultureInfo.CurrentCulture;
			}
			if ((style & ~(NumberStyles.HexNumber | NumberStyles.AllowLeadingSign)) != NumberStyles.None)
			{
				throw new NotSupportedException(string.Format(CultureInfo.CurrentUICulture, Res.UnsupportedNumberStyle, new object[] { style }));
			}
			error = null;
			NumberFormatInfo format = (NumberFormatInfo)formatProvider.GetFormat(typeof(NumberFormatInfo));
			uint num = (uint)(((style & NumberStyles.AllowHexSpecifier) != NumberStyles.None) ? 0x10 : 10);
			int indexA = 0;
			bool flag = false;
			if ((style & NumberStyles.AllowLeadingWhite) != NumberStyles.None)
			{
				while ((indexA < s.Length) && IsWhiteSpace(s[indexA]))
				{
					indexA++;
				}
			}
			if ((style & NumberStyles.AllowLeadingSign) != NumberStyles.None)
			{
				int length = format.NegativeSign.Length;
				if (((length + indexA) < s.Length) && (string.Compare(s, indexA, format.NegativeSign, 0, length, false, CultureInfo.CurrentCulture) == 0))
				{
					flag = true;
					indexA += format.NegativeSign.Length;
				}
			}
			value = Zero;
			MS_BigInteger one = One;
			if (indexA == s.Length)
			{
				error = Res.ParsedStringWasInvalid;
				return false;
			}
			for (int i = s.Length - 1; i >= indexA; i--)
			{
				if (((style & NumberStyles.AllowTrailingWhite) != NumberStyles.None) && IsWhiteSpace(s[i]))
				{
					int num5 = i;
					while (num5 >= indexA)
					{
						if (!IsWhiteSpace(s[num5]))
						{
							break;
						}
						num5--;
					}
					if (num5 < indexA)
					{
						error = Res.ParsedStringWasInvalid;
						return false;
					}
					i = num5;
				}
				uint num6 = ParseSingleDigit(s[i], (ulong)num, out error);
				if (error != null)
				{
					return false;
				}
				if (num6 != 0)
				{
					value += num6 * one;
				}
				one *= num;
			}
			if ((value._sign == 1) && flag)
			{
				value = 0 - value;//op_UnaryNegation(value);
			}
			return true;
		}

		private static uint ParseSingleDigit(char c, ulong radix, out string error)
		{
			error = null;
			if ((c >= '0') && (c <= '9'))
			{
				return (uint)(c - '0');
			}
			if (radix == 0x10L)
			{
				c = (char)(c & '￟');
				if ((c >= 'A') && (c <= 'F'))
				{
					return (uint)((c - 'A') + 10);
				}
			}
			error = Res.InvalidCharactersInString;
			return uint.MaxValue;
		}

		private static bool IsWhiteSpace(char ch)
		{
			return ((ch == ' ') || ((ch >= '\t') && (ch <= '\r')));
		}

		public string ToString(string format)
		{
			return ToString(format, CultureInfo.CurrentCulture);
		}

		public string ToString(IFormatProvider formatProvider)
		{
			if (formatProvider == null)
			{
				formatProvider = CultureInfo.CurrentCulture;
			}
			return ToString(10, false, (NumberFormatInfo)formatProvider.GetFormat(typeof(NumberFormatInfo)));
		}

		public string ToString(string format, IFormatProvider formatProvider)
		{
			if (formatProvider == null)
			{
				formatProvider = CultureInfo.CurrentCulture;
			}
			uint radix = 10;
			bool useCapitalHexDigits = false;
			if (!string.IsNullOrEmpty(format))
			{
				char ch = format[0];
				switch (ch)
				{
					case 'X':
					case 'x':
						radix = 0x10;
						useCapitalHexDigits = ch == 'X';
						goto Label_0069;
				}
				if (((ch != 'G') && (ch != 'g')) && ((ch != 'D') && (ch != 'd')))
				{
					throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture, "Currently not supported format: {0}", new object[] { format }));
				}
			}
		Label_0069:
			return ToString(radix, useCapitalHexDigits, (NumberFormatInfo)formatProvider.GetFormat(typeof(NumberFormatInfo)));
		}

		public override string ToString()
		{
			return ToString(10, false, CultureInfo.CurrentCulture.NumberFormat);
		}

		private string ToString(uint radix, bool useCapitalHexDigits, NumberFormatInfo info)
		{
			Contract.Requires((radix >= 2) && (radix <= 0x24));
			if (_sign == 0)
			{
				return "0";
			}
			int length = Length;
			List<uint> list = new List<uint>();
			uint[] n = copy(_data);
			int nl = Length;
			uint d = groupRadixValues[radix];
			while (nl > 0)
			{
				uint item = div(n, ref nl, d);
				list.Add(item);
			}
			StringBuilder buf = new StringBuilder();
			if (_sign == -1)
			{
				buf.Append(info.NegativeSign);
			}
			int num4 = list.Count - 1;
			char[] tmp = new char[maxCharsPerDigit[radix]];
			AppendRadix(list[num4--], radix, useCapitalHexDigits, tmp, buf, false);
			while (num4 >= 0)
			{
				AppendRadix(list[num4--], radix, useCapitalHexDigits, tmp, buf, true);
			}
			return buf.ToString();
		}

		private static void AppendRadix(uint rem, uint radix, bool useCapitalHexDigits, char[] tmp, StringBuilder buf, bool leadingZeros)
		{
			string str = useCapitalHexDigits ? "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ" : "0123456789abcdefghijklmnopqrstuvwxyz";
			int length = tmp.Length;
			int startIndex = length;
			while ((startIndex > 0) && (leadingZeros || (rem != 0)))
			{
				uint num3 = rem % radix;
				rem /= radix;
				tmp[--startIndex] = str[(int)num3];
			}
			if (leadingZeros)
			{
				buf.Append(tmp);
			}
			else
			{
				buf.Append(tmp, startIndex, length - startIndex);
			}
		}

		private static uint div(uint[] n, ref int nl, uint d)
		{
			ulong num = 0L;
			int index = nl;
			bool flag = false;
			while (--index >= 0)
			{
				num = num << 0x20;
				num |= n[index];
				uint num3 = (uint)(num / ((ulong)d));
				n[index] = num3;
				if (num3 == 0)
				{
					if (!flag)
					{
						nl--;
					}
				}
				else
				{
					flag = true;
				}
				num = num % ((ulong)d);
			}
			return (uint)num;
		}

		public override int GetHashCode()
		{
			if (_sign == 0)
			{
				return 0;
			}
			return (int)_data[0];
		}

		public bool Equals(MS_BigInteger other)
		{
			if (_sign != other._sign)
			{
				return false;
			}
			int length = Length;
			int num2 = other.Length;
			if (length != num2)
			{
				return false;
			}
			for (uint i = 0; i < length; i++)
			{
				if (_data[i] != other._data[i])
				{
					return false;
				}
			}
			return true;
		}

		public override bool Equals(object obj)
		{
			return (((obj != null) && (obj is MS_BigInteger)) && Equals((MS_BigInteger)obj));
		}

		public int CompareTo(MS_BigInteger other)
		{
			return Compare(this, other);
		}

		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			if (!(obj is MS_BigInteger))
			{
				throw new ArgumentException(Res.MustBeBigInt);
			}
			return Compare(this, (MS_BigInteger)obj);
		}

		static MS_BigInteger()
		{
			maxCharsPerDigit = new uint[] { 
            0, 0, 0x1f, 20, 15, 13, 12, 11, 10, 10, 9, 9, 8, 8, 8, 8, 
            7, 7, 7, 7, 7, 7, 7, 7, 6, 6, 6, 6, 6, 6, 6, 6, 
            6, 6, 6, 6, 6
         };
			groupRadixValues = new uint[] { 
            0, 0, 0x80000000, 0xcfd41b91, 0x40000000, 0x48c27395, 0x81bf1000, 0x75db9c97, 0x40000000, 0xcfd41b91, 0x3b9aca00, 0x8c8b6d2b, 0x19a10000, 0x309f1021, 0x57f6c100, 0x98c29b81, 
            0x10000000, 0x18754571, 0x247dbc80, 0x3547667b, 0x4c4b4000, 0x6b5a6e1d, 0x94ace180, 0xcaf18367, 0xb640000, 0xe8d4a51, 0x1269ae40, 0x17179149, 0x1cb91000, 0x23744899, 0x2b73a840, 0x34e63b41, 
            0x40000000, 0x4cfa3cc1, 0x5c13d840, 0x6d91b519, 0x81bf1000
         };
			zeroArray = new uint[0];
		}
	}

	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0"), DebuggerNonUserCode, CompilerGenerated]
	internal class Res
	{
		// Fields
		private static CultureInfo resourceCulture;
		private static ResourceManager resourceMan;

		// Methods
		internal Res()
		{
		}

		// Properties
		internal static string BigIntInfinity
		{
			get
			{
				return ResourceManager.GetString("BigIntInfinity", resourceCulture);
			}
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return resourceCulture;
			}
			set
			{
				resourceCulture = value;
			}
		}

		internal static string InvalidCharactersInString
		{
			get
			{
				return ResourceManager.GetString("InvalidCharactersInString", resourceCulture);
			}
		}

		internal static string MustBeBigInt
		{
			get
			{
				return ResourceManager.GetString("MustBeBigInt", resourceCulture);
			}
		}

		internal static string MustBePositive
		{
			get
			{
				return ResourceManager.GetString("MustBePositive", resourceCulture);
			}
		}

		internal static string NonNegative
		{
			get
			{
				return ResourceManager.GetString("NonNegative", resourceCulture);
			}
		}

		internal static string NotANumber
		{
			get
			{
				return ResourceManager.GetString("NotANumber", resourceCulture);
			}
		}

		internal static string Overflow_Byte
		{
			get
			{
				return ResourceManager.GetString("Overflow_Byte", resourceCulture);
			}
		}

		internal static string Overflow_Decimal
		{
			get
			{
				return ResourceManager.GetString("Overflow_Decimal", resourceCulture);
			}
		}

		internal static string Overflow_Double
		{
			get
			{
				return ResourceManager.GetString("Overflow_Double", resourceCulture);
			}
		}

		internal static string Overflow_Int16
		{
			get
			{
				return ResourceManager.GetString("Overflow_Int16", resourceCulture);
			}
		}

		internal static string Overflow_Int32
		{
			get
			{
				return ResourceManager.GetString("Overflow_Int32", resourceCulture);
			}
		}

		internal static string Overflow_Int64
		{
			get
			{
				return ResourceManager.GetString("Overflow_Int64", resourceCulture);
			}
		}

		internal static string Overflow_SByte
		{
			get
			{
				return ResourceManager.GetString("Overflow_SByte", resourceCulture);
			}
		}

		internal static string Overflow_Single
		{
			get
			{
				return ResourceManager.GetString("Overflow_Single", resourceCulture);
			}
		}

		internal static string Overflow_UInt16
		{
			get
			{
				return ResourceManager.GetString("Overflow_UInt16", resourceCulture);
			}
		}

		internal static string Overflow_UInt32
		{
			get
			{
				return ResourceManager.GetString("Overflow_UInt32", resourceCulture);
			}
		}

		internal static string Overflow_UInt64
		{
			get
			{
				return ResourceManager.GetString("Overflow_UInt64", resourceCulture);
			}
		}

		internal static string ParsedStringWasInvalid
		{
			get
			{
				return ResourceManager.GetString("ParsedStringWasInvalid", resourceCulture);
			}
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(resourceMan, null))
				{
					ResourceManager manager = new ResourceManager("System.Res", typeof(Res).Assembly);
					resourceMan = manager;
				}
				return resourceMan;
			}
		}

		internal static string UnsupportedNumberStyle
		{
			get
			{
				return ResourceManager.GetString("UnsupportedNumberStyle", resourceCulture);
			}
		}
	}




}





namespace Microsoft.Contracts
{
	internal static class Contract
	{
		// Methods
		[Conditional("DEBUG"), Pure]
		public static void Assert(bool b)
		{
		}

		[Pure, Conditional("DEBUG")]
		public static void Assert(bool b, string message)
		{
		}

		[Conditional("USE_SPECSHARP_ASSEMBLY_REWRITER"), Pure]
		public static void AssertOnException<E>(bool b) where E : Exception
		{
			string text1 = "This method will be modified to the following after rewriting:" + "if (!b) throw new AssertionException();";
		}

		[Conditional("USE_SPECSHARP_ASSEMBLY_REWRITER"), Pure]
		public static void AssertOnReturn(bool b)
		{
			string text1 = "This method will be modified to the following after rewriting:" + "if (!b) throw new AssertionException();";
		}

		[Conditional("DEBUG"), Pure]
		public static void Assume(bool b)
		{
		}

		[Pure, Conditional("DEBUG")]
		public static void Assume(bool b, string message)
		{
			if (!b)
			{
				throw new AssumptionException(message);
			}
		}

		[Conditional("DEBUG"), Pure]
		public static void DebugRequires(bool b)
		{
		}

		[Pure, Conditional("USE_SPECSHARP_ASSEMBLY_REWRITER")]
		public static void Ensures(bool b)
		{
			string text1 = "This method will be modified to the following after rewriting:" + "if (!b) throw new PostConditionException();";
		}

		public static bool Exists(int lo, int hi, Predicate<int> p)
		{
			Requires(lo <= hi);
			Requires(p != null);
			for (int i = lo; i < hi; i++)
			{
				if (p(i))
				{
					return true;
				}
			}
			return false;
		}

		public static bool ForAll(int lo, int hi, Predicate<int> p)
		{
			Requires(lo <= hi);
			Requires(p != null);
			for (int i = lo; i < hi; i++)
			{
				if (!p(i))
				{
					return false;
				}
			}
			return true;
		}

		[Pure, Conditional("USE_SPECSHARP_ASSEMBLY_REWRITER")]
		public static void Invariant(bool b)
		{
			string text1 = "This method will be modified to the following after rewriting:" + "if (!b) throw new InvariantException();";
		}

		[Pure]
		public static T Old<T>(T t)
		{
			return t;
		}

		[Pure]
		public static T Parameter<T>(out T t)
		{
			t = default(T);
			return t;
		}

		[Pure]
		public static void Requires(bool b)
		{
			if (!b)
			{
				throw new PreconditionException();
			}
		}

		[Pure]
		public static void Requires(Exception x)
		{
			if (x != null)
			{
				throw x;
			}
		}

		[Pure]
		public static T Result<T>()
		{
			return default(T);
		}

		[Pure]
		public static void RewriterEnsures(bool b)
		{
			if (!b)
			{
				throw new PostconditionException();
			}
		}

		[Pure]
		public static void RewriterInvariant(bool b)
		{
			if (!b)
			{
				throw new InvariantException();
			}
		}

		[Pure, Conditional("USE_SPECSHARP_ASSEMBLY_REWRITER")]
		public static void Throws<E>() where E : Exception
		{
		}

		[Pure, Conditional("USE_SPECSHARP_ASSEMBLY_REWRITER")]
		public static void ThrowsEnsures<E>(bool b) where E : Exception
		{
			string text1 = "This method will be modified to the following after rewriting:" + "if (!b) throw new PostconditionException();";
		}

		// Nested Types
		[Serializable]
		public sealed class AssertionException : Exception
		{
			// Methods
			public AssertionException()
			{
			}

			public AssertionException(string s)
				: base(s)
			{
			}

			private AssertionException(SerializationInfo info, StreamingContext context)
				: base(info, context)
			{
			}

			public AssertionException(string s, Exception inner)
				: base(s, inner)
			{
			}
		}

		[Serializable]
		public sealed class AssumptionException : Exception
		{
			// Methods
			public AssumptionException()
			{
			}

			public AssumptionException(string s)
				: base(s)
			{
			}

			private AssumptionException(SerializationInfo info, StreamingContext context)
				: base(info, context)
			{
			}

			public AssumptionException(string s, Exception inner)
				: base(s, inner)
			{
			}
		}

		[Serializable]
		public sealed class InvariantException : Exception
		{
			// Methods
			public InvariantException()
			{
			}

			public InvariantException(string s)
				: base(s)
			{
			}

			private InvariantException(SerializationInfo info, StreamingContext context)
				: base(info, context)
			{
			}

			public InvariantException(string s, Exception inner)
				: base(s, inner)
			{
			}
		}

		[Serializable]
		public sealed class PostconditionException : Exception
		{
			// Methods
			public PostconditionException()
				: this("Postcondition failed.")
			{
			}

			public PostconditionException(string s)
				: base(s)
			{
			}

			private PostconditionException(SerializationInfo info, StreamingContext context)
				: base(info, context)
			{
			}

			public PostconditionException(string s, Exception inner)
				: base(s, inner)
			{
			}
		}

		[Serializable]
		public sealed class PreconditionException : Exception
		{
			// Methods
			public PreconditionException()
				: this("Precondition failed.")
			{
			}

			public PreconditionException(string s)
				: base(s)
			{
			}

			private PreconditionException(SerializationInfo info, StreamingContext context)
				: base(info, context)
			{
			}

			public PreconditionException(string s, Exception inner)
				: base(s, inner)
			{
			}
		}
	}



	[AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	internal sealed class ContractClassAttribute : Attribute
	{
		// Fields
		private Type _typeWithContracts;

		// Methods
		public ContractClassAttribute(Type t)
		{
			_typeWithContracts = t;
		}

		// Properties
		public Type Type
		{
			get
			{
				return _typeWithContracts;
			}
		}
	}




	[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	internal sealed class ImmutableAttribute : Attribute { }

	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	internal sealed class InvariantMethodAttribute : Attribute { }

	[AttributeUsage(AttributeTargets.Event | AttributeTargets.Property | AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	internal sealed class PureAttribute : Attribute { }

	[AttributeUsage(AttributeTargets.Assembly)]
	internal sealed class RuntimeContractsAttribute : Attribute { }

	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Method | AttributeTargets.Constructor | AttributeTargets.Struct | AttributeTargets.Class | AttributeTargets.Assembly)]
	internal sealed class VerifyAttribute : Attribute
	{
		// Fields
		private bool _value;

		// Methods
		public VerifyAttribute()
		{
			_value = true;
		}
		public VerifyAttribute(bool value)
		{
			_value = value;
		}

		// Properties
		public bool Value
		{
			get
			{
				return _value;
			}
		}
	}
}

