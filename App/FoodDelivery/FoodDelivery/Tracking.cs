using System;

namespace FoodDelivery
{
    public class Tracking
    {
        private String _gpslat;
        private String _gpslon;
        private String _date;
        private String _hour;

        public string gpslat
        {
            get { return _gpslat; }
            set { _gpslat = value; }
        }

        public string gpslong
        {
            get { return _gpslon; }
            set { _gpslon = value; }
        }

        public string date
        {
            get { return _date; }
            set { _date = value; }
        }

        public string hour
        {
            get { return _hour; }
            set { _hour = value; }
        }
    }
}