using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace BL.services
{
 public   class NnotificationTimer
    {
        public static void StartTimer()
        {
            //todo timer
            // Create a timer with a two second interval-ask if interval is appropriate.
            //Interval property is in milliseconds.
            // 1 sec = 1000 milliseconds
            Timer timer = new Timer(60000);
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }
        private async static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
          await NotificationService.SendNotification("hello everyone", "have a nice day!!!", "dPv0MIzGuNalZexTO6l7z7:APA91bExex--KgM8i8HbOZkoZgeGmZXFSR16NezbDTp479A9K3vVAd5m69z3rLJOI-bkRAkuCkzag280V1o-z3wJ4occOiXOr2-4k5DTzOzBftt0DgZXqMT2kI_0FcGud_gOR5vFkXV7");
        }

    }
}
