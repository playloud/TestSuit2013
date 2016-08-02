using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_amazon_MeetingSchedules
{
	class Program
	{
		static void Main(string[] args)
		{
			List<Slot> slots = new List<Slot>();
			int numSlots = 0;
			int duration = 0;


			// Getting input
			string line = null;

			// get input
			line = Console.ReadLine();

			string[] arr = line.Split().ToArray();
			numSlots = int.Parse(arr[0]);
			duration = int.Parse(arr[1]);

			// line by lines
			for (int i = 0; i < numSlots; i++)
			{
				line = Console.ReadLine();
				slots.Add(new Slot(line));
			}

			bool hasMergable = true;

			//1.  merging
			while (hasMergable)
			{
				// getting duplication and merge
				var query =
				from s1 in slots
				from s2 in slots
				where
					s1.IsMergable(s2) && s1 != s2
				select new
				{
					s1,
					s2
				};

				if (query.Any())
				{
					Slot s1 = query.First().s1;
					Slot s2 = query.First().s2;
					Slot newSlot = Slot.Merge(s1, s2);
					slots.Remove(s1);
					slots.Remove(s2);
					slots.Add(newSlot);
				}
				else
					hasMergable = false;
			}

			//2. find empty slots
			List<Slot> emptySlots = new List<Slot>();

			slots = slots.OrderBy(a => a.dtStart).ToList();
			int emptyMinStart = 0;
			for (int i = 0; i < slots.Count; i++)
			{
				emptySlots.Add(new Slot(emptyMinStart, slots[i].startMin));
				emptyMinStart = slots[i].endMin;

				if (slots[i] == slots.Last())
				{
					emptySlots.Add(new Slot(slots[i].endMin, 60 * 24));
				}
			}

			// now dump
			//Console.WriteLine("all slots");
			//slots.ForEach(a => Console.WriteLine(a));
			//Console.WriteLine("empty slots");
			//emptySlots.ForEach(a => Console.WriteLine(a));
			Console.WriteLine();
			foreach (Slot slot in emptySlots)
			{
				if (slot.GetDuration() >= duration && slot.GetDuration() > 0 && slot.GetDuration() < 60 * 24)
					Console.WriteLine(slot.ToPrintFormat() + ":" + slot.GetDuration());
			}
		}
	}

	public class Slot
	{
		public DateTime dtStart;
		public DateTime dtEnd;

		public int startMin = 0;
		public int endMin = 0;

		public Slot(string input)
		{
			int startHH, startMM, endHH, endMM;

			string[] arr = input.Split().ToArray();
			startHH = int.Parse(arr[0]);
			startMM = int.Parse(arr[1]);

			endHH = int.Parse(arr[2]);
			endMM = int.Parse(arr[3]);

			dtStart = new DateTime(2000, 1, 1, startHH, startMM, 0);
			dtEnd = new DateTime(2000, 1, 1, endHH, endMM, 0);

			startMin = startHH * 60 + startMM;
			endMin = endHH * 60 + endMM;

		}

		public Slot(int startM, int endM)
		{
			this.startMin = startM;
			this.endMin = endM;

			dtStart = new DateTime(2000, 1, 1, startM / 60, startM % 60, 0);

			if (endM / 60 == 24)
				dtEnd = new DateTime(2000, 1, 2, 0, endM % 60, 0);
			else
				dtEnd = new DateTime(2000, 1, 1, endM / 60, endM % 60, 0);
		}

		public override string ToString()
		{
			return string.Format("{0} {1} ({2}-{3}) {4} min", dtStart.ToShortTimeString(), dtEnd.ToShortTimeString(), startMin, endMin, GetDuration());
		}

		public bool IsMergable(Slot b)
		{
			if (this.startMin <= b.startMin && this.endMin >= b.startMin)
				return true;

			if (this.startMin >= b.startMin && this.startMin <= b.endMin)
				return true;

			return false;
		}

		public int GetDuration()
		{
			return (endMin - startMin);
		}

		public static Slot Merge(Slot a, Slot b)
		{
			int start = 0;
			int end = 0;
			start = Math.Min(a.startMin, b.startMin);
			end = Math.Max(a.endMin, b.endMin);
			return new Slot(start, end);
		}

		internal string ToPrintFormat()
		{
			return string.Format("{0:00} {1:00} {2:00} {3:00}", dtStart.Hour, dtStart.Minute, dtEnd.Hour, dtEnd.Minute);
		}
	}

}
