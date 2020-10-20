using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BL.services
{
    public class StatisticCalc
    {
        const int minSequence = 2;
        const int minToDivide = 6;
        public static bool IsSignificantDeviation(customersInLine turn,double actualDuration)
        {//actualHour(1)=
            double serviceMinutes = (turn.exitHour.Value - turn.ActualHour.Value).TotalMinutes;

            if (serviceMinutes * 1.3 < actualDuration || serviceMinutes * 0.7 > actualDuration)
                return true;
            return false;

        }

        void calcAvgServiceDuration(int activityTimeId)
        {
            var line = DAL.TurnDal.GetLinePerActivityTime(activityTimeId);
            List<int> significantDeviationIndexes = new List<int>();
            int i = 0;
            bool isSignificantStatus = false;
            DAL.activityTime activityTime= ActivityTimeDal.GetActivityTimeById(activityTimeId);
            double serviceAvg = activityTime.ActualDurationOfService.Value; 
            foreach (var item in line)
            {
                bool isSignificant = IsSignificantDeviation(item, serviceAvg);
                if (isSignificant && !isSignificantStatus)
                {
                    significantDeviationIndexes.Add(i);
                    isSignificantStatus = true;
                }
                else if (!isSignificant && isSignificantStatus)
                {
                    significantDeviationIndexes.Add(i - 1);
                    isSignificantStatus = false;
                }

                i++;
            }

            int sum = 0;
            int index = 0;
            for ( i = 1; i < significantDeviationIndexes.Count; i+=2)
            {
                int length = significantDeviationIndexes[i] - significantDeviationIndexes[i - 1];
                if (length > minSequence)
                {
                    sum += length;
                    if(index==0)
                        index= significantDeviationIndexes[i - 1];
                }
                if(significantDeviationIndexes[i+1] - significantDeviationIndexes[i ]>minSequence)
                {
                    if (sum >= minToDivide)
                        SetUnUsualActivityTime(line.Skip(index).Take(significantDeviationIndexes[i]));
                    else
                        UpdateStatistics(line.Skip(index).Take(significantDeviationIndexes[i]),activityTime);
                    index = 0;
                    sum = 0;

                }
            }
            
            
        }

        private void UpdateStatistics(IEnumerable<customersInLine> line,activityTime activityTime)
        {
            //שליפת ממוצע משמרת הכפלה ברוחב המדגם הוספת הנתונים החדשים, הוספת מספר הנתונים לרוחב המדגם וחלוקה של הסכום ברוחב המדגם
            //חישוב מחודש של סטיית טקן
            double activityTimeAvg = activityTime.ActualDurationOfService.Value;
            double newAvg = line.Average(t => (t.exitHour.Value - t.ActualHour.Value).TotalMinutes);
            double weightedAverage = (activityTimeAvg * activityTime.sampleSize.Value + newAvg * line.Count()) / (activityTime.sampleSize.Value + line.Count());
               

//todo: לממש את הפונקציה
           // DAL.ActivityTimeDal.updateActivityTime(activityTime);
        }

        private void SetUnUsualActivityTime(IEnumerable<customersInLine> unusalLine)
        {
            unusalLine= unusalLine.ToList();
            DAL.unusual unusual = new unusual()
            {
                activityTimeId = unusalLine.ToList()[0].activityTimeId,
                // average=unusalLine.Average(t=>(t.exitHour.Value-t.ActualHour.Value).TotalMinutes)
            };

            scanUnusuals(unusual);
        }

        double calcStandartDeviation()
        {
            int[] values=new int[5];
            //todo: init this array
            double ret = 0;
            int count = values.Count();
            if (count > 1)
            {
                //Compute the Average
                double avg = values.Average();

                //Perform the Sum of (value-avg)^2
                double sum = values.Sum(d => (d - avg) * (d - avg));

                //Put it all together
                ret = Math.Sqrt(sum / count);
            }
            return ret;
        }


        void calcAvgWaitingPeople()
        {
            //ההבדל בין הזמן המשוער לזמן האמיתי
            //הנתון המענין כמה דחיפות היו בזמן הזה צריך לדעת ממוצע דחיפות
            //numOfPushTimes לפי
            //מאד דומה לפונקציה למעלה
            //todo: כשמאתחלים משמרת חדשה לאפס נתונים
        }

        void scanUnusuals(DAL.unusual unusual)
        {
            //כרגע אין פה התיחסות לחריגות של משמרות מיוחדות
            var servicesUnusuals = DAL.UnusualDal.GetUnusuals(unusual.activityTime.serviceId).Where(u=>u.kindOfUnusual==unusual.kindOfUnusual);
            var weeklyUnusuals = servicesUnusuals.Where(u => u.activityTime.dayInWeek == unusual.activityTime.dayInWeek).ToList();
            var monthlyyUnusuals = servicesUnusuals.Where(u => u.startTime.Value.Day == unusual.startTime.Value.Day);
            var yearUnusuals = servicesUnusuals.Where(u => u.startTime.Value.DayOfYear == unusual.startTime.Value.DayOfYear);
            //todo: לטפל בחריגות הפוכה
            //חריגות הפוכה
           // weeklyUnusuals = servicesUnusuals.Where(u => u.activityTime.dayInWeek == unusual.activityTime.dayInWeek).ToList();
           // monthlyyUnusuals = servicesUnusuals.Where(u => DateTime.Today.Day == DateTime.Today.Day);
           // yearUnusuals = servicesUnusuals.Where(u => u.activityTime.dayInWeek == unusual.activityTime.dayInWeek);

            if (weeklyUnusuals.Count() > 10)
            {
                TimeSpan startTime = weeklyUnusuals.Max(u => u.startTime.Value.TimeOfDay);
                TimeSpan  endTime= weeklyUnusuals.Min(u => u.endTime.Value.TimeOfDay);

                for (int i = 0; i < weeklyUnusuals.Count(); i++)
                {
                    
                    if (weeklyUnusuals.ElementAt(i).startTime.Value.TimeOfDay > endTime ||
                        weeklyUnusuals.ElementAt(i).endTime.Value.TimeOfDay < startTime)
                        weeklyUnusuals.RemoveAt(i--);

                }
                if (weeklyUnusuals.Count() > 10)
                {
                  
                }




            }


       
      
  }



    }
    
}
