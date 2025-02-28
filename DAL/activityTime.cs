//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class activityTime
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public activityTime()
        {
            this.customersInLines = new HashSet<customersInLine>();
            this.unusuals = new HashSet<unusual>();
        }
    
        public int activityTimeId { get; set; }
        public int serviceId { get; set; }
        public int dayInWeek { get; set; }
        public System.TimeSpan startTime { get; set; }
        public System.TimeSpan endTime { get; set; }
        public Nullable<System.DateTime> startDate { get; set; }
        public Nullable<System.DateTime> endDate { get; set; }
        public int numOfWorkers { get; set; }
        public int estimatedDurationOfService { get; set; }
        public Nullable<double> ActualDurationOfService { get; set; }
        public Nullable<double> StandardDeviation { get; set; }
        public Nullable<double> averageNumOfWaitingPeople { get; set; }
        public Nullable<int> avgServiceDuration { get; set; }
        public Nullable<int> sampleSize { get; set; }
    
        public virtual service service { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<customersInLine> customersInLines { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<unusual> unusuals { get; set; }
    }
}
