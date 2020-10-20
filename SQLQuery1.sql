create table specialActivityTimes(
    activityTimeId  int  IDENTITY (1, 1) primary key,
    [serviceId]                  INT        NOT NULL,
    [startTime]                  TIME (7)   NOT NULL,
    [endTime]                    TIME (7)   NOT NULL,
	dayInMonth int ,
	dayOf

)