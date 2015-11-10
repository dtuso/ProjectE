using System.Collections.Generic;
using System.Text;

namespace ProjectEuler.Helpers
{
	public class TwoDiceRoll
	{
		public Die Die1;
		public Die Die2;
		public TwoDiceRoll(Die d1, Die d2)
		{
			Die1 = d1;
			Die2 = d2;
		}
		
		public static bool operator ==(TwoDiceRoll r1, TwoDiceRoll r2)
		{
			return ((r1.Die1 == r2.Die1 && r1.Die2 == r2.Die2) || (r1.Die2 == r2.Die1 && r1.Die1 == r2.Die2));

		}
		public static bool operator !=(TwoDiceRoll r1, TwoDiceRoll r2)
		{
			return !((r1.Die1 == r2.Die1 && r1.Die2 == r2.Die2) || (r1.Die2 == r2.Die1 && r1.Die1 == r2.Die2));
		}

		public override bool Equals(object obj)
		{
			return (this == (TwoDiceRoll) obj);
			
		}
		public override int GetHashCode()
		{
			return Die1.Value * 1000000 + Die2.Value;
		}
	}
	public class Die
	{
		public int Value;
		public List<int> Sides;
		public Die()
		{
			Sides = new List<int>(6);
		}
		public Die(int s1, int s2, int s3, int s4, int s5, int s6)
		{
			Sides = new List<int>(6);
			Sides.Add(s1);
			Sides.Add(s2);
			Sides.Add(s3);
			Sides.Add(s4);
			Sides.Add(s5);
			Sides.Add(s6);
			Sides.Sort();

			Value = 0;
			int digit = 1;
			for (int idx = 0; idx < 6; idx++)
			{
				Value += (digit * Sides[idx]);
				digit *= 10;
			}
		}


		public bool Contains6or9
		{
			get
			{
				foreach (int i in Sides)
				{
					if (i == 6 || i == 9) return true;
				}
				return false;
			}
		}
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder("[ ");

			foreach (int i in Sides)
			{
				sb.AppendFormat("{0} ", i);
			}
			sb.Append("]");
			return sb.ToString();
		}

		
		public static bool operator ==(Die d1, Die d2)
		{
			return (d1.Value == d2.Value);
		}
		public static bool operator !=(Die d1, Die d2)
		{
			return (d1.Value != d2.Value);
		}

		public override bool Equals(object obj)
		{
			return (this == (Die)obj);
		}
		public override int GetHashCode()
		{
			return Value;
		}
	}
}
