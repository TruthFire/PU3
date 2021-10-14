using System;
using System.Collections.Generic;
using System.Text;

namespace PU3
{
   public class Person
{

	private string Name, Surename;
	private DateTime Dob; 


	public Person(string name, string surename, DateTime dob)
	{
		this.Name = name;
		this.Surename = surename;
		this.Dob = dob;
	}

	public string GetAge()
    {
		return DateTime.Now.AddYears(-(Dob.Year + Convert.ToInt32(IsBdBeen()))).ToString("yy");
	}

	public string GetName()
        {
			return this.Name;
        }
	public string GetSurename()
        {
			return this.Surename;
        }
		public DateTime GetDob()
        {
			return this.Dob;
        }
	public int DaysToBd() // kiek dienu liko iki gd
    {
		DateTime curr = DateTime.Now;
		int year = curr.Year + Convert.ToInt32(!IsBdBeen()); //
		double days = (new DateTime(year, Dob.Month, Dob.Day) - curr).TotalDays;
			return (int)days;
	}
	
	private bool IsBdBeen() // ar buvo gimtadienis simet?
    {
		bool rez = false;
		DateTime curr = DateTime.Now;
			if (curr.Month < Dob.Month || curr.Month == Dob.Month && curr.Day < Dob.Day) //jei buvo = grazinam true(1), else false (0)
				rez = true;

			return rez;
    }

	public string PrintInfo()
    {
		string s = "Vardas: " + Name + "\nPavarde: " + Surename + "\nAmžius: " + GetAge() + "\nDienus iki gimtadienio: " + DaysToBd();
            return s;
    }
}
}
