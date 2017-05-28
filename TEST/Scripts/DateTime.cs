using UnityEngine;
using System.Collections;

namespace CustomDateTime{
	public class DateTime {
		public int Hours{
			get{
				return hours;
			}
			set{
				hours = value;
				Reduction ();
			}
		}
		public int	Minutes{
			get{
				return minutes;
			}
			set{
				minutes = value;
				Reduction ();
			}
		}


		private int hours, minutes;

		public DateTime(){
			hours = 0;
			minutes = 0;
		}

		public DateTime(int Hour , int Min){
			hours = Hour;
			minutes = Min;
			Reduction ();
		}

		public string GetString(){
			string hour,min;
			if (hours <= 9) {
				hour = "0" + hours.ToString();
			} else {
				hour = hours.ToString();
			}
			if (minutes <= 9) {
				min = "0" + minutes.ToString();
			} else {
				min = minutes.ToString();
			}
			return (hour + ":" + min);

		}

		public void Set(int hour , int min){
			hours = hour;
			minutes = min;
			Reduction ();
		}

		public void SetTimeInMinutes(int min){
			minutes = min;
			hours = 0;
			Reduction ();
		}

		public int GetTimeInMinutes(){
			return minutes + hours * 60;
		}

		public int GetMinutes(){
			return minutes;
		}

		public int GetHours(){
			return hours;
		}

		public void AddOneMinute(){
			minutes++;
			Reduction ();
		}
		public void SubtractOneMinute(){
			minutes--;
			Reduction ();
		}
		public void AddOneHour(){
			hours++;
			Reduction ();
		}
		public void SubtractOneHour(){
			hours--;
			Reduction ();
		}

		public void AddMinutes(int amount){
			minutes += amount;
			Reduction ();
		}
		public void SubstractMinutes(int amount){
			minutes -= amount;
			Reduction ();
		}

		public void AddHours(int amount){
			minutes += amount;
			Reduction ();
		}
		public void SubstractHours(int amount){
			minutes -= amount;
			Reduction ();
		}

		public void CopyAttributes(DateTime Copied){
			this.minutes = Copied.minutes;
			this.hours = Copied.hours;
		}

		private void Reduction(){
			while (minutes < 0 || minutes >= 60) {
				if (minutes >= 60) {
					minutes -= 60;
					hours++;
				}else if(minutes<0){
					minutes += 60;
					hours--;
				}
			}
			while (hours < 0 || hours >= 24) {
				if (hours >= 24) {
					hours -= 24;
				}else if(hours<0){
					hours += 24;
				}
			}
		}

		public static DateTime  operator ++(DateTime i) {
			//DateTime oldValue = new DateTime();
			//oldValue.CopyAttributes (i);
			i.AddOneMinute();
			return i;

		}
	 
		public static DateTime  operator --(DateTime i) {
			//DateTime oldValue = new DateTime();
			//oldValue.CopyAttributes (i);
			i.SubtractOneMinute();
			return i;
		}



	}
}